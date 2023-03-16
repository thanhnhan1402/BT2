using System;
using System.Collections.Generic;

#nullable disable

namespace BT2.UI.Model
{
    public partial class OutputInfo
    {
        public string Id { get; set; }
        public string IdObject { get; set; }
        public string IdInputInfo { get; set; }
        public int IdCustomer { get; set; }
        public string IdOutput { get; set; }
        public int? Quantity { get; set; }
        public string Status { get; set; }

        public virtual Customer IdCustomerNavigation { get; set; }
        public virtual InputInfo IdInputInfoNavigation { get; set; }
        public virtual Object IdObjectNavigation { get; set; }
        public virtual Output IdOutputNavigation { get; set; }
    }
}
