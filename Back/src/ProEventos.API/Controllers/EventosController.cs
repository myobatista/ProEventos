using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Persistence.Contexto;
using ProEventos.Domain;
using ProEventos.Application.Contratos;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService eventoService;

        public EventosController(IEventoService eventoService)
        {
            this.eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await this.eventoService.GetAllEventosAsync(true);
                if(eventos == null) return NotFound("Nenhum Evento encontrado!");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar Eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await this.eventoService.GetEventoByIdAsync(id, true);
                if(evento == null) return NotFound("Evento por Id não encontrado!");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar o Evento. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var evento = await this.eventoService.GetEventoByTemaAsync(tema, true);
                if(evento == null) return NotFound("Eventos por Tema não encontrado!");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar o Evento. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var evento = await this.eventoService.AddEvento(model);
                if(evento == null) return BadRequest("Erro ao tentar adicionar Evento!");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar o Evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await this.eventoService.UpdateEvento(id, model);
                if(evento == null) return BadRequest("Erro ao tentar atualizar Evento!");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar o Evento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if(await this.eventoService.DeleteEvento(id))
                    return Ok("Evento deletado");
                else
                    return BadRequest("Erro ao tentar excluir Evento!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar o Evento. Erro: {ex.Message}");
            }
        }
    }
}
