using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebSearcher.DataAccess.Abstract;
using WebSearcher.Entities;

namespace WebSearcher.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebPageConnectionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public WebPageConnectionsController(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWork = unitOfWorkFactory.Create();
        }

        [HttpGet]
        public IEnumerable<WebPageConnection> Get()
        {
            return _unitOfWork.WebPagesConnections.GetAll();
        }
    }
}
