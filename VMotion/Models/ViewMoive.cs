using System.ComponentModel.DataAnnotations;

namespace VMotion.Models
{
    public class ViewMoive
    {
        

        [Key]
        public int MovieId { get; set; }
        public int Key { get; set; }

        public string? CategoryName {get; set;}
        public int? DirectorId { get; set; }

        public int? ActorId { get; set; }
        public int? EpisodeNumber { get; set; }

        public int? CategoryId { get; set; }

        public string? MovieName { get; set; }

        public string? ImgMin { get; set; }

        public string? ImgMax { get; set; }
        public string? ImgBG { get; set; }

        public string? Video { get; set; }

        public bool IsActive { get; set; }
        public bool Comingsoon { get; set; } // Các phim sắp được ra mắt
        public bool Rank { get; set; } // Hiển thị xếp hạng phim
        public bool Popular { get; set; } // Kiểu hiển thị phổ biến của web

        public int? MovieOrder { get; set; }

        public int? Position { get; set; }
        public int? SView { get; set; }


        public string? Trailer { get; set; }

        public string? Link { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public DateTime? ComingsoonDate { get; set; }

        public DateTime? Yearr { get; set; }

        public int? Rating { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? ModifierBy { get; set; }

        public string? Description { get; set; }
        public string? RankIMG { get; set; }
        public string? RankNumber { get; set; }
        public string? PosterIMG { get; set; }
        public string? FullName { get; set; }
        public string? ActorName { get; set; }



        // Bảng slide
        public string? ImgBgmin { get; set; }

        public string? ImgBgmax { get; set; }

        public string? ImgName { get; set; }

        //
        public virtual ICollection<TblMovieGenre> MovieGenres { get; set; }
        public virtual ICollection<TblActorMovie> MovieActors { get; set; }
        public virtual ICollection<TblDirectorMovie> MovieDirectors { get; set; }         
        public virtual ICollection<TblEpisode> MovieEpisodes { get; set; }



    }
}
