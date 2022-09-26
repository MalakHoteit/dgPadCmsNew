using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dgPadCmsNew.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string Detail { get; set; }
        public string Summary { get; set; }

        [ForeignKey("PostTypeId")]
        public int PostTypeId { get; set; }

        public virtual PostType PostType { get; set; }



        public virtual ICollection<PostAndTerm> postAndTerm { get; set; }

        


    }
}
