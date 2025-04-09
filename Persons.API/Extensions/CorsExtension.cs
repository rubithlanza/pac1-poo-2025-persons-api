namespace Persons.API.Extensions
{
    public static class CorsExtension //Para no crear una instancia de la clase 
    {
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddCors(opt =>
            {
                var allowURLS = configuration.GetSection("AllowURLS").Get<string[]>();
                if (allowURLS == null) 
                {
                    allowURLS = [" "];
                }

                //opt.AddPolicy("CorsPolity"), builder => builder.WithOriginis(allowURLS)
                //.AllowAnyMeth

                opt.AddPolicy("CorsPolicy", builder => builder
                 .WithOrigins(allowURLS)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            });


            return services;
        }
    }
}
