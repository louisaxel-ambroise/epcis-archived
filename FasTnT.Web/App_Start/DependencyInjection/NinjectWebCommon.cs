using FasTnT.DependencyInjection;
using FasTnT.Web;
using FasTnT.Web.App_Start.DependencyInjection;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Extensions.Wcf;
using Ninject.Web.Common;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]
namespace FasTnT.Web
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();
        private static IKernel _kernel;

        public static void Start()
        {
            _kernel = CreateKernel();

            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(() => _kernel);

            BaseNinjectServiceHostFactory.SetKernel(_kernel);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(_kernel));
        }

        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["FasTnT.Database"].ConnectionString;

            kernel.Load
            (
                new DataModule(Scopes.WebRequestScope, connectionString),
                new DomainModule(Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "App_Data"), "*.xsd"))
            );
        }
    }
}