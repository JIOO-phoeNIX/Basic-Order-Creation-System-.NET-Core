using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Task2.DataAccess.Models
{
    public class TABITEM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal? ITEMID { get; set; }

        [Required]
        public string ITEMNAME { get; set; }

        [Required]
        public decimal? ITEMRATE { get; set; }
    }
}
