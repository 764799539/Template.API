using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public class DisplayAttribute : Attribute
    {
        public bool Display { get; set; }

        private bool display;

        public DisplayAttribute(bool display)
        {
            this.display = display;
        }
        
    }
}
