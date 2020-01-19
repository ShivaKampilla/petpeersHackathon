using AutoMapper;
using Elastic.Apm.NetCoreAll;
using Elastic.Apm.SerilogEnricher;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetPeers.Models;
using PetPeers.Repo.Interfaces;
using PetPeers.Repo.Service;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;

namespace PetPeers
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: false)
                    .AddEnvironmentVariables();
            configuration = builder.Build();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static Serilog.ILogger Logger { get; set; }
        public static string ConnectionString { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //CORS Policy
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            //Swagger Configuration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Pet Peers API",
                    Description = "Pet Peers API"
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<AquanautDBContext>(op => op.UseSqlServer(Configuration["ConnectionStrings:HackathonDatabase"]));

            //AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfiles());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Dependency Injection
            //services.AddScoped<IHackathonRepository<Login>, HackathonRepository>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IUserService, UserService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAllElasticApm(Configuration);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ConnectionString = Configuration["ConnectionStrings:HackathonDatabase"];
            app.UseCors("ApiCorsPolicy");

            Logger = new LoggerConfiguration()
                               .Enrich.WithElasticApmCorrelationInfo()
                               .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("https://b41acc712c7447afafe97412d9c8de6c.southeastasia.azure.elastic-cloud.com:9243/"))
                               {
                                   AutoRegisterTemplate = true,
                                   AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                                   IndexFormat = "perpeers",
                                   ModifyConnectionSettings = x => x.BasicAuthentication(username: "elastic", password: "3Z0MJM86dZ6JEVqSAlIlEw0I")
                               })
                               .CreateLogger();
            Log.Logger = Logger;

            //Swagger Configuration
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pet Peers API");
            });

            app.UseMvc();
        }
    }
}
