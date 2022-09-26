using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dgPadCmsNew.Models
{
    public class Term { 

        public int Id { get; set; }
    
        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string Name { get; set; }

        public string code { get; set; }

        public int TaxonomyId { get; set; }
        [ForeignKey("TaxonomyId")]

        public virtual Taxonomy Taxonomy { get; set; }
       

        public virtual ICollection<PostAndTerm> postAndTerm { get; set; }
        
    }
}
