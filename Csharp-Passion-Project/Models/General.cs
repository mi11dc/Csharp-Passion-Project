﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Csharp_Passion_Project.Models
{
    public class General
    {
        public string getLowerStringForSearch(string str)
        {
            str = String.IsNullOrEmpty(str) ? String.Empty : str;
            return str.ToLower();
        }
    }
}