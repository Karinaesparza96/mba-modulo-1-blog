namespace BlogApi.Configurations;

public static class ApiConfig
{
    public static WebApplicationBuilder AddApiConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true);
        return builder;
    }
}