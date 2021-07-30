using System;
using System.Collections.Generic;
using System.Linq;

namespace L.Util
{
    public static class EnumEx
    {
        public static List<T> GetEnumMemberValus<T>(this T enumValue)
        {
            var typeEnum = typeof(T);
            if (!typeEnum.IsEnum) throw new ArgumentException("T must be an enumerated type");
            return $"{enumValue:G}".Split(',').Select(t => (T)Enum.Parse(typeEnum, t)).ToList();
        }
    }
}
