﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public class DisplayAttribute : Attribute
    {
        // Fields
        private bool display;

        // Methods
        public DisplayAttribute(bool display)
        {
            this.display = display;
        }

        // Properties
        public bool Display { get; set; }
    }
}
