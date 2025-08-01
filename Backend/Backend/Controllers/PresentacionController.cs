using Backend.DTOs.PresentationDTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class PresentacionController : ControllerBase
        {
            private readonly IPresentationService _presentationService;
            public PresentacionController(IPresentationService presentationService)
            {
                _presentationService = presentationService;
            }
            // Primer Controller
            [HttpGet]
            public async Task<IActionResult> GetAllPresentations()
            {
                var presentations = await _presentationService.GetAllPresentationsAsync();
                return Ok(presentations);
            }
            // Segundo Controller
            [HttpGet("{id}")]
            public async Task<IActionResult> GetPresentationById(int id)
            {
                try
                {
                    var presentation = await _presentationService.GetPresentationByIdAsync(id);
                    return Ok(presentation);
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
            }
            // Tercer Controller
            [HttpPost]
            public async Task<IActionResult> CreatePresentation([FromBody] CreatePresentationDto createPresentationDto)
            {
                try
                {
                    var presentation = await _presentationService.CreatePresentationAsync(createPresentationDto);
                    return CreatedAtAction(nameof(GetPresentationById), new { id = presentation.IdPresentation }, presentation);
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            // Cuarto Controller
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdatePresentation(int id, [FromBody] CreatePresentationDto updatePresentationDto)
            {
                try
                {
                    var updatedPresentation = await _presentationService.UpdatePresentationAsync(id, updatePresentationDto);
                    return Ok(updatedPresentation);
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            // Quinto Controller
            [HttpPut("Deactivate/{id}")]
            public async Task<IActionResult> DeactivatePresentation(int id)
            {
                var result = await _presentationService.DeactivatePresentationAsync(id);
                if (result)
                {
                    return Ok(new { message = "Presentation deactivated successfully." });
                }
                else
                {
                    return NotFound(new { message = "Presentation not found or already deactivated." });
                }
            }
            // Sexto Controller
            [HttpPut("Activate/{id}")]
            public async Task<IActionResult> ActivatePresentation(int id)
            {
                var result = await _presentationService.ActivatePresentationAsync(id);
                if (result)
                {
                    return Ok(new { message = "Presentation activated successfully." });
                }
                else
                {
                    return NotFound(new { message = "Presentation not found or already active." });
                }
            }
        }
    }
