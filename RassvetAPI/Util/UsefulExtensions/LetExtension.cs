using System;

namespace RassvetAPI.Util.UsefulExtensions
{
    public static class LetExtension
    {
        public static R Let<T,R>(this T self, Func<T,R> func)
            => func(self);
    }
}
