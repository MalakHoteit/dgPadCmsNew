using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dgPadCmsNew.Models
{
    public class PostType
    {
        public int PostTypeId { get; set; }

        
        public string Title { get; set; }
       
        public string Code { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<PostTypeAndTaxonomy> postTypeAndTaxonomy { get; set; }



    }
}
