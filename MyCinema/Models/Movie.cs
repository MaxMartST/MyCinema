﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyCinema.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name cannot be null or empty")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Language cannot be null or empty")]
        public string Language { get; set; }
        public string Duration { get; set; }
        public DateTime PlayingDate { get; set; }
        public DateTime PlayingTime { get; set; }
        public double TicketPrice { get; set; }
        [Required(ErrorMessage = "Rating cannot be null or empty")]
        public double Rating { get; set; }
        public string Genre { get; set; }
        public string TrageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
