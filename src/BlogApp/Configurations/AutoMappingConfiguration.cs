namespace BlogApp.Configurations
{
    public static class AutoMappingConfiguration
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMappingConfiguration).Assembly);
            return services;
        }
    }
}
