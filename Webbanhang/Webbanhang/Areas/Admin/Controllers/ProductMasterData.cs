using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Webbanhang.Areas.Admin.Controllers
{
    public partial class ProductMasterData
    {
        

        public int Id { get; set; }
     
        public string Name { get; set; }
        public string Avatar { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> PriceDiscount { get; set; }
        public Nullable<int> TypeId { get; set; }
        public string Slug { get; set; }
        public Nullable<int> Brandid { get; set; }
        public Nullable<int> Deleted { get; set; }
        public Nullable<int> ShowOnHomePage { get; set; }
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public string FullDescription { get; set; }


    }
}