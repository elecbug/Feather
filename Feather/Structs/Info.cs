using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Feather.Structs
{
    /// <summary>
    /// 저장 버전 하나의 정보
    /// </summary>
    public class Map
    {
        /// <summary>
        /// 저장소를 저장 당시 지정한 이름
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// 현재 버전 번호
        /// </summary>
        public required int Index { get; set; }

        /// <summary>
        /// 현재 버전의 부모 버전 번호
        /// </summary>
        public required int Parent { get; set; }

        /// <summary>
        /// 현재 버전을 저장한 시각
        /// </summary>
        public string Time { get; set; } = DateTime.Now.ToString("yyyy.MM.dd. HH:mm:ss");
    }

    /// <summary>
    /// 저장소의 전반적인 정보
    /// </summary>
    public class Info
    {
        /// <summary>
        /// 현재 버전의 버전 번호
        /// </summary>
        public int Current { get; set; } = 0;

        /// <summary>
        /// 저장된 버전 중 최종 버전 번호
        /// </summary>
        public int Last { get; set; } = 0;

        /// <summary>
        /// 모든 저장 버전들의 정보
        /// </summary>
        public List<Map> Maps { get; set; } = new List<Map>() { new Map() { Name = "INIT", Index = 0, Parent = -1 } };

        /// <summary>
        /// 기본 생성자
        /// </summary>
        public Info() { }

        /// <summary>
        /// 해당 경로의 직렬화 된 정보를 읽어 초기화하는 생성자
        /// </summary>
        /// <param name="filePath"> 직렬화된 정보가 저장된 경로 </param>
        /// <exception cref="Exception"> 경로의 정보가 잘못됨 </exception>
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

        /// <summary>
        /// 정보를 경로에 저장
        /// </summary>
        /// <param name="filePath"> 저장할 위치 </param>
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
