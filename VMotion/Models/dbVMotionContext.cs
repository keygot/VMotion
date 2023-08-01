using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VMotion.Areas.Admin.Models;

namespace VMotion.Models;

public class dbVMotionContext : DbContext
{
    public dbVMotionContext()
    {
    }

    public dbVMotionContext(DbContextOptions<dbVMotionContext> options)
        : base(options)
    {
    }

    public DbSet<TblAccount> TblAccounts { get; set; }
    public DbSet<TblRole> TblRoles { get; set; } = null!;
    public  DbSet<TblActor> TblActors { get; set; }

    public  DbSet<TblActorMovie> TblActorMovies { get; set; }

    public DbSet<TblAdminMenu> TblAdminMenus { get; set; }

    public  DbSet<TblCategory> TblCategories { get; set; }

    public  DbSet<TblComment> TblComments { get; set; }

    public  DbSet<TblCountry> TblCountries { get; set; }

    public  DbSet<TblDirector> TblDirectors { get; set; }

    public  DbSet<TblDirectorMovie> TblDirectorMovies { get; set; }

    public  DbSet<TblEpisode> TblEpisodes { get; set; }

    public  DbSet<TblLanguage> TblLanguages { get; set; }

    public  DbSet<TblMenu> TblMenus { get; set; }

    public  DbSet<TblMovie> TblMovies { get; set; }

    public  DbSet<TblPost> TblPosts { get; set; }

    public  DbSet<TblPostStatus> TblPostStatuses { get; set; }

    public  DbSet<TblSlider> TblSliders { get; set; }
    public  DbSet<TblPhanTrang> TblPhanTrangs { get; set; }

    public DbSet<TblGenre> TblGenres { get; set; }
    public DbSet<TblMovieGenre> TblMovieGenres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblActorMovie>()
    .HasKey(am => new { am.MovieId, am.ActorId });


        modelBuilder.Entity<TblMovieGenre>()
            .HasKey(mg => new { mg.MovieId, mg.GenreId });

       

        

        modelBuilder.Entity<TblActorMovie>()
            .HasOne(am => am.Movie)
            .WithMany(m => m.MovieActors)
            .HasForeignKey(am => am.MovieId);

      


        modelBuilder.Entity<TblDirectorMovie>()
            .HasKey(hg => new { hg.MovieId, hg.DirectorId });

        modelBuilder.Entity<TblDirectorMovie>()
          .HasOne(hg => hg.Movie)
          .WithMany(a => a.MovieDirectors)
          .HasForeignKey(hg => hg.MovieId);
    }


  




}
