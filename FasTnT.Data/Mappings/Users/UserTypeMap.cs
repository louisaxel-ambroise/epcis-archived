using FasTnT.Domain.Model.Users;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Users
{
    public class UserTypeMap : ClassMap<UserType>
    {
        public UserTypeMap()
        {
            Table("user_type");
            Schema("users");

            Id(x => x.Id).Column("id");
            Map(x => x.Name).Column("name");
        }
    }
}
