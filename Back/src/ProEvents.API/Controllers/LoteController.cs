using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEvents.Application.Dtos;
using ProEvents.Application.Interfaces;

namespace ProEvents.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoteController : ControllerBase
    {
        private readonly ILoteService _loteService;

        public LoteController(ILoteService loteService)
        {
            this._loteService = loteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLotes()
        {
            try
            {
                //Thread.Sleep(1000); // delay for spinner in frontend
                var Lotes = await _loteService.GetAllLotesAsync();

                if (Lotes == null)
                {
                    return NoContent();
                }

                return Ok(Lotes);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error in Get Lotes. Error: {e.Message}");
            }
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                Thread.Sleep(1000); // delay for spinner in frontend
                var Lote = await _loteService.GetLoteByIdAsync(id);

                if (Lote == null)
                {
                    return NoContent();
                }
                return Ok(Lote);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error in Get Lote By Id. Error: {e.Message}");
            }
        }

        [HttpGet("eventid/{eventId}")]
        public async Task<IActionResult> GetLotesByEventIdAsync(int eventId)
        {
            try
            {
                //Thread.Sleep(1000); // delay for spinner in frontend
                var Lotes = await _loteService.GetLotesByEventIdAsync(eventId);

                if (Lotes == null)
                {
                    return NoContent();
                }

                return Ok(Lotes);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error in Get Lotes. Error: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateLotes(int eventId, LoteDTO[] models)
        {
            try
            {
                var _lotes = await _loteService.AddOrUpdateLotes(eventId, models);

                if (_lotes == null)
                {
                    return NoContent();
                }
                return Ok(_lotes);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error in Save or Update Lotes. Error: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var wasDeleted = await _loteService.DeleteLote(id);

                if (wasDeleted)
                {
                    return Ok("Lote Deleted");
                }
                return BadRequest("Lote not Deleted.");
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error in Delete Lote. Error: {e.Message}");
            }
        }
    }
}
