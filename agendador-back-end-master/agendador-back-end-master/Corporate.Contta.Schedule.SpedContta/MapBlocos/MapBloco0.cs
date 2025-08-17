//using Contta.Common;
//using Contta.SpedFiscal;
//using MongoDB.Driver;
//using Corporate.Contta.Schedule.SpedContta.Base;
//using Corporate.Contta.Schedule.SpedContta.Data.Repository;
//using System;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Contta.Corporate.Contta.Schedule.SpedContta
//{
//    public class MapBloco0
//    {
//        private EmpresaEmitCollection DbEmpresaEmit = new EmpresaEmitCollection();
//        private EmpresaDestCollection DbEmpresaDest = new EmpresaDestCollection();
//        private NfeCollection DbNfe = new NfeCollection();

//        public async Task<string> GetBloco0()
//        {
//            IMongoClient mongoClient = new MongoClient("mongodb://localhost");
//            IMongoDatabase database = mongoClient.GetDatabase("ConttaSistemas");
//            IMongoCollection<Bloco0.Registro0000> tabela0000 = database.GetCollection<Bloco0.Registro0000>("registro0000");
//            IMongoCollection<Bloco0.Registro0001> tabela0001 = database.GetCollection<Bloco0.Registro0001>("registro0001");
//            IMongoCollection<Bloco0.Registro0005> tabela0005 = database.GetCollection<Bloco0.Registro0005>("registro0005");
//            IMongoCollection<Bloco0.Registro0015> tabela0015 = database.GetCollection<Bloco0.Registro0015>("registro0015");
//            IMongoCollection<Bloco0.Registro0100> tabela0100 = database.GetCollection<Bloco0.Registro0100>("registro0100");
//            IMongoCollection<Bloco0.Registro0150> tabela0150 = database.GetCollection<Bloco0.Registro0150>("registro0150");
//            IMongoCollection<Bloco0.Registro0175> tabela0175 = database.GetCollection<Bloco0.Registro0175>("registro0175");
//            IMongoCollection<Bloco0.Registro0190> tabela0190 = database.GetCollection<Bloco0.Registro0190>("registro0190");
//            IMongoCollection<Bloco0.Registro0200> tabela0200 = database.GetCollection<Bloco0.Registro0200>("registro0200");
//            IMongoCollection<Bloco0.Registro0205> tabela0205 = database.GetCollection<Bloco0.Registro0205>("registro0205");
//            IMongoCollection<Bloco0.Registro0206> tabela0206 = database.GetCollection<Bloco0.Registro0206>("registro0206");
//            IMongoCollection<Bloco0.Registro0210> tabela0210 = database.GetCollection<Bloco0.Registro0210>("registro0210");
//            IMongoCollection<Bloco0.Registro0220> tabela0220 = database.GetCollection<Bloco0.Registro0220>("registro0220");
//            IMongoCollection<Bloco0.Registro0300> tabela0300 = database.GetCollection<Bloco0.Registro0300>("registro0300");
//            IMongoCollection<Bloco0.Registro0305> tabela0305 = database.GetCollection<Bloco0.Registro0305>("registro0305");
//            IMongoCollection<Bloco0.Registro0400> tabela0400 = database.GetCollection<Bloco0.Registro0400>("registro0400");
//            IMongoCollection<Bloco0.Registro0450> tabela0450 = database.GetCollection<Bloco0.Registro0450>("registro0450");
//            IMongoCollection<Bloco0.Registro0460> tabela0460 = database.GetCollection<Bloco0.Registro0460>("registro0460");
//            IMongoCollection<Bloco0.Registro0500> tabela0500 = database.GetCollection<Bloco0.Registro0500>("registro0500");
//            IMongoCollection<Bloco0.Registro0600> tabela0600 = database.GetCollection<Bloco0.Registro0600>("registro0600");
//            IMongoCollection<Bloco0.Registro0990> tabela0990 = database.GetCollection<Bloco0.Registro0990>("registro0600");


