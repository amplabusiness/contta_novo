using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Corporate.Contta.Schedule.Api.Controllers
{
    [Route("api/produtos")]
    public class ProdutoController : Controller
    {
        private IMediator _mediator;
        private IProductRepository _productRepository;
        public ProdutoController(IMediator mediator, IProductRepository productRepository)
        {
            _mediator = mediator;
            _productRepository = productRepository;
        }

        [Route("getall")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll(GetAllByProductIdCompanyRequest request)
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

        [Route("depara")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllDepara(GetAllByProductIdCompanyRequest request)
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

        [Route("reclassificacao")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(GetByIdProductRequest request)
        {
            try
            {
                if (request.EmpresaId == Guid.Empty || request.EmpresaId == null)
                    return BadRequest(new { message = "Dados de pesquisa inválidos." });
                var result = await _mediator.Send(request);
                if (result.Errors.Any())
                    return BadRequest(new { message = result.Errors });

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }


        [Route("getallncm")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllNcm(GetAllByProductNcmRequest request)
        {
            try
            {
                if (request.Id == Guid.Empty || request.Id == null)
                    return BadRequest(new { message = "Dados de pesquisa inválidos." });
                var result = await _mediator.Send(request);
                if (result.Errors.Any())
                    return BadRequest(new { message = result.Errors });

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        [Route("getallncmmono")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllNcmMono(GetAllByProductIdCompanyRequest request)
        {
            try
            {
                if (request.EmpresaId == Guid.Empty || request.EmpresaId == null)
                    return BadRequest(new { message = "Dados de pesquisa inválidos." });
                var result = await _mediator.Send(request);
                if (result.Errors.Any())
                    return BadRequest(new { message = result.Errors });

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        [Route("getbyid")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdReclassificacao(GetByIdProductRequest request)
        {
            try
            {
                if (request.Id == Guid.Empty || request.Id == null)
                    return BadRequest(new { message = "Dados de pesquisa inválidos." });
                var result = await _mediator.Send(request);
                if (result.Errors.Any())
                    return BadRequest(new { message = result.Errors });

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        [Route("codprod")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProd(GetCodProductRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest(new { message = "Dados de pesquisa inválidos." });
                var result = await _mediator.Send(request);
                if (result.Errors.Any())
                    return BadRequest(new { message = result.Errors });

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        [Route("getbyidnfe")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdNfe(GetByIdNfeProductRequest request)
        {
            try
            {
                if (request.NfeId == Guid.Empty || request.NfeId == null)
                    return BadRequest(new { message = "Dados de pesquisa inválidos." });
                var result = await _mediator.Send(request);
                if (result.Errors.Any())
                    return BadRequest(new { message = result.Errors });

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        //[Authorize]
        [Route("updateproduto")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduto([FromBody] UpdateProdutoFornecRequest updateProduto)
        {
            Response responseErro;

            try
            {
                if (updateProduto == null)
                {
                    responseErro = new Response();
                    responseErro.AddError("Operação inválida.");
                    return BadRequest(new { message = responseErro.Errors });
                }

                //var result = await _mediator.Send(updateProduto);

                var result = _productRepository.UpdateProdutoFornect(updateProduto.listaProdutos);

                return Ok(200);

            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }      

        //[Authorize]
        [Route("listupdateproduto")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListUpdateProduto([FromBody] UpdateProdutoRequest updateProduto, Guid empresaId)
        {
            Response responseErro;
            updateProduto.EmpresaId = empresaId;

            try
            {
                if (updateProduto.ListProdutos.Count == 0)
                {
                    responseErro = new Response();
                    responseErro.AddError("Operação inválida.");
                    return BadRequest(new { message = responseErro.Errors });
                }

                var result = _productRepository.Update(updateProduto.ListProdutos, empresaId);

                return Ok(200);

            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        //[Authorize]
        [Route("confirlistprod")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateListaProduto([FromBody] List<ImpostosProd> listaProd, Guid companyId, DateTime dhEm)
        {
            Response responseErro;          

            try
            {
                if (listaProd.Count == 0 || listaProd == null)
                {
                    responseErro = new Response();
                    responseErro.AddError("Operação inválida.");
                    return BadRequest(new { message = responseErro.Errors });
                }

                var result = await _productRepository.UpdateProd(listaProd, companyId, dhEm);

                if (result)
                {
                    return Ok(200);
                }
                else
                {
                    return BadRequest();
                }                

            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        //[Authorize]
        [Route("deparaprod")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDeparaProd([FromBody] UpdateProdutoRequest updateProduto, Guid empresaId)
        {
            Response responseErro;

            try
            {
                if (updateProduto == null)
                {
                    responseErro = new Response();
                    responseErro.AddError("Operação inválida.");
                    return BadRequest(new { message = responseErro.Errors });
                }

                var result = _productRepository.UpdateDepara(updateProduto.CodProFornecedor, updateProduto.CodProCliente, updateProduto.EmpresaId, updateProduto.Marca);

                if (!result.Result)
                {
                    return BadRequest("Não foi licalizado o produto na nossa base!,Tentar novamente");
                }

                return Ok(200);

            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }
        [Route("download")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Download(GetFileProductRequest request)
        {
            try
            {
                if (request.CompanyInformation == Guid.Empty)
                    return BadRequest(new { message = "Dados de pesquisa inválidos." });
                var result = await _productRepository.GetFileProduct(request.CompanyInformation);
                var sb = new StringBuilder();
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream, System.Text.Encoding.UTF8);
                sb.Append("Codigo" + ";");
                sb.Append("Descrição" + ";");
                sb.Append("Marca" + ";");
                sb.Append("Valor Unitario" + ";");
                sb.Append("Unidade Medida" + ";");
                sb.Append("Quantidade" + ";");
                sb.Append(System.Environment.NewLine);

                foreach (var tempResult in result)
                {
                    sb.Append(tempResult.CodProduto + ";");
                    sb.Append(tempResult.DescProduto + ";");
                    sb.Append("MARCA X" + ";");
                    sb.Append(tempResult.VlUnitario.ToString() + ";");
                    sb.Append(tempResult.Unidade + ";");
                    sb.Append(tempResult.Qtd + ";");
                    sb.Append(System.Environment.NewLine);
                }
                writer.Write(sb.ToString());
                writer.Flush();
                stream.Position = 0;
                return File(stream, "text/xlsx", $"Produtos - {DateTime.Now}.xlsx");
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        //[Authorize]
        [Route("updateListImpostos")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListUpdateProduto([FromBody] UpdateProdIcmsMonoRequest produtosImpostos)
        {
            Response responseErro;

            try
            {
                if (produtosImpostos.Count == 0)
                {
                    responseErro = new Response();
                    responseErro.AddError("Operação inválida.");
                    return BadRequest(new { message = responseErro.Errors });
                }

                var result = _mediator.Send(produtosImpostos);

                //var result = await _mediator.Send(updateProduto);

                return Ok(200);

            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }
    }
}
