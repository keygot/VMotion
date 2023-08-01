using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMotion.Models;

[Table("tblComments")]
public  class TblComment
{
    [Key]
    public int CommentId { get; set; }

    public int? PostId { get; set; }

    public int? AccountId { get; set; }

    public int? ParentId { get; set; }

    public string? Contents { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool IsActive { get; set; }

    public string? Thumb { get; set; }

    public int? Slike { get; set; }
}
