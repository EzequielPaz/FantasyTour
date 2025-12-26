using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT.Utils
{
    public static class Enums
    {
        // 1. Enum para Duracion de los tours
        public enum DurationDays
        {
            HalfDay = 1,
            FullDay = 2
        }

        //2. Enum para Estado
        public enum State
        {
            Inactive = 0,
            Active = 1,
            Deleted = 2
        }   

        public enum Profile
        {
            Admin = 1,
            User = 2
        }

    }
}
