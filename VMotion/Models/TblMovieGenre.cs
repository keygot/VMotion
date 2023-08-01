using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMotion.Models
{
    [Table("tblMovieGenres")]
    public class TblMovieGenre
    {
        [Key]
        public int? GenreId { get; set; }
        [Key]
        public int? MovieId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? ModifierBy { get; set; }

        public string? Description { get; set; }

        public virtual TblMovie Movie { get; set; }
        public virtual TblGenre Genre { get; set; }

    }
}
