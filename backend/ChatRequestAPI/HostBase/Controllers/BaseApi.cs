using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace HostBase.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public abstract class BaseApi<TEntity> : ControllerBase
    {
        private readonly IBaseService<TEntity> _service;

        public BaseApi(IBaseService<TEntity> service)
        {
            _service = service;
        }

        [HttpPost()]
        public virtual async Task<IActionResult> Insert([FromBody]List<TEntity> model)
        {
            try
            {
                var res = await _service.Insert(model);
                return Ok(res);
            }
            catch (Exception ex) 
            {
                return Ok(false);
            }
        }

        [HttpPut("UpdateByID")]
        public virtual async Task<IActionResult> UpdateByID([FromBody] TEntity model, Guid ID)
        {
            try
            {
                var res = await _service.UpdateByID(model, ID);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
        }

        [HttpDelete("DeleteByID")]
        public virtual async Task<IActionResult> DeleteByID([FromBody] Guid ID)
        {
            try
            {
                var res = await _service.DeleteByID(ID);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
        }

        [HttpGet("GetAll")]
        public virtual async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _service.GetAll();
                return Ok(res);
            }
            catch (Exception ex) 
            {
                return Ok(ex);
            }
        }

    }
}
