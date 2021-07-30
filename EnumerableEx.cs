using System;
using System.Collections;
using System.Collections.Generic;

namespace L.Util
{
    public static partial class EnumerableEx
    {
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (action == null)
                throw Error.ArgumentNull(nameof(action));
            foreach (TSource source1 in source)
            {
                action(source1);
            }
        }

        public static void ForEach<TSource>(this IEnumerable source, Action<TSource> action)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (action == null)
                throw Error.ArgumentNull(nameof(action));
            foreach (object source1 in source)
            {
                if (source1 is TSource item) action(item);
            }
        }

        public static void ForEachAll(this IEnumerable source, Action<object> action)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (action == null)
                throw Error.ArgumentNull(nameof(action));
            foreach (object source1 in source)
            {
                action(source1);
            }
        }
    }
}