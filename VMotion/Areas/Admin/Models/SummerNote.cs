﻿namespace VMotion.Areas.Admin.Models
{
    public class SummerNote
    {
         public SummerNote(string idEditor, bool loadLibrary = true)
        {
            IDEditor = idEditor;
            LoadLibrary = loadLibrary;
        }

        public string IDEditor { get; set; }
        public bool LoadLibrary { get; set; }
        public int tabsize { get; set; } = 2;
        public int Height { get; set; } = 300;
        public string toolBar { get; set; } = @"
          [
              ['style', ['style']],
              ['font', ['bold', 'underline', 'clear']],
              ['color', ['color']],
              ['para', ['ul', 'ol', 'paragraph']],
              ['table', ['table']],
              ['insert', ['link', 'picture', 'video', 'elfinder']],
              ['view', ['fullscreen', 'codeview', 'help']]
          ]
        ";
    }
}
