using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMotion.Models
{
    [Table("tblGenres")]
    public class TblGenre
    {
        [Key]
        public int? GenreId { get; set; }
        public string? GenreName { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? ModifierBy { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<TblMovieGenre> MovieGenres { get; set; }

    }
}
