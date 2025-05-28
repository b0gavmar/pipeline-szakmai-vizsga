using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PipeLine.Backend.Assemblers.BaseAssemblers;
using PipeLine.Backend.Repos.Base;
using PipeLine.Shared.Models;
using PipeLine.Shared.Responses;
using PipeLine.Backend.Assemblers;
using PipeLine.Backend.Repos.ChargingStationRepos.ChargingPortRepos;
using PipeLine.Backend.Services.Base;


namespace PipeLine.Backend.Controllers
{
    public abstract class BaseController<TModel, TDto> : ControllerBase
        where TModel : class, IDbEntity<TModel>, new()
        where TDto : class
    {
        protected readonly IBaseService<TModel, TDto> _service;

        public BaseController(IBaseService<TModel, TDto> service)
        {
            _service = service;
        }

        [HttpGet()]
        public virtual async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _service.GetByIdAsync(id);
                return entity == null ? NotFound(new Response($"No entity found with id = {id}")) : Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error: {ex.Message}"));
            }
        }

        [HttpPost()]
        public virtual async Task<IActionResult> CreateAsync([FromBody] TDto dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto);
                return result.HasError ? BadRequest(result) : Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new Response($"Error: {ex.Message}"));
            }
        }

        [HttpPut()]
        public virtual async Task<IActionResult> UpdateAsync([FromBody] TDto dto)
        {
            try
            {
                var result = await _service.UpdateAsync(dto);
                return result.HasError ? BadRequest(result) : Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new Response($"Error: {ex.Message}"));
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            try{
                var result = await _service.DeleteAsync(id);
                return result.HasError ? BadRequest(result) : Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new Response($"Error: {ex.Message}"));
            }
        }

    }
}
