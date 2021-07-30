using System;
using System.Collections;
using System.Collections.Generic;

namespace L.Util
{
    public static class IListEx
    {
        //public static void For<TSource>(this IList<TSource> source, Action<int> action)
        //{
        //    if (source == null)
        //        throw Error.ArgumentNull(nameof(source));
        //    if (action == null)
        //        throw Error.ArgumentNull(nameof(action));
        //    for (int i = 0; i < source.Count; i++)
        //    {
        //        action(i);
        //    }
        //}

        ///// <summary>
        ///// Reverse
        ///// </summary>
        //public static void For_<TSource>(this IList<TSource> source, Action<int> action)
        //{
        //    if (source == null)
        //        throw Error.ArgumentNull(nameof(source));
        //    if (action == null)
        //        throw Error.ArgumentNull(nameof(action));
        //    for (int i = source.Count - 1; i >= 0; i--)
        //    {
        //        action(i);
        //    }
        //}

        public static void For(this IList source, Action<int> action)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (action == null)
                throw Error.ArgumentNull(nameof(action));
            for (int i = 0; i < source.Count; i++)
            {
                action(i);
            }
        }

        public static void For_(this IList source, Action<int> action)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (action == null)
                throw Error.ArgumentNull(nameof(action));
            for (int i = source.Count - 1; i >= 0; i--)
            {
                action(i);
            }
        }

        public static void For<T>(this IList<T> source, Action<IList<T>, int> action)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (action == null)
                throw Error.ArgumentNull(nameof(action));
            for (int i = 0; i < source.Count; i++)
            {
                action(source, i);
            }
        }

        /// <summary>
        /// Reverse
        /// </summary>
        public static void For_<T>(this IList<T> source, Action<IList<T>, int> action)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (action == null)
                throw Error.ArgumentNull(nameof(action));
            for (int i = source.Count - 1; i >= 0; i--)
            {
                action(source, i);
            }
        }

        public static void For(this IList source, Action<IList, int> action)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (action == null)
                throw Error.ArgumentNull(nameof(action));
            for (int i = 0; i < source.Count; i++)
            {
                action(source, i);
            }
        }

        /// <summary>
        /// Reverse
        /// </summary>
        public static void For_(this IList source, Action<IList, int> action)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (action == null)
                throw Error.ArgumentNull(nameof(action));
            for (int i = source.Count - 1; i >= 0; i--)
            {
                action(source, i);
            }
        }

        /// <summary>
        /// 不在范围内则加到末尾
        /// </summary>
        public static void MoveToOrDefault(this IList list, object item, int index)
        {
            list.Remove(item);
            if (index < 0 || index>list.Count) list.Add(item);
            else list.Insert(index, item);
        }

        public static void MoveTo(this IList list, object item, int index)
        {
            list.Remove(item);
            list.Insert(index, item);
        }

        public static void MoveToTopMost(this IList list, object item) => MoveTo(list, item, -1);

        public static void MoveToBottomMost(this IList list, object item) => MoveTo(list, item, 0);

        /// <returns>移动到的位置</returns>
        public static int MoveToNext(this IList list, object item)
        {
            var index = list.IndexOf(item);
            if (index >= list.Count - 1) return index;
            index++;
            list.MoveTo(item, index);
            return index;
        }

        /// <returns>移动到的位置</returns>
        public static int MoveToLast(this IList list, object item)
        {
            var index = list.IndexOf(item);
            if (index <= 0) return index;
            index--;
            list.MoveTo(item, index);
            return index;
        }
    }
}