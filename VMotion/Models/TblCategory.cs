using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMotion.Models;

[Table("tblCategories")]
public class TblCategory
{
    [Key]
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? ShortName { get; set; }

    public bool IsActive { get; set; }

    public int? CategoryOrder { get; set; }

    public bool HomeFlag { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    //public virtual ICollection<TblPost> TblPosts { get; } = new List<TblPost>();
}
