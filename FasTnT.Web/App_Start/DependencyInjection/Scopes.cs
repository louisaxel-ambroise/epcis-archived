using FasTnT.DependencyInjection;

namespace FasTnT.Web.App_Start.DependencyInjection
{
    public static class Scopes
    {
        public static IScope WebRequestScope = new RequestScope();
    }
}