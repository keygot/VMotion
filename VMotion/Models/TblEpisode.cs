using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMotion.Models;

[Table("tblEpisodes")]
public  class TblEpisode
{
    [Key]
    public int EpisodeId { get; set; }

    public string? Title { get; set; }
    public string? Img { get; set; }

    public int? EpisodeNumber { get; set; }

    public int? MovieId { get; set; }

    public int? Rating { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifierBy { get; set; }

    public string? Description { get; set; }

    public string? Video { get; set; }
    public bool IsActive { get; set; }

    public TblMovie? Movie { get; set; }
}
