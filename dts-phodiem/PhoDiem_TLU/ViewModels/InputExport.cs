using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoDiem_TLU.ViewModels
{
    public class InputExport
    {
        public InputExport()
        {

        }
        public InputExport(long? departmentId, long? semesterId, long? enrollmentClassId,
            long? teacherId,long? courseSubjectId)
        {
            this.departmentId = departmentId;
            this.semesterId = semesterId;
            this.enrollmentClassId = enrollmentClassId;
            this.teacherId = teacherId;
            this.courseSubjectId = courseSubjectId;
        }


        public long? departmentId { get; set; }
        public long? semesterId { get; set; }
        public long? enrollmentClassId{ get; set; }
        public long? teacherId { get; set; }
        public long? courseSubjectId { get; set; }
    }
}