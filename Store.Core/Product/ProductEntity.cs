using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Store.Core.Account;
using Store.Core.Events;
using Store.Core.Events.Common;

namespace Store.Core.Product
{
    public class ProductEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public int Likes { get; protected set; }

        public ICollection<ProductLikesEntity> AccountLikes { get; set; }

        public ProductEntity Create(string name, int stock, double price)
        {
            if (string.IsNullOrEmpty(name.Trim()) || stock < 0 || price < 0)
                return null;
            return new ProductEntity()
            {
                Name = name,
                Stock = stock,
                Price = price
            };
        }

        public void ToggleLike(AccountEntity accountWhoLikes)
        {
            var account = AccountLikes.FirstOrDefault(a => a.AccountId == accountWhoLikes.Id);
            if (account != null)
            {
                Likes--;
                AccountLikes.Remove(account);
            }
            else
            {
                Likes++;
                AccountLikes.Add(new ProductLikesEntity
                {
                    AccountId = accountWhoLikes.Id,
                    ProductId = Id
                });
            }
        }        

        public bool Buy(int quantity)
        {
            if (Stock < quantity)
                return false;
            Stock -= quantity;
            DomainEventsDispatcher.Raise(new ProductBuyed() { Product = this, Quantity = quantity });
            return true;
        }

        public bool ChangePrice(double newPrice)
        {
            if (newPrice < 0)
                return false;
            var lastPrice = Price;
            Price = newPrice;
            DomainEventsDispatcher.Raise(new PriceUpdated() { Product = this, LastPrice = lastPrice  });
            return true;
        }

        public static List<ProductEntity> CreateDumpData()
        {
            return new List<ProductEntity>
            {
                new ProductEntity()
                {
                    Id = 1,
                    Name = "Popotitos",
                    Stock = 5,
                    Price = 0.15,
                    Likes = 2
                },
                new ProductEntity()
                {
                    Id = 2,
                    Name = "Rancheritos",
                    Stock = 5,
                    Price = 0.15,
                    Likes = 1
                },
                new ProductEntity()
                {
                    Id = 3,
                    Name = "Crujitos",
                    Stock = 5,
                    Price = 0.25,
                    Likes = 1
                },
                new ProductEntity()
                {
                    Id = 4,
                    Name = "Quesitos",
                    Stock = 5,
                    Price = 0.15,
                    Likes = 0
                },
                new ProductEntity()
                {
                    Id = 5,
                    Name = "Platanitos",
                    Stock = 5,
                    Price = 0.25,
                    Likes = 0
                },
                new ProductEntity()
                {
                    Id = 6,
                    Name = "Popotitos Queso",
                    Stock = 5,
                    Price = 0.15,
                    Likes = 0
                },
                new ProductEntity()
                {
                    Id = 7,
                    Name = "Rancheritos Queso",
                    Stock = 5,
                    Price = 0.15,
                    Likes = 0
                },
                new ProductEntity()
                {
                    Id = 8,
                    Name = "Crujitos Queso",
                    Stock = 5,
                    Price = 0.25,
                    Likes = 0
                },
                new ProductEntity()
                {
                    Id = 9,
                    Name = "Quesitos Queso",
                    Stock = 5,
                    Price = 0.15,
                    Likes = 0
                },
                new ProductEntity()
                {
                    Id = 10,
                    Name = "Platanitos Queso",
                    Stock = 5,
                    Price = 0.25,
                    Likes = 0
                }
            };
        }

        public bool ChangeStock(int stock)
        {
            if (stock < 0)
                return false;
            Stock = stock;
            return true;
        }
    }
}
