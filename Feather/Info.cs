using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Feather
{
    public class Map
    {
        public required string Name { get; set; }
        public required int Index { get; set; }
        public required int Parent {  get; set; }
    }

    public class Info
    {
        public int Current { get; set; } = 0;
        public int Last { get; set; } = 0;
        public List<Map> Maps { get; set; } = new List<Map>() { new Map() { Name = "INIT", Index = 0, Parent = -1 } };

        public Info() { }

        public Info(string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(JsonSerializer.Serialize(this));
                }
            }
            else 
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line = reader.ReadToEnd();
                    Info? info = JsonSerializer.Deserialize<Info>(line);

                    if (info == null) 
                    {
                        throw new Exception("File is currupted");
                    }
                    else
                    {
                        Current = info.Current;
                        Maps = info.Maps;
                        Last = info.Last;
                    }
                }
            }
        }

        public void Save(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                string json = JsonSerializer.Serialize(this);
                writer.Write(json);
            }
        }
    }
}
