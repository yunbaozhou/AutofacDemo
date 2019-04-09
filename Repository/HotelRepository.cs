using IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class HotelRepository : IHotelRepository
    {
        public string GetHotelName(long hotelId)
        {
            return $"hotel name {hotelId} from hotelRepository";
        }
    }
}
