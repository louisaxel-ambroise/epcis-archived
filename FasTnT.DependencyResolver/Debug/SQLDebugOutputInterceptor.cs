using NHibernate;
using NHibernate.SqlCommand;

namespace FasTnT.DependencyInjection
{
    public class SQLDebugOutputInterceptor : EmptyInterceptor, IInterceptor
    {
        public override SqlString OnPrepareStatement(SqlString sql)
        {
            System.Diagnostics.Debug.WriteLine("[SQL Statement] " + sql);

            return base.OnPrepareStatement(sql);
        }
    }
}
