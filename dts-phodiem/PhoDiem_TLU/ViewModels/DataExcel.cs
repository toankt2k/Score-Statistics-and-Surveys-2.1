using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoDiem_TLU.ViewModels
{
    public class DataExcel
    {
        public DataExcel(long? groupID, string groupName, string studentCode, string studentName, double mark)
        {
            this.groupID = groupID;
            this.groupName = groupName;
            this.studentCode = studentCode;
            this.studentName = studentName;
            this.mark = mark;
        }

        long? groupID {get;set;}
        string groupName { get;set;}
        string studentCode {get;set;}
        string studentName { get;set;}
        double mark { get;set;}
    }
}