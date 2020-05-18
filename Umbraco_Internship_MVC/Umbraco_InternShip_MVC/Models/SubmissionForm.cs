using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Umbraco_InternShip_MVC.Models
{
    public class SubmissionForm
    {
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please"), Required]
        public string FirstName { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please"), Required]
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public DateTime Age { get; set; }

        public SubmissionForm(string firstName, string lastName, string emailAddress, string serialNumber, DateTime age)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            SerialNumber = serialNumber;
            Age = age;
        }

        public SubmissionForm()
        {

        }
    }
}
