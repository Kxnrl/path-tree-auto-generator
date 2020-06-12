using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PathTree
{
    class Program
    {
        static readonly string worker = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        static readonly string myself = Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        static readonly string myfile = "kxnrl.path.tree.txt";
        static readonly string[] exts = new string[] {};

        static void Main()
        {
            Console.Title = "目录树生成器   Path: [" + worker + "]";
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Scanning...");

            var files = new List<string>();
            Directory.GetFileSystemEntries(worker, "*.*", SearchOption.AllDirectories)
                .OrderBy(p => p).ToList().ForEach(f =>
                {
                    var fa = File.GetAttributes(f);
                    if (fa.HasFlag(FileAttributes.Directory) || f.Contains(myfile) || f.Contains(myself))
                        return;

                    if (Array.IndexOf(exts, Path.GetExtension(f)) >= 0)
                    {
                        files.Add(f.Replace(worker, "").TrimStart('\\').Replace('\\', '/'));
                    }
                });
            Console.WriteLine("Total: {0} files", files.Count);
            Console.WriteLine("Writing...");

            var list = Path.Combine(worker, myfile);
            File.WriteAllLines(list, files.ToArray(), new UTF8Encoding(false));

            Console.WriteLine("Wrote to {0}", list);
            Console.WriteLine("Done...");
            Console.ReadKey(true);
        }
    }
}
