using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HtmlTemplate
{
    public static class CompletionDataHelper
    {
        private static readonly Dictionary<string, Type> types;
        private static readonly Dictionary<string, List<Type>> namespaces;
        static CompletionDataHelper()
        {
            namespaces = new Dictionary<string, List<Type>>();
            Load("mscorlib.dll");
            types = new Dictionary<string, Type>();
            types.Add("string", typeof (string));
            types.Add("int", typeof (int));
            types.Add("double", typeof (double));
            types.Add("float", typeof (float));
            types.Add("decimal", typeof (decimal));
        }
        private static void Load(string dllName)
        {
            var libAssembly = Assembly.Load(dllName);
            foreach (var type in libAssembly.GetTypes())
            {
                var index = type.FullName.LastIndexOf('.');
                if (index == -1)
                {
                    continue;
                }
                var name = type.FullName.Substring(0, index);
                if (!namespaces.ContainsKey(name))
                {
                    namespaces.Add(name, new List<Type>());
                }
                namespaces[name].Add(type);
            }
        }
        public static MyCompletionData[] GetMembers(string typeName)
        {
            Type type;
            if (types.ContainsKey(typeName))
            {
                type = types[typeName];
            }
            else
            {
                var libAssembly = Assembly.Load("mscorlib.dll");
                if (string.IsNullOrEmpty(typeName))
                {
                    return null;
                }
                type = libAssembly.GetType(typeName);
                if (type == null)
                {
                    type = libAssembly.GetType("System." + typeName);
                }
            }
            if (type == null)
            {
                return null;
            }
            var members = type.GetMembers();
            return members.Select(m => m.Name).Distinct().Select(n => new MyCompletionData(n)).OrderBy(c => c.Text).ToArray();
        }
        public static MyCompletionData[] GetTypes(string namespaceName)
        {
            if (namespaces.ContainsKey(namespaceName))
            {
                return namespaces[namespaceName].Select(n => new MyCompletionData(n.Name)).OrderBy(c => c.Text).ToArray();
            }
            return null;
        }
    }
}