//            REGISTRO - 0000 INDENTIFICAÇÃO
//            Bloco0.Registro0000 registro0000 = new Bloco0.Registro0000();
//            Bloco0.Registro0001 registro0001 = new Bloco0.Registro0001();
//            Bloco0.Registro0005 registro0005 = new Bloco0.Registro0005();
//            Bloco0.Registro0015 registro0015 = new Bloco0.Registro0015();
//            Bloco0.Registro0100 registro0100 = new Bloco0.Registro0100();

//            PARTICIPANTE
//            Bloco0.Registro0150 registro0150 = new Bloco0.Registro0150();
//            Bloco0.Registro0175 regitro0175 = new Bloco0.Registro0175();
//            Bloco0.Registro0190 registro0190 = new Bloco0.Registro0190();
//            Bloco0.Registro0200 registro0200 = new Bloco0.Registro0200();
//            Bloco0.Registro0205 registro0205 = new Bloco0.Registro0205();
//            Bloco0.Registro0206 registro0206 = new Bloco0.Registro0206();
//            Bloco0.Registro0210 registro0210 = new Bloco0.Registro0210();
//            Bloco0.Registro0220 registro0220 = new Bloco0.Registro0220();
//            Bloco0.Registro0300 registro0300 = new Bloco0.Registro0300();
//            Bloco0.Registro0305 registro0305 = new Bloco0.Registro0305();
//            Bloco0.Registro0400 registro0400 = new Bloco0.Registro0400();
//            Bloco0.Registro0450 registro0450 = new Bloco0.Registro0450();
//            Bloco0.Registro0460 registro0460 = new Bloco0.Registro0460();

//            BENS / COMPONENTES DO ATIVO IMOBILIZADO
//            Bloco0.Registro0500 registro0500 = new Bloco0.Registro0500();
//            Bloco0.Registro0600 registro0600 = new Bloco0.Registro0600();
//            Bloco0.Registro0990 registro0990 = new Bloco0.Registro0990();

//            var listaEmpresaEmitente = DbEmpresaEmit.GetAllEmpresaEmit();
//            var listaEmpresaDest = DbEmpresaDest.GetAllEmpresaDest();
//            var listaNotaFiscal = DbNfe.GetAllNfe();

//            / < summary >
//            / Cadstro de empresa emitente
//            foreach (var item in listaEmpresaEmitente)
//            {
//                var empresaDTO = tabela0000.Find(c => c.Cnpj == item.Cnpj).FirstOrDefault();
//                var CodigoAtividade = 0;

//                if (empresaDTO == null)
//                {
//                    var notafiscalDTo = listaNotaFiscal.Where(c => c.EmpresaEmetId == item.Id).FirstOrDefault();

//                    registro0000.Id = Guid.NewGuid();
//                    registro0000.CodVer = Convert.ToInt32(013 - 1.12);
//                    Esse campo e passado o valor fixo
//                    registro0000.CodFin = 0;
//                    registro0000.DtIni = DateTime.Now;
//                    registro0000.DtFin = DateTime.Now;
//                    registro0000.Nome = newCompany.Name;
//                    registro0000.Cnpj = newCompany.Cnpj;
//                    registro0000.Cpf = newCompany.Cnpj;//Ver esse campo para poder passar cpf
//                    registro0000.Uf = newCompany.Address.State;
//                    registro0000.Ie = newCompany.StateWriting;
//                    registro0000.Im = newCompany.MunicipalOffice;
//                    registro0000.SimpleNumber = newCompany.SimplesNacional.SimpleNumber;
//                    registro0000.CodMun = IbgeEnum.Goiânia; /*newCompany.Address.CityIbge;*/
//                    registro0000.IndPerfil = IndPerfilArquivo.A;
//                    registro0000.Suframa = newCompany.Sugrama;//Trazer essa informação do banco XML

