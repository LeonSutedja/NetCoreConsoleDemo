using Autofac;
using Autofac.Core;
using System.Linq;
using System.Reflection;
using TypeExtensions = Autofac.TypeExtensions;

namespace NetCoreConsoleDemo
{
    public static class AutofacContainer
    {
        public static IContainer Container { get; private set; }

        public static void Initiate()
        {
            var bootstrapper = new AutofacBootstrapper();
            Container = bootstrapper.InitiateAutofacContainerBuilder();
        }

        private class AutofacBootstrapper
        {
            public IContainer InitiateAutofacContainerBuilder()
            {
                var builder = new ContainerBuilder();
                _registerAutofacContainers(builder);
                return builder.Build();
            }

            private void _registerAutofacContainers(ContainerBuilder builder)
            {
                builder.RegisterType<AppSettingsConfiguration>()
                    .As<IConfiguration>()
                    .SingleInstance();

                // this is all current assemblies
                //var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                //builder.RegisterAssemblyTypes(allAssemblies)
                //    .AsClosedTypesOf(typeof(IRepository<>))
                //    .SingleInstance();

                // Registering Services by convention
                //builder.RegisterAssemblyTypes(allAssemblies)
                //    .Where(t => t.Name.EndsWith("Service"))
                //    .AsImplementedInterfaces()
                //    .SingleInstance();

                _registerCommandHandlers(builder);
            }

            private void _registerCommandHandlers(ContainerBuilder builder)
            {
                builder.RegisterType<CommandHandlerFactory>().As<ICommandHandlerFactory>();

                // Register the open generic with a name so the
                // decorator can use it.
                builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                    .As(type => type.GetInterfaces()
                        .Where(interfaceType => TypeExtensions.IsClosedTypeOf(interfaceType, typeof(ICommandHandler<,>)))
                        .Select(interfaceType => new KeyedService("commandHandler", interfaceType)))
                    .InstancePerLifetimeScope();

                // Register the generic decorator so it can wrap
                // the resolved named generics.
                builder.RegisterGenericDecorator(
                    typeof(CommandHandlerLoggerDecorator<,>),
                    typeof(ICommandHandler<,>),
                    fromKey: "commandHandler");
            }
        }
    }
}