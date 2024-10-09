namespace BlogApi.Configurations;

public static class AutoMapperConfig
{
    public static WebApplicationBuilder AddAutoMapperConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(AutoMappingConfiguration).Assembly);
        return builder;
    }
}