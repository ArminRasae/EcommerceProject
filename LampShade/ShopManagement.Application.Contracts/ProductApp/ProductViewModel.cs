﻿

namespace ShopManagement.Application.Contracts.ProductApp
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string UnitPrice { get; set; }
        public string Picture { get; set; }
        public long CategoryId { get; set; }
        public string CreationDate { get; set; }
        public string CategoryName { get; set; }
        public bool IsInStock { get; set; }


    }
}



