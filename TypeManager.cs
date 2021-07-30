using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace L.Util
{
    public class AssemblyLoader
    {
        public List<Assembly> Assemblies { get; } = new List<Assembly>();
        public AssemblyLoader()
        {
            Assemblies.Add(Assembly.GetExecutingAssembly());
        }

        public AssemblyLoader(bool withCurrentExecutable = false,params string[] paths)
        {
            if(withCurrentExecutable) Assemblies.Add(Assembly.GetExecutingAssembly());
            paths.ForEach(t=>Assemblies.Add(Assembly.LoadFile(t)));
        }

        public List<Type> AllTypes { get; private set; } = new List<Type>();

        public void InitAllType()
        {
            AllTypes = GetAssemblyAllTypes(this.Assemblies);
        }

        public static List<Type> GetAssemblyAllTypes(IEnumerable<Assembly> assemblies)
        {
            var list = new List<Type>();
            assemblies.ForEach(t=>list.AddRange(t.GetTypes().ToList()));
            return list;
        }
        
        //public static object CreateInstance(this string typeName, object[] args)
        //{
        //    var type = Type.GetType(typeName);
        //    if (type == null) throw new NotSupportedException($"Not Found {typeName}.");
        //    return Activator.CreateInstance(type, args);
        //}

        //public static object CreateInstanc(this string typeName, Type[] templateTypes, object[] args)
        //{
        //    var type = Type.GetType(typeName);
        //    if (type == null) throw new NotSupportedException($"Not Found {typeName}.");
        //    type = type.MakeGenericType(templateTypes);
        //    return Activator.CreateInstance(type, args);
        //}
    }
}
