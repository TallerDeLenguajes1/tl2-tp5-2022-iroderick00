using CadeteriaWeb.Interfaces;
using CadeteriaWeb.Repositorios;
using NLog;
using NLog.Web;

namespace CadeteriaWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("comienzo del main");

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
                builder.Services.AddControllersWithViews();
                //mapeo
                builder.Services.AddAutoMapper(typeof(Program));
                //inyeccion de dependencias
                builder.Services.AddTransient<ICadeteRepositorio, CadeteRepositorio>();
                builder.Services.AddTransient<IPedidosRepositorio, PedidosRepositorio>();
                builder.Services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
                builder.Services.AddTransient<IClienteRepositorio, ClienteRepositorio>();
                //logger
                builder.Host.UseNLog();
                //usuarios
                builder.Services.AddDistributedMemoryCache();
                builder.Services.AddSession(options =>
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(10);
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                });

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthorization();

                app.UseSession();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Usuarios}/{action=Index}/{id?}");

                app.Run();
            }
            catch (Exception e)
            {
                logger.Error(e, "El programa se detuvo porque hubo una excepción");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }

        }
    }
}