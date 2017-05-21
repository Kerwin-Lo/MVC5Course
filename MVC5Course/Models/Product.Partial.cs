namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using ValidationAttributes;
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {
        public int 訂單數量
        {
            get
            {
                return this.OrderLine.Count;
                //return this.OrderLine.Where(p => p.Qty > 400).Count;
                //return this.OrderLine.Where(p => p.Qty > 400).ToList().Count;
                //return this.OrderLine.Count(p => p.Qty > 400);
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Price > 100 && this.Stock > 5)
            {
                yield return new ValidationResult("價格與庫存數量不合理",
                new string[] { "Price", "Stock" });
            }
            using (var db = new FabricsEntities())
            { 
                var prod = db.Product.FirstOrDefault(p => p.ProductId == this.ProductId);
                if (prod != null && prod.OrderLine.Count() > 5 && this.Stock == 0)
                {
                    yield return new ValidationResult("Stock 與訂單數量不匹配",
                    new string[] { "Stock" });
                }
            }
            yield break;
        }
    }

    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        //[Required(ErrorMessage = "請輸入產品")]
        //[StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        [DisplayName("產品名稱")]
        [商品名稱必須包含Will字串(ErrorMessage = "商品名稱必須包含Will字串")]
        public string ProductName { get; set; }

        [DisplayFormat(DataFormatString ="{0:0,0}",HtmlEncode =true)]
        [DisplayName("價格")]
        public Nullable<decimal> Price { get; set; }
        [DisplayName("啟用")]
        public Nullable<bool> Active { get; set; }
        [DisplayName("庫存")]
        public Nullable<decimal> Stock { get; set; }

        [DisplayName("建立時間")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", HtmlEncode = true)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public System.DateTime CreateDate { get; set; }

        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
