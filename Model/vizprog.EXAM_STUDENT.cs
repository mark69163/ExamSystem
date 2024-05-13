using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class STUDENT_EXAM
    {
        public string neptun_id { get; set; }
        public int course_id { get; set; }
        public int? result { get; set; }

        public STUDENT STUDENT { get; set; }
        public EXAM EXAM { get; set; }
    }
}
