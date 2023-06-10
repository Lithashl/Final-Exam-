using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace mysql_connect.Models
{

	public class Book
	{
        [Key]
        public Int64 no { get; set; }//not required 
        [Required]
        public string? judulbuku { get; set; }
        [Required]
        public string? pengarang { get; set; }
        [Required]
        public Int64 harga { get; set; }
        public string? penerbit { get; set; }
        [Required]
        public string? kategori { get; set; }
        public string? gambar { get; set; }//not required

    }
}

