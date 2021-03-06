﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DrawLogic
{
    public class SerialNumber
    {
        public int Id { get; set; }
        public string SerialNumberValue { get; set; }
        public bool IsActive { get; set; }
        public int AmountUsed { get; set; }

        public ICollection<Draw> Draws { get; set; }
    }
}
