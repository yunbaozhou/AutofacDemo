using IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.AOP;

namespace Service
{
    [ManuallyRegister]
    public class AHotelService : IHotelService
    {
        public string GetHotelName(long hotelId)
        {
            return "HotelService2";
        }
    }
}
