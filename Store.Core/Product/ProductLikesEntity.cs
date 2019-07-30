using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Store.Core.Account;

namespace Store.Core.Product
{
    public class ProductLikesEntity : Entity
    {
        [Key]
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
        [Key]
        public int AccountId { get; set; }
        public AccountEntity Account { get; set; }

        public static List<ProductLikesEntity> CreateDumpData()
        {
            return new List<ProductLikesEntity>
            {
                new ProductLikesEntity()
                {
                    ProductId = 1,
                    AccountId = 1
                },
                new ProductLikesEntity()
                {
                    ProductId = 2,
                    AccountId = 1
                },
                new ProductLikesEntity()
                {
                    ProductId = 3,
                    AccountId = 1
                },
                new ProductLikesEntity()
                {
                    ProductId = 1,
                    AccountId = 2
                },
            };
        }
    }
}
