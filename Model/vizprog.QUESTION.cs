﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2024. 04. 14. 10:53:13
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
    public partial class QUESTION {

        public QUESTION()
        {
            OnCreated();
        }

        public int id { get; set; }

        public string question { get; set; }

        public string answers { get; set; }

        public int solution { get; set; }

        public int point_value { get; set; }

        public int course_id { get; set; }

        public virtual EXAM EXAM { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
