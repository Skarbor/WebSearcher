using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebSearcher.DataAccess.Abstract;
using WebSearcher.Entities;

namespace WebSearcher.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebPagesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public WebPagesController(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWork = unitOfWorkFactory.Create();
        }

        [HttpGet]
        public IEnumerable<WebPage> Get()
        {
            return _unitOfWork.WebPages.GetAll();
        }
    }
}
