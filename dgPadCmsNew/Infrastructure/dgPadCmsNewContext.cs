using dgPadCmsNew.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dgPadCmsNew.Infrastructure
{
    public class dgPadCmsNewContext : IdentityDbContext<AppUser>
    {
        public dgPadCmsNewContext()
        {
        }

        public dgPadCmsNewContext(DbContextOptions<dgPadCmsNewContext> options)
            : base(options)
        {
        }

        public DbSet<Taxonomy> Taxonomies { get; set; }

        public DbSet<Term> Term { get; set; }
        public DbSet<PostType> PostTypes { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostAndTerm> PostsAndTerms { get; set; }
        public DbSet<PostTypeAndTaxonomy> PostTypesAndTaxonomies { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            //Term has many postTerms
            builder.Entity<Term>()
                .HasMany(a => a.postAndTerm)
                .WithOne(ab => ab.Term);

            //Post has many postTerms
            builder.Entity<Post>()
                .HasMany(a => a.postAndTerm)
                .WithOne(ab => ab.Post);

            builder.Entity<Taxonomy>()
                .HasMany(a => a.Terms)
                .WithOne(ab => ab.Taxonomy);

            builder.Entity<Taxonomy>()
                .HasMany(a => a.postTypeAndTaxonomy)
                .WithOne(ab => ab.Taxonomy);


            builder.Entity<PostType>()
                .HasMany(a => a.postTypeAndTaxonomy)
                .WithOne(ab => ab.PostType);

            builder.Entity<PostType>()
                .HasMany(a => a.Posts)
                .WithOne(ab => ab.PostType);

            base.OnModelCreating(builder);
        }


    }
}

