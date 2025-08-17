using Contta.Inteligencia.Cnpj.Model.Entity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Contta.Inteligencia.Cnpj.Service
{
    public class ParceEmpresa
    {
        private readonly IMongoDatabase _database = MongoClient();

        public static IMongoDatabase MongoClient()
        {

            IMongoClient mongoClient = new MongoClient("mongodb://contta:contta123456@192.46.218.34:27017/?authSource=admin&readPreference=primary&ssl=false");
            IMongoDatabase _database = mongoClient.GetDatabase("BaseCnpjContta");

            return _database;
        }
        public List<Empresa> GetEmpresas(string diretorioEmpresa, string cnpj)
        {
            List<Empresa> listEmpresa = new List<Empresa>();
            List<Empresa> listFilial = new List<Empresa>();
            List<Socio> listSocio = new List<Socio>();

            string filePath = diretorioEmpresa;
            string filePathSocio = "";

            #region Diretorio SociosEmpresas
            filePathSocio = GetDiretorioSocios(diretorioEmpresa, filePathSocio);
            #endregion

            #region PesquisaEmpresasSocios

            if (filePathSocio != null && filePathSocio != "")
            {
                string lineSocio;
                using (var fs = File.OpenRead(filePathSocio))
                using (var reader = new StreamReader(fs))
                    while ((lineSocio = reader.ReadLine()) != null)
                    {
                        var socio = lineSocio.Split(',');

                        if (socio[0].Contains(cnpj))
                        {
                            listSocio.Add(new Socio
                            {
                                cnpj = socio[0].Replace(@"\", "").Replace(@"""", ""),
                                tipo_socio = Convert.ToInt32(socio[1].Replace(@"\", "").Replace(@"""", "")),
                                nome_socio = socio[2].Replace(@"\", "").Replace(@"""", ""),
                                cnpj_cpf_socio = socio[3].Replace(@"\", "").Replace(@"""", ""),
                                cod_qualificacao = Convert.ToInt32(socio[4].Replace(@"\", "").Replace(@"""", "")),
                                perc_capital = Convert.ToDouble(socio[5].Replace(@"\", "").Replace(@"""", "")),
                                data_entrada = socio[6].Replace(@"\", "").Replace(@"""", ""),
                                cod_pais_ext = socio[7].Replace(@"\", "").Replace(@"""", ""),
                                nome_pais_ext = socio[8].Replace(@"\", "").Replace(@"""", ""),
                                cpf_repres = socio[9].Replace(@"\", "").Replace(@"""", ""),
                                nome_repres = socio[10].Replace(@"\", "").Replace(@"""", ""),
                                cod_qualif_repres = socio[11].Replace(@"\", "").Replace(@"""", ""),

                            });
                        }
                    }
            }

            #endregion

            #region Pesquisa Empresa

            if (filePath != null && filePath != "")
            {
                string line;
                using (var fs = File.OpenRead(filePath))
                using (var reader = new StreamReader(fs))
                    while ((line = reader.ReadLine()) != null)
                    {
                        var empre = line.Split(',');

                        if (empre[0].Contains(cnpj))
                        {
                            //Filias
                            if (Convert.ToDouble(empre[1].Replace(@"\", "").Replace(@"""", "")) > 1)
                            {
                                if (empre[0].Contains(cnpj.Substring(0, 6)))
                                {
                                    listFilial.Add(new Empresa
                                    {
                                        cnpj = empre[0].Replace(@"\", "").Replace(@"""", ""),
                                        matriz_filial = empre[1].Replace(@"\", "").Replace(@"""", ""),
                                        razao_social = empre[2].Replace(@"\", "").Replace(@"""", ""),
                                        nome_fantasia = empre[3].Replace(@"\", "").Replace(@"""", ""),
                                        situacao = empre[4].Replace(@"\", "").Replace(@"""", ""),
                                        data_situacao = empre[5].Replace(@"\", "").Replace(@"""", ""),
                                        motivo_situacao = empre[6].Replace(@"\", "").Replace(@"""", ""),
                                        nm_cidade_exterior = empre[7].Replace(@"\", "").Replace(@"""", ""),
                                        cod_pais = empre[8].Replace(@"\", "").Replace(@"""", ""),
                                        nome_pais = empre[9].Replace(@"\", "").Replace(@"""", ""),
                                        cod_nat_juridica = empre[10].Replace(@"\", "").Replace(@"""", ""),
                                        data_inicio_ativ = empre[11].Replace(@"\", "").Replace(@"""", ""),
                                        cnae_fiscal = empre[12].Replace(@"\", "").Replace(@"""", ""),
                                        tipo_logradouro = empre[13].Replace(@"\", "").Replace(@"""", ""),
                                        logradouro = empre[14].Replace(@"\", "").Replace(@"""", ""),
                                        numero = empre[15].Replace(@"\", "").Replace(@"""", ""),
                                        complemento = empre[16].Replace(@"\", "").Replace(@"""", ""),
                                        bairro = empre[17].Replace(@"\", "").Replace(@"""", ""),
                                        cep = empre[18].Replace(@"\", "").Replace(@"""", ""),
                                        uf = empre[19].Replace(@"\", "").Replace(@"""", ""),
                                        cod_municipio = empre[20].Replace(@"\", "").Replace(@"""", ""),
                                        municipio = empre[21].Replace(@"\", "").Replace(@"""", ""),
                                        ddd_1 = empre[22].Replace(@"\", "").Replace(@"""", ""),
                                        telefone_1 = empre[23].Replace(@"\", "").Replace(@"""", ""),
                                        ddd_2 = empre[24].Replace(@"\", "").Replace(@"""", ""),
                                        telefone_2 = empre[25].Replace(@"\", "").Replace(@"""", ""),
                                        ddd_fax = empre[26].Replace(@"\", "").Replace(@"""", ""),
                                        num_fax = empre[27].Replace(@"\", "").Replace(@"""", ""),
                                        email = empre[28].Replace(@"\", "").Replace(@"""", ""),
                                        qualif_resp = empre[29].Replace(@"\", "").Replace(@"""", ""),
                                        capital_social = empre[30].Replace(@"\", "").Replace(@"""", ""),
                                        porte = empre[31].Replace(@"\", "").Replace(@"""", ""),
                                        opc_simples = empre[32].Replace(@"\", "").Replace(@"""", ""),
                                        data_opc_simples = empre[33].Replace(@"\", "").Replace(@"""", ""),
                                        data_exc_simples = empre[34].Replace(@"\", "").Replace(@"""", ""),
                                        opc_mei = empre[35].Replace(@"\", "").Replace(@"""", ""),
                                        sit_especial = empre[36].Replace(@"\", "").Replace(@"""", ""),
                                        data_sit_especial = empre[37].Replace(@"\", "").Replace(@"""", ""),

                                    });
                                }
                            }

                            //Empresa
                            listEmpresa.Add(new Empresa
                            {
                                cnpj = empre[0].Replace(@"\", "").Replace(@"""", ""),
                                matriz_filial = empre[1].Replace(@"\", "").Replace(@"""", ""),
                                razao_social = empre[2].Replace(@"\", "").Replace(@"""", ""),
                                nome_fantasia = empre[3].Replace(@"\", "").Replace(@"""", ""),
                                situacao = empre[4].Replace(@"\", "").Replace(@"""", ""),
                                data_situacao = empre[5].Replace(@"\", "").Replace(@"""", ""),
                                motivo_situacao = empre[6].Replace(@"\", "").Replace(@"""", ""),
                                nm_cidade_exterior = empre[7].Replace(@"\", "").Replace(@"""", ""),
                                cod_pais = empre[8].Replace(@"\", "").Replace(@"""", ""),
                                nome_pais = empre[9].Replace(@"\", "").Replace(@"""", ""),
                                cod_nat_juridica = empre[10].Replace(@"\", "").Replace(@"""", ""),
                                data_inicio_ativ = empre[11].Replace(@"\", "").Replace(@"""", ""),
                                cnae_fiscal = empre[12].Replace(@"\", "").Replace(@"""", ""),
                                tipo_logradouro = empre[13].Replace(@"\", "").Replace(@"""", ""),
                                logradouro = empre[14].Replace(@"\", "").Replace(@"""", ""),
                                numero = empre[15].Replace(@"\", "").Replace(@"""", ""),
                                complemento = empre[16].Replace(@"\", "").Replace(@"""", ""),
                                bairro = empre[17].Replace(@"\", "").Replace(@"""", ""),
                                cep = empre[18].Replace(@"\", "").Replace(@"""", ""),
                                uf = empre[19].Replace(@"\", "").Replace(@"""", ""),
                                cod_municipio = empre[20].Replace(@"\", "").Replace(@"""", ""),
                                municipio = empre[21].Replace(@"\", "").Replace(@"""", ""),
                                ddd_1 = empre[22].Replace(@"\", "").Replace(@"""", ""),
                                telefone_1 = empre[23].Replace(@"\", "").Replace(@"""", ""),
                                ddd_2 = empre[24].Replace(@"\", "").Replace(@"""", ""),
                                telefone_2 = empre[25].Replace(@"\", "").Replace(@"""", ""),
                                ddd_fax = empre[26].Replace(@"\", "").Replace(@"""", ""),
                                num_fax = empre[27].Replace(@"\", "").Replace(@"""", ""),
                                email = empre[28].Replace(@"\", "").Replace(@"""", ""),
                                qualif_resp = empre[29].Replace(@"\", "").Replace(@"""", ""),
                                capital_social = empre[30].Replace(@"\", "").Replace(@"""", ""),
                                porte = empre[31].Replace(@"\", "").Replace(@"""", ""),
                                opc_simples = empre[32].Replace(@"\", "").Replace(@"""", ""),
                                data_opc_simples = empre[33].Replace(@"\", "").Replace(@"""", ""),
                                data_exc_simples = empre[34].Replace(@"\", "").Replace(@"""", ""),
                                opc_mei = empre[35].Replace(@"\", "").Replace(@"""", ""),
                                sit_especial = empre[36].Replace(@"\", "").Replace(@"""", ""),
                                data_sit_especial = empre[37].Replace(@"\", "").Replace(@"""", ""),
                                listFilias = listFilial,
                                listSocio = listSocio
                            });
                        }
                    }
            }

            return listEmpresa;
        }

        private static string GetDiretorioSocios(string diretorioEmpresa, string filePathSocio)
        {
            if (diretorioEmpresa.Contains("Empresa01"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            else if (diretorioEmpresa.Contains("Empresa02"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            else if (diretorioEmpresa.Contains("Empresa03"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            else if (diretorioEmpresa.Contains("Empresa04"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            else if (diretorioEmpresa.Contains("Empresa05"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            else if (diretorioEmpresa.Contains("Empresa06"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            else if (diretorioEmpresa.Contains("Empresa07"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            else if (diretorioEmpresa.Contains("Empresa08"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            else if (diretorioEmpresa.Contains("Empresa09"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            else if (diretorioEmpresa.Contains("Empresa10"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            else if (diretorioEmpresa.Contains("Empresa11"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            else if (diretorioEmpresa.Contains("Empresa12"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            else if (diretorioEmpresa.Contains("Empresa13"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            else if (diretorioEmpresa.Contains("Empresa14"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            else if (diretorioEmpresa.Contains("Empresa15"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            else if (diretorioEmpresa.Contains("Empresa16"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            else if (diretorioEmpresa.Contains("Empresa17"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            else if (diretorioEmpresa.Contains("Empresa18"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            else if (diretorioEmpresa.Contains("Empresa19"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            else if (diretorioEmpresa.Contains("Empresa20"))
                filePathSocio = DiretorioSocio(diretorioEmpresa, filePathSocio);
            return filePathSocio;
        }

        #endregion

        #region DiretorioSocio
        private static string DiretorioSocio(string diretorioEmpresa, string filePathSocio)
        {
            #region Empresa01
            if (diretorioEmpresa.Contains("Empresa01"))
            {
                filePathSocio = "socios1A";
            }
            else if (diretorioEmpresa.Contains("Empresa02"))
            {
                filePathSocio = "socios1B";
            }
            else if (diretorioEmpresa.Contains("Empresa03"))
            {
                filePathSocio = "socios1C";
            }
            else if (diretorioEmpresa.Contains("Empresa04"))
            {
                filePathSocio = "socios1D";
            }
            else if (diretorioEmpresa.Contains("Empresa05"))
            {
                filePathSocio = "socios1E";
            }

            #endregion

            #region Empresa02
            else if (diretorioEmpresa.Contains("Empresa06"))
            {
                filePathSocio = "socios2A";
            }
            else if (diretorioEmpresa.Contains("Empresa07"))
            {
                filePathSocio = "socios2B";
            }
            else if (diretorioEmpresa.Contains("Empresa08"))
            {
                filePathSocio = "socios2C";
            }
            else if (diretorioEmpresa.Contains("Empresa09"))
            {
                filePathSocio = "socios2D";
            }
            else if (diretorioEmpresa.Contains("Empresa10"))
            {
                filePathSocio = "socios2E";
            }


            #endregion

            #region Empresa03
            else if (diretorioEmpresa.Contains("empresas3A"))
            {
                filePathSocio = "socios3A";
            }
            else if (diretorioEmpresa.Contains("empresas3B"))
            {
                filePathSocio = "socios3B";
            }
            else if (diretorioEmpresa.Contains("empresas3C"))
            {
                filePathSocio = "socios3C";
            }
            else if (diretorioEmpresa.Contains("empresas3D"))
            {
                filePathSocio = "socios3D";
            }
            else if (diretorioEmpresa.Contains("empresas3E"))
            {
                filePathSocio = "socios3E";
            }


            #endregion

            #region Empresa04
            else if (diretorioEmpresa.Contains("empresas4A"))
            {
                filePathSocio = "socios4A";
            }
            else if (diretorioEmpresa.Contains("empresas4B"))
            {
                filePathSocio = "socios4B";
            }
            else if (diretorioEmpresa.Contains("empresas4C"))
            {
                filePathSocio = "socios4C";
            }
            else if (diretorioEmpresa.Contains("empresas4D"))
            {
                filePathSocio = "socios4D";
            }
            else if (diretorioEmpresa.Contains("empresas4E"))
            {
                filePathSocio = "socios4E";
            }


            #endregion

            #region Empresa05
            else if (diretorioEmpresa.Contains("empresas5A"))
            {
                filePathSocio = "socios5A";
            }
            else if (diretorioEmpresa.Contains("empresas5B"))
            {
                filePathSocio = "socios5B";
            }
            else if (diretorioEmpresa.Contains("empresas5C"))
            {
                filePathSocio = "socios5C";
            }
            else if (diretorioEmpresa.Contains("empresas5D"))
            {
                filePathSocio = "socios5D";
            }
            else if (diretorioEmpresa.Contains("empresas5E"))
            {
                filePathSocio = "socios5E";
            }


            #endregion

            #region Empresa06
            else if (diretorioEmpresa.Contains("empresas6A"))
            {
                filePathSocio = "socios6A";
            }
            else if (diretorioEmpresa.Contains("empresas6B"))
            {
                filePathSocio = "socios6B";
            }
            else if (diretorioEmpresa.Contains("empresas6C"))
            {
                filePathSocio = "socios6C";
            }
            else if (diretorioEmpresa.Contains("empresas6D"))
            {
                filePathSocio = "socios6D";
            }
            else if (diretorioEmpresa.Contains("empresas6E"))
            {
                filePathSocio = "socios6E";
            }


            #endregion

            #region Empresa07
            else if (diretorioEmpresa.Contains("empresas7A"))
            {
                filePathSocio = "socios7A";
            }
            else if (diretorioEmpresa.Contains("empresas7B"))
            {
                filePathSocio = "socios7B";
            }
            else if (diretorioEmpresa.Contains("empresas7C"))
            {
                filePathSocio = "socios7C";
            }
            else if (diretorioEmpresa.Contains("empresas7D"))
            {
                filePathSocio = "socios7D";
            }
            else if (diretorioEmpresa.Contains("empresas7E"))
            {
                filePathSocio = "socios7E";
            }


            #endregion

            #region Empresa08
            else if (diretorioEmpresa.Contains("empresas8A"))
            {
                filePathSocio = "socios8A";
            }
            else if (diretorioEmpresa.Contains("empresas8B"))
            {
                filePathSocio = "socios8B";
            }
            else if (diretorioEmpresa.Contains("empresas8C"))
            {
                filePathSocio = "socios8C";
            }
            else if (diretorioEmpresa.Contains("empresas8D"))
            {
                filePathSocio = "socios8D";
            }
            else if (diretorioEmpresa.Contains("empresas8E"))
            {
                filePathSocio = "socios8E";
            }


            #endregion

            #region Empresa09
            else if (diretorioEmpresa.Contains("empresas9A"))
            {
                filePathSocio = "socios9A";
            }
            else if (diretorioEmpresa.Contains("empresas9B"))
            {
                filePathSocio = "socios9B";
            }
            else if (diretorioEmpresa.Contains("empresas9C"))
            {
                filePathSocio = "socios9C";
            }
            else if (diretorioEmpresa.Contains("empresas9D"))
            {
                filePathSocio = "socios9D";
            }
            else if (diretorioEmpresa.Contains("empresas9E"))
            {
                filePathSocio = "socios9E";
            }


            #endregion

            #region Empresa10
            else if (diretorioEmpresa.Contains("empresas10A"))
            {
                filePathSocio = "socios10A";
            }
            else if (diretorioEmpresa.Contains("empresas10B"))
            {
                filePathSocio = "socios10B";
            }
            else if (diretorioEmpresa.Contains("empresas10C"))
            {
                filePathSocio = "socios10C";
            }
            else if (diretorioEmpresa.Contains("empresas10D"))
            {
                filePathSocio = "socios10D";
            }
            else if (diretorioEmpresa.Contains("empresas10E"))
            {
                filePathSocio = "socios10E";
            }


            #endregion

            #region Empresa11
            else if (diretorioEmpresa.Contains("empresas11A"))
            {
                filePathSocio = "socios11A";
            }
            else if (diretorioEmpresa.Contains("empresas11B"))
            {
                filePathSocio = "socios11B";
            }
            else if (diretorioEmpresa.Contains("empresas11C"))
            {
                filePathSocio = "socios11C";
            }
            else if (diretorioEmpresa.Contains("empresas11D"))
            {
                filePathSocio = "socios11D";
            }
            else if (diretorioEmpresa.Contains("empresas11E"))
            {
                filePathSocio = "socios11E";
            }


            #endregion

            #region Empresa12
            else if (diretorioEmpresa.Contains("empresas12A"))
            {
                filePathSocio = "socios12A";
            }
            else if (diretorioEmpresa.Contains("empresas12B"))
            {
                filePathSocio = "socios12B";
            }
            else if (diretorioEmpresa.Contains("empresas12C"))
            {
                filePathSocio = "socios12C";
            }
            else if (diretorioEmpresa.Contains("empresas12D"))
            {
                filePathSocio = "socios12D";
            }
            else if (diretorioEmpresa.Contains("empresas12E"))
            {
                filePathSocio = "socios12E";
            }


            #endregion

            #region Empresa13
            else if (diretorioEmpresa.Contains("empresas13A"))
            {
                filePathSocio = "socios13A";
            }
            else if (diretorioEmpresa.Contains("empresas13B"))
            {
                filePathSocio = "socios13B";
            }
            else if (diretorioEmpresa.Contains("empresas13C"))
            {
                filePathSocio = "socios13C";
            }
            else if (diretorioEmpresa.Contains("empresas13D"))
            {
                filePathSocio = "socios13D";
            }
            else if (diretorioEmpresa.Contains("empresas13E"))
            {
                filePathSocio = "socios13E";
            }


            #endregion

            #region Empresa14
            else if (diretorioEmpresa.Contains("empresas14A"))
            {
                filePathSocio = "socios14A";
            }
            else if (diretorioEmpresa.Contains("empresas14B"))
            {
                filePathSocio = "socios14B";
            }
            else if (diretorioEmpresa.Contains("empresas14C"))
            {
                filePathSocio = "socios14C";
            }
            else if (diretorioEmpresa.Contains("empresas14D"))
            {
                filePathSocio = "socios14D";
            }
            else if (diretorioEmpresa.Contains("empresas14E"))
            {
                filePathSocio = "socios14E";
            }


            #endregion

            #region Empresa15
            else if (diretorioEmpresa.Contains("empresas15A"))
            {
                filePathSocio = "socios15A";
            }
            else if (diretorioEmpresa.Contains("empresas15B"))
            {
                filePathSocio = "socios15B";
            }
            else if (diretorioEmpresa.Contains("empresas15C"))
            {
                filePathSocio = "socios15C";
            }
            else if (diretorioEmpresa.Contains("empresas15D"))
            {
                filePathSocio = "socios15D";
            }
            else if (diretorioEmpresa.Contains("empresas15E"))
            {
                filePathSocio = "socios15E";
            }


            #endregion

            #region Empresa16
            else if (diretorioEmpresa.Contains("empresas16A"))
            {
                filePathSocio = "socios16A";
            }
            else if (diretorioEmpresa.Contains("empresas16B"))
            {
                filePathSocio = "socios16B";
            }
            else if (diretorioEmpresa.Contains("empresas16C"))
            {
                filePathSocio = "socios16C";
            }
            else if (diretorioEmpresa.Contains("empresas16D"))
            {
                filePathSocio = "socios16D";
            }
            else if (diretorioEmpresa.Contains("empresas16E"))
            {
                filePathSocio = "socios16E";
            }


            #endregion

            #region Empresa17
            else if (diretorioEmpresa.Contains("empresas17A"))
            {
                filePathSocio = "socios17A";
            }
            else if (diretorioEmpresa.Contains("empresas17B"))
            {
                filePathSocio = "socios17B";
            }
            else if (diretorioEmpresa.Contains("empresas17C"))
            {
                filePathSocio = "socios17C";
            }
            else if (diretorioEmpresa.Contains("empresas17D"))
            {
                filePathSocio = "socios17D";
            }
            else if (diretorioEmpresa.Contains("empresas17E"))
            {
                filePathSocio = "socios17E";
            }


            #endregion

            #region Empresa18
            else if (diretorioEmpresa.Contains("empresas18A"))
            {
                filePathSocio = "socios18A";
            }
            else if (diretorioEmpresa.Contains("empresas18B"))
            {
                filePathSocio = "socios18B";
            }
            else if (diretorioEmpresa.Contains("empresas18C"))
            {
                filePathSocio = "socios18C";
            }
            else if (diretorioEmpresa.Contains("empresas18D"))
            {
                filePathSocio = "socios18D";
            }
            else if (diretorioEmpresa.Contains("empresas18E"))
            {
                filePathSocio = "socios18E";
            }


            #endregion

            #region Empresa19
            else if (diretorioEmpresa.Contains("empresas19A"))
            {
                filePathSocio = "socios19A";
            }
            else if (diretorioEmpresa.Contains("empresas19B"))
            {
                filePathSocio = "socios19B";
            }
            else if (diretorioEmpresa.Contains("empresas19C"))
            {
                filePathSocio = "socios19C";
            }
            else if (diretorioEmpresa.Contains("empresas19D"))
            {
                filePathSocio = "socios19D";
            }
            else if (diretorioEmpresa.Contains("empresas19E"))
            {
                filePathSocio = "socios19E";
            }


            #endregion

            #region Empresa20
            else if (diretorioEmpresa.Contains("empresas20A"))
            {
                filePathSocio = "socios20A";
            }
            else if (diretorioEmpresa.Contains("empresas20B"))
            {
                filePathSocio = "socios20B";
            }
            else if (diretorioEmpresa.Contains("empresas20C"))
            {
                filePathSocio = "socios20C";
            }
            else if (diretorioEmpresa.Contains("empresas20D"))
            {
                filePathSocio = "socios20D";
            }
            else if (diretorioEmpresa.Contains("empresas20E"))
            {
                filePathSocio = "socios20E";
            }


            #endregion

            return filePathSocio;
        }
        #endregion


        public Empresa GetEmpresaContta(string diretorioEmpresa, string cnpj)
        {
            try
            {
                #region Diretorio SociosEmpresas
                var filePathSocio = GetDiretorioSocios(diretorioEmpresa, diretorioEmpresa);
                #endregion

                var cnpjFicial = cnpj.Substring(0, 6);

                IMongoCollection<Empresa> _DBContext = _database.GetCollection<Empresa>(diretorioEmpresa);
                IMongoCollection<Socio> _DBContextSocio = _database.GetCollection<Socio>(filePathSocio);

                CreateIndexOnCollection(_DBContext);
                CreateIndexOnCollection(_DBContextSocio);

                var empresaDb = _DBContext.Find(c => c.cnpj == cnpj).FirstOrDefault();
                var listSocios = _DBContextSocio.Find(c => c.cnpj == cnpj).ToList();
                if (Convert.ToInt32(empresaDb.matriz_filial) > 1)
                {
                    var lisFilias = _DBContext.Find(c => c.cnpj.Contains(cnpjFicial)).ToList();
                    if (lisFilias.Count > 0)
                        empresaDb.listFilias = lisFilias;
                }              

                if (listSocios.Count > 0)
                    empresaDb.listSocio = listSocios;
                

                return empresaDb;

            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        public async Task CreateIndexOnCollection(IMongoCollection<Empresa> _collection)
        {
            var cnpj = Builders<Empresa>.IndexKeys.Ascending(c => c.cnpj);
            await _collection.Indexes.CreateOneAsync(cnpj);
        }

        public async Task CreateIndexOnCollection(IMongoCollection<Socio> _collection)
        {
            var cnpj = Builders<Socio>.IndexKeys.Ascending(c => c.cnpj);
            await _collection.Indexes.CreateOneAsync(cnpj);
        }
    }
}
