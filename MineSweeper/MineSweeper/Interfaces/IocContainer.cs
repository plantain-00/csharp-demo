using Autofac;
using Autofac.Configuration;

namespace MineSweeper.Interfaces
{
    internal static class IocContainer
    {
        private static readonly IContainer _container;
        static IocContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
            _container = builder.Build();
        }
        internal static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}