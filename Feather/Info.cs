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
        public required string Index { get; set; }
    }

    public class Info
    {
        public string Current { get; set; } = "0";
        public List<Map> Maps { get; set; } = new List<Map>() { new Map() { Name = "INIT", Index = "0" } };

        private string _filePath;

        public Info(string filePath)
        {
            _filePath = filePath;

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
                    }
                }
            }
        }

        public void Save()
        {
            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                string json = JsonSerializer.Serialize(this);
                writer.Write(json);
            }
        }
    }
}
