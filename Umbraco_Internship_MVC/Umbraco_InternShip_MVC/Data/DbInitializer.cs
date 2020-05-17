using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco_InternShip_MVC.Models;

namespace Umbraco_InternShip_MVC.Data
{
    public class DbInitializer
    {
        public static void Initialize(MvcDrawContext context)
        {


            context.Database.EnsureCreated();

            //Look for any serial numbers
            if (context.SerialNumbers.Any())
            {
                return; //DB has been seeded
            }

            var serialNumbers = new SerialNumber[100];

            for (int i = 0; i < 100; i++)
            {
                serialNumbers[i] = new SerialNumber() { SerialNumberValue = Guid.NewGuid().ToString(), IsActive = true, AmountUsed = 0 };
            }

            context.SerialNumbers.AddRange(serialNumbers);
            context.SaveChanges();


        }
    }
}
            

  

