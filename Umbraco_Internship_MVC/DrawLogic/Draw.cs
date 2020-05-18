using System;
using System.Collections.Generic;
using System.Text;

namespace DrawLogic
{
    public class Draw
    {
        public int Id { get; set; }
        public int UserDrawId { get; set; }
        public int SerialNumberId { get; set; }

        public UserDraw UserDraw { get; set; }
        public SerialNumber SerialNumber { get; set; }
    }
}
