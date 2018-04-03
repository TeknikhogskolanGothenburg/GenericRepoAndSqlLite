using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Domain;

namespace UI
{
    public class SingleObjectModifiaction
    {
        private static MoviesContext _context = new MoviesContext();
        public static void SelectUsingStoredProcedure()
        {
            string searchString = "hej";
            var movies = _context.Movies.FromSql("EXEC FilterMovieByTitlePart {0}", searchString).ToList();
            foreach (var movie in movies)
            {
                Console.WriteLine(movie.Title);
            }
        }

        public static void SelectRawSqlWithOrderingAndFilter()
        {
            var movies = _context.Movies.FromSql("SELECT * FROM Movies")
                .OrderByDescending(m => m.ReleaseDate)
                .Where(m => m.Title.StartsWith("Hej"))
                .ToList();
            foreach (var movie in movies)
            {
                Console.WriteLine(movie.Title);
            }
        }

        public static void AddActors()
        {
            Actor actor1 = new Actor();
            actor1.Name = "Anna Karenina";
            actor1.Birthday = new DateTime(1976, 6, 14);
            actor1.Nationality = "Danish";

            Actor actor2 = new Actor();
            actor2.Name = "Jean Paul Bumba";
            actor2.Birthday = new DateTime(1983, 4, 19);
            actor2.Nationality = "German";

            _context.Actors.AddRange(actor1, actor2);
            _context.SaveChanges();
        }

        public static void SelectRawSql()
        {
            string sql = "SELECT * FROM Movies";
            var movies = _context.Movies.FromSql(sql).ToList();
            foreach (var movie in movies)
            {
                Console.WriteLine(movie.Title);
            }
        }

        public static void DeleteManyDisconnected()
        {
            string titleStart = "Fint";
            var movies = _context.Movies.Where(m => m.Title.StartsWith(titleStart)).ToList();

            //Här tänker vi att vi inte längre har kvar orginal contexten
            var newContext = new MoviesContext();
            newContext.Movies.RemoveRange(movies);
            newContext.SaveChanges();
        }

        public static void DeleteMany()
        {
            string titleStart = "En het";
            var movies = _context.Movies.Where(m => m.Title.StartsWith(titleStart)).ToList();
            _context.Movies.RemoveRange(movies);
            _context.SaveChanges();
        }

        public static void DeleteOne()
        {
            var movie = _context.Movies.Find(3);
            // var movie2 = new Movie { Id = 99, Title = "kjshdf", ReleaseDate = DateTime.Now};
            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }

        public static void UpdateDisconnected()
        {
            var movie = _context.Movies.Find(3);
            movie.ReleaseDate = new DateTime(1992, 10, 14);

            //Här tänker vi att vi inte längre har kvar orginal contexten

            var newContext = new MoviesContext();
            newContext.Movies.Update(movie);
            newContext.SaveChanges();

        }

        public static void Update()
        {
            string titleStart = "En het";
            var movies = _context.Movies.Where(m => m.Title.StartsWith(titleStart)).ToList();
            movies.ForEach(m => m.ReleaseDate = new DateTime(1983, 12, 24));
            _context.Movies.Add(new Movie { Title = "Smart som får", ReleaseDate = DateTime.Now });
            _context.SaveChanges();
        }

        public static void Find()
        {
            var movie1 = _context.Movies.FirstOrDefault(m => m.Id == 2);
            var movie2 = _context.Movies.Find(2);
            Console.WriteLine(movie1.Title);
            Console.WriteLine(movie2.Title);
        }

        public static void GetFirst()
        {
            string titleStart = "Kul";
            //var movie = (from m in _context.Movies where m.Title.StartsWith(titleStart) select m).FirstOrDefault();
            var movie = _context.Movies.FirstOrDefault(m => m.Title.StartsWith("En het"));
            Console.WriteLine(movie.Title);
        }

        public static void GetAllMovies()
        {
            var movies1 = _context.Movies.ToList();
            //SELECT m.title FROM m movies
            var movies2 = (from m in _context.Movies select m.Title).ToList();
            var movies3 = _context.Movies.Where(m => m.Title.StartsWith("En het")).ToList();
            string startTitle = "En het";
            var movies4 = (from m in _context.Movies where m.Title.StartsWith(startTitle) select m).ToList();
            // SELECT * FROM movies
            foreach (var movie in _context.Movies)
            {
                Console.WriteLine(movie.Title);
            }
        }

        public static void AddMovies()
        {
            Movie myMovie1 = new Movie { Title = "Hej hej Karlsson", ReleaseDate = DateTime.Now };
            Movie myMovie2 = new Movie { Title = "Fint Folk i Finkan 3", ReleaseDate = DateTime.Now };
            List<Movie> myMovies = new List<Movie> { myMovie1, myMovie2 };
            _context.Movies.AddRange(myMovies);
            _context.SaveChanges();
        }

        public static void AddMovie()
        {
            Movie myMovie = new Movie();
            myMovie.Title = "En het potatis";
            myMovie.ReleaseDate = System.DateTime.Now;

            _context.Movies.Add(myMovie);
            _context.SaveChanges();
        }

    }
}
