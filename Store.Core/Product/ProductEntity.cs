using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Store.Core.Account;

namespace Store.Core.Product
{
    public class ProductEntity
    {
        private int? _likes;
        private ICollection<ProductLikesEntity> _accountLikes;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public int Likes
        {
            get => _likes ?? AccountLikes.Count;
            protected set => _likes = value;
        }

        public ICollection<ProductLikesEntity> AccountLikes
        {
            get => _accountLikes ?? new List<ProductLikesEntity>();
            protected set => _accountLikes = value;
        }

        public void ToggleLike(AccountEntity accountWhoLikes)
        {
            var account = AccountLikes.FirstOrDefault(a => a.AccountId == accountWhoLikes.Id);
            if (account != null)
            {
                _likes--;
                _accountLikes.Remove(account);
            }
            else
            {
                _likes++;
                _accountLikes.Add(new ProductLikesEntity
                {
                    AccountId = accountWhoLikes.Id,
                    ProductId = Id
                });
            }
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
                    Likes = 0
                },
                new ProductEntity()
                {
                    Id = 2,
                    Name = "Rancheritos",
                    Stock = 5,
                    Price = 0.15,
                    Likes = 0
                },
                new ProductEntity()
                {
                    Id = 3,
                    Name = "Crujitos",
                    Stock = 5,
                    Price = 0.25,
                    Likes = 0
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
    }
}
