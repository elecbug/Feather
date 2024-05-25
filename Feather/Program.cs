using Feather.Commands;
using Feather.Resources;

namespace Feather
{
    public class Program
    {
        public static int Result { get; set; } = 0;

        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                ConsoleReturn("ver 0.0.0", true);
                return Result;
            }

            switch (args[0].ToLower())
            {
                case Command.HELP: new Commands.Help(args); break;
                case Command.INIT: new Init(args); break;
                case Command.DEL: new Del(args); break;
                case Command.PULL: new Pull(args); break;
                case Command.SHOW: new Show(args); break;
                case Command.COMMIT: new Commit(args); break;
                case Command.LOG: new Log(args); break;
            }

            return Result;
        }

        /// <summary>
        /// 현재 경로를 기반으로 상대 경로를 적용한 절대 경로를 획득
        /// </summary>
        /// <param name="path"> 계산할 상대 경로 </param>
        /// <returns> path가 적용된 절대 경로 </returns>
        public static string GetPath(string path)
        {
            return new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, path)).FullName;
        }

        /// <summary>
        /// 메시지를 콘솔에 출력하고 프로그램 반환값을 세팅
        /// </summary>
        /// <param name="message"> 출력할 메시지 </param>
        /// <param name="successed"> 프로그램의 정상 수행 여부 </param>
        public static void ConsoleReturn(string message, bool successed)
        {
            Console.WriteLine(message + "\n");
            Result = successed ? 0 : -1;
        }

        /// <summary>
        /// 가장 가까운 상위 feather 저장소를 검색
        /// </summary>
        /// <param name="path"> 검색을 시작을 위치 </param>
        /// <returns> 가장 가까운 상위 feather 저장소 </returns>
        /// <exception cref="DirectoryNotFoundException"> 저장소를 찾지 못할 시 발생 </exception>
        public static string GetWorkspace(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);

            DirectoryInfo? get = info.GetDirectories().FirstOrDefault(x => x.Name == ".feather");

            if (get != null)
            {
                return info.FullName;
            }
            else
            {
                if (info.Parent != null)
                {
                    return GetWorkspace(info.Parent.FullName);
                }
                else
                {
                    throw new DirectoryNotFoundException();
                }
            }
        }
    }
}