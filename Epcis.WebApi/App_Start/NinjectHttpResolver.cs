using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Modules;

namespace Epcis.WebApi
{
    public class NinjectHttpResolver : IDependencyResolver
    {
        public IKernel Kernel { get; private set; }

        public NinjectHttpResolver(params INinjectModule[] modules)
        {
            Kernel = new StandardKernel(modules);
        }

        public NinjectHttpResolver(Assembly assembly)
        {
            Kernel = new StandardKernel();
            Kernel.Load(assembly);
        }

        public object GetService(Type serviceType)
        {
            return Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }

        public void Dispose()
        {
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }
    }
}