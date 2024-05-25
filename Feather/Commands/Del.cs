using Feather.Resources;

namespace Feather.Commands
{
    public class Del
    {
        public Del(string[] args)
        {
            try
            {
                Program.GetWorkspace(Program.GetPath(""));
            }
            catch
            {
                Program.ConsoleReturn(Messages.FeatherNotFound, false);
                return;
            }
        }
    }
}