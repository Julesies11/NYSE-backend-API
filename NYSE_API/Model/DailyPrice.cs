using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NYSE.API.Model
{
    public class DailyPrice
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime date { get; set; }
        [Required]
        [StringLength(10)]
        public string stock_symbol { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal stock_price_open { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal stock_price_close { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal stock_price_low { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal stock_price_high { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal stock_price_adj_close { get; set; }
        [Required]
        public int stock_volume { get; set; }
        [Required]
        [StringLength(10)]
        public string stock_exchange { get; set; }

    }
}
