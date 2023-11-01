﻿using ECE.Core.DomainObjects;

namespace ECE.Catalog.API.Models
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal Value { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Image { get; set; }
        public int StockAmount { get; set; }

        public void RemoveFromStock(int amount)
        {
            if (StockAmount >= amount)
                StockAmount -= amount;
        }

        public bool HasAvailable(int amount)
        {
            return Active && StockAmount >= amount;
        }

        public void ReverseStock(int amount)
        {
            if (StockAmount <= amount)
                StockAmount += amount;
        }
    }
}
