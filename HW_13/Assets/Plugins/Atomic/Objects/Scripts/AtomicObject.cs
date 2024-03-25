using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace Atomic.Objects
{
    public abstract class AtomicObject : AtomicEntity
    {
        /// <summary>
        ///     <para>Constructor for atomic object. Don't forget call!</para>
        /// </summary>
        public virtual void Compose()
        {
#if UNITY_EDITOR
            Profiler.BeginSample("AtomicObjectCompose", this);
#endif
            AtomicObjectInfo objectInfo = AtomicCompiler.CompileObject(this.GetType());
            
            this.AddTypes(objectInfo.types);
            this.AddProperties(this, objectInfo.properties);
            this.AddSections(this, objectInfo.sections);
            
#if UNITY_EDITOR
            Profiler.EndSample();
#endif
        }

        private void AddProperties(object source, IEnumerable<PropertyInfo> definitions)
        {
            foreach (PropertyInfo definition in definitions)
            {
                string id = definition.id;
                object value = definition.value(source);
                
                if (definition.@override)
                {
                    this.properties[id] = value;
                    continue;
                }

                this.properties.TryAdd(id, value);
            }
        }
        
        private void AddSections(object parent, IEnumerable<SectionInfo> definitions)
        {
            foreach (var definition in definitions)
            {
                object section = definition.GetValue(parent);
                
                this.AddTypes(definition.types);
                this.AddProperties(section, definition.references);
                this.AddSections(section, definition.children);
            }
        }
        
#if UNITY_EDITOR
        [ContextMenu(nameof(Compose))]
        public void ComposeEditor()
        {
            this.types.Clear();
            this.properties.Clear();
            
            this.Compose();
        }
#endif
    }
}