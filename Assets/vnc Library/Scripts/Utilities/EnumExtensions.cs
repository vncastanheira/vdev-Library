using System;

namespace vnc.Utilities
{
    public static class EnumExtensions
    {
        public static bool HasFlag<T>(this T source, T flag)
        {
            if (!source.GetType().IsEnum)
                throw new ArgumentException("source is not an enum type.");

            if (!flag.GetType().IsEnum)
                throw new ArgumentException("flag is not an enum type.");

            // check if from the same type.
            if (source.GetType() != flag.GetType())
            {
                throw new ArgumentException("The checked flag is not from the same type as the checked variable.");
            }

            ulong num = Convert.ToUInt64(source);
            ulong num2 = Convert.ToUInt64(flag);

            return (num2 & num) != 0;
        }
    }
}