//                    if (CodigoAtividade == 1)
//                    {
//                        registro0000.IndAtiv = IndTipoAtividade.IndustrialOuEquiparado;
//                    }
//                    else
//                    {
//                        registro0000.IndAtiv = IndTipoAtividade.Outros;
//                    }

//                    Enserir Dados
//                    tabela0000.InsertOne(registro0000);

//                    Get Id empresa cadastrada
//                    var EmpresaId = tabela0000.Find(c => c.Cnpj == item.Cnpj).FirstOrDefault();

//                    registro0001.IndMov = IndMovimento.BlocoComDados;
//                    registro0001.EmpresaID = item.Id;

//                    registro0005.Fantasia = newCompany.NameFantasy;
//                    registro0005.Cep = newCompany.Address.Zip;
//                    registro0005.End = newCompany.Address.Street;
//                    registro0005.Num = newCompany.Address.City;
//                    registro0005.Compl = newCompany.Address.Details;
//                    registro0005.Bairro = newCompany.Address.Neighborhood;
//                    registro0005.Fone = newCompany.Phone;
//                    registro0005.Fax = newCompany.Fax;
//                    registro0005.Email = newCompany.Email;
//                    registro0005.EmpresaID = EmpresaId.Id;

//                    registro0015.UfSt = newCompany.Address.State;
//                    registro0015.IeSt = "";//Ver que informação vai ser passada nesse campo
//                    registro0015.EmpresaID = EmpresaId.Id;

//                    Enserir Dados
//                    tabela0100.InsertOne(registro0100);
//                    tabela0001.InsertOne(registro0001);
//                    tabela0005.InsertOne(registro0005);
//                    tabela0015.InsertOne(registro0015);
//                }
//            }


//            //Dados da contabilidade vai ser passado pelo portal do cliente
//            registro0100.Nome = "";
//            registro0100.Cpf = "";
//            registro0100.Crc = ""; //INSCRIÇÃO DO CONTADOR
//            registro0100.Cep = "";
//            registro0100.End = "";
//            registro0100.Compl = "";
//            registro0100.Bairro = "";
//            registro0100.Fone = "";
//            registro0100.Fax = "";
//            registro0100.Email = "";
//            registro0100.CodMun = "";
//            registro0100.Num = "";
//            registro0100.Cnpj = "";
//            registro0100.EmpresaID = EmpresaId.Id;
//            /// <summary>
//            /// Cadstro de empresa destinatario
//            foreach (var item in listaEmpresaDest)
//            {
//                var notafiscalDTo = listaNotaFiscal.Where(c => c.EmpresaDesId == item.Id).FirstOrDefault();

//                if (notafiscalDTo == null)
//                {
//                    registro0150.CodPart = "";//Como obter o código do participante 
//                    registro0150.Nome = item.RazaoSocial;
//                    if (item.CodPais == "01058")
//                    {
//                        registro0150.CodPais = "01058";
//                    }
//                    else
//                    {
//                        registro0150.CodPais = "9999999";
//                    }

//                    registro0150.Cnpj = item.Cnpj;
//                    registro0150.Cpf = item.CPF;
//                    registro0150.Ie = item.IncrEstadual;
//                    registro0150.CodMun = item.Cidade;
//                    registro0150.Suframa = item.Sugrama;//Pegar dados do banco XML
//                    registro0150.End = item.Endereco;
//                    registro0150.Num = item.Numero;
//                    registro0150.Compl = item.Complemento;
//                    registro0150.Bairro = item.Bairro;
//                    registro0150.EmpresaId = notafiscalDTo.EmpresaEmetId;
//                    registro0150.NfeId = notafiscalDTo.Id;

//                    regitro0175.DtAlt = DateTime.Today;
//                    regitro0175.NrCampo = "";//Informação quando tiver alteração no sped
//                    regitro0175.ContAnt = "";//Informação quando tiver alteração no sped

//                    //Enserir Dados
//                    tabela0150.InsertOne(registro0150);
//                    tabela0175.InsertOne(regitro0175);
//                }
//            }

