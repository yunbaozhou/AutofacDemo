using Autofac.Extras.DynamicProxy2;
using IRepository;
using IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.AOP;

namespace Service
{
    [Intercept(typeof(GlobalInterceptor))]
    public class HotelService : IHotelService
    {
        public IHotelRepository HotelRepository { get; set; }

        //public HotelService(IHotelRepository repository)
        //{
        //    hotelRepository = repository;
        //}

        public string GetHotelName(long hotelId)
        {
            return HotelRepository.GetHotelName(hotelId);
        }
    }
}
