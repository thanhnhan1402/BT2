using System;
using System.Collections.Generic;

#nullable disable

namespace BT2.UI.Model
{
    public partial class InputInfo
    {
        public InputInfo()
        {
            OutputInfos = new HashSet<OutputInfo>();
        }

        public string Id { get; set; }
        public string IdObject { get; set; }
        public string IdInput { get; set; }
        public int? Quantity { get; set; }
        public double? InputPrice { get; set; }
        public double? OutputPrice { get; set; }
        public string Status { get; set; }

        public virtual Input IdInputNavigation { get; set; }
        public virtual Object IdObjectNavigation { get; set; }
        public virtual ICollection<OutputInfo> OutputInfos { get; set; }
    }
}
