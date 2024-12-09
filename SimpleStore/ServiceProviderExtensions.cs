namespace SimpleStore
{
    public static class ServiceProviderExtensions
    {
        public static void AddLoginService(this IServiceCollection services)
        {
            services.AddSingleton<ILoginService, SimpleLoginService>();
        }
    }
}
