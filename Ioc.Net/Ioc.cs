using System;
using System.Collections.Generic;
using System.Web;

namespace Ioc.Net
{
    public class Ioc
    {
        private static readonly Dictionary<Type, Type> mapper = new Dictionary<Type, Type>();

        public static T CreateInstance<T>(InstanceType instanceType = InstanceType.New)
        {
            var t = typeof (T);
            switch (instanceType)
            {
                case InstanceType.Singleton:
                    if (HttpContext.Current.Application[t.FullName] == null
                        || t != HttpContext.Current.Application[t.FullName].GetType())
                    {
                        HttpContext.Current.Application[t.FullName] = CreateInstance<T>(t);
                    }
                    return (T) HttpContext.Current.Application[t.FullName];
                case InstanceType.PerRequest:
                    if (HttpContext.Current.Items[t.FullName] == null
                        || t != HttpContext.Current.Items[t.FullName].GetType())
                    {
                        HttpContext.Current.Items[t.FullName] = CreateInstance<T>(t);
                    }
                    return (T) HttpContext.Current.Items[t.FullName];
            }
            return CreateInstance<T>(t);
        }

        private static T CreateInstance<T>(Type t)
        {
            if (mapper.ContainsKey(t))
            {
                return (T) Activator.CreateInstance(mapper[t]);
            }
            return Activator.CreateInstance<T>();
        }

        public static void Map<TInterface, TImplementation>() where TImplementation : TInterface
        {
            var inferfaceType = typeof (TInterface);
            var implementationType = typeof (TImplementation);
            if (mapper.ContainsKey(inferfaceType))
            {
                mapper[inferfaceType] = implementationType;
            }
            else
            {
                mapper.Add(inferfaceType, implementationType);
            }
        }
    }

    public enum InstanceType
    {
        New,
        PerRequest,
        Singleton
    }
}