using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain
{
    public class TomatoRating
    {
        public int Id { get; set; }
        public int TomatoMeter { get; set; }
        public int AudienceScore { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

    }
}
