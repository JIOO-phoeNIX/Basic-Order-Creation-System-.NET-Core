using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Task2.DataAccess.Models
{
    public class TABSORDER
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal? SORDID { get; set; }

        [Required]
        public DateTime SORDDATE { get; set; }

        [Required]
        public decimal? CUSTID { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal? SORDAMNT { get; set; }

        [Required]
        public string DATAEXST { get; set; }
    }
}
