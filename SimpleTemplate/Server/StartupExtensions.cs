using SimpleTemplate.Identity;
using SimpleTemplate.Persistance;

namespace SimpleTemplate.Server
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(
        this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen();
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {

            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Template API");
                });
            }

            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            return app;

        }
    }
}