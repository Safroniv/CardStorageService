using CardStorageService.Data;
using CardStorageService.Models;
using CardStorageService.Services;
using CardStorageService.Services.Impl;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

namespace CardStorageService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Options Services

            builder.Services.Configure<DatabaseOptions>(options =>
            {
                builder.Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
            });

            #endregion

            #region Logging service

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit= 4096;
                logging.RequestHeaders.Add("Autorization");
                logging.RequestHeaders.Add("X-Real-IP");
                logging.RequestHeaders.Add("X-Forwarded-For");
            });

            builder.Host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole(); 

            }).UseNLog(new NLogAspNetCoreOptions() {RemoveLoggerFactoryFilter = true });

            #endregion

            #region Configure EF DBContext Service (CardStorageService DataBase)

            builder.Services.AddDbContext<CardStorageServiceDbContext>(options => 
            {
                options.UseSqlServer(builder.Configuration["Settings:DatabaseOptions:ConnectionString"]);
            });

            #endregion

            #region Configure Repository Services

            builder.Services.AddScoped<IClientRepositoryService, ClientRepository>();

            builder.Services.AddScoped<ICardRepositoryService, CardRepository>();

            #endregion


            builder.Services.AddControllers();
            // Learn mo re about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseHttpLogging();

            app.MapControllers();

            app.Run();
        }
    }
}