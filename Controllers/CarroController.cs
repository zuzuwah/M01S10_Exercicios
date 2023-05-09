using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExerSemana10.DTO;
using ExerSemana10.Model;
using Microsoft.AspNetCore.Mvc;

namespace ExerSemana10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarroController : ControllerBase
    {
        private readonly LocacaoContext locacaoContext1;
        private LocacaoContext locacaoContext;

        public CarroController (LocacaoContext locacaoContext)
        {
           this.locacaoContext = locacaoContext;
        }

    [HttpPost]
    public ActionResult<CarroCreateDTO> Post ([FromBody] CarroCreateDTO carroDTO)
    {
        //instanciando a model
        //passar meus parametros
        CarroModel carroModel = new CarroModel ();
        carroModel.DataLocacao = carroDTO.DataLocacao;
        carroModel.Nome = carroDTO.Nome;
        carroModel.MarcaId = carroDTO.MarcaId;

        //fazer validação (verificar se existe a marca no BD)
        var marcaModel = locacaoContext.marca.Find(carroDTO.MarcaId);
        if(marcaModel != null)
        {
            //add no DBset
            locacaoContext.carro.Add(carroModel);

             //salvar
            locacaoContext.SaveChanges();

            carroDTO.Id = carroModel.Id;
            return Ok (carroModel);
        }
        else
        {
            return BadRequest ("erro ao salvar o carro no Banco de Dados");
        }
    }
    }
}