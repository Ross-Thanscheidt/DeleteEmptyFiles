

namespace DeleteEmptyFiles
{
    internal class Program
    {
        private static readonly bool ACTUALLY_DELETE = true;

        private static bool _recursive;
        private static bool _deleteEmptyFiles;
        private static bool _verbose;
        private static string _topFolder;

        private static int _subfoldersDeleted = 0;
        private static int _filesDeleted = 0;
        private static int _filesNotDeleted = 0;

        private static List<string> filesNotDeleted = new();

        private static void ParseArguments(string[] args)
        {
            foreach (var arg in args)
            {
                if (arg.StartsWith("--"))
                {
                    if ("recursive".StartsWith(arg[2..]))
                    {
                        _recursive = true;
                    }
                    else if ("norecursive".StartsWith(arg[2..]))
                    {
                        _recursive = false;
                    }
                    else if ("folders".StartsWith(arg[2..]))
                    {
                        _deleteEmptyFiles = true;
                    }
                    else if ("nofolders".StartsWith(arg[2..]))
                    {
                        _deleteEmptyFiles = false;
                    }
                    else if ("verbose".StartsWith(arg[2..]))
                    {
                        _verbose = true;
                    }
                    else if ("noverbose".StartsWith(arg[2..]))
                    {
                        _verbose = false;
                    }
                }
                else if (arg == "-r")
                {
                    _recursive = true;
                }
                else if (arg == "-f")
                {
                    _deleteEmptyFiles = true;
                }
                else if (arg == "-v")
                {
                    _verbose = true;
                }
                else
                {
                    _topFolder = arg;
                }
            }
            Console.WriteLine($"(recursive={_recursive}, folders={_deleteEmptyFiles}, verbose={_verbose}, top folder={_topFolder})");
        }

        private static bool DeleteInFolder(string folder)
        {
            bool folderEmpty = true;

            foreach (string subfolder in Directory.GetDirectories(folder))
            {
                bool subfolderEmpty;

                if (_recursive && (subfolderEmpty = DeleteInFolder(subfolder)))
                {
                    if (subfolderEmpty)
                    {
                        if (_verbose)
                        {
                            Console.WriteLine($"Delete empty subfolder: {subfolder}");
                        }

                        if (ACTUALLY_DELETE)
                        {
                            Directory.Delete(subfolder); 
                        }

                        _subfoldersDeleted++;
                    }
                    else
                    {
                        folderEmpty = false;
                    }
                }
                else
                {
                    folderEmpty = false;
                }
            }

            foreach (string file in Directory.GetFiles(folder))
            {
                FileInfo fileInfo = new FileInfo(file);

                if (fileInfo.Length == 0)
                {
                    if (_verbose)
                    {
                        Console.WriteLine($"Delete empty file: {file}");
                    }

                    if (ACTUALLY_DELETE)
                    {
                        File.Delete(file);
                    }

                    _filesDeleted++;
                }
                else
                {
                    if (_verbose)
                    {
                        Console.WriteLine($"File Not Empty: {file}");
                    }

                    filesNotDeleted.Add(file);
                    _filesNotDeleted++;
                    folderEmpty = false;
                }

            }

            return folderEmpty;
        }

        static void Main(string[] args)
        {
            ParseArguments(args);

            if (!string.IsNullOrWhiteSpace(_topFolder))
            {
                bool success = DeleteInFolder(_topFolder);

                Console.WriteLine();
                Console.WriteLine("Summary:");
                Console.WriteLine($"{_subfoldersDeleted:N0} empty subfolders deleted");
                Console.WriteLine($"{_filesDeleted:N0} empty files deleted");
                Console.WriteLine($"{_filesNotDeleted:N0} non-empty files not deleted");

                if (filesNotDeleted.Count > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Files Not Deleted:");

                    foreach (var file in filesNotDeleted)
                    {
                        Console.WriteLine($"{file}");
                    }
                }
            }
        }
    }
}