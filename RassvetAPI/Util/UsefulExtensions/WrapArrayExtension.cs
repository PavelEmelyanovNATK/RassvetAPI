namespace RassvetAPI.Util.UsefulExtensions
{
    public static class WrapArrayExtension
    {
        /// <summary>
        /// Оборачивает объект в массив.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T[] WrapArray<T>(this T self)
            => new T[] { self };
    }
}
