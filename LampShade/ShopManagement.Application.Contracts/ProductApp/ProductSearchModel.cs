﻿
 

namespace ShopManagement.Application.Contracts.ProductApp
{
    public class ProductSearchModel
    {
        public string? Name { get;  set; }
        public string? Code { get; set; } 
        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
