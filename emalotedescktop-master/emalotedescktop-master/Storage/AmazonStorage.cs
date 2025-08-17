using Amazon.S3;
using Amazon.S3.Model;
using EmaloteContta.Data;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EmaloteContta.Storage
{
    public class AmazonStorage
    {
    // Read configuration from environment variables. Never hardcode credentials.
    private static readonly string AWS_BUCKET_NAME = Environment.GetEnvironmentVariable("AWS_S3_BUCKET") ?? string.Empty;
    private static readonly string AWS_REGION = Environment.GetEnvironmentVariable("AWS_REGION") ?? "us-east-1";

        public static async Task UploadFileAsync(string filePath, string subDirectoryCnpj, string contentType)
        {
            try
            {
                if (!filePath.Contains("desconhecido"))
                {
                    if (string.IsNullOrWhiteSpace(AWS_BUCKET_NAME))
                        throw new InvalidOperationException("AWS_S3_BUCKET environment variable is not set.");

                    var fileInfo = new FileInfo(filePath);
                    // Use default credential chain (env vars, shared credentials, or IAM role) and configured region
                    var s3Client = new AmazonS3Client(Amazon.RegionEndpoint.GetBySystemName(AWS_REGION));
                    var bucketName = AWS_BUCKET_NAME;
                    string keyName = string.Empty;
                    if (contentType.Equals("application/xml"))
                        keyName = subDirectoryCnpj.Trim() + "/" + fileInfo.Name;
                    else
                        keyName = "DANFE/" + subDirectoryCnpj.Trim() + "/" + fileInfo.Name;
                    var request = new PutObjectRequest()
                    {
                        BucketName = bucketName,
                        Key = keyName,
                        ContentType = contentType,
                        FilePath = filePath,

                    };
                    var result = await s3Client.PutObjectAsync(request);
                    if (contentType.Equals("application/xml"))
                        SalvarClienteView(subDirectoryCnpj, keyName, result.ETag);
                    else if (contentType.Equals("application/pdf"))
                        Console.WriteLine("Enviado para amazon");
                }
                
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }

        private static void SalvarPdfClientView(string subDirectoryCnpj, string url, string tokenAz)
        {
            var repositorio = new PdfClienteViewModel();
            repositorio.Cadastre(new PdfCliente
            {
                CNPJ = subDirectoryCnpj,               
                IdCompany = Guid.NewGuid(),
                TokenAz = tokenAz,
                Url = url
            });
        }

        private static void SalvarClienteView(string subDirectoryCnpj, string url, string tokenAz)
        {
            //var repositorio = new XmlClienteViewModel();

            //repositorio.Cadastre(new XmlCliente
            //{
            //    CNPJ = subDirectoryCnpj,              
            //    IdCompany = Guid.NewGuid(),
            //    TokenAz = tokenAz,
            //    Url = url
            //});
        }
    }
}
