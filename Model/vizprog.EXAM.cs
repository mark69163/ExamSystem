﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2024. 05. 11. 12:23:01
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Model
{
    public partial class EXAM {

        public EXAM()
        {
            this.STUDENTs = new List<STUDENT>();
            this.INSTRUCTORs = new List<INSTRUCTOR>();
            this.QUESTIONs = new List<QUESTION>();
            OnCreated();
        }

        public int course_id { get; set; }

        public string title { get; set; }

        public string level { get; set; }

        public int kredit_value { get; set; }

        public DateTime? start_time { get; set; }

        public DateTime? end_time { get; set; }

        public int? time_limit { get; set; }

        public string imgSource { get; set; }

        public virtual IList<STUDENT> STUDENTs { get; set; }

        public virtual IList<INSTRUCTOR> INSTRUCTORs { get; set; }

        public virtual IList<QUESTION> QUESTIONs { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
