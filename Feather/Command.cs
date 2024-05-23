using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather
{
    public static class Command
    {
        public const string HELP = "help";
        public const string INIT = "init";
        public const string LOG = "log";
        public const string SHOW = "show";
        public const string PULL = "pull";
        public const string COMMIT = "commit";
        public const string DEL = "del";

        public const string MOUNT_FLAG = "-m";
        public const string ALL_FLAG = "-a";
        public const string INDEX_FLAG = "-i";
        public const string PARENT_FLAG = "-p";
    }
}
