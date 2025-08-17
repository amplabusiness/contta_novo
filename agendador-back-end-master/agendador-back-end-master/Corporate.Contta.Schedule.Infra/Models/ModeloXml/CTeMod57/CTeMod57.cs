using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Corporate.Contta.Schedule.Domain.Enum;

namespace ConttaComsumidor.Models.CTeMod57
{

	[XmlRoot(ElementName = "toma3", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class Toma3
	{
		[XmlElement(ElementName = "toma", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Toma;
	}

	[XmlRoot(ElementName = "ide", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class Ide
	{

		[XmlElement(ElementName = "cUF", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string CUF;

		[XmlElement(ElementName = "cCT", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string CCT;

		[XmlElement(ElementName = "CFOP", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string CFOP;

		[XmlElement(ElementName = "natOp", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string NatOp;

		[XmlElement(ElementName = "mod", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Mod;

		[XmlElement(ElementName = "serie", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Serie;

		[XmlElement(ElementName = "nCT", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string NCT;

		[XmlElement(ElementName = "dhEmi", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string DhEmi;

		[XmlElement(ElementName = "tpImp", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string TpImp;

		[XmlElement(ElementName = "tpEmis", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string TpEmis;

		[XmlElement(ElementName = "cDV", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string CDV;

		[XmlElement(ElementName = "tpAmb", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string TpAmb;

		[XmlElement(ElementName = "tpCTe", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string TpCTe;

		[XmlElement(ElementName = "procEmi", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string ProcEmi;

		[XmlElement(ElementName = "verProc", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string VerProc;

		[XmlElement(ElementName = "cMunEnv", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string CMunEnv;

		[XmlElement(ElementName = "xMunEnv", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XMunEnv;

		[XmlElement(ElementName = "UFEnv", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string UFEnv;

		[XmlElement(ElementName = "modal", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Modal;

		[XmlElement(ElementName = "tpServ", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string TpServ;

		[XmlElement(ElementName = "cMunIni", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string CMunIni;

		[XmlElement(ElementName = "xMunIni", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XMunIni;

		[XmlElement(ElementName = "UFIni", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string UFIni;

		[XmlElement(ElementName = "cMunFim", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string CMunFim;

		[XmlElement(ElementName = "xMunFim", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XMunFim;

		[XmlElement(ElementName = "UFFim", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string UFFim;

		[XmlElement(ElementName = "retira", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Retira;

		[XmlElement(ElementName = "indIEToma", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string IndIEToma;

		[XmlElement(ElementName = "toma3", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public Toma3 Toma3;
	}

	[XmlRoot(ElementName = "ObsCont", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class ObsCont
	{

		[XmlElement(ElementName = "xTexto", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XTexto;

		[XmlAttribute(AttributeName = "xCampo", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XCampo;



	}

	[XmlRoot(ElementName = "compl", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class Compl
	{

		[XmlElement(ElementName = "ObsCont", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public ObsCont ObsCont;
	}

	[XmlRoot(ElementName = "enderEmit", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class EnderEmit
	{

		[XmlElement(ElementName = "xLgr", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XLgr;

		[XmlElement(ElementName = "nro", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Nro;

		[XmlElement(ElementName = "xBairro", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XBairro;

		[XmlElement(ElementName = "cMun", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string CMun;

		[XmlElement(ElementName = "xMun", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XMun;

		[XmlElement(ElementName = "CEP", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string CEP;

		[XmlElement(ElementName = "UF", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string UF;

		[XmlElement(ElementName = "fone", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Fone;
	}

	[XmlRoot(ElementName = "emit", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class Emit
	{

		[XmlElement(ElementName = "CNPJ", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string CNPJ;

		[XmlElement(ElementName = "IE", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string IE;

		[XmlElement(ElementName = "xNome", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XNome;

		[XmlElement(ElementName = "xFant", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XFant;

		[XmlElement(ElementName = "enderEmit", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public EnderEmit EnderEmit;
	}

	[XmlRoot(ElementName = "enderReme", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class EnderReme
	{

		[XmlElement(ElementName = "xLgr", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XLgr;

		[XmlElement(ElementName = "nro", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Nro;

		[XmlElement(ElementName = "xBairro", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XBairro;

		[XmlElement(ElementName = "cMun", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string CMun;

		[XmlElement(ElementName = "xMun", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XMun;

		[XmlElement(ElementName = "CEP", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string CEP;

		[XmlElement(ElementName = "UF", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string UF;
	}

	[XmlRoot(ElementName = "rem", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class Rem
	{

		[XmlElement(ElementName = "CNPJ", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string CNPJ;

		[XmlElement(ElementName = "IE", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string IE;

		[XmlElement(ElementName = "xNome", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XNome;

		[XmlElement(ElementName = "fone", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Fone;

		[XmlElement(ElementName = "enderReme", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public EnderReme EnderReme;
	}

	[XmlRoot(ElementName = "enderDest", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class EnderDest
	{

		[XmlElement(ElementName = "xLgr", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XLgr;

		[XmlElement(ElementName = "nro", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Nro;

		[XmlElement(ElementName = "xBairro", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XBairro;

		[XmlElement(ElementName = "cMun", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string CMun;

		[XmlElement(ElementName = "xMun", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XMun;

		[XmlElement(ElementName = "CEP", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string CEP;

		[XmlElement(ElementName = "UF", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string UF;
	}

	[XmlRoot(ElementName = "dest", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class Dest
	{

		[XmlElement(ElementName = "CNPJ", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string CNPJ;

		[XmlElement(ElementName = "IE", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string IE;

		[XmlElement(ElementName = "xNome", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XNome;

		[XmlElement(ElementName = "fone", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Fone;

		[XmlElement(ElementName = "enderDest", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public EnderDest EnderDest;
	}

	[XmlRoot(ElementName = "Comp", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class Comp
	{

		[XmlElement(ElementName = "xNome", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XNome;

		[XmlElement(ElementName = "vComp", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string VComp;
	}

	[XmlRoot(ElementName = "vPrest", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class VPrest
	{

		[XmlElement(ElementName = "vTPrest", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string VTPrest;

		[XmlElement(ElementName = "vRec", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string VRec;

		[XmlElement(ElementName = "Comp", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public Comp Comp;
	}

	[XmlRoot(ElementName = "ICMS45", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class ICMS45
	{

		[XmlElement(ElementName = "CST", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string CST;
	}

	[XmlRoot(ElementName = "ICMS", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class ICMS
	{

		[XmlElement(ElementName = "ICMS45", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public ICMS45 ICMS45;
	}

	[XmlRoot(ElementName = "imp", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class Imp
	{

		[XmlElement(ElementName = "ICMS", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public ICMS ICMS;
	}

	[XmlRoot(ElementName = "infQ", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class InfQ
	{

		[XmlElement(ElementName = "cUnid", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string CUnid;

		[XmlElement(ElementName = "tpMed", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string TpMed;

		[XmlElement(ElementName = "qCarga", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string QCarga;
	}

	[XmlRoot(ElementName = "infCarga", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class InfCarga
	{

		[XmlElement(ElementName = "vCarga", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string VCarga;

		[XmlElement(ElementName = "proPred", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string ProPred;

		[XmlElement(ElementName = "infQ", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public List<InfQ> InfQ;
	}

	[XmlRoot(ElementName = "infNF", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class InfNF
	{

		[XmlElement(ElementName = "mod", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Mod;

		[XmlElement(ElementName = "serie", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Serie;

		[XmlElement(ElementName = "nDoc", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string NDoc;

		[XmlElement(ElementName = "dEmi", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string DEmi;

		[XmlElement(ElementName = "vBC", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string VBC;

		[XmlElement(ElementName = "vICMS", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string VICMS;

		[XmlElement(ElementName = "vBCST", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string VBCST;

		[XmlElement(ElementName = "vST", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string VST;

		[XmlElement(ElementName = "vProd", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string VProd;

		[XmlElement(ElementName = "vNF", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string VNF;

		[XmlElement(ElementName = "nCFOP", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string NCFOP;

		[XmlElement(ElementName = "nPeso", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string NPeso;
	}

	[XmlRoot(ElementName = "infDoc", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class InfDoc
	{

		[XmlElement(ElementName = "infNF", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public InfNF InfNF;
	}

	[XmlRoot(ElementName = "rodo", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class Rodo
	{

		[XmlElement(ElementName = "RNTRC", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string RNTRC;
	}

	[XmlRoot(ElementName = "infModal", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class InfModal
	{

		[XmlElement(ElementName = "rodo", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public Rodo Rodo;

		[XmlAttribute(AttributeName = "versaoModal", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string VersaoModal;


		public string Text;
	}

	[XmlRoot(ElementName = "infCTeNorm", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class InfCTeNorm
	{

		[XmlElement(ElementName = "infCarga", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public InfCarga InfCarga;

		[XmlElement(ElementName = "infDoc", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public InfDoc InfDoc;

		[XmlElement(ElementName = "infModal", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public InfModal InfModal;
	}

	[XmlRoot(ElementName = "infCte", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class InfCte
	{

		[XmlElement(ElementName = "ide", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public Ide Ide;

		[XmlElement(ElementName = "compl", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public Compl Compl;

		[XmlElement(ElementName = "emit", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public Emit Emit;

		[XmlElement(ElementName = "rem", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public Rem Rem;

		[XmlElement(ElementName = "dest", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public Dest Dest;

		[XmlElement(ElementName = "vPrest", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public VPrest VPrest;

		[XmlElement(ElementName = "imp", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public Imp Imp;

		[XmlElement(ElementName = "infCTeNorm", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public List<InfCTeNorm> InfCTeNorm;

		[XmlAttribute(AttributeName = "Id", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Id;

		[XmlAttribute(AttributeName = "versao", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Versao;

	}

	[XmlRoot(ElementName = "CTe", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class CTe
	{

		[XmlElement(ElementName = "infCte", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public InfCte InfCte;

		[XmlAttribute(AttributeName = "xmlns", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Xmlns;

	}

	[XmlRoot(ElementName = "infProt", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class InfProt
	{

		[XmlElement(ElementName = "tpAmb", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string TpAmb;

		[XmlElement(ElementName = "verAplic", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string VerAplic;

		[XmlElement(ElementName = "chCTe", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string ChCTe;

		[XmlElement(ElementName = "dhRecbto", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string DhRecbto;

		[XmlElement(ElementName = "nProt", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string NProt;

		[XmlElement(ElementName = "digVal", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string DigVal;

		[XmlElement(ElementName = "cStat", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string CStat;

		[XmlElement(ElementName = "xMotivo", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string XMotivo;

		[XmlAttribute(AttributeName = "Id", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Id;

	}

	[XmlRoot(ElementName = "protCTe", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class ProtCTe
	{

		[XmlElement(ElementName = "infProt", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public InfProt InfProt;

		[XmlAttribute(AttributeName = "versao", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Versao;

	}

	[XmlRoot(ElementName = "cteProc", Namespace = "http://www.portalfiscal.inf.br/cte")]
	public class CteProc
	{
		[XmlElement(ElementName = "CTe", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public CTe CTe;

		[XmlElement(ElementName = "protCTe", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public ProtCTe ProtCTe;

		[XmlAttribute(AttributeName = "xmlns", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Xmlns;

		[XmlAttribute(AttributeName = "versao", Namespace = "http://www.portalfiscal.inf.br/cte")]
		public string Versao;

		public string FileInfo { get; set; }
		public string FileName { get; set; }

		public ETipoNota ETipoNota { get; set; }
	}
}
