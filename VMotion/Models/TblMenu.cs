using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMotion.Models;


[Table("tblMenus")]
public class TblMenu
{
    [Key]
    public int MenuId { get; set; }

    public int? CategoryId { get; set; }

    public bool IsActive { get; set; }

    public string? MenuName { get; set; }

    public string? ControllerName { get; set; }

    public string? ActionName { get; set; }

    public string? IdNone { get; set; }

    public int? Levels { get; set; }

    public int? ParentId { get; set; }

    public int? MenuOrder { get; set; }

    public int? Position { get; set; }

    public string? Link { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifierBy { get; set; }

    public string? Description { get; set; }

    public TblCategory? Category { get; set; }
}
