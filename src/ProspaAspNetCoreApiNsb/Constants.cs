namespace ProspaAspNetCoreApiNsb
{
    public static class Constants
    {
        public static readonly string HealthEndpoint = "/health";

        public static class Auth
        {
            public static class Policies
            {
                public const string ReadPolicy = "Read";
                public const string WritePolicy = "Write";
            }
        }

        public static class ConnectionStrings
        {
            public static readonly string ServiceBus = nameof(ServiceBus);
        }
    }
}