//            //Este Registro tem por objetivo descrever as unidades de medidas (UM) utilizadas no arquivo digital. 
//            //Não podem ser informados 2 (dois) ou mais registros com o mesmo código de UM. Somente devem constar as UM informadas em qualquer outro registro.
//            registro0190.Unid = "";
//            registro0190.Descr = "";

//            //Este registro tem por objetivo informar mercadorias, serviços, produtos ou quaisquer outros itens concernentes às transações fiscais e aos movimentos de estoques em processos produtivos,
//            //bem como os insumos. Quando ocorrer alteração somente na descrição do item, sem que haja descaracterização deste, ou seja, criação de um novo item, a alteração deve constar no registro 0205.
//            registro0200.CodItem = "";
//            registro0200.DescrItem = "";//Na arquitetura vai ter que montar uma lista de produtos?
//            registro0200.CodBarra = "";//Na arquitetura vai ter que montar uma lista de produtos?
//            //registro0200.CodAntItem = ""; campo somente de leitura ver que irfomação vai ser passada aki
//            registro0200.UnidInv = ""; //Unidade de medida utilizada na quantificação de estoques.
//            registro0200.TipoItem = ""; //Esse campo tem regras de negocio, para saber que tipo que e 00,01,02,03,04,05,06,07,08,09,10,99
//            registro0200.CodNcm = ""; //Código do NCM do produto em particular.
//            registro0200.ExIpi = ""; // Código EX, conforme a TIPI
//            registro0200.CodGen = ""; // Código do gênero do item
//            registro0200.CodLst = ""; // Verificar a lista do anexo I da lei Complementar federal n116/2003
//            registro0200.AliqIcms = 0; // Alíquota de ICMS aplicável ao item nas operações internas.
//            registro0200.Cest = ""; //Verificar Guia Prático EFD-ICMS/IPI versão 2.2.20 oque se refere a esse campo         

//            //Este registro tem por objetivo informar alterações ocorridas na descrição do produto ou quando ocorrer alteração na codificação do produto, desde que não o descaracterize ou haja modificação que o identifique como sendo novo produto.
//            //registro0200.  = //Verficar o porque esse campo e somente leitura
//            //ToDo:Esse campo se refere ao nome,código dos produtos anteriores dado ao mesmo.
//            registro0205.DescrAntItem = "";
//            registro0205.DtIni = DateTime.Now;//Verificar como vai ser passado esse valor "Historico do cliente"Crir uma logica.
//            registro0205.DtFin = DateTime.Now;//Verificar como vai ser passado esse valor "Historico do cliente"Crir uma logica.
//            registro0205.CodAntItem = "";//Verificar como vai ser passado esse valor "Historico do cliente"Crir uma logica.

//            //Este registro tem por objetivo informar o código correspondente ao produto constante na Tabela da Agência Nacional de Petróleo(ANP).
//            registro0206.CodComb = "";//Campo espesifico para empresa do ramo de combustivel em geral,ver regras.

//            //A partir de janeiro de 2018, a obrigatoriedade da apresentação deste registro ficará a critério de cada UF, caso exista produção e consumo nos Registros K230/K235 e K250/K255.
//            //https://www.valor.srv.br/guias/guiasIndex.php?idGuia=203
//            registro0210.CodItemComp = "";//Verificar como vai ser passado esse campo do código do insumo.
//            registro0210.QtdComp = 0;//quantidade de insumo,ver como vai ser a regar para entrar dados nesse campo.           
//            registro0210.Perda = 0;//perda da quantidade do insumo produsido,ver qual vai ser a regra para esse tipo de caso.

//            //Este registro tem por objetivo informar os fatores de conversão dos itens discriminados na Tabela de Identificação do Item (Produtos e Serviços)
//            //entre a unidade informada no registro 0200 e as unidades informadas nos registros dos documentos fiscais ou nos registros do controle da produção e do estoque - Bloco K.
//            //https://www.valor.srv.br/guias/guiasIndex.php?idGuia=12
//            registro0220.UnidConv = "";//Ver como vai se passado esse campo padrão para esse modelo de empresa.
//            registro0220.FatConv = 0;//Ver como vai ser passado esse valor para esse campo conforme regra estabelecida para esse caso.

