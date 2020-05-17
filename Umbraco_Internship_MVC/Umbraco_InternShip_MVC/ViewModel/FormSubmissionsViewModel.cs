using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco_InternShip_MVC.Models;

namespace Umbraco_InternShip_MVC.ViewModel
{
    public class FormSubmissionsViewModel
    {
        public List<UserDraw> _userDraws { get; set; }
        public List<SerialNumber> _serialNumbers { get; set; }

        public FormSubmissionsViewModel(List<UserDraw> userDraws, List<SerialNumber> serialNumbers)
        {
            this._userDraws = userDraws;
            this._serialNumbers = serialNumbers;
        }
    }
}
