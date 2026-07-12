using CommandLine;
using JSL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using static JumpSavesCLI.Program;

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
                if (!Load(options, out JSL.SaveDir dir, out JSL.SaveFile file, out JSL.SaveFile donorFile))
                {
                    return 1;
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
                        ShortName = "l",
                        LongName = "load",
                        Description = "Reloads the save file(s)",
                        Execute = () => { Load(options, out dir, out file, out donorFile); }
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
                        ShortName = "i",
                        LongName = "insert",
                        Description = "Insert a value into the main save at a given location",
                        Execute = () => { InsertValue(file); }
                    },
                    new Command
                    {
                        ShortName = "r",
                        LongName = "remove",
                        Description = "Remove a value from the main save at a given location",
                        Execute = () => { RemoveValue(file); }
                    },
                    new Command
                    {
                        ShortName = "c",
                        LongName = "copy",
                        Description  = "Copy an object from one location in the main save to another",
                        Execute = () => { CopyValue(file); }
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
                            found = true;

#if !DEBUG
                            try
#endif
                            {
                                c.Execute();
                            }
#if !DEBUG
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
#endif
                            
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

        private bool Load(Options options, out JSL.SaveDir dir, out JSL.SaveFile file, out JSL.SaveFile donorFile)
        {
            dir = null;
            file = null;
            donorFile = null;

            try
            {
                if (String.IsNullOrEmpty(options.SavePath))
                {
                    Console.WriteLine("No save directory provided. Attempting to auto-detect...");
                    dir = JSL.SaveDir.Default;
                    if (dir == null)
                    {
                        Console.WriteLine("Failed to auto-detect the save directory. Please provide --save <...> on the command line.");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine($"Auto-detected save directory: {dir.Path}");
                    }
                }

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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load save file!");
                return false;
            }

            try
            {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load donor save file!");
                return false;
            }

            return true;
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

            Console.Write("Enter object location x/y/... after which to search, or leave empty for none: ");
            Console.Out.Flush();
            string after = Console.ReadLine().Trim();

            JSL.Location location = file.State.FindObject(name, new JSL.Location(after));
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
            JSL.Location location = new JSL.Location(locationStr);
            if (!PrintObjectAtLocation(file, location))
            {
                Console.WriteLine($"Location '{locationStr}' is invalid. Contrived valid example: 1/7/3");
            }
        }

        private bool PrintObjectAtLocation(JSL.SaveFile file, JSL.Location location)
        {
            if (!location.IsValid)
            {
                return false;
            }

            object obj = file.State.GetObject(location);
            if (obj == null)
            {
                return false;
            }

            Console.WriteLine($"{JSL.SaveState.JSONFromObject(obj)}\n");

            Console.WriteLine($"\nThe object of type {StringFromType(obj.GetType())} is at location {location}");
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
            JSL.Location location = new JSL.Location(locationStr);
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
            Console.Write("Enter new value (as JSON), or ~ to cancel: ");
            Console.Out.Flush();

            string newValueStr = Console.ReadLine().Trim();
            if (newValueStr == "~")
            {
                return;
            }

            try
            {
                object newValue = JSL.SaveState.ObjectFromJSON(newValueStr, obj.GetType());
                JSL.ArrayBasedObject editor = new JSL.ArrayBasedObject(file.State.GetObject(location.Parent), null);
                editor.SetProperty(location.Leaf, newValue);
                Console.WriteLine($"Successfully set new value at {locationStr}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting new value: {ex.Message}");
            }
        }

        private void InsertValue(JSL.SaveFile file)
        {
            if (file == null)
            {
                Console.WriteLine("Error: No such file!");
                return;
            }

            Console.Write("Enter object location x/y/... to insert value: ");
            Console.Out.Flush();
            string locationStr = Console.ReadLine().Trim();
            JSL.Location location = new JSL.Location(locationStr);
            if (!location.IsValid)
            {
                Console.WriteLine($"Location '{locationStr}' is invalid. Contrived valid example: 1/7/3");
                return;
            }

            object obj = file.State.GetObject(location.Parent);
            if (obj == null || obj.GetType() != typeof(object[]))
            {
                Console.WriteLine($"No array found at location '{location.Parent}'.");
                return;
            }

            Console.WriteLine($"Current array at {location.Parent}:\n{JSL.SaveState.JSONFromObject(obj)}");

            Console.WriteLine("Available data types: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/built-in-types");
            Console.Write("Enter the type of new value to insert, or ~ to cancel: ");
            Console.Out.Flush();
            string newValueTypeStr = Console.ReadLine().Trim();
            if (newValueTypeStr == "~")
            {
                return;
            }

            Type type = TypeFromString(newValueTypeStr);

            Console.Write("Enter new value to insert (as JSON), or ~ to cancel: ");
            Console.Out.Flush();
            string newValueStr = Console.ReadLine().Trim();
            if (newValueStr == "~")
            {
                return;
            }

            object newValue = JSL.SaveState.ObjectFromJSON(newValueStr, type);
            object[] grandparent = file.State.GetObject(location.Parent.Parent) as object[];
            JSL.ArrayBasedObject editor = new JSL.ArrayBasedObject(file.State.GetObject(location.Parent), grandparent);
            if (!editor.InsertProperty(location.Leaf, newValue))
            {
                Console.WriteLine($"Failed to insert new value at location {location}");
                return;
            }

            Console.WriteLine($"Successfully inserted new value at {locationStr}.");
        }

        public void RemoveValue(JSL.SaveFile file)
        {
            if (file == null)
            {
                Console.WriteLine("Error: No such file!");
                return;
            }

            Console.Write("Enter object location x/y/... to remove value: ");
            Console.Out.Flush();
            string locationStr = Console.ReadLine().Trim();
            JSL.Location location = new JSL.Location(locationStr);
            if (!location.IsValid)
            {
                Console.WriteLine($"Location '{locationStr}' is invalid. Contrived valid example: 1/7/3");
                return;
            }

            object obj = file.State.GetObject(location.Parent);
            if (obj == null || obj.GetType() != typeof(object[]))
            {
                Console.WriteLine($"No array found at location '{location.Parent}'.");
                return;
            }

            object[] grandparent = file.State.GetObject(location.Parent.Parent) as object[];
            JSL.ArrayBasedObject editor = new JSL.ArrayBasedObject(obj, grandparent);
            if (!editor.RemoveProperty(location.Leaf))
            {
                Console.WriteLine($"Failed to remove value at location {location}");
                return;
            }

            Console.WriteLine($"Successfully removed value at {locationStr}.");
        }

        private void CopyValue(JSL.SaveFile file)
        {
            if (file == null)
            {
                Console.WriteLine("Error: No such file!");
                return;
            }

            Console.Write("Enter source object location x/y/...: ");
            Console.Out.Flush();
            string srcLocationStr = Console.ReadLine().Trim();
            JSL.Location srcLocation = new JSL.Location(srcLocationStr);
            if (!srcLocation.IsValid)
            {
                Console.WriteLine($"Source location '{srcLocationStr}' is invalid. Contrived valid example: 1/7/3");
                return;
            }

            Console.Write("Enter destination object location x/y/...: ");
            Console.Out.Flush();
            string dstLocationStr = Console.ReadLine().Trim();
            JSL.Location dstLocation = new JSL.Location(dstLocationStr);
            if (!dstLocation.IsValid)
            {
                Console.WriteLine($"Destination location '{dstLocationStr}' is invalid. Contrived valid example: 1/7/3");
                return;
            }

            object obj = file.State.GetObject(srcLocation);
            if (obj == null)
            {
                Console.WriteLine($"No object found at location '{srcLocation}'.");
                return;
            }

            object dstParentObj = file.State.GetObject(dstLocation.Parent);
            if (dstParentObj == null)
            {
                Console.WriteLine($"No parent object found at location '{dstLocation.Parent}'.");
                return;
            }

            object[] grandparent = file.State.GetObject(dstLocation.Parent.Parent) as object[];
            JSL.ArrayBasedObject editor = new ArrayBasedObject(dstParentObj, grandparent);
            if (!editor.InsertProperty(dstLocation.Leaf, obj))
            {
                Console.WriteLine($"Failed to copy value to location {dstLocation}");
                return;
            }

            Console.WriteLine($"Successfully copied value to {dstLocation}.");
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
            JSL.Location srcLocation = donorFile.State.FindObject(srcName);
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
            JSL.Location dstLocation = mainFile.State.FindObject(dstName);
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

        private static Type TypeFromString(string s)
        {
            if (string.Equals(s, "bool", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(bool);
            }
            else if (string.Equals(s, "byte", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(byte);
            }
            else if (string.Equals(s, "short", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(short);
            }
            else if (string.Equals(s, "int", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(int);
            }
            else if (string.Equals(s, "float", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(float);
            }
            else if (string.Equals(s, "double", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(double);
            }
            else if (string.Equals(s, "string", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(string);
            }
            else if (string.Equals(s, "object[]", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(object[]);
            }

            return Type.GetType(s);
        }

        private static string StringFromType(Type t)
        {
            if (t == typeof(bool))
            {
                return "bool";
            }
            else if (t == typeof(byte))
            {
                return "byte";
            }
            else if (t == typeof(short))
            {
                return "short";
            }
            else if (t == typeof(int))
            {
                return "int";
            }
            else if (t == typeof(float))
            {
                return "float";
            }
            else if (t == typeof(double))
            {
                return "double";
            }
            else if (t == typeof(string))
            {
                return "string";
            }
            else if (t == typeof(object[]))
            {
                return "object[]";
            }

            return t.FullName;
        }
    }
}
