//using System;
//using ConttaComsumidor.Models.NotaFiscalDeServico;
//using ConttaComsumidor.Models.Speed;
//using ConttaComsumidor.Models.Speed.EmpresaServico;

//namespace ConttaComsumidor.Adapter
//{
//    public class EntidadeXmlToEntidadeMongodbModServico
//    {
//        public static EmpresaEmit CretaEntidadeMongoEmpresaEmitente(Prestador prestador)
//        {
//            if (prestador == null)  
//                throw new Exception("Emitente nao informado.");
//            if (string.IsNullOrEmpty(prestador.CpfCnpj.Cnpj))
//                throw new Exception("Cnpj do emitente nao informado.");

//            return new EmpresaEmit
//            {
//                Cnpj = prestador.CpfCnpj.Cnpj,
//                InscricaoMunicipal = prestador.InscricaoMunicipal

//            };
//        }

//        public static EmpresaDest CretaEntidadeMongoEmpresaDestinatario(Tomador tomador)
//        {
//            if (tomador == null)
//                throw new Exception("Destinatário nao informado.");
//            if (string.IsNullOrEmpty(tomador.IdentificacaoTomador.CpfCnpj.Cnpj) && string.IsNullOrEmpty(tomador.IdentificacaoTomador.CpfCnpj.Cnpj))
//                throw new Exception("Cnpj ou cpf do destinatário nao informado.");

//            return new EmpresaDest
//            {
//                Cnpj = tomador.IdentificacaoTomador.CpfCnpj.Cnpj,
//                RazaoSocial = tomador.RazaoSocial,
//                Endereco = tomador.Endereco.Enderecos,
//                Numero = tomador.Endereco.Numero,
//                Complemento = tomador.Endereco.Complemento,
//                Bairro = tomador.Endereco.Bairro,
//                CodigoMunicipio = tomador.Endereco.CodigoMunicipio,
//                Uf = tomador.Endereco.Uf,
//                Cep = tomador.Endereco.Cep

//            };
//        }

//        public static ServicoEntity CreateEntidadeMongoNotaFiscal(Models.NotaFiscalDeServico.GerarNfseResposta service)
//        {
//            if (service.ListaNfse.CompNfse.Nfse.InfNfse == null
//                )
//                throw new Exception("Erro no processamento do xml");

//            var servico = new ServicoEntity
//            {
//                CodigoVerificacao = service.ListaNfse.CompNfse.Nfse.InfNfse.CodigoVerificacao,
//                DataEmissao = Convert.ToDateTime(service.ListaNfse.CompNfse.Nfse.InfNfse.DataEmissao),
//                ValorServicos = service.ListaNfse.CompNfse.Nfse.InfNfse.DeclaracaoPrestacaoServico.InfDeclaracaoPrestacaoServico.Servico.Valores.ValorServicos,
//                ValorDeducoes = service.ListaNfse.CompNfse.Nfse.InfNfse.DeclaracaoPrestacaoServico.InfDeclaracaoPrestacaoServico.Servico.Valores.ValorDeducoes,
//                ValorPis = service.ListaNfse.CompNfse.Nfse.InfNfse.DeclaracaoPrestacaoServico.InfDeclaracaoPrestacaoServico.Servico.Valores.ValorPis,
//                ValorCofins = service.ListaNfse.CompNfse.Nfse.InfNfse.DeclaracaoPrestacaoServico.InfDeclaracaoPrestacaoServico.Servico.Valores.ValorCofins,
//                ValorInss = service.ListaNfse.CompNfse.Nfse.InfNfse.DeclaracaoPrestacaoServico.InfDeclaracaoPrestacaoServico.Servico.Valores.ValorInss,
//                ValorIr = service.ListaNfse.CompNfse.Nfse.InfNfse.DeclaracaoPrestacaoServico.InfDeclaracaoPrestacaoServico.Servico.Valores.ValorIr,
//                ValorCsll = service.ListaNfse.CompNfse.Nfse.InfNfse.DeclaracaoPrestacaoServico.InfDeclaracaoPrestacaoServico.Servico.Valores.ValorCsll,
//                ValorIss = service.ListaNfse.CompNfse.Nfse.InfNfse.DeclaracaoPrestacaoServico.InfDeclaracaoPrestacaoServico.Servico.Valores.ValorIss,
//                Aliquota = service.ListaNfse.CompNfse.Nfse.InfNfse.DeclaracaoPrestacaoServico.InfDeclaracaoPrestacaoServico.Servico.Valores.Aliquota,
//                DescontoIncondicionado = service.ListaNfse.CompNfse.Nfse.InfNfse.DeclaracaoPrestacaoServico.InfDeclaracaoPrestacaoServico.Servico.Valores.DescontoIncondicionado,
//                CodigoTributacaoMunicipio = service.ListaNfse.CompNfse.Nfse.InfNfse.DeclaracaoPrestacaoServico.InfDeclaracaoPrestacaoServico.Servico.CodigoTributacaoMunicipio,
//                Discriminacao = service.ListaNfse.CompNfse.Nfse.InfNfse.DeclaracaoPrestacaoServico.InfDeclaracaoPrestacaoServico.Servico.Discriminacao,
//                CodigoMunicipio = service.ListaNfse.CompNfse.Nfse.InfNfse.DeclaracaoPrestacaoServico.InfDeclaracaoPrestacaoServico.Servico.CodigoMunicipio,
//                MunicipioIncidencia = service.ListaNfse.CompNfse.Nfse.InfNfse.DeclaracaoPrestacaoServico.InfDeclaracaoPrestacaoServico.Servico.MunicipioIncidencia,
//                ETipoNota = service.ETipoNota,
//                ModeloNota = service.ModeloNota,
//                CnpjEmitente = service.CnpjEmitente


//            };
//            return servico;
//        }

//    }
       
//}

