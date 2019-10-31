using InstituicaoFinanceira.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;

namespace InstituicaoFinanceira.Aplication
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            CheckBirthday();
        }
        public void CheckBirthday()
        {
            new Timer(
            callback: new TimerCallback(Check),
            state: true,
            dueTime: 0,
            period: 3600 * 1000);
        }
        private static TransactionService<Transaction> service = new TransactionService<Transaction>();
        private static Transaction transaction = new Transaction();
        public static void Check(object timerState)
        {            
            if(DateTime.Now.Hour == 21)
            {
                service.InsertJuros(transaction);
            }
        }
    }
}
