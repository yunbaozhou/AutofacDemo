using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.AOP;

namespace IService
{
    public interface IHotelService
    {
        [Logger("ServiceModule", "hotel")]
        string GetHotelName(long hotelId);
    }
}
