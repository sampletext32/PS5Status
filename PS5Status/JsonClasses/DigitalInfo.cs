﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PS5Status.JsonClasses
{
    public class DigitalInfo
    {
        public string appearedAt { get; set; }
        public bool available { get; set; }
        public object kind { get; set; }
        public string updatedAt { get; set; }
    }
}
