using System;
using System.Collections.Generic;

#nullable disable

namespace BT2.UI.Model
{
    public partial class UserRole
    {
        public UserRole()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string DisplayName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
