using System;
using System.Collections.Generic;

namespace WarhammerDataConverter.DataConverters
{
    internal static class DataConverter
    {
        public static T[] Convert<T>(string[] lines, char columnSeparator, Func<string[], T> createObject)
        {
            List<T> items = new List<T>(lines.Length);

            foreach (string line in lines)
            {
                T item = createObject.Invoke(line.Split(columnSeparator));

                if (item != null)
                    items.Add(item);
            }

            return items.ToArray();
        }
    }
}
