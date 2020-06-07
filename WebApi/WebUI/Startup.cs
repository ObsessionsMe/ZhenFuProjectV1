using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.DBContext;
using Infrastructure.LogConfig;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repository.RepositoryService;
using Repository.ServiceInterface;
using RepositoryFactory.RepositoryService;
using RepositoryFactory.ServiceInterface;
using WebUI.App_Start;

namespace WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //注册数据库连接字符串
            //services.AddDbContext<ESMDBContext>(options => options.UseMySql(Configuration.GetConnectionString("ESMConnection")));
            services.AddDbContext<ZFDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DbConnectionString")), ServiceLifetime.Transient);
            //注册HttpContextm等服务
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSession();
            //配置跨域请求-所有都可访问
            services.AddCors(options => options.AddPolicy("ZFCors",
            builder => builder.WithOrigins("http://*.*.*.*")
            .AllowAnyMethod().AllowAnyHeader().AllowCredentials()));

            //注册业务逻辑服务接口
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IGoodsRepository, GoodsRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IReceiveAddressRepository, ReceiveAddressRepository>();
            services.AddTransient<IUserPorintsRecordRepository, UserPorintsRecordRepository>();
            services.AddTransient<IUserPrintsSumRepository, UserPrintsSumRepository>();
            services.AddTransient<ICashRepository, CashRepository>();
            services.AddTransient<IAttachMentRepository, AttachMentRepository>();
            services.AddTransient<IUserBasePorintsRecordRepository, UserBasePorintsRecordRepository>();
            services.AddTransient<IDictionaryRepository, DictionaryRepository>();
            services.AddTransient<IAliNotifyRepository, AliNotifyRepository>();
            services.AddTransient<IUserProductFrameworkRepository,UserProductFrameworkRepository>();
            services.AddTransient<IProductCfgRepository, ProductCfgRepository>();
            //注册文件访问权限
            services.AddDirectoryBrowser();

            //注册全局日志
            services.AddLogging(logConfig =>
            {
                LogHelper.LoadLogger();
            });
            services.AddMvc(option =>
            {
                option.Filters.Add<GlobleExceptionAttribute>();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //====================自定义配置===================
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            //启用Cors，配置跨域
            app.UseMiddleware<CorsMiddleware>();
            app.UseCors("ZFCors");
            //配置文件目录访问
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Configs")),
                RequestPath = "/Configs",
                EnableDirectoryBrowsing = true
            });
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Upload")),
                RequestPath = "/Upload",
                EnableDirectoryBrowsing = true
            });
            //==============================================
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
