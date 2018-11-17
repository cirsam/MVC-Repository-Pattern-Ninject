using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMVCAPP.DataModels
{
    public class Books
    {
        [Key]
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public float Price { get; set; }
    }
}