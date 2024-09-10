using System.Diagnostics;

namespace GenericFunctions {

    public static class Cli {
        private const ConsoleKey answerYes = ConsoleKey.Y;
        private const ConsoleKey answerNo = ConsoleKey.N;

        /* Read Input */
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

    // CMD, PowerShell and Bash Processes
    public class Shell {
        
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

    public class Log {
        
        public static void Trace(String message) {
            
        }
        public static void Debug(String message) {
        }
        public static void Error(String message) {
            
        }
        private static Boolean logFileExists() {
            
        }
    }
}