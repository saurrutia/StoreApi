using System;
using System.ComponentModel.DataAnnotations;

namespace Store.Core.Product
{
    public class LogEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string UserName { get; set; }
    }
}
