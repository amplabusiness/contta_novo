using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Request.Impostos;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Api.Controllers
{
    [Route("api/impostos")]
    public class ImpostosController : Controller
    {
        private IMediator _mediator;
        private IImpostosProdutosRepository _impostosProdutosRepository;

        public ImpostosController(IMediator mediator, IImpostosProdutosRepository impostosProdutosRepository)
        {
            _mediator = mediator;
            _impostosProdutosRepository = impostosProdutosRepository;
        }

        [Route("getall")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll(GetAllProdutosImpostosRequest request)
        {
            try
            {
                if (request.EmpresaId == Guid.Empty || request.EmpresaId == null)
                    return BadRequest(new { message = "Dados de pesquisa inválidos." });
                var result = await _mediator.Send(request);

                if (result == null)
                {
                    return BadRequest(new { message = result.Errors });
                }
                else if ((result.Errors.Any()))
                {

                    return BadRequest(new { message = result.Errors });
                }

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        [Route("getallCfop")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCfopGeral()
        {
            try
            {
                var result = _impostosProdutosRepository.GetAllCfopGeral();

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        [Route("getallCfopsm")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCfop()
        {
            try
            {
                var result = _impostosProdutosRepository.GetAllCfop();

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        [Route("newCfop")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCfopNew([FromBody] TbCFOP tbCfop)
        {
            try
            {
                if (tbCfop.CFOP == 0)
                {
                    return BadRequest(new { message = $"Código CFOP invalido está passando o valor = {tbCfop.CFOP}" });
                }

                var result = _impostosProdutosRepository.NewCfop(tbCfop);

                if(result.Result == null)
                {
                    return BadRequest("Cfop já cadastrado na nossa base");
                }

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        [Route("deleteCfop")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCfopDelete(Guid CfopId)
        {
            try
            {
                var result = _impostosProdutosRepository.DeleteCfop(CfopId);

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        [Route("newantecipacao")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewAntecipacao([FromBody] NewImpostoAntecipacaoRequest newImpostoAnte)
        {
            Response responseError;
            try
            {
                if (newImpostoAnte == null)
                {
                    responseError = new Response();
                    responseError.AddError("Operação inválida.");
                    return BadRequest(new { message = responseError.Errors });
                }
                var result = await _mediator.Send(newImpostoAnte);

                return Ok(result);
            }
            catch (Exception ex)
            {
                responseError = new Response();
                responseError.AddError("Operação inválida.");
                return BadRequest(new { message = responseError.Errors, ex });
            }
        }


        [Route("newinsento")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewInsento([FromBody] NewImpostoInsentoRequest newImpostoInsento)
        {
            Response responseError;
            try
            {
                if (newImpostoInsento == null)
                {
                    responseError = new Response();
                    responseError.AddError("Operação inválida.");
                    return BadRequest(new { message = responseError.Errors });
                }
                var result = await _mediator.Send(newImpostoInsento);

                return Ok(result);
            }
            catch (Exception ex)
            {
                responseError = new Response();
                responseError.AddError("Operação inválida.");
                return BadRequest(new { message = responseError.Errors, ex });
            }
        }

        [Route("newimune")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewImune([FromBody] NewImpostoImuneRequest newImpostoImune)
        {
            Response responseError;
            try
            {
                if (newImpostoImune == null)
                {
                    responseError = new Response();
                    responseError.AddError("Operação inválida.");
                    return BadRequest(new { message = responseError.Errors });
                }
                var result = await _mediator.Send(newImpostoImune);

                return Ok(result);
            }
            catch (Exception ex)
            {
                responseError = new Response();
                responseError.AddError("Operação inválida.");
                return BadRequest(new { message = responseError.Errors, ex });
            }
        }

        [Route("newredcestbasic")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewRedCestaBasic([FromBody] NewImpostoRedCestabasicaRequest newImpostoRedCestBasic)
        {
            Response responseError;
            try
            {
                if (newImpostoRedCestBasic == null)
                {
                    responseError = new Response();
                    responseError.AddError("Operação inválida.");
                    return BadRequest(new { message = responseError.Errors });
                }
                var result = await _mediator.Send(newImpostoRedCestBasic);

                return Ok(result);
            }
            catch (Exception ex)
            {
                responseError = new Response();
                responseError.AddError("Operação inválida.");
                return BadRequest(new { message = responseError.Errors, ex });
            }
        }

        [Route("newexigibilidade")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewExigibilidade([FromBody] NewImpostoExigibilidadeRequest newImpostoExigibilidade)
        {
            Response responseError;
            try
            {
                if (newImpostoExigibilidade == null)
                {
                    responseError = new Response();
                    responseError.AddError("Operação inválida.");
                    return BadRequest(new { message = responseError.Errors });
                }
                var result = await _mediator.Send(newImpostoExigibilidade);

                return Ok(result);
            }
            catch (Exception ex)
            {
                responseError = new Response();
                responseError.AddError("Operação inválida.");
                return BadRequest(new { message = responseError.Errors, ex });
            }
        }

        [Route("newreducao")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewReducao([FromBody] NewImpostoReducaoRequest newImpostoReducao)
        {
            Response responseError;
            try
            {
                if (newImpostoReducao == null)
                {
                    responseError = new Response();
                    responseError.AddError("Operação inválida.");
                    return BadRequest(new { message = responseError.Errors });
                }
                var result = await _mediator.Send(newImpostoReducao);

                return Ok(result);
            }
            catch (Exception ex)
            {
                responseError = new Response();
                responseError.AddError("Operação inválida.");
                return BadRequest(new { message = responseError.Errors, ex });
            }
        }


        [Route("exigibilidade")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateExigibilidade([FromBody] UpdateExigibilidadeRequest newImpostoReducao)
        {
            Response responseError;
            try
            {
                if (newImpostoReducao == null)
                {
                    responseError = new Response();
                    responseError.AddError("Operação inválida.");
                    return BadRequest(new { message = responseError.Errors });
                }
                var result = await _mediator.Send(newImpostoReducao);

                return Ok(result);
            }
            catch (Exception ex)
            {
                responseError = new Response();
                responseError.AddError("Operação inválida.");
                return BadRequest(new { message = responseError.Errors, ex });
            }
        }

        [Route("AntecipEncerr")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCestaBasica([FromBody] UpdateImpIncerramentoRequest newImpostoReducao)
        {
            Response responseError;
            try
            {
                if (newImpostoReducao == null)
                {
                    responseError = new Response();
                    responseError.AddError("Operação inválida.");
                    return BadRequest(new { message = responseError.Errors });
                }
                var result = await _mediator.Send(newImpostoReducao);

                return Ok(result);
            }
            catch (Exception ex)
            {
                responseError = new Response();
                responseError.AddError("Operação inválida.");
                return BadRequest(new { message = responseError.Errors, ex });
            }
        }


        [Route("difal")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DownloadDifal(Guid EmpresaId, DateTime periudo)
        {
            try
            {
                if (EmpresaId == Guid.Empty)
                    return BadRequest(new { message = "Dados de pesquisa inválidos." });
                var result = await _impostosProdutosRepository.GetNfeDifal(EmpresaId, periudo);

                if(result.Count > 0)
                {
                    var totalNfe = result.Select(c => c.VtTotalNfe);
                    var valorDifal = totalNfe.Sum() * 7 / 100;

                    return Ok(valorDifal);
                }         

                //var TotalDifal = 0.0;

                //var sb = new StringBuilder();
                //MemoryStream stream = new MemoryStream();
                //StreamWriter writer = new StreamWriter(stream, System.Text.Encoding.UTF8);
                //sb.Append("Data da Entrada da Mercadoria" + ";");
                //sb.Append("CNPJ Remetente" + ";");
                //sb.Append("N° NFE-e" + ";");
                //sb.Append("CHAVE da NF-e" + ";");
                //sb.Append("Voper DIFAL" + ";");
                //sb.Append("Valor do Difal" + ";");               
                //sb.Append(System.Environment.NewLine);

                //foreach (var tempResult in result)
                //{
                //    var valorDifal = tempResult.VtTotalNfe * 7 / 100;
                //    TotalDifal = TotalDifal + valorDifal;

                //    sb.Append(tempResult.DhEmi.ToString() + ";");
                //    sb.Append(tempResult.CnpjFornec + ";");
                //    sb.Append(tempResult.Nnfe + ";");
                //    sb.Append(tempResult.CodBarra.ToString() + ";");
                //    sb.Append(tempResult.VtTotalNfe.ToString("n2") + ";");
                //    sb.Append(valorDifal.ToString("n2") + ";");
                //    sb.Append(System.Environment.NewLine);
                //}
                //sb.Append("1-Total DIFAL - Aquisiçôes" + ";" + TotalDifal );
                //writer.Write(sb.ToString());
                //writer.Flush();
                //stream.Position = 0;
                //return File(stream, "text/csv", $"Difal - {DateTime.Now}.csv");

                return Ok(result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

    }
}
