using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSCloseOut.Models
{
    [Serializable]
    public class CloseOutOption
    {

        public int? ID { get; set; }
        public int? ParentID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public List<CloseOutOption> Children { get; set; }

        public CloseOutOption()
        {
            this.Children = new List<CloseOutOption>();
        }

        public void AddChildOption(CloseOutOption option)
        {
            if (option == null)
                return;

            if (this.Children.FirstOrDefault(o => o.ID == option.ID) == null)
            {
                option.ParentID = this.ID;
                this.Children.Add(option);
            }
        }
    }
}