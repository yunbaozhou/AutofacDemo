using Autofac;
using Autofac.Integration.WebApi;
using CeeKee.PublicLib.Autofac;
using IService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutofacTest.Controllers
{
    public class ValuesController : ApiController
    {
        public IHotelService hotelService { get; set; }

        //public ValuesController(IHotelService service)
        //{
        //    hotelService = service;
        //}

        public ValuesController()
        {
            //hotelService = AutofacManagerUtils.GetInstance<IHotelService>("hotelservice");
            hotelService = AutofacManagerUtils.GetInstance<IHotelService>(HotelServiceType.HotelService);
            //hotelService = GlobalConfiguration.Configuration.DependencyResolver.GetRootLifetimeScope().ResolveNamed<IHotelService>("ahotelservice");
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2", hotelService.GetHotelName(123) };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
