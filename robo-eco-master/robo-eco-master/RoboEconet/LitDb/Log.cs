using LiteDB;
using RoboEconet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RoboEconet.LitDb
{
    public class Log
    {
        private static string APP_NAME;
        private static string LOG_LITE_DB_PATH => Path.Combine(GetAppDataPath(), "ConttaData.db");

        public static void SetAppName(string appName)
        {
            APP_NAME = appName;
        }


        //public static List<PisConfinsDto> GetAllDiretorio()
        //{
        //    using (var db = new LiteDatabase(LOG_LITE_DB_PATH))
        //    {
        //        var context = db.GetCollection<PisConfinsDto>(nameof(PisConfinsDto));
        //        return context.Find(c => c.cnpj != null).ToList();
        //    }
        //}

        public  void InsertPisConfins(List<PisConfinsDto> pisConfinsDto)
        {
            try
            {
                foreach (var item in pisConfinsDto)
                {
                    item.Id = Guid.NewGuid();
                    using (var db = new LiteDatabase(LOG_LITE_DB_PATH))
                    {
                        var context = db.GetCollection<PisConfinsDto>(nameof(PisConfinsDto));
                        context.Insert(item);
                    }
                }               
            }
            finally
            {
            }
        }

        private static string GetAppDataPath()
        {
            //string path = Path.Combine(Directory.GetCurrentDirectory(), "datacontta", APP_NAME);
            var path = Path.Combine(Directory.GetCurrentDirectory(), APP_NAME);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }        
    }
}
