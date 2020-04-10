using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotingSystemMVCJQueryAJAX.Models
{
    public class Registration
    {
        //[Required]
        //[FromQuery]
        public string VoterID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string BloodGroup { get; set; }
        public string Address { get; set; }
        public string DOB { get; set; }
        public string Telephone { get; set; }
    }
}