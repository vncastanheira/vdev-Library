using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vnc.Attributes
{
    public class MinMaxRangeAttribute : PropertyAttribute
    {
        public float minLimit, maxLimit;

        public MinMaxRangeAttribute(float minLimit, float maxLimit)
        {
            this.minLimit = minLimit;
            this.maxLimit = maxLimit;
        }
    }

}
