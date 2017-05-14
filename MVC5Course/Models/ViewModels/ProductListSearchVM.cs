using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels 

{
    /// <summary>
    /// ListProductQuery
    /// </summary>
    public class ProductListSearchVM : IValidatableObject
    {
        public ProductListSearchVM()
        {
            this.s1 = 0;
            this.s1 = 999;
        }
        public string q { get; set; }
        public int s1 { get; set; }
        public int s2 { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.s1 < this.s2)
            {
                yield return new ValidationResult("庫存資料篩選條件錯誤", new string[] { "s1", "s2" });
            }
        }

    }

   
}