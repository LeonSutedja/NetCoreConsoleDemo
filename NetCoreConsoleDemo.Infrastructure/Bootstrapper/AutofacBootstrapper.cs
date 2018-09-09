using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using NetCoreConsoleDemo.Infrastructure.CommandHandler;
using NetCoreConsoleDemo.Infrastructure.Configuration;
using Serilog;
using TypeExtensions = Autofac.TypeExtensions;

namespace NetCoreConsoleDemo.Infrastructure.Bootstrapper
{
    public static class AutofacContainer
    {
        private static IContainer _container { get; set; }

        public static void Initiate()
        {
            var a = Assembly.GetCallingAssembly().GetReferencedAssemblies();
            var referencedProjectAssemblyNames = 
                Assembly
                    .GetCallingAssembly()
                    .GetReferencedAssemblies()
                    .Where(assembly => assembly.Name.StartsWith("NetCoreConsoleDemo"))
                    .ToList();
            var assemblies = referencedProjectAssemblyNames
                .Select(assemblyName => Assembly.Load(assemblyName))
                .ToArray();

            var bootstrapper = new AutofacBootstrapper();
            _container = bootstrapper.InitiateAutofacContainerBuilder(assemblies);
        }

        public static T Resolve<T>()
        {
            try
            {
                return _container.Resolve<T>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static object Resolve(Type type)
        {
            try
            {
                return _container.Resolve(type);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private class AutofacBootstrapper
        {
            public IContainer InitiateAutofacContainerBuilder(Assembly[] allReferencedAssemblies)
            {
                var builder = new ContainerBuilder();
                _registerAutofacContainers(builder, allReferencedAssemblies);
                return builder.Build();
            }

            private void _registerAutofacContainers(ContainerBuilder builder, Assembly[] allReferencedAssemblies)
            {
                builder.Register<ILogger>((c, p) =>
                    new LoggerConfiguration()
                        .WriteTo.RollingFile("rollinglog.log")
                        .WriteTo.Console()
                        .CreateLogger())
                    .SingleInstance();

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

                _registerCommandHandlers(builder, allReferencedAssemblies);
                _registerEventHandlers(builder, allReferencedAssemblies);
            }

            private void _registerCommandHandlers(ContainerBuilder builder, Assembly[] allReferencedAssemblies)
            {
                builder.RegisterType<CommandHandlerFactory>().As<ICommandHandlerFactory>();

                // Register the open generic with a name so the
                // decorator can use it.
                builder.RegisterAssemblyTypes(allReferencedAssemblies)
                    .As(type => type.GetInterfaces()
                        .Where(interfaceType => TypeExtensions.IsClosedTypeOf(interfaceType, typeof(ICommandHandler<>)))
                        .Select(interfaceType => new KeyedService("commandHandler", interfaceType)))
                    .InstancePerLifetimeScope();

                // Register the generic decorator so it can wrap
                // the resolved named generics.
                builder.RegisterGenericDecorator(
                    typeof(CommandHandlerErrorHandlingDecorator<>),
                    typeof(ICommandHandler<>),
                    fromKey: "commandHandler")
                    .Keyed("commandHandlerErrorDecorated", typeof(ICommandHandler<>));

                builder.RegisterGenericDecorator(
                    typeof(CommandHandlerLoggerDecorator<>),
                    typeof(ICommandHandler<>),
                    fromKey: "commandHandlerErrorDecorated");
            }

            private void _registerEventHandlers(ContainerBuilder builder, Assembly[] allReferencedAssemblies)
            {
                var eventHandlerTypes = new List<Type>();
                allReferencedAssemblies.ToList().ForEach(assembly =>
                    eventHandlerTypes.AddRange(
                        assembly.GetTypes().Where(
                            t => !t.IsInterface &&
                                 t.IsClass &&
                                 !t.IsGenericType &&
                                 t.Name.EndsWith("EventHandler"))));

                eventHandlerTypes.ForEach(t => builder.RegisterType(t).AsImplementedInterfaces());
            }
        }
    }
}