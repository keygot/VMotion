using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMotion.Models
{
    [Table("tblPhanTrangs")]
    public class TblPhanTrang
    {
        [Key]
        public string TuKhoa { get; set; }
        public string GiaTri { get; set; }
        public string MoTa { get; set; }
    }
}
