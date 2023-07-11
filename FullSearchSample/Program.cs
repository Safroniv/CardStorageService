using FullSearchSample.Services;
using FullSearchSample.Services.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FullSearchSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    #region  Configure EF DBContext Service (CardStorageService Database)

                    services.AddDbContext<DocumentDbContext>(options =>
                    {
                        options.UseSqlServer(@"data source=SAFRONIV-HONOR\SQLEXPRESS; initial catalog=DocumentsDatabase;User Id=DocumentsDatabaseUser;Password=12345;MultipleActiveResultSets=True;App=EntityFramework;TrustServerCertificate=True");
    
                    });

                    #endregion

                    #region Configure Repositories

                    services.AddTransient<IDocumentRepository, DocumentRepository>();

                    #endregion


                    //#region Configure Repositories
                    //services.AddTransient<IDocumentRepository, DocumentRepository>();
                    //#endregion



                })
                .Build();

            host.Services.GetRequiredService<IDocumentRepository>().LoadDocuments();
        }
    }
}