using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMotion.Models;

[Table("tblSliders")]
public  class TblSlider
{
    [Key]
    public int SliderId { get; set; }

    public int? MovieId { get; set; }

    public string? Title { get; set; }

    public string? Abstract { get; set; }

    public bool IsActive { get; set; }

    public string? ImgBgmin { get; set; }

    public string? ImgBgmax { get; set; }

    public string? ImgName { get; set; }

    public DateTime? Yearr { get; set; }

    public int? Episodes { get; set; }

    public int? Position { get; set; }

    public int? SliderOrder { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifierBy { get; set; }

    public string? Description { get; set; }
}
