using System;
using System.ComponentModel.DataAnnotations;

namespace Ids4.Data
{
    public class AppClient
    {
        [Key]
        public string ClientId { get; set; }
        public string ClientName { get; set; }
    }
}
