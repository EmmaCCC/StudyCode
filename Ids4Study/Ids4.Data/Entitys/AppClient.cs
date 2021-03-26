using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ids4.Data.Entitys
{
    public class AppClient
    {
        [Key]
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string Secret { get; set; }
        public string AllowedScopes { get; set; }
        public string GrantTypes { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
    }
}
