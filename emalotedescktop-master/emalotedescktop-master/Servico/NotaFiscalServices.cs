using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmaloteContta.Adapter;
using EmaloteContta.Models.NotaFiscalEletronicaMod55;
using EmaloteContta.Models.Respositories;
using EmaloteContta.Models.Service;
using Microsoft.Practices.Unity;

namespace EmaloteContta.Servico
{
    public class NotaFiscalServices : INotaFiscalServices
    {
        private IEmpresaDestinatariaRepositorio _destinatariaRepositorio;
        private IEmpresaEmitenteRepositorio _emitenteRepositorio;
        private INotaFiscalRepositorio _notaRepositorio;
        private IImpostosRepositorio _impostoRepositorio;
        private IProdutosRepositorio _produtoRepositorio;

        public NotaFiscalServices()
        {
            _notaRepositorio = Nucleo.Container.Resolve<INotaFiscalRepositorio>();
            _emitenteRepositorio = Nucleo.Container.Resolve<IEmpresaEmitenteRepositorio>(); 
            _destinatariaRepositorio = Nucleo.Container.Resolve<IEmpresaDestinatariaRepositorio>();
            _impostoRepositorio = Nucleo.Container.Resolve<IImpostosRepositorio>();
            _produtoRepositorio = Nucleo.Container.Resolve<IProdutosRepositorio>();
        }
        public async Task<bool> GravarNota(NfeProc nota)
        {
            var nfe = EntidadeXmlToEntidadeMongodb.CreateEntidadeMongoNotaFiscal(nota);
            if (await _notaRepositorio.NotaJaFoiGravada(nfe.CodBarra))
                throw new Exception("Nota fiscal já cadastrada.");
            if (nfe == null)
                throw new Exception("Erro ao processar nota fiscal informada.");

            if (string.IsNullOrEmpty(nota.NFe.InfNFe.Emit.CNPJ))
                throw new Exception("Cnpj emitente nao informado.");


            var emit = await _emitenteRepositorio.ObterPorCnpj(nota.NFe.InfNFe.Emit.CNPJ.Replace(".", "").Replace("/", "").Replace("-", ""));
            var cpfCnpj = string.IsNullOrEmpty(nota.NFe.InfNFe.Dest.CNPJ) ? nota.NFe.InfNFe.Dest.CPF : nota.NFe.InfNFe.Dest.CNPJ;
            if (string.IsNullOrEmpty(cpfCnpj))
                throw new Exception("Cnpj destinatário nao informado.");
            var dest = await _destinatariaRepositorio.ObterPorCnpj(cpfCnpj.Replace(".", "").Replace("/", "").Replace("-", ""));

            if (emit == null)
            {
                var emitMongo = await _emitenteRepositorio.Add(EntidadeXmlToEntidadeMongodb.CretaEntidadeMongoEmpresaEmitente(nota.NFe.InfNFe.Emit));
                nfe.EmpresaEmetId = emitMongo.Id.Value;
                emit = emitMongo;
            }
            else { nfe.EmpresaEmetId = emit.Id.Value; }
            if (dest == null)
            {
                var destMongo = await _destinatariaRepositorio.Add(EntidadeXmlToEntidadeMongodb.CretaEntidadeMongoEmpresaDestinatario(nota.NFe.InfNFe.Dest));
                nfe.EmpresaDesId = destMongo.Id.Value;
            }
            else { nfe.EmpresaDesId = dest.Id.Value; }

            var nfeMongo = await _notaRepositorio.Add(nfe);

            nfeMongo.Produtos.ForEach(c =>
            {
                //c.EmpresaEmitId = emit.Id.Value;
                _produtoRepositorio.Add(c);
            });
            nfeMongo.Impostos.ForEach(c =>
            {
                _impostoRepositorio.Add(c);
            });
            return nfeMongo.Id != Guid.Empty;
        }
    }
}
