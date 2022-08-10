using System.Collections.Generic;
using System.Text;

namespace RassvetAPI.Util.UsefulExtensions
{
    public static class GroupErrorsExtension
    {
        public static string GroupErrors(this IEnumerable<string> self)
        {
            var builder = new StringBuilder();

            foreach(var error in self)
            {
                builder.Append(error);
                builder.Append(' ');
            }

            if(builder.Length >= 1)
                builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }
    }
}
