using System.Collections.Generic;

namespace Atomic.Objects
{
    internal sealed class AtomicObjectInfo
    {
        internal readonly IEnumerable<string> types;
        internal readonly IEnumerable<PropertyInfo> properties;
        internal readonly IEnumerable<SectionInfo> sections;

        internal AtomicObjectInfo(
            IEnumerable<string> types,
            IEnumerable<PropertyInfo> properties,
            IEnumerable<SectionInfo> sections
        )
        {
            this.types = types;
            this.properties = properties;
            this.sections = sections;
        }
    }
}