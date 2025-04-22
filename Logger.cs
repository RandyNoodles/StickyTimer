using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StickyTimer
{
    internal static class Logger
    {
        private static readonly object _lock = new object();


        public static void d(String tag, String message)
        {
            tag = TruncateTag(tag);
            String tabs = GetTabString(tag);

            lock (_lock)
            {

                try
                {
                    using(StreamWriter writer = File.AppendText("logs.txt"))
                    {
                        writer.WriteLine($"[{DateTime.Now.ToString("G")}] {tag}:{tabs}{message}");
                    }
                }
                catch
                {
                    //Idk, I think we're pooched.
                }
            }
        }

        private static String TruncateTag(String tag)
        {
            String temp = tag;
            if(temp.Length > 20)
            {

                temp = String.Concat(tag.Substring(0, 20), "..");
            }
            return temp;
        }

        private static String GetTabString(String tag)
        {
            if(tag.Length < 8)
            {
                return "\t\t\t";
            }
            if(tag.Length < 16)
            {
                return "\t\t";
            }
            return "\t";
        }

    }
}