//            //Este registro tem o objetivo de identificar e caracterizar todos os bens ou componentes arrolados no registro G125 do Bloco G e os bens em construção.
//            //O bem ou componente deverá ter código individualizado atribuído pelo contribuinte em seu controle patrimonial do ativo imobilizado e não poderá ser reutilizado, duplicado, atribuído a bens ou componentes diferentes.
//            registro0300.CodIndBem = "";
//            registro0300.IdentMerc = 0;
//            registro0300.DescrItem = "";
//            registro0300.CodCta = "";
//            registro0300.CodPrnc = "";
//            registro0300.NrParc = 0;//Ver que tipo de parcelas e necessarias nesse campo

//            //Este registro tem o objetivo de prestar informações sobre a utilização do bem, sendo obrigatório quando o conteúdo do campo IDENT_MERC do registro 0300 for igual a “1” (Bem).    
//            registro0305.CodCcus = "";//Código do centro de custo onde o bem está sendo ou será   utilizado 
//            registro0305.VidaUtil = 0;//Vida útil estimada do bem, em número de meses
//            registro0305.Func = "";

//            //Este registro tem por objetivo codificar os textos das diferentes naturezas da operação / prestações discriminadas nos documentos fiscais. Esta codificação e suas descrições são livremente criadas e mantidas pelo contribuinte.
//            registro0400.CodNat = "";
//            registro0400.DescrNat = "";

//            //Este registro tem por objetivo codificar todas as informações complementares dos documentos fiscais exigidas pela legislação fiscal. Estas informações constam no campo “Dados Adicionais” dos documentos fiscais.
//            registro0450.CodInf = "";
//            registro0450.Txt = "";

//            //Este registro é utilizado para informar anotações de escrituração determinadas pela legislação pertinente aos lançamentos fiscais,
//            //tais como: ajustes efetuados por diferimento parcial de imposto, antecipações, diferencial de alíquota e outros.
//            registro0460.CodObs = "";
//            registro0460.Txt = "";

//            //Este registro tem o objetivo de identificar as contas contábeis utilizadas pelo contribuinte informante em sua Contabilidade Geral, 
//            //relativas às contas referenciadas no registro 0300. Não podem ser informados dois ou mais registros com a mesma combinação de conteúdo nos campos DT_ALT e COD_CTA.
//            registro0500.DtAlt = DateTime.Now;
//            registro0500.CodNatCc = 0;
//            registro0500.IndCta = "";
//            registro0500.CodCta = "";
//            registro0500.Nivel = 0;// Nível da conta analítica/grupo de contas./ver que tipo de nivel 
//            registro0500.NomeCta = "";

//            //Este registro tem o objetivo de identificar os centros de custos referenciados no registro 0305 – Informação sobre utilização do bem.
//            registro0600.DtAlt = DateTime.Now;
//            registro0600.CodCcus = "";
//            registro0600.Ccus = "";

//            registro0990.QtdLin0 = 0;


//            tabela0190.InsertOne(registro0190);
//            tabela0200.InsertOne(registro0200);
//            tabela0205.InsertOne(registro0205);
//            tabela0206.InsertOne(registro0206);
//            tabela0210.InsertOne(registro0210);
//            tabela0220.InsertOne(registro0220);
//            tabela0305.InsertOne(registro0305);
//            tabela0400.InsertOne(registro0400);
//            tabela0450.InsertOne(registro0450);
//            tabela0460.InsertOne(registro0460);
//            tabela0300.InsertOne(registro0300);
//            tabela0500.InsertOne(registro0500);
//            tabela0600.InsertOne(registro0600);
//            tabela0990.InsertOne(registro0990);

//            return "Cadastro efetuado com sucesso!";
//        }
//    }
//}
