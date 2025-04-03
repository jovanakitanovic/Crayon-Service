using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Repository.Models
{
    [Table("ServiceSubbscripitons")]
    //[PrimaryKey(nameof(ServiceId), nameof(AccountId))]
    public class ServiceOrder
    {
        [Key]
        public Guid ServiceSubscripitonId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid AccountId { get; set; }
        public string ServiceName { get; set; }
        public int Quantity { get; set; }
        public int State { get; set; }
        public DateTime ValidThrough { get; set; }
    }
}
