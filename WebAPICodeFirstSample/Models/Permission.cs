using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICodeFirstSample.Models
{
    public class Permission
    {
        public Permission()
        {
            AccountPermission = new HashSet<AccountPermission>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public bool IsDeleted { get; set; }

        [InverseProperty("IDPermissionNavigation")]
        public virtual ICollection<AccountPermission> AccountPermission { get; set; }
    }
}
