//using System;
//using System.Collections.Generic;
//using ConttaComsumidor.Models.NotaFiscalCanceladas;
//using ConttaComsumidor.Models.Speed;

//namespace ConttaComsumidor.Adapter
//{
//    public class EntidadeXmlToEntidadeMongodbModCanc
//    {
//        public static NfeCanceldas CretaEntidadeMongoNfeCanceladas(ProcEventoNFe procEventoNFe)
//        {
//            if (procEventoNFe == null)
//                throw new Exception("Emitente nao informado.");
//            if (string.IsNullOrEmpty(procEventoNFe.Evento.InfEvento.CNPJ))
//                throw new Exception("Cnpj do emitente nao informado.");

//            return new NfeCanceldas
//            {
//                Cnpj = procEventoNFe.Evento.InfEvento.CNPJ != null ? procEventoNFe.Evento.InfEvento.CNPJ : string.Empty,
//                CodBarra = procEventoNFe.Evento.InfEvento.ChNFe != null ? procEventoNFe.Evento.InfEvento.ChNFe : string.Empty,
//                DhEvento = Convert.ToDateTime(procEventoNFe.Evento.InfEvento.DhEvento),
//                TpEvento = procEventoNFe.Evento.InfEvento.TpEvento != null ? procEventoNFe.Evento.InfEvento.TpEvento : string.Empty,
//                DescEvento = procEventoNFe.Evento.InfEvento.DetEvento.DescEvento != null ? procEventoNFe.Evento.InfEvento.DetEvento.DescEvento : string.Empty,
//                NProt = procEventoNFe.Evento.InfEvento.NProt != null ? procEventoNFe.Evento.InfEvento.NProt : string.Empty,
//                Justificativa = procEventoNFe.Evento.InfEvento.DetEvento.XJust,
//                RefNfe = procEventoNFe.RetEvento.InfEvento.ChNFe,
//                XEvento = procEventoNFe.RetEvento.InfEvento.XEvento,
//                DhRegEvento = procEventoNFe.RetEvento.InfEvento.DhRegEvento,
//                ETipoNota = procEventoNFe.ETipoNota,
//                ModeloNota = procEventoNFe.ModeloNota,
//                CnpjEmitente = procEventoNFe.CnpjEmitente
//            };
//        }

//        private static DateTime? ExtraindoData(string data)
//        {
//            var dataConvertida = new DateTime();
//            if (!string.IsNullOrEmpty(data))
//            {
//                var result = DateTime.TryParse(data, out dataConvertida);
//                if (result)
//                    return dataConvertida;
//                else
//                    return null;
//            }
//            return null;
//        }
//    }
//}
