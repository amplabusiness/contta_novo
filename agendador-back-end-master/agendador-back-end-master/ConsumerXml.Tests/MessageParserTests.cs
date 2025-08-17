using ConsumerXml;
using FluentAssertions;
using Xunit;

namespace ConsumerXml.Tests
{
    public class MessageParserTests
    {
        [Fact]
        public void ParseNfeProc_Should_Parse_Xml_NfeProc()
        {
            var xml = @"<nfeProc xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""4.00""><NFe><infNFe Id=""NFe123""><ide><dhEmi>2025-01-01T12:00:00-03:00</dhEmi></ide><emit><CNPJ>00000000000000</CNPJ></emit><dest><CPF>00000000000</CPF></dest><det><prod><vProd>10.00</vProd></prod></det><total><ICMSTot><vNF>10.00</vNF></ICMSTot></total></infNFe></NFe><protNFe><infProt><chNFe>CHAVE</chNFe></infProt></protNFe></nfeProc>";
            var result = MessageParser.ParseNfeProc(xml);
            result.Should().NotBeNull();
            result!.NFe.Should().NotBeNull();
            result.NFe.InfNFe.Id.Should().Be("NFe123");
            result.NFe.InfNFe.Total.ICMSTot.VNF.Should().Be(10.00m);
        }

        [Fact]
        public void ParseNfeProc_Should_Parse_Json_NfeProc()
        {
            var json = @"{ ""NFe"": { ""InfNFe"": { ""Id"": ""NFe999"", ""Total"": { ""ICMSTot"": { ""VNF"": 123.45 } } } } }";
            var result = MessageParser.ParseNfeProc(json);
            result.Should().NotBeNull();
            result!.NFe.Should().NotBeNull();
            result.NFe.InfNFe.Id.Should().Be("NFe999");
            result.NFe.InfNFe.Total.ICMSTot.VNF.Should().Be(123.45m);
        }
    }
}
