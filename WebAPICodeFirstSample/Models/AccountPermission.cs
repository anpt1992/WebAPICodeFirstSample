using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICodeFirstSample.Models
{
    public class AccountPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public long AccountId { get; private set; }
        public int PermissionId { get; private set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty(nameof(Account.AccountPermission))]
        public virtual Account IDAccountNavigation { get; set; }

        [ForeignKey(nameof(PermissionId))]
        [InverseProperty(nameof(Permission.AccountPermission))]
        public virtual Permission IDPermissionNavigation { get; set; }
    }
}
