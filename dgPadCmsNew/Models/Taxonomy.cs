using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dgPadCmsNew.Models
{
    public class Taxonomy
    {
        public int TaxonomyId { get; set; }
        
        
        public string Name { get; set; }

       
        public string Code { get; set; }

        public virtual ICollection<Term> Terms { get; set; }

        public virtual ICollection<PostTypeAndTaxonomy> postTypeAndTaxonomy { get; set; }

    }
}
