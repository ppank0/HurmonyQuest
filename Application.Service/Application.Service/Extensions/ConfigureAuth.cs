namespace Application.Service.Extensions
{
    public static class ConfigureAuth
    {
        public static void AddCustomAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.Authority = $"{configuration["DuendeIdentityServer:BaseUrl"]}";
                    options.TokenValidationParameters.ValidateAudience = false;
                }
            );

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ReadApplication", policy =>
                {
                    policy.RequireAssertion(ctx => ctx.User.HasClaim(c =>
                        c.Type == "scope" && c.Value == "application.read"));
                });
                options.AddPolicy("CreateApplication", policy =>
                {
                    policy.RequireAssertion(ctx => ctx.User.HasClaim(c =>
                        c.Type == "scope" && c.Value == "application.create"));
                });
            });
        }
    }
}
