using System;
using FasTnT.DependencyInjection;
using Ninject.Activation;
using Ninject.Web.Common;
using System.Linq;

namespace FasTnT.Web.App_Start.DependencyInjection
{
    public class RequestScope : IScope
    {
        public Func<IContext, object> Value => (ctx) =>
        {
            var scope = ctx.Kernel.Components.GetAll<INinjectHttpApplicationPlugin>().Select(c => c.GetRequestScope(ctx)).FirstOrDefault(s => s != null);
            return scope;
        };
    }
}