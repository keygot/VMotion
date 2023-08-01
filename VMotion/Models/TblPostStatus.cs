using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMotion.Models;

[Table("tblPostStatus")]
public class TblPostStatus
{
    [Key]
    public int Status { get; set; }

    public string? StatusName { get; set; }

    public string? Description { get; set; }
}
