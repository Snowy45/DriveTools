using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace DriveTools.Comamnds
{
    public class FilePingCommand : CommandListener
    {
        public static List<String> fileLocations = new List<string>();
        public static List<String> dirEntered = new List<string>();
        public static int stillSearching = 0;
        public static int threadCount = 0;
        public static int totalSeacrhes = 0;
        

        public void OnDriveCommand(object source, string[] args, string rawInput, string directory)
        {
            switch (args[0].ToLower())
            {
                
                case "fping":

                    //Print Scan Resaults
                    if (args.Length < 2) break;
                    if (fileLocations.Count > 0)
                    {
                        if (args[1].ToLower().Equals("-r"))
                        {
                            int showAmount = 0;
                            try
                            {
                                showAmount = int.Parse(args[2]);
                            }
                            catch
                            {
                                showAmount = fileLocations.Count;
                            }

                            Console.WriteLine("File Locations");
                            for (int i = 0; i < showAmount; i++)
                            {
                                utils.tColor(ConsoleColor.Red);
                                Console.Write(fileLocations[i]);
                                utils.tColor(ConsoleColor.White);
                                Console.WriteLine(" | Last Modified: " + File.GetLastWriteTime(fileLocations[i]));
                            }
                            break;
                        }
                        
                    }

                    if (args[1].ToLower().Equals("-s"))
                    {
                        //Checking that the user has provided arguments for this command
                        if (args.Length < 4) break; if (utils.removeArgsFromRaw(args,3,rawInput).Length < 1) break;

                        string targetFile = utils.removeArgsFromRaw(args, 3, rawInput);
                        int searchLevel = 0;
                        try
                        {

                            searchLevel = int.Parse(args[2]);
                        }
                        catch
                        {
                            break;
                        }


                        fileLocations.Clear();
                        dirEntered.Clear();
                        stillSearching = 0;
                        totalSeacrhes = 0;
                        threadCount = 0;


                        DirectorySearchThread sThread = new DirectorySearchThread(directory, targetFile, searchLevel);
                        Thread t = new Thread(new ThreadStart(sThread.SearchOnNewThread));
                        t.Start();

                        bool searching = true;
                        int lastSearchNum = 0;
                        while (searching)
                        {
                            Thread.Sleep(200);
                            if (stillSearching < 10 && lastSearchNum != 0)
                            {
                                int lastResult = stillSearching;

                                Thread.Sleep(2000);
                                if (stillSearching == lastResult) { searching = false; }
                            }
                            if (stillSearching <= 0) { searching = false; }
                            if (lastSearchNum != 0) utils.ClearCurrentLine();
                            lastSearchNum = stillSearching;

                            Console.Write("Searched: {0} folders", totalSeacrhes);

                        }
                        utils.ClearCurrentLine();
                        Thread.Sleep(2000);

                        Console.WriteLine("A file named {0} was found in {1} locations:", targetFile, fileLocations.Count);
                        break;
                    }
                    break;
            }

                    
        }
    }


    public class DirectorySearchThread
    {
        private string directory;
        private string targetFile;
        private int currentLevel;

        public DirectorySearchThread(string startingDirectory, string targetFile, int level)
        {
            directory = startingDirectory;
            this.targetFile = targetFile;
            currentLevel = level;
           
        }

        
        public void SearchOnSameThread(string sDir,string tFile, int sLevel)
        {
            if (sLevel <= 0) return;

            string[] directories;
            string[] files;

            try
            {
                directories = Directory.GetDirectories(sDir);
                files = Directory.GetFiles(sDir);
            }
            catch
            {
                return;
            }

            FilePingCommand.stillSearching++;
            FilePingCommand.totalSeacrhes++;

            foreach (string dir in directories)
            {
                if (FilePingCommand.threadCount >= 200)
                {
                    try
                    {
                        SearchOnSameThread(dir, tFile, sLevel - 1);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        break;
                    }

                }
                else
                {
                    DirectorySearchThread sThread = new DirectorySearchThread(dir, tFile, sLevel - 1);
                    Thread t = new Thread(new ThreadStart(sThread.SearchOnNewThread));
                    t.IsBackground = true;
                    t.Priority = ThreadPriority.Highest;
                    t.Start();
                }
                
            }

            foreach (string file in files)
            {
                if (file.ToLower().Substring(sDir.Length + 1).Contains(tFile.ToLower()) && !FilePingCommand.fileLocations.Contains(file))
                {
                    FilePingCommand.fileLocations.Add(file);
                }
            }

            FilePingCommand.stillSearching--;
        }

        public void SearchOnNewThread()
        {
            try
            {
                if (currentLevel > 0)
                {
                    FilePingCommand.threadCount++;
                    FilePingCommand.totalSeacrhes++;
                    FilePingCommand.stillSearching++;
                    FilePingCommand.dirEntered.Add(directory);
                    string[] directorys = Directory.GetDirectories(directory);


                    foreach (string file in Directory.GetFiles(directory))
                    {
                        if (file.ToLower().Substring(directory.Length + 1).Contains(targetFile.ToLower()) && !FilePingCommand.fileLocations.Contains(file))
                        {
                            
                            FilePingCommand.fileLocations.Add(file);
                        }
                    }

                    
                    

                    foreach (string dir in directorys)
                    {
                        if (FilePingCommand.threadCount >= 200)
                        {
                            try
                            {
                                SearchOnSameThread(dir, targetFile, currentLevel - 1);
                            }
                            catch (UnauthorizedAccessException ex)
                            {
                                break;
                            }
                            
                        }
                        else
                        {

                            DirectorySearchThread sThread = new DirectorySearchThread(dir, targetFile, currentLevel - 1);
                            Thread t = new Thread(new ThreadStart(sThread.SearchOnNewThread));
                            t.IsBackground = true;
                            t.Priority = ThreadPriority.Highest;
                            t.Start();
                        }
                        
                        
                    }


                    FilePingCommand.stillSearching--;
                    
                }
                
            }
            catch (System.Exception ex)
            {
                if (currentLevel > 0) FilePingCommand.stillSearching--;
                FilePingCommand.threadCount--;
            }
            
        }
    }


}
