using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(FuelLogFuelingBackend.StartUp))]

namespace FuelLogFuelingBackend
{
    class StartUp: FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            FunctionsHostBuilderContext context = builder.GetContext();
            var config = new ConfigurationBuilder()
                            .SetBasePath(context.ApplicationRootPath)
                            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables()
                            .Build();

            //Get Connection String
            //string SqlConnection = config.GetConnectionString("SqlConnectionString");
            string SqlConnection = "Server=tcp:wwfs.database.windows.net,1433;Initial Catalog=wwfs;Persist Security Info=False;User ID=wwfs;Password=sfww2020!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            /*
            builder.Services.AddDbContext<UserDataContext>(
                options => options.UseSqlServer(SqlConnection));
            */

            builder.Services.AddDbContext<FuelingDataContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, SqlConnection));


        }
    }
}
