using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Emalote
{
    public static class ToolsEmalote
    {
        public const string APP_NAME = "Contta.Sistema";

        public static string AppDataPath => GetAppDataPath();

        public static string AppDataJsonPath => GetAppDataJsonPath();
           

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
       
    }
}
