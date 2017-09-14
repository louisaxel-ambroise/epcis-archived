using Ninject.Activation;
using System;

namespace FasTnT.DependencyInjection
{
    public interface IScope
    {
        Func<IContext, object> Value { get; }
    }
}
