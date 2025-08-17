using Contta.Corporate.Contta.Schedule.SpedContta;
using MongoDB.Driver;
using Corporate.Contta.Schedule.SpedContta.MapBlocos;

namespace Corporate.Contta.Schedule.SpedContta
{

    class Program
    {
        static void Main(string[] args)
        {        
            //MapBloco0 mapBloco0 = new MapBloco0();
            MapBlocoC mapBlocoC = new MapBlocoC();
            //mapBloco0.GetBloco0();
            //mapBlocoC.GetBlocoC();


            //IMongoClient mongoClient = new MongoClient("mongodb://localhost");
            //IMongoDatabase database = mongoClient.GetDatabase("conttadb");

            mapBlocoC.GetBlocoC();

            //var listaEmpresa = empresaIBGE9373.Find(c => c.cnpj == "00010280000119").ToList();
            //var listaEmpresa = empresaIBGE9373.Find(c => c.codigo_municipio == "9373").ToList();

            //foreach (var item in listaEmpresa)
            //{
            //    Thread.Sleep(3000);

            //    var getInfomationByDocumentRequest = new GetInfomationByDocumentRequest { Document = item.cnpj };
            //}

        }
    }
}
