﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Teamplate.NuGet
{
    public enum EnumAppendItemType
    {
        None = 0,
        [Description("--所有--")]
        All = 1,
        [Description("--请选择--")]
        Select = 2
    }
}
