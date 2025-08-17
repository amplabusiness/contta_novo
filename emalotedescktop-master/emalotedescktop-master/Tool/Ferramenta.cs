using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace EmaloteContta.Tool
{
    public static class Ferramenta
    {
        public const string APP_NAME = "Contta";

        public static string AppDataPath => GetAppDataPath();

        public static string AppDataJsonPath => GetAppDataJsonPath();

        public static void ExecuteCmd(string comando, bool waitForExit = true)
        {
            var process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/C " + comando;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            if (waitForExit)
            {
                process.WaitForExit();
            }
        }

        public static void Gravar(object dados, string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            var jsonWrite = JsonConvert.SerializeObject(dados, Formatting.None);
            File.WriteAllText(path, jsonWrite);
        }

        public static string GetNumbers(this string value)
        {
            return value != null ? new string(value.Where(char.IsDigit).ToArray()) : null;
        }

        public static int? ToInt(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            var numbers = Regex.Match(value, @"-?\d+").Value;

            if (string.IsNullOrWhiteSpace(numbers))
            {
                return null;
            }

            if (int.TryParse(numbers, out int result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            FieldInfo field = value.GetType().GetField(value.ToString());

            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        private static string GetAppDataPath()
        {
            //var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), APP_NAME);
            //Directory.CreateDirectory(path);

            string path = Path.Combine(Directory.GetCurrentDirectory(), "data", APP_NAME);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }

        private static string GetAppDataJsonPath()
        {
            //var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), APP_NAME, "Jsons");
            //Directory.CreateDirectory(path);

            string path = Path.Combine(Directory.GetCurrentDirectory(), "data", APP_NAME, "Jsons");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }

        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }       
    }
}

