using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaContentHSE.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser 
    {
        public string Id { get; set; }

        public string FullName { get; set; }
    }
}
