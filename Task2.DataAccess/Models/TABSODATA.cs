using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Task2.DataAccess.Models
{
    public class TABSODATA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal? SODATAID { get; set; }

        [Required(ErrorMessage = "")]
        public decimal? SORDID { get; set; }

        [Required]
        public decimal? ITEMID { get; set; }

        [Required]
        public decimal? ITEMRATE { get; set; }

        [Required]
        public string DATAEXST { get; set; }
    }
}
