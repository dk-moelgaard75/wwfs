using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(FuelLogUserBackend.StartUp))]

namespace FuelLogUserBackend
{

    class StartUp : FunctionsStartup
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
            string SqlConnection = config.GetConnectionString("SqlConnectionString");
            //string SqlConnection = "server=localhost; Database=wwfs; User Id=wwfs; Password=sfww2020!; Trusted_Connection=false";
            /*
            builder.Services.AddDbContext<UserDataContext>(
                options => options.UseSqlServer(SqlConnection));
            */
            
            builder.Services.AddDbContext<UserDataContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, SqlConnection));
            

        }
    }
}
