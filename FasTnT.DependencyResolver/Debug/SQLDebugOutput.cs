using NHibernate;
using NHibernate.SqlCommand;

namespace FasTnT.DependencyInjection
{
    public class SQLDebugOutput : EmptyInterceptor, IInterceptor
    {
        public override SqlString OnPrepareStatement(SqlString sql)
        {
            System.Diagnostics.Debug.WriteLine("NH: " + sql);

            return base.OnPrepareStatement(sql);
        }
    }
}
