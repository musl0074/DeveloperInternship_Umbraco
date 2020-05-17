using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Umbraco_InternShip_MVC.Models
{
    public class UserDraw
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        public ICollection<Draw> Draws{ get; set; }
    }
}
