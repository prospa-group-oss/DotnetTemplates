namespace ProspaAspNetCoreApi
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
    }
}
