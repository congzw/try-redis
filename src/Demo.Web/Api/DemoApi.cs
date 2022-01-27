using System;
using System.Collections.Generic;
using Demo.Web.AppServices;
using Demo.Web.Common;
using Demo.Web.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Web.Api
{
    [Route("Api/Demo/[action]")]
    public class DemoApi : ControllerBase
    {
        private readonly IDemoAppService _appService;

        public DemoApi(IDemoAppService appService)
        {
            _appService = appService;
        }
        
        [HttpGet]
        public List<DemoItem> GetItems()
        {
            return _appService.GetAll();
        }
        
        [HttpGet]
        public DemoItem GetItem([FromQuery]string id)
        {
            return _appService.GetById(id);
        }
        
        [HttpPost]
        public MessageResult Save([FromBody] DemoItem item)
        {
            _appService.Save(item);
            var demoItems = _appService.GetAll();
            return MessageResult.Create("OK", true, demoItems);
        }

        [HttpGet]
        public MessageResult TestDi([FromServices] IServiceProvider sp)
        {
            var distributedCache = sp.GetService<IDistributedCache>();
            var demoDb = sp.GetService<DemoDb>();
            var demoAppService = sp.GetService<IDemoAppService>();
            var desc = ObjectDesc.GetObjectDesc(distributedCache, demoDb, demoAppService);
            return MessageResult.Create("OK", true, desc);
        }
    }
}
