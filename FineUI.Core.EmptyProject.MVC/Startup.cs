using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Schema;

namespace FineUI.Core.EmptyProject.MVC
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // 登记应用使用的服务。
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession();

            // 配置请求参数限制
            services.Configure<FormOptions>(x =>
            {
                x.ValueCountLimit = 1024;   // 请求参数的个数限制（默认值：1024）
                x.ValueLengthLimit = 4194304;   // 单个请求参数值的长度限制（默认值：4194304 = 1024 * 1024 * 4）
            });

            // FineUI 服务
            services.AddFineUI(Configuration);

            // AddFineUI 已自动登记 FineUI 专属模型绑定器。
            services.AddControllersWithViews().AddNewtonsoftJson().AddRazorRuntimeCompilation();


        }

        // 配置应用的 HTTP 请求管道。
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            // FineUI 中间件（确保 UseFineUI 位于 UseEndpoints 的前面）
            app.UseFineUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "area",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
