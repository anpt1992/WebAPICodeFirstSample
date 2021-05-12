using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICodeFirstSample.Models
{
    public class Account
    {
        public Account()
        {
            AccountPermission = new HashSet<AccountPermission>();           
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty("IDAccountNavigation")]
        public virtual ICollection<AccountPermission> AccountPermission { get; set; }
        
    }
}
