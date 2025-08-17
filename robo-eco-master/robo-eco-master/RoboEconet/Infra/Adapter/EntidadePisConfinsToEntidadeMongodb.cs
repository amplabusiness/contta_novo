using RoboEconet.Models;
using System;
using System.Collections.Generic;

namespace RoboEconet.Infra.Adapter
{
    public class EntidadePisConfinsToEntidadeMongodb
    {
        public  List<PisConfinsDto> CretaEntidadeMongoPisConfins(List<PisConfins> listPisConfins,string ncmPai)
        {
            List<NCMDto> listNcm = new List<NCMDto>();
            List<ObservacaoDto> listObservacoes = new List<ObservacaoDto>();
            List<AliquotaPisConfinsDto> listAliquotas = new List<AliquotaPisConfinsDto>();
            List<CSTDto> listCst = new List<CSTDto>();
            List<EFDDto> listEfd = new List<EFDDto>();

            List<PisConfinsDto> lisitem = new List<PisConfinsDto>();

            foreach (var item in listPisConfins.FindAll(c => c.NCM !=null))
            {
                foreach (var ncm in item.NCMS)
                {
                    listNcm.Add(new NCMDto
                    {                        
                        Codigo = ncm.Codigo,
                        Descricao = ncm.Descricao
                    });
                }

                foreach (var aliquata in item.Aliquotas)
                {
                    listAliquotas.Add(new AliquotaPisConfinsDto
                    {
                        RegimeTributacao = aliquata.RegimeTributacao,
                        PIS = aliquata.PIS,
                        CONFINS = aliquata.CONFINS,
                        DispositivoLegal = aliquata.DispositivoLegal
                    });

                }

                foreach (var Observacoe in item.Observacoes)
                {
                    listObservacoes.Add(new ObservacaoDto
                    {
                        Descricao = Observacoe.Descricao
                    });

                }

                foreach (var cst in item.CSTS)
                {
                    listCst.Add(new CSTDto
                    {
                        Nome = cst.Nome,
                        Codigo = cst.Codigo,
                        Descricao = cst.Descricao

                    });
                }

                foreach (var Efds in item.EFDS)
                {
                    listEfd.Add(new EFDDto
                    {
                        Descricao = Efds.Descricao
                    });
                }

                lisitem.Add(new PisConfinsDto
                { 
                    Descricao = item.Descricao,
                    NCM = item.NCM,
                    NCMS = listNcm,
                    Aliquotas = listAliquotas,
                    CSTS = listCst,
                    Observacoes = listObservacoes,
                    EFDS = listEfd,
                    Monofasico = item.Monofasico,
                    NCMPai = ncmPai
                });

            }

            return lisitem;

        }
    }
}
