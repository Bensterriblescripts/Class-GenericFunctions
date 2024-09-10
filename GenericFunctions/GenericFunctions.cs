using System.Diagnostics;
using Nanson.GenericFunctions;

internal static class Test {
    public static void Main() {

        TestLog();
        
    }

    private static void TestLog() {
        Log.Info("Test");
        Log.Debug("Test");
        Log.Error("Test");
    }
}

namespace Nanson.GenericFunctions {
    
    /* Command Line Tools */ 
    public static class Cli {
        
        private const ConsoleKey answerYes = ConsoleKey.Y;
        private const ConsoleKey answerNo = ConsoleKey.N;

        // Read Input
        public static Boolean CheckResponse_YN(String info) {
            Console.WriteLine(info);
            ConsoleKey keyInfo = Console.ReadKey(true).Key;
            while (true) {
                if (keyInfo == answerYes) {
                    return true;
                }

                if (keyInfo == answerNo) {
                    return false;
                }

                Console.WriteLine("Please enter a valid response. (y/n)");
                keyInfo = Console.ReadKey(true).Key;
            }
        }

        public static Int16 CheckResponse_Numbers(ConsoleKey[] responses) {
            ConsoleKey keyInfo = Console.ReadKey(true).Key;
            while (true) {
                for (Int16 i = 0; i < responses.Length; i++) {
                    if (responses.Contains(keyInfo)) {
                        return i;
                    }
                }

                Console.WriteLine("Please enter a valid response. (1, 2, 3, 4)");
                keyInfo = Console.ReadKey(true).Key;
            }
        }
    }

    /* CMD, PowerShell and Bash Processes */
    public static class Shell {
        
        /* CMD */
        public static Boolean AutoClose(String command) {
            using Process p = new();
            ProcessStartInfo psi = new() {
                FileName = "cmd.exe",
                Arguments = $"/C {command}",
                RedirectStandardOutput = false,
                UseShellExecute = true,
                CreateNoWindow = false
            };
            p.StartInfo = psi;
            p.Start();
            p.WaitForExit();

            return true;
        }
        public static String? GetOutput(String command) {
            using Process p = new();
            ProcessStartInfo psi = new() {
                FileName = "cmd.exe",
                Arguments = $"/C {command}",
                RedirectStandardOutput = false,
                UseShellExecute = true,
                CreateNoWindow = false
            };
            p.StartInfo = psi;
            p.Start();
            String output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            if (output.Trim() == String.Empty) {
                return null;
            }
            return output.Trim();
        }
        public static Boolean KeepOpen(String command) {
            using Process p = new();
            ProcessStartInfo psi = new() {
                FileName = "cmd.exe",
                Arguments = $"/K {command}",
                RedirectStandardOutput = false,
                UseShellExecute = true,
                CreateNoWindow = false
            };
            p.StartInfo = psi;
            p.Start();
            p.WaitForExit();

            return true;
        }
    }

    /* Logging */
    public static class Log {
        private static readonly String currentDir = Directory.GetCurrentDirectory();
        
        // File Logging
        public static void Info(String message) {
            String? file = FileHandler("info");
            if (file is not null) {
                WriteToFile(file, message);
            }
        }
        public static void Debug(String message) {
            String? file = FileHandler("debug");
            if (file is not null) {
                WriteToFile(file, message);
            }
        }
        public static void Error(String message) {
            String? file = FileHandler("error");
            if (file is not null) {
                WriteToFile(file, message);
            }
        }

        private static void WriteToFile(String logFile, String message) {
            try {
                using StreamWriter sw = File.AppendText(logFile);
                sw.WriteLine(message);
            }
            catch (Exception e) {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }
        private static String? FileHandler(String level) {
            String currentTime = DateTime.Now.ToString("yyyy-MM-dd");
            String logFile = $@"{currentDir}\log\{currentTime}.{level}.log";

            if (File.Exists(logFile)) {
                return logFile;
            }
            try {
                if (!Directory.Exists($@"{currentDir}\log")) {
                    Directory.CreateDirectory($@"{currentDir}\log");
                }
                using FileStream fs = File.Create(logFile);
                return logFile;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}