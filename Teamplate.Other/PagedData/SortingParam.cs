﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Teamplate.NuGet
{
    [Serializable]
    public class SortingParam
    {
        // Properties
        public string SortName { get; set; }

        public string SortOrder { get; set; }

        public string SortString
        {
            get
            {
                if (!string.IsNullOrEmpty(this.SortName) && !string.IsNullOrEmpty(this.SortOrder))
                {
                    return (this.SortName + " " + this.SortOrder);
                }
                return "";
            }
        }
    }

}