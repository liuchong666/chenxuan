using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeXinYuan.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int CategoryId { get; set; }

        public string ProductRemark { get; set; }

        public string ImgUrl { get; set; }

        public bool IsDel { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public int Pid { get; set; }

        public bool IsDel { get; set; }
    }
}