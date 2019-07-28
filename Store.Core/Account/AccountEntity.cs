using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Store.Core.Product;

namespace Store.Core.Account
{
    public class AccountEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }
        public string UserName { get; set; }
        public Role Role { get; set; }
        public ICollection<ProductLikesEntity> LikedProducts { get; set; }

        public static List<AccountEntity> CreateDumpData()
        {
            return new List<AccountEntity>
            {
                new AccountEntity()
                {
                    Id = 1,
                    UserName = "admin01",
                    Role = Role.Admin
                },
                new AccountEntity()
                {
                    Id = 2,
                    UserName = "user02",
                    Role = Role.Buyer
                }
            };
        }
    }

    public enum Role
    {
        Admin = 0,
        Buyer = 1
    }
}
