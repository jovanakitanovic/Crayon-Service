using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Repository.Models
{
    [Table("AccountCustomerLink")]
    public class AccountCustomerLink
    {
        [Key]
        public Guid AccountCustomerLinkId { get; set; }

        public Guid CustomerId { get; set; }

        public Guid AccountId { get; set; }
    }
}
