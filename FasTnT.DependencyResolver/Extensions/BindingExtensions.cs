using Ninject.Syntax;

namespace FasTnT.DependencyInjection
{
    public static class BindingExtensions
    {
        public static void UsingScope<T>(this IBindingWhenInNamedWithOrOnSyntax<T> binding, IScope scope)
        {
            binding.InScope(scope.Value);
        }
    }
}
