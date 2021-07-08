﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCinema.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public string ReservatioTime { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
    }
}
