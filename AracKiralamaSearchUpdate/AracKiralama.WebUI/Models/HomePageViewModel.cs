﻿using AracKiralama.Entities;

namespace AracKiralama.WebUI.Models
{
    public class HomePageViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Arac> Araclar { get; set; }
    }
}