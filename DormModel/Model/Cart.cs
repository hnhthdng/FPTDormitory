using DormModel.DTO.Room;
using DormModel.DTO.SideService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormModel.Model
{
    public class Cart
    {
        public List<RoomResponseDTO>? roomResponseDTOs {  get; set; }
        public List<SideServiceResponseDTO>? sideServiceResponseDTOs { get; set; }
    }
}
