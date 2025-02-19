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

        [HttpGet("GetAll")]
        public virtual async Task<IActionResult> GetAll()
        {
            var res = await _service.GetAll();
            return Ok(res);
        }
    }
}
