using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMotion.Models;

[Table("tblActors")]
public  class TblActor
{
    [Key]
    public int? ActorId { get; set; }
    public string? FullName { get; set; }

    public string? Avatar { get; set; }

    public DateTime? BirthDate { get; set; }

    public int? Gender { get; set; }
    public string? ActorRole { get; set; }


    public int? CountryId { get; set; }

    public bool IsActive { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Description { get; set; }

    public TblCountry? Country { get; set; }

    public virtual ICollection<TblActorMovie> MovieActors { get; set; }



}
