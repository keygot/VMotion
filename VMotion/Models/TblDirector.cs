using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMotion.Models;

[Table("tblDirectors")]
public class TblDirector
{
    [Key]
    public int? DirectorId { get; set; }
    public string? FullName { get; set; }

    public string? Avatar { get; set; }

    public DateTime? BirthDate { get; set; }

    public int? Gender { get; set; }
    public string? DirectorRole { get; set; }

    public int? CountryId { get; set; }

    public bool IsActive { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<TblDirectorMovie> MovieDirectors { get; set; }


}
