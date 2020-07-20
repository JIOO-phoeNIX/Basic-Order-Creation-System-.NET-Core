using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Task2.DataAccess.Models
{
    public class TABCUST
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal CUSTID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Customer Name")]
        [StringLength(20)]
        public string CUSTNAME { get; set; }
    }
}
