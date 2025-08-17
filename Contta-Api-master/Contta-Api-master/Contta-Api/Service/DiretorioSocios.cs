using Contta.Inteligencia.Cnpj.Model.Entity;
using System.Collections.Generic;
using System.IO;

namespace Contta.Inteligencia.Cnpj.Service
{
    public class DiretorioSocios
    {
        public List<Socio> GetDadosSocios(string nome, string cpf, string cnpj)
        {
            var filePathSocio = @"C:\contta\DadosEmpresa\socios\socios.csv";
            List<Socio> listSocio = new List<Socio>();

            if (filePathSocio != null && filePathSocio != "")
            {
                var pesquisa = "";
                var cpfFormat = "";
                var tipo = 7;

                if (nome != null && nome != "")
                {
                    pesquisa = nome.ToUpper();
                    tipo = 3;
                }
                else if (cpf != null && cpf != "")
                {
                    pesquisa = cpf;
                    cpfFormat = pesquisa.Remove(1, 3);
                    cpfFormat = cpfFormat.Remove(6, 2);
                    tipo = 2;
                }
                else if (cnpj != null && cnpj != "")
                {
                    pesquisa = cnpj;
                    tipo = 0;
                }

                if (tipo != 7)
                {
                    string lineSocio;
                    var cpfString = "";
                    using (var fs = File.OpenRead(filePathSocio))
                    using (var reader = new StreamReader(fs))
                        while ((lineSocio = reader.ReadLine()) != null)
                        {                           
                            var socio = lineSocio.Split(',');                          
                            if (tipo == 2)
                            {
                                cpfString = socio[tipo].Replace("*", "");
                                if (cpfString.Equals(cpfFormat))
                                {
                                    listSocio.Add(new Socio
                                    {
                                        cnpj = socio[0].Replace(@"\", "").Replace(@"""", ""),
                                       
                                        cnpj_cpf_socio = socio[2].Replace(@"\", "").Replace(@"""", ""),
                                        nome_socio = socio[3].Replace(@"\", "").Replace(@"""", ""),
                                        tipo = socio[6].Replace(@"\", "").Replace(@"""", ""),
                                        cargo = socio[7].Replace(@"\", "").Replace(@"""", "")
                                    });                                                                       
                                }
                            }
                            else if (socio[tipo].Contains(pesquisa))
                            {
                                listSocio.Add(new Socio
                                {
                                    cnpj = socio[0].Replace(@"\", "").Replace(@"""", ""),
                                   
                                    cnpj_cpf_socio = socio[2].Replace(@"\", "").Replace(@"""", ""),
                                    nome_socio = socio[3].Replace(@"\", "").Replace(@"""", ""),
                                    tipo = socio[6].Replace(@"\", "").Replace(@"""", ""),
                                    cargo = socio[7].Replace(@"\", "").Replace(@"""", "")
                                });                                                               
                            }
                        }
                }
            }

            return listSocio;
        }

    }
}
