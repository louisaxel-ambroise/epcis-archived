namespace FasTnT.Domain.Model.Users
{
    public class UserType
    {
        public virtual short Id { get; set; }
        public virtual string Name { get; set; }

        public static UserType ApiUser = new UserType { Id = 1, Name = "ApiUser" };
        public static UserType DashboardUser = new UserType { Id = 2, Name = "DashboardUser" };

        public override bool Equals(object obj)
        {
            var other = obj as UserType;

            if (other == null) return false;

            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
