using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsShared;
using API.Services;
using Microsoft.AspNetCore.OData.Query;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase where T : class
    {
        private readonly IDBService<T> _db;

        public BaseController(IDBService<T> db)
        {
            _db = db;
        }

        [HttpGet]
        [EnableQuery]//permite hacer filtros, joins,order by,groups, count,entre otros.Mediate OData
        public async Task<IActionResult> GetElementos()
        {
            return Ok(await _db.Get());
        }
        

        [HttpPatch("/update")]
        public async Task<IActionResult> PutElemento(
                                                    //entidad con las modificaciones
                                                    [FromBody] T entity)
        {
            

            return Ok(await _db.Patch(entity));
        }

      
        [HttpPost]
        public async Task<ActionResult<Elemento>> PostElemento(T entity)
        {
            return Ok(await _db.Post(entity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElemento(int id)
        {

            return NoContent();
        }
    }
}
