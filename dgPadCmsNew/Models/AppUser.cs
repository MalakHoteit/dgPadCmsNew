using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace dgPadCmsNew.Models
{
    
    public class AppUser : IdentityUser
    {
      
        public string Occupation { get; set; }
    }
}
