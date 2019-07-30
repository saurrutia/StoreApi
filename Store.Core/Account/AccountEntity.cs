using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Store.Common;
using Store.Core.Product;

namespace Store.Core.Account
{
    public class AccountEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public ICollection<ProductLikesEntity> LikedProducts { get; set; }

        public static List<AccountEntity> CreateDumpData()
        {
            return new List<AccountEntity>
            {
                new AccountEntity()
                {
                    UserName = "admin01",
                    Password = "admin01".ToSha256(),
                    Role = Role.Admin
                },
                new AccountEntity()
                {
                    UserName = "user02",
                    Password = "user02".ToSha256(),
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
