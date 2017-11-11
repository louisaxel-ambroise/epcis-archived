using System;
using FasTnT.DependencyInjection;
using Ninject.Activation;
using System.Web;

namespace FasTnT.Web.App_Start.DependencyInjection
{
    public class RequestScope : IScope
    {
        public Func<IContext, object> Value => (ctx) => HttpContext.Current;
    }
}