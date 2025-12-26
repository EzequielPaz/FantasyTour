using FT.BLL.DTOs.TourDtos;
using FT.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FT.Utilities.Request;
using FT.Domain.Utilities;

namespace FT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToursController : ControllerBase
    {
        private readonly TourService _tourService;

        public ToursController(TourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTours([FromQuery] ToursFilterRequest? filters = null)
        {
            var response = await _tourService.TourList(filters);
            return StatusCode(response.Code, response);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response), 200)]
        [ProducesResponseType(typeof(Response), 404)]
        public async Task<IActionResult> GetTourById(int id)
        {
            var response = await _tourService.GetTourByIdAsync(id);
            return StatusCode(response.Code, response);
        }



        [HttpPost]
        public async Task<IActionResult> CreateTour([FromBody] CreateTourDto dto)
        {
            var response = await _tourService.AddTour(dto);
            // 🔹 Devuelve el código interno (200, 400, etc.)
            return StatusCode(response.Code, response);

        }

        [HttpPut()]
        public async Task<IActionResult> UpdateTour(UpdateTourDto dto)
        {
            var response = await _tourService.UpdateTourAsync(dto);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response), 200)]
        [ProducesResponseType(typeof(Response), 404)]
        public async Task<IActionResult> DeleteTour(int id)
        {
            var response = await _tourService.RemoveTourAsync(id);
            return StatusCode(response.Code, response);
        }

    }
}
