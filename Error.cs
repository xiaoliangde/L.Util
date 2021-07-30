using System;

namespace L.Util
{
    internal static class Error
    {
        internal static Exception ArgumentArrayHasTooManyElements(object p0) => (Exception)new ArgumentException(Strings.ArgumentArrayHasTooManyElements(p0));

        internal static Exception ArgumentNotIEnumerableGeneric(object p0) => (Exception)new ArgumentException(Strings.ArgumentNotIEnumerableGeneric(p0));

        internal static Exception ArgumentNotSequence(object p0) => (Exception)new ArgumentException(Strings.ArgumentNotSequence(p0));

        internal static Exception ArgumentNotValid(object p0) => (Exception)new ArgumentException(Strings.ArgumentNotValid(p0));

        internal static Exception IncompatibleElementTypes() => (Exception)new ArgumentException(Strings.IncompatibleElementTypes);

        internal static Exception ArgumentNotLambda(object p0) => (Exception)new ArgumentException(Strings.ArgumentNotLambda(p0));

        internal static Exception MoreThanOneElement() => (Exception)new InvalidOperationException(Strings.MoreThanOneElement);

        internal static Exception MoreThanOneMatch() => (Exception)new InvalidOperationException(Strings.MoreThanOneMatch);

        internal static Exception NoArgumentMatchingMethodsInQueryable(object p0) => (Exception)new InvalidOperationException(Strings.NoArgumentMatchingMethodsInQueryable(p0));

        internal static Exception NoElements() => (Exception)new InvalidOperationException(Strings.NoElements);

        internal static Exception NoMatch() => (Exception)new InvalidOperationException(Strings.NoMatch);

        internal static Exception NoMethodOnType(object p0, object p1) => (Exception)new InvalidOperationException(Strings.NoMethodOnType(p0, p1));

        internal static Exception NoMethodOnTypeMatchingArguments(object p0, object p1) => (Exception)new InvalidOperationException(Strings.NoMethodOnTypeMatchingArguments(p0, p1));

        internal static Exception NoNameMatchingMethodsInQueryable(object p0) => (Exception)new InvalidOperationException(Strings.NoNameMatchingMethodsInQueryable(p0));

        internal static Exception ArgumentNull(string paramName) => (Exception)new ArgumentNullException(paramName);

        internal static Exception ArgumentOutOfRange(string paramName) => (Exception)new ArgumentOutOfRangeException(paramName);

        internal static Exception NotImplemented() => (Exception)new NotImplementedException();

        internal static Exception NotSupported() => (Exception)new NotSupportedException();
    }
}
