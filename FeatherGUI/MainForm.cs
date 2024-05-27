namespace FeatherGUI
{
    public class MainForm : Form
    {
        public string Workspace { get; set; }

        public MainForm(string[] args)
        {
            while (true)
            {
                try
                {
                    if (args.Length == 0)
                    {
                        FolderBrowserDialog dialog = new FolderBrowserDialog();

                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            Workspace = dialog.SelectedPath;
                        }
                        else
                        {
                            Workspace = Environment.CurrentDirectory;
                        }
                    }
                    else
                    {
                        Workspace = args[0];
                    }

                    Workspace = GetWorkspace(Workspace);
                }
                catch
                {
                    MessageBox.Show("");
                    continue;
                }

                break;
            }
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
