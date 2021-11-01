﻿using System;
using System.ComponentModel;
namespace IDAL
{
    namespace DO
    {
        public enum WeightCategories
        {
            [Description("Light weight")]
            Light,
            [Description("Medium weight")]
            Medium,
            [Description("Heavy weight")]
            Heavy
        }

        public enum Priorities
        {
            [Description("Normal Priority")]
            Normal,
            [Description("Fast Priority")]
            Fast,
            [Description("Emergency Priority")]
            Emergency
        }
    }
}