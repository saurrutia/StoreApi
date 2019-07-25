using System;
using System.Collections.Generic;
using System.Text;
using Store.Core.Account;

namespace Store.Core.Product
{
    public class ProductLikesEntity
    {
        public int ProductId { get; set; }
        public ProductEntity Product { get; protected set; }

        public int AccountId { get; set; }
        public AccountEntity Account { get; protected set; }
    }
}
