using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Repository.Models
{
    [Table("Accounts")]

    public class Accounts
    {
        [Key]
        public Guid AccountId { get; set; }
    }
}
