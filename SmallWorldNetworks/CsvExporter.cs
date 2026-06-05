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
            allLines.Add(header);
            if (rows != null)
            {
                allLines.AddRange(rows);
            }

            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "analysis", "data");
            Directory.CreateDirectory(outputPath);
            string fullPath = Path.Combine(outputPath, filename);

            File.WriteAllLines(fullPath, allLines);
            Console.WriteLine($"Saved to {fullPath}");
        }
    }
}
