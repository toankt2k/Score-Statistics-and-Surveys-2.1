using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoDiem_TLU.ViewModels
{
    public class IDChecked
    {
        public IDChecked(long? checkedID)
        {
            this.checkedID = checkedID;
        }

        public long? checkedID { get; set; }
    }
}