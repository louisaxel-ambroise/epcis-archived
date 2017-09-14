namespace FasTnT.Domain.Utils
{
    public static class SystemContext
    {
        private static IClock _clock;

        public static IClock Clock { get { return _clock ?? (_clock = new Clock()); } }
    }
}
