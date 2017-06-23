using System;

namespace vnc.Tools.Behaviors
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    internal class ParamAttribute : Attribute
    {
        public string name;
        public object value;

        public ParamAttribute(string name)
        {
            this.name = name;
        }
    }
}

