﻿
namespace ex07_WarehouseAPI.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

 
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public int Quantity { get; set; }

        public decimal UnitPrice => Article?.Price ?? 0;
    }

    public class OrderDetailDto
    {
        public int ArticleId { get; set; }
        public int Quantity { get; set; }
    }
}
