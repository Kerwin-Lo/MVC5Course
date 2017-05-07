using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ValidationAttributes
{
    public class 商品名稱不可重複Attribute : DataTypeAttribute
    {
        FabricsEntities db = new FabricsEntities();
        public 商品名稱不可重複Attribute() : base(DataType.Text)
        {

        }

        public override bool IsValid(object value)

        {
            var strProductName = (string)value;
            var result = db.Product.Where(p => p.ProductName == strProductName && p.Is刪除 == false).FirstOrDefault();
            return result == null;
        }
    }
}