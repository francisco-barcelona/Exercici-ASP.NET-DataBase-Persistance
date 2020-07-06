using EmpleatsSQL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpleatsSQL.Data
{
    public class DummyData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<workerContext>();
                context.Database.EnsureCreated();

                // Check if the workerContexts is created and it has data. If so, return
                if(context.workerContexts != null && context.workerContexts.Any())
                {
                    return;
                }

                // If it doesn't have any data
                var workers = GetWorkers(context).ToArray();
                context.workerContexts.AddRange(workers);
                context.SaveChanges();

            }
        }
        public static List<worker> GetWorkers(workerContext db)
        {
            List<worker> Workers = new List<worker>()
            {
                new worker
                {
                    Name="Paco",
                    Surname="Rivas",
                    Job="Dev Jr",
                    Salary= 1000
                },
                new worker
                {
                    Name="María",
                    Surname="Pérez",
                    Job="Jefa",
                    Salary= 3000
                },
                new worker
                {
                    Name="Cris",
                    Surname="López",
                    Job="Subdirectora",
                    Salary= 2000
                }
            };
            return Workers;
        }            
        
    }
}
