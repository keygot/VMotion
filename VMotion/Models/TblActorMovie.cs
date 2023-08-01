using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMotion.Models;

[Table("tblActor_movies")]
public  class TblActorMovie
{
    [Key]
    public int? ActorId { get; set; }
    [Key]
    public int? MovieId { get; set; }
    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Description { get; set; }

    public virtual TblActor? Actor { get; set; }    
    public virtual TblMovie? Movie { get; set; }
}
