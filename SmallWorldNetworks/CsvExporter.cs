using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallWorldNetworks
{
    public class CsvExporter
    {
        public static void Export(string filename, string header, List<string> rows) 
        { 
            List<string> allLines = new List<string>();
            if (rows != null)
            {
                allLines.AddRange(rows);
            }
            File.WriteAllLines(filename, allLines);
            Console.WriteLine($"Saved to {filename}");
        }
    }
}
