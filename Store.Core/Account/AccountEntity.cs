using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Store.Core.Product;

namespace Store.Core.Account
{
    public class AccountEntity
    {
        private ICollection<ProductLikesEntity> _likedProducts;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }
        public string UserName { get; set; }
        public Role Role { get; set; }
        public ICollection<ProductLikesEntity> LikedProducts
        {
            get => _likedProducts ?? new List<ProductLikesEntity>();
            protected set => _likedProducts = value;
        }
    }

    public enum Role
    {
        Admin = 0,
        Buyer = 1
    }
}
