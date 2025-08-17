using ConttaComsumidor.Infra.Base;
using MongoDB.Driver;
using System;

namespace Contta.Inteligencia.Cnpj.Service
{
    public class DiretorioEmpresa
    { 
        public string GetDiretorioEmpresa(string cnpj)
        {
            #region Empresa01
            var filePath = "";
            var cnpjDto = Convert.ToDouble(cnpj);
            if (cnpjDto >= 191 && cnpjDto <= 284309000150)
                filePath = "Empresa01";
            else if (cnpjDto >= 00284310000185 && cnpjDto <= 702366000102)
                filePath = "Empresa02";
            else if (cnpjDto >= 702367000157 && cnpjDto <= 1173550000175)
                filePath = "Empresa03";
            else if (cnpjDto >= 1173551000110 && cnpjDto <= 1626397000194)
                filePath = "Empresa04";
            else if (cnpjDto >= 1626398000139 && cnpjDto <= 1888721000142)
                filePath = "Empresa05";

            #endregion

            #region Empresa02           
            else if (cnpjDto >= 01888722000197 && cnpjDto <= 02279379000146)
                filePath = "Empresa06";
            else if (cnpjDto >= 02279380000170 && cnpjDto <= 02670121000176)
                filePath = "Empresa07";
            else if (cnpjDto >= 02670122000110 && cnpjDto <= 03041623000362)
                filePath = "Empresa08";
            else if (cnpjDto >= 03041624000145 && cnpjDto <= 03425575000144)
                filePath = "Empresa09";
            else if (cnpjDto >= 03425576000199 && cnpjDto <= 03683872000270)
                filePath = "Empresa10";
            #endregion

            #region Empresa03          
            else if (cnpjDto >= 03683873000134 && cnpjDto <= 04059662000198)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa03\empresas3A.csv";
            else if (cnpjDto >= 04059663000132 && cnpjDto <= 04430180000100)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa03\empresas3B.csv";
            else if (cnpjDto >= 04430181000147 && cnpjDto <= 04796377000150)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa03\empresas3C.csv";
            else if (cnpjDto >= 04796378000102 && cnpjDto <= 05160888000143)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa03\empresas3D.csv";
            else if (cnpjDto >= 05160889000198 && cnpjDto <= 05431544000121)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa03\empresas3E.csv";
            #endregion

            #region Empresa04          
            else if (cnpjDto >= 05431545000176 && cnpjDto <= 05829961000127)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa04\empresas4A.csv";
            else if (cnpjDto >= 05829962000171 && cnpjDto <= 06220022000143)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa04\empresas4B.csv";
            else if (cnpjDto >= 06220023000198 && cnpjDto <= 06878944000142)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa04\empresas4C.csv";
            else if (cnpjDto >= 06878945000197 && cnpjDto <= 07257346000208)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa04\empresas4D.csv";
            else if (cnpjDto >= 07257347000163 && cnpjDto <= 07515551008626)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa04\empresas4E.csv";
            #endregion

            #region Empresa05          
            else if (cnpjDto >= 07515551008707 && cnpjDto <= 07871784000172)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa05\empresas5A.csv";
            else if (cnpjDto >= 07871785000117 && cnpjDto <= 08195244000189)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa05\empresas5B.csv";
            else if (cnpjDto >= 08195245000123 && cnpjDto <= 08518189000110)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa05\empresas5C.csv";
            else if (cnpjDto >= 08518190000145 && cnpjDto <= 08860465000124)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa05\empresas5D.csv";
            else if (cnpjDto >= 08860466000179 && cnpjDto <= 09100445000118)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa05\empresas5E.csv";
            #endregion

            #region Empresa06   
            else if (cnpjDto >= 09100446000162 && cnpjDto <= 09449071000140)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa06\empresas6A.csv";
            else if (cnpjDto >= 09449072000195 && cnpjDto <= 10042853000140)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa06\empresas6B.csv";
            else if (cnpjDto >= 10042854000194 && cnpjDto <= 10485932000125)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa06\empresas6C.csv";
            else if (cnpjDto >= 10485933000170 && cnpjDto <= 10834514000104)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa06\empresas6D.csv";
            else if (cnpjDto >= 10834515000140 && cnpjDto <= 11081231000193)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa06\empresas6E.csv";
            #endregion

            #region Empresa07  
            else if (cnpjDto >= 11081232000138 && cnpjDto <= 11479183000196)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa07\empresas7A.csv";
            else if (cnpjDto >= 11479184000130 && cnpjDto <= 11932947000157)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa07\empresas7B.csv";
            else if (cnpjDto >= 11932948000100 && cnpjDto <= 12402163000180)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa07\empresas7C.csv";
            else if (cnpjDto >= 12402164000124 && cnpjDto <= 12880168000118)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa07\empresas7D.csv";
            else if (cnpjDto >= 12880169000162 && cnpjDto <= 13195945000158)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa07\empresas7E.csv";
            #endregion

            #region Empresa08 
            else if (cnpjDto >= 13195946000100 && cnpjDto <= 13645974000174)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa08\empresas8A.csv";
            else if (cnpjDto >= 13645975000119 && cnpjDto <= 14131363000170)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa08\empresas8B.csv";
            else if (cnpjDto >= 14131364000115 && cnpjDto <= 14617879000120)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa08\empresas8C.csv";
            else if (cnpjDto >= 14617880000154 && cnpjDto <= 15106777000102)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa08\empresas8D.csv";
            else if (cnpjDto >= 15106778000157 && cnpjDto <= 15433109000270)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa08\empresas8E.csv";
            #endregion

            #region Empresa09
            else if (cnpjDto >= 15433109000351 && cnpjDto <= 16010793000160)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa09\empresas9A.csv";
            else if (cnpjDto >= 16010794000104 && cnpjDto <= 16708967000162)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa09\empresas9B.csv";
            else if (cnpjDto >= 16708967000243 && cnpjDto <= 17186223000198)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa09\empresas9C.csv";
            else if (cnpjDto >= 17186224000132 && cnpjDto <= 17638682000165)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa09\empresas9D.csv";
            else if (cnpjDto >= 17638683000100 && cnpjDto <= 17979042000119)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa09\empresas9E.csv";
            #endregion

            #region Empresa10
            else if (cnpjDto >= 17979043000163 && cnpjDto <= 18486622000137)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa10\empresas10A.csv";
            else if (cnpjDto >= 18486623000181 && cnpjDto <= 18991354000100)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa10\empresas10B.csv";
            else if (cnpjDto >= 18991355000155 && cnpjDto <= 19486465000122)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa10\empresas10C.csv";
            else if (cnpjDto >= 19486466000177 && cnpjDto <= 19994427000180)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa10\empresas10D.csv";
            else if (cnpjDto >= 19994428000125 && cnpjDto <= 20344294000186)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa10\empresas10E.csv";
            #endregion

            #region Empresa11
            else if (cnpjDto >= 20344295000120 && cnpjDto <= 20869668000187)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa11\empresas11A.csv";
            else if (cnpjDto >= 20869669000121 && cnpjDto <= 21378164000127)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa11\empresas11B.csv";
            else if (cnpjDto >= 21378165000171 && cnpjDto <= 21898335000149)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa11\empresas11C.csv";
            else if (cnpjDto >= 21898336000193 && cnpjDto <= 22422563000100)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa11\empresas11D.csv";
            else if (cnpjDto >= 22422564000155 && cnpjDto <= 22778973000199)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa11\empresas11E.csv";
            #endregion

            #region Empresa12
            else if (cnpjDto >= 22778974000133 && cnpjDto <= 23312737000145)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa12\empresas12A.csv";
            else if (cnpjDto >= 23312738000190 && cnpjDto <= 23836830000159)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa12\empresas12B.csv";
            else if (cnpjDto >= 23836831000101 && cnpjDto <= 24372819000148)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa12\empresas12C.csv";
            else if (cnpjDto >= 24372820000172 && cnpjDto <= 24911016000114)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa12\empresas12D.csv";
            else if (cnpjDto >= 24911017000169 && cnpjDto <= 25271174000110)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa12\empresas12E.csv";
            #endregion

            #region Empresa13
            else if (cnpjDto >= 25271175000164 && cnpjDto <= 26041657000190)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa13\empresas13A.csv";
            else if (cnpjDto >= 26041658000135 && cnpjDto <= 26564235000108)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa13\empresas13B.csv";
            else if (cnpjDto >= 26564236000144 && cnpjDto <= 27095114000119)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa13\empresas13C.csv";
            else if (cnpjDto >= 27095115000163 && cnpjDto <= 27626925000107)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa13\empresas13D.csv";
            else if (cnpjDto >= 27626926000143 && cnpjDto <= 27994611000159)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa13\empresas13E.csv";
            #endregion

            #region Empresa14
            else if (cnpjDto >= 27994612000101 && cnpjDto <= 28522189000100)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa14\empresas14A.csv";
            else if (cnpjDto >= 28522190000126 && cnpjDto <= 29066003000100)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa14\empresas14B.csv";
            else if (cnpjDto >= 29066004000154 && cnpjDto <= 29617058000160)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa14\empresas14C.csv";
            else if (cnpjDto >= 29617059000105 && cnpjDto <= 30158390000193)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa14\empresas14D.csv";
            else if (cnpjDto >= 30158391000138 && cnpjDto <= 30538885000148)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa14\empresas14E.csv";
            #endregion

            #region Empresa15
            else if (cnpjDto >= 30538886000192 && cnpjDto <= 31085764000150)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa15\empresas15A.csv";
            else if (cnpjDto >= 31085765000103 && cnpjDto <= 31648735000150)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa15\empresas15B.csv";
            else if (cnpjDto >= 31648736000102 && cnpjDto <= 32200867000187)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa15\empresas15C.csv";
            else if (cnpjDto >= 32200868000121 && cnpjDto <= 32768648000107)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa15\empresas15D.csv";
            else if (cnpjDto >= 32768649000143 && cnpjDto <= 33081047000186)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa15\empresas15E.csv";
            #endregion

            #region Empresa16
            else if (cnpjDto >= 33081048000120 && cnpjDto <= 33590294000108)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa16\empresas16A.csv";
            else if (cnpjDto >= 33590295000152 && cnpjDto <= 34066948000161)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa16\empresas16B.csv";
            else if (cnpjDto >= 34066949000106 && cnpjDto <= 34629206000105)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa16\empresas16C.csv";
            else if (cnpjDto >= 34629207000141 && cnpjDto <= 35198679000150)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa16\empresas16D.csv";
            else if (cnpjDto >= 35198680000184 && cnpjDto <= 35595108000159)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa16\empresas16E.csv";
            #endregion

            #region Empresa17
            else if (cnpjDto >= 35595109000101 && cnpjDto <= 36169260000132)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa17\empresas17A.csv";
            else if (cnpjDto >= 36169261000187 && cnpjDto <= 36756681000160)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa17\empresas17B.csv";
            else if (cnpjDto >= 36756682000104 && cnpjDto <= 37341008000121)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa17\empresas17C.csv";
            else if (cnpjDto >= 37341009000176 && cnpjDto <= 37914324000145)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa17\empresas17D.csv";
            else if (cnpjDto >= 37914325000190 && cnpjDto <= 38314744000153)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa17\empresas17E.csv";
            #endregion

            #region Empresa18
            else if (cnpjDto >= 38314745000106 && cnpjDto <= 39075593000352)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa18\empresas18A.csv";
            else if (cnpjDto >= 39075594000135 && cnpjDto <= 39696124000199)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa18\empresas18B.csv";
            else if (cnpjDto >= 39696125000133 && cnpjDto <= 44367522000363)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa18\empresas18C.csv";
            else if (cnpjDto >= 44367522000444 && cnpjDto <= 50535350000120)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa18\empresas18D.csv";
            else if (cnpjDto >= 50535376000179 && cnpjDto <= 54276936002112)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa18\empresas18E.csv";
            #endregion

            #region Empresa19
            else if (cnpjDto >= 54276944000115 && cnpjDto <= 59616789000152)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa19\empresas19A.csv";
            else if (cnpjDto >= 59616797000107 && cnpjDto <= 60746948184841)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa19\empresas19B.csv";
            else if (cnpjDto >= 60746948184922 && cnpjDto <= 61189288032897)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa19\empresas19C.csv";
            else if (cnpjDto >= 61189288032978 && cnpjDto <= 66045378000110)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa19\empresas19D.csv";
            else if (cnpjDto >= 66045386000166 && cnpjDto <= 70100045000113)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa19\empresas19E.csv";
            #endregion

            #region Empresa20
            else if (cnpjDto >= 70100052000115 && cnpjDto <= 76496199000403)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa20\empresas20A.csv";
            else if (cnpjDto >= 76496199000586 && cnpjDto <= 82088493000128)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa20\empresas20B.csv";
            else if (cnpjDto >= 82088501000136 && cnpjDto <= 88895768000111)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa20\empresas20C.csv";
            else if (cnpjDto >= 88895776000168 && cnpjDto <= 93353118000184)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa20\empresas20D.csv";
            else if (cnpjDto >= 93353126000120 && cnpjDto <= 99017782000139)
                filePath = @"C:\contta\DadosEmpresa\CNPJ-full\output\Empresa20\empresas20E.csv";
            #endregion

            return filePath;
        }
    }
}
