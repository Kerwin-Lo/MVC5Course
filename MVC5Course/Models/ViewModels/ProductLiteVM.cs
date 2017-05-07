using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    /// <summary>
    /// 精簡版Product
    /// </summary>
    public class ProductLiteVM
    {
        public int ProductId { get; set; }
        [Required]
        [DisplayName("產品名稱")]
        public string ProductName { get; set; }
        [Required]
        [DisplayName("價格")]
        public Nullable<decimal> Price { get; set; }
        [Required]
        [DisplayName("庫存")]
        public Nullable<decimal> Stock { get; set; }
    }
}