using System;
using System.Collections.Generic;

namespace Atomic.Objects
{
    public static class AtomicCompiler
    {
        private static readonly Dictionary<Type, AtomicObjectInfo> compiledObjects = new();

        //Call it in background thread!
        public static void PrecompileObject(Type objectType)
        {
            CompileObject(objectType);
        }

        internal static AtomicObjectInfo CompileObject(Type objectType)
        {
            if (compiledObjects.TryGetValue(objectType, out AtomicObjectInfo objectInfo))
            {
                return objectInfo;
            }

            objectInfo = CompileObjectInternal(objectType);
            compiledObjects.Add(objectType, objectInfo);
            return objectInfo;
        }

        private static AtomicObjectInfo CompileObjectInternal(Type objectType)
        {
            var types = new HashSet<string>();
            var references = new List<PropertyInfo>();
            var sections = new List<SectionInfo>();

            foreach (Type @interface in objectType.GetInterfaces())
            {
                types.UnionWith(AtomicScanner.ScanTypes(@interface));
                references.AddRange(AtomicScanner.ScanValues(@interface));
            }

            while (objectType != typeof(AtomicObject))
            {
                types.UnionWith(AtomicScanner.ScanTypes(objectType));
                references.AddRange(AtomicScanner.ScanValues(objectType));
                sections.AddRange(AtomicScanner.ScanSections(objectType));
                
                objectType = objectType!.BaseType;
            }

            return new AtomicObjectInfo(types, references, sections);
        }
    }
}
