using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dgPadCmsNew.Models
{
    public class PostTypeAndTaxonomy
    {
        public int PostTypeAndTaxonomyId { get; set; }

        [ForeignKey("PostTypeId")]
        public int PostTypeId { get; set; }

        public virtual PostType PostType { get; set; }

        [ForeignKey("TaxonomyId")]
        public int TaxonomyId { get; set; }
        public virtual Taxonomy Taxonomy { get; set; }
    }
}
