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
    public partial class STUDENT {

        public STUDENT()
        {
            this.EXAMs = new List<EXAM>();
            OnCreated();
        }

        public string neptun_id { get; set; }

        public string hash_password { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string user_status { get; set; }

        public virtual IList<EXAM> EXAMs { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
