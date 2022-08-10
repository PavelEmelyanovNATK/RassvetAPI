using System;

namespace RassvetAPI.Util.UsefulExtensions
{
    public static class ApplyExtension
    {
        public static T Apply<T>(this T self, Action<T> func)
        {
            func(self);
            return self;
        }
    }
}
