using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Context;
using ModelsShared;
using API.Services;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ElementoesController : BaseController<Elemento>
    {
        public ElementoesController(IDBService<Elemento> db) : base(db)
        {
        }
    }
}
