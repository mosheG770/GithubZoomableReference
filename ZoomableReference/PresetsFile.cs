using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoomableReference
{
    class PresetsFile
    {
        string folderPath;
        string fileName = "Presets.csv";

        public PresetsFile()
        {
            folderPath = Environment.CurrentDirectory + @"\";
        }


        public IEnumerable<WindowPositionSizeArgs> LoadFromFile()
        {
            List<WindowPositionSizeArgs> argsList = new List<WindowPositionSizeArgs>();

            try
            {
                var lines = File.ReadAllLines(folderPath + fileName);

                foreach (var item in lines)
                    argsList.Add(CSV.ToWindowArgs(item));
            }
            catch (FileNotFoundException e) { }

            return argsList;
        }


        public void SaveToFile(IList<WindowPositionSizeArgs> argsList)
        {
            string[] lines = CSV.ToStringArray(argsList);
            File.WriteAllLines(folderPath + fileName, lines);
        }
    }
}
