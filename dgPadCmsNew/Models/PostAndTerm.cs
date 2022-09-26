using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dgPadCmsNew.Models
{
    public class PostAndTerm
    {
        public int PostAndTermId { get; set; }

        [ForeignKey("PostId")]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }


        [ForeignKey("TermId")]
        public int TermId { get; set; }
        public virtual Term Term { get; set; }



    }
}
