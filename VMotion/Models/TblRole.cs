using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMotion.Models
{
    [Table("tblRoles")]
    public partial class TblRole
    {
       
        [Key]
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? Description { get; set; }

    }
}
