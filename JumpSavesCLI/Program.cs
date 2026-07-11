using CommandLine;
using JSL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace JumpSavesCLI
{
    internal class Program
    {
        public class Options
        {
            [Option('s', "save", Required = false, HelpText = "Path to main Jump Space save file/dir (auto-detect if missing)")]
            public string SavePath { get; set; }

            [Option('d', "donorSave", Required = false, HelpText = "Path to donor Jump Space save file/dir")]
            public string DonorSavePath { get; set; }
        }

        static int Main(string[] args)
        {
            int result = 0;

            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(options =>
            {
                Program program = new Program();
                result = program.Run(options);
            });

            return result;
        }

        private class Command
        {
            public string ShortName { get; set; }
            public string LongName { get; set; }
            public string Description { get; set; }
            public Action Execute { get; set; }
        }

        private int Run(Options options)
        {
            Console.WriteLine($"JumpSavesCLI: JumpSpace command line save file editor v.{Assembly.GetExecutingAssembly().GetName().Version} by gurudennis");
            Console.WriteLine("*** This is not an official tool! Use at your own risk. ***\n");

#if !DEBUG
            try
#endif
            {
                JSL.SaveDir dir = null;
                if (String.IsNullOrEmpty(options.SavePath))
                {
                    Console.WriteLine("No save directory provided. Attempting to auto-detect...");
                    dir = JSL.SaveDir.Default;
                    if (dir == null)
                    {
                        Console.WriteLine("Failed to auto-detect the save directory. Please provide --save <...> on the command line.");
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine($"Auto-detected save directory: {dir.Path}");
                    }
                }

                JSL.SaveFile file = null;
                if (dir != null)
                {
                    Console.WriteLine($"Opening latest save file from dir: {dir.Path}");
                    file = dir.SaveFile;
                }
                else if (Directory.Exists(options.SavePath))
                {
                    Console.WriteLine($"Opening latest save file from dir: {options.SavePath}");
                    dir = new JSL.SaveDir(options.SavePath);
                    file = dir.SaveFile;
                }
                else
                {
                    Console.WriteLine($"Opening save file: {options.SavePath}");
                    file = new JSL.SaveFile(options.SavePath);
                }
                Console.WriteLine($"Opened {file.Path} successfully.\n");

                JSL.SaveFile donorFile = null;
                if (!string.IsNullOrEmpty(options.DonorSavePath))
                {
                    if (Directory.Exists(options.DonorSavePath))
                    {
                        Console.WriteLine($"Opening latest donor save file from dir: {options.DonorSavePath}");
                        JSL.SaveDir donorDir = new JSL.SaveDir(options.DonorSavePath);
                        donorFile = donorDir.SaveFile;
                    }
                    else
                    {
                        Console.WriteLine($"Opening donor save file: {options.DonorSavePath}");
                        donorFile = new JSL.SaveFile(options.DonorSavePath);
                    }
                    Console.WriteLine($"Opened {donorFile.Path} successfully.\n");
                }

                List<Command> commandsCopy = null;
                List<Command> commands = new List<Command>
                {
                    new Command
                    {
                        ShortName = "h",
                        LongName = "help",
                        Description = "Show this help message",
                        Execute = () => { PrintHelp(commandsCopy); }
                    },
                    new Command
                    {
                        ShortName = "pa",
                        LongName = "print_all",
                        Description = "Print the current save state as JSON",
                        Execute = () => { Console.WriteLine($"\n{file.State}\n"); }
                    },
                    new Command
                    {
                        ShortName = "w",
                        LongName = "write",
                        Description = "Write the current save state to the save file/dir",
                        Execute = () => { Save(file, dir); }
                    },
                    new Command
                    {
                        ShortName = "f",
                        LongName = "find",
                        Description = "Find an object in the main save",
                        Execute = () => { FindObject(file); }
                    },
                    new Command
                    {
                        ShortName = "fd",
                        LongName = "find_donor",
                        Description = "Find an object in the donor save",
                        Execute = () => { FindObject(donorFile); }
                    },
                    new Command
                    {
                        ShortName = "p",
                        LongName = "print",
                        Description = "Print an object from the main save",
                        Execute = () => { PrintObject(file); }
                    },
                    new Command
                    {
                        ShortName = "pd",
                        LongName = "print_donor",
                        Description = "Print an object from the donor save",
                        Execute = () => { PrintObject(donorFile); }
                    },
                    new Command
                    {
                        ShortName = "s",
                        LongName = "set",
                        Description = "Set a value into the main save at a given location",
                        Execute = () => { SetValue(file); }
                    },
                    new Command
                    {
                        ShortName = "t",
                        LongName = "transplant",
                        Description  = "Transplant an object from the donor save to the main save",
                        Execute = () => { TransplantObject(file, donorFile); }
                    },
                };
                commandsCopy = commands;

                while (true)
                {
                    Console.Write("\nInput comamnd ('h' to get help, 'q' to quit): ");
                    Console.Out.Flush();
                    string input = Console.ReadLine().Trim();
                    if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase) || string.Equals(input, "quit", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }

                    bool found = false;
                    foreach (Command c in commands)
                    {
                        if (string.Equals(input, c.ShortName, StringComparison.OrdinalIgnoreCase) || string.Equals(input, c.LongName, StringComparison.OrdinalIgnoreCase))
                        {
                            c.Execute();
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine($"Unrecognized command '{input}'");
                    }

                    Console.WriteLine();
                }
            }
#if !DEBUG
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 1;
            }
#endif

            return 0;
        }

        private void PrintHelp(List<Command> commands)
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("  q, quit - Quit the program");
            foreach (Command c in commands)
            {
                Console.WriteLine($"  {c.ShortName}, {c.LongName} - {c.Description}");
            }
        }

        private void FindObject(JSL.SaveFile file)
        {
            if (file == null)
            {
                Console.WriteLine("Error: No such file!");
                return;
            }

            Console.Write("Enter object name to find: ");
            Console.Out.Flush();
            string name = Console.ReadLine().Trim();
            JSL.SaveState.Location location = file.State.FindObject(name);
            if (!PrintObjectAtLocation(file, location))
            {
                Console.WriteLine($"Object '{name}' not found.");
            }
        }

        private void PrintObject(JSL.SaveFile file)
        {
            if (file == null)
            {
                Console.WriteLine("Error: No such file!");
                return;
            }
            Console.Write("Enter object location x/y/... to print: ");
            Console.Out.Flush();
            string locationStr = Console.ReadLine().Trim();
            JSL.SaveState.Location location = new JSL.SaveState.Location(locationStr);
            if (!PrintObjectAtLocation(file, location))
            {
                Console.WriteLine($"Location '{locationStr}' is invalid. Contrived valid example: 1/7/3");
            }
        }

        private bool PrintObjectAtLocation(JSL.SaveFile file, JSL.SaveState.Location location)
        {
            if (!location.IsValid)
            {
                return false;
            }

            object obj = file.State.GetObject(location);

            Console.WriteLine($"{JSL.SaveState.JSONFromObject(obj)}\n");

            Console.WriteLine($"\nThe object is at location {location}");
            if (obj != null && obj.GetType() == typeof(object[]))
            {
                Console.WriteLine($"It has {((object[])obj).Length} children.");
            }

            return true;
        }

        private void SetValue(JSL.SaveFile file)
        {
            if (file == null)
            {
                Console.WriteLine("Error: No such file!");
                return;
            }
            
            Console.Write("Enter object location x/y/... to set value: ");
            Console.Out.Flush();
            string locationStr = Console.ReadLine().Trim();
            JSL.SaveState.Location location = new JSL.SaveState.Location(locationStr);
            if (!location.IsValid)
            {
                Console.WriteLine($"Location '{locationStr}' is invalid. Contrived valid example: 1/7/3");
                return;
            }
            
            object obj = file.State.GetObject(location);
            if (obj == null)
            {
                Console.WriteLine($"No object found at location '{locationStr}'.");
                return;
            }
            
            Console.WriteLine($"Current value at {locationStr}: {JSL.SaveState.JSONFromObject(obj)}");
            Console.Write("Enter new value (as JSON): ");
            Console.Out.Flush();

            string newValueStr = Console.ReadLine().Trim();
            try
            {
                object newValue = JSL.SaveState.ObjectFromJSON(newValueStr, obj.GetType());
                JSL.ArrayBasedObject editor = new JSL.ArrayBasedObject(file.State.GetObject(location.Parent));
                editor.SetProperty(location.Sequence[location.Sequence.Count - 1], newValue);
                Console.WriteLine($"Successfully set new value at {locationStr}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting new value: {ex.Message}");
            }
        }

        private void TransplantObject(JSL.SaveFile mainFile, JSL.SaveFile donorFile)
        {
            if (mainFile == null || donorFile == null)
            {
                Console.WriteLine("Error: Both main and donor save files must be loaded.");
                return;
            }

            Console.Write("Enter object name to transplant from donor: ");
            Console.Out.Flush();
            string srcName = Console.ReadLine().Trim();
            JSL.SaveState.Location srcLocation = donorFile.State.FindObject(srcName);
            if (!srcLocation.IsValid)
            {
                Console.WriteLine($"Object '{srcName}' not found in donor save.");
                return;
            }

            object srcObject = donorFile.State.GetObject(srcLocation);
            if (srcObject == null)
            {
                Console.WriteLine($"Error retrieving object '{srcName}' from donor save.");
                return;
            }

            Console.Write("Enter object name to transplant to (will be overwritten!): ");
            Console.Out.Flush();
            string dstName = Console.ReadLine().Trim();
            JSL.SaveState.Location dstLocation = mainFile.State.FindObject(dstName);
            if (!dstLocation.IsValid)
            {
                Console.WriteLine($"Object '{dstName}' not found in main save.");
                return;
            }

            byte? placement = JSL.SaveState.GetObjectPlacement(mainFile.State.GetObject(dstLocation));
            if (placement is null || placement < 0 || placement > 5)
            {
                Console.WriteLine($"Destination object '{srcName}' from the main save has an invalid placement value of {placement ?? -1}.");
                return;
            }

            JSL.SaveState.SetObjectPlacement(srcObject, (byte)placement);
            mainFile.State.SetObject(dstLocation, srcObject);
        }

        private void Save(JSL.SaveFile file, JSL.SaveDir dir)
        {
            if (dir == null)
            {
                file.Save();
                Console.WriteLine($"Saved file {file.Path}");
            }
            else
            {
                dir.Save(file);
                Console.WriteLine($"Saved to directory {dir.Path}");
            }
        }
    }
}
