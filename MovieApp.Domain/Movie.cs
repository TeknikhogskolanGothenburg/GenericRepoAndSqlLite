﻿using System;
using System.Collections.Generic;

namespace MovieApp.Domain
{
    public class Movie
    {
        public Movie()
        {
            Actors = new List<MovieActor>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<MovieActor> Actors { get; set; }
        public TomatoRating Tomato { get; set; }
    }
}
