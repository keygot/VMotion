using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMotion.Models;

[Table("tblDirector_movies")]
public class TblDirectorMovie
{
    [Key]

    public int? DirectorId { get; set; }

    [Key]

    public int? MovieId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Description { get; set; }

    public virtual TblDirector Director { get; set; }
    public virtual TblMovie Movie { get; set; }
}
