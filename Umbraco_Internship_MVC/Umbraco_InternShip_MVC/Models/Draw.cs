using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Umbraco_InternShip_MVC.Models
{
    public class Draw
    {
        public int Id { get; set; }
        public int UserDrawId { get; set; }
        public int SerialNumberId { get; set; }

        //Navigation property - anvender naming convention til at finde ude af hvilken entity er associeret. 
        public UserDraw UserDraw { get; set; }
        public SerialNumber SerialNumber { get; set; }

    }
}
