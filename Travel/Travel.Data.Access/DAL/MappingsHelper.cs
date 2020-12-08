using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Travel.Data.Access.Maps.Common;
using Travel.Data.Access.Maps.Main;

namespace Travel.Data.Access.DAL
{
    public static class MappingsHelper
    {
        public static IEnumerable<IMap> GetMainMappings()
        {
            var assemblyTypes = typeof(UserMap).GetTypeInfo().Assembly.DefinedTypes;
            var mappings = assemblyTypes
                .Where(t => t.Namespace != null && t.Namespace.Contains(typeof(UserMap).Namespace ?? throw new InvalidOperationException()))
                .Where(x => typeof(IMap).GetTypeInfo().IsAssignableFrom(x));
            mappings = mappings.Where(x => !x.IsAbstract);
            return mappings.Select(m => (IMap) Activator.CreateInstance(m.AsType())).ToArray();
        }
    }
}
