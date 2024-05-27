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
        /// ���� ��θ� ������� ��� ��θ� ������ ���� ��θ� ȹ��
        /// </summary>
        /// <param name="path"> ����� ��� ��� </param>
        /// <returns> path�� ����� ���� ��� </returns>
        public static string GetPath(string path)
        {
            return new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, path)).FullName;
        }

        /// <summary>
        /// ���� ����� ���� feather ����Ҹ� �˻�
        /// </summary>
        /// <param name="path"> �˻��� ������ ��ġ </param>
        /// <returns> ���� ����� ���� feather ����� </returns>
        /// <exception cref="DirectoryNotFoundException"> ����Ҹ� ã�� ���� �� �߻� </exception>
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
