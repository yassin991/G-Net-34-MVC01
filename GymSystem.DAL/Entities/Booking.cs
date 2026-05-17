using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Entities
{
    public class Booking :BaseEntity
    {
        public Member Member { get; set; } = null!;

        int MemberId { get; set; }

        Session Session { get; set; } = null!;

        int SessionId { get; set; }

        bool IsAttended { get; set; }

    }
}
