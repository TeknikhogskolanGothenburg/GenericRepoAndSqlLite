using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Domain;

namespace UI
{
    class Program
    {
       
        static void Main(string[] args)
        {
            var movieRepo = new MovieRepsitory();
            var movie = new Movie { Title = "Hej och hå i skärgården", ReleaseDate = new DateTime(1938, 10, 4) };
            movieRepo.Add(movie);
            movieRepo.Save();
            //Erat projekt skall:
            //* Lägga till poster
            //* Selektera poster på något villkor
            //* Updatera poster
            //* Radera poster

            //* Med kod skapa 1-* och *-* relationer
            //* Selektera poster med många til mångaberoende
            //* Selektera poster med 1-* beroende


            //Bonus om ni vill, använd raw sql på något ställe

            //AddMovie();
            //AddMovies();
            //GetAllMovies();
            //GetFirst();
            //Find();
            //Update();
            //UpdateDisconnected();
            //DeleteOne();
            //DeleteMany();
            //DeleteManyDisconnected();
            //SelectRawSql();
            //SelectRawSqlWithOrderingAndFilter();
            //SingleObjectModifiaction.SelectUsingStoredProcedure();

            //SingleObjectModifiaction.AddActors();
            //AddActorsToMovie();
            //DisplayMoviesEagerLoad();
            //AddManyToManyObject();
            //AddQuotesToActor();
            //AddTomato();
            //ProjectionLoading();
            //ProjectionLoading2();

            //var result = AsyncSelect();
            //Console.WriteLine("Waiting for result");
            //foreach(var movie in result.Result)
            //{
            //    Console.WriteLine(movie.Title);
            //    foreach(var actor in movie.Actors)
            //    {
            //        Console.WriteLine("\t" + actor.Actor.Name);
            //    }
            //}

 
            //var movie = movieRepo.FindBy(m => m.Id == 8).FirstOrDefault();
            //Console.WriteLine(movie.Title);
            //foreach(var movie in movies)
            //{
            //    Console.WriteLine(movie.Title);
            //}

            //var movies = movieRepo.GetAllAsync();
            //Console.WriteLine("Waiting for movies");
            //foreach(var movie in movies.Result)
            //{
            //    Console.WriteLine(movie.Title);
            //}

            //var movies = movieRepo.GetAll();
            //foreach(var movie in movies)
            //{
            //    Console.WriteLine(movie.Title);
            //}

            //var actorRepo = new ActorRepisitory();
            //var actors = actorRepo.GetAll();
            //foreach (var actor in actors)
            //{
            //    Console.WriteLine(actor.Name);
            //}
        }

        private static async Task<List<Movie>> AsyncSelect()
        {
            var context = new MoviesContext();

            var result = await context.Movies
                .Include(m => m.Actors)
                .ThenInclude(ma => ma.Actor)
                .ToListAsync();
            Console.WriteLine("Got it");
            return result;
        }

        private static void ProjectionLoading2()
        {
            var context = new MoviesContext();
            var projectedActor = context.Actors.Select(a =>
                new { a.Name, a.Nationality })
                .ToList();

            projectedActor.ForEach(pa => Console.WriteLine(pa.Name + " is " + pa.Nationality));
        }

        private static void ProjectionLoading()
        {
            var context = new MoviesContext();
            var projectedActor = context.Actors.Select(a =>
                new { a.Name, QuoteCount = a.Quotes.Count })
                .Where(a => a.QuoteCount > 0)
                .ToList();

            projectedActor.ForEach(pa => Console.WriteLine(pa.Name + " has " + pa.QuoteCount + " quotes"));
        }

        private static void AddTomato()
        {
            var context = new MoviesContext();
            var movie = context.Movies.Find(6);
            var tomato = new TomatoRating { MovieId = movie.Id, TomatoMeter = 74, AudienceScore = 95 };
            context.Add(tomato);
            context.SaveChanges();
            
        }

        private static void AddQuotesToActor()
        {
            var context = new MoviesContext();
            var actor = context.Actors.FirstOrDefault(a => a.Name.StartsWith("Engelbert"));
            if(null != actor)
            {
                actor.Quotes.Add(new Quote { Text = "Please, release me, let me go" });
                actor.Quotes.Add(new Quote { Text = "To waste our lives would be a sin" });
                context.SaveChanges();
            }
        }

        private static void AddManyToManyObject()
        {
            var context = new MoviesContext();
            var actor = new Actor { Name = "Engelbert Humperdinck", Birthday = new DateTime(1936, 5, 2), Nationality = "British" };
            var movie = context.Movies.Find(6);
            context.Add(actor);
            context.Add(new MovieActor { Movie = movie, Actor = actor });
            context.SaveChanges();
        }

        private static void DisplayMoviesEagerLoad()
        {
            var context = new MoviesContext();
            var movies = context.Movies
                .Include(m => m.Actors)
                    .ThenInclude(ma => ma.Actor)
                        .ThenInclude(a => a.Quotes)
                .Include(m => m.Tomato)
                .ToList();

            Console.WriteLine("\n\n\n====================\n");
            foreach(var movie in movies)
            {
                Console.Write(movie.Title);
                if(null != movie.Tomato)
                {
                    Console.WriteLine(" => TM: " + movie.Tomato.TomatoMeter + " AC: " + movie.Tomato.AudienceScore);
                }
                else
                {
                    Console.WriteLine(" => Not Rated!");
                }
                foreach(var actor in movie.Actors)
                {
                    Console.WriteLine("\t" + actor.Actor.Name);
                    foreach(var quote in actor.Actor.Quotes)
                    {
                        Console.WriteLine("\t\t" + quote.Text);
                    }
                }
            }
        }

        public static void AddActorsToMovie()
        {
            var context = new MoviesContext();
            var movie = context.Movies.First();
            var actors = context.Actors.ToList();
            foreach(var actor in actors)
            {
                context.MovieActor.Add(new MovieActor { ActorId = actor.Id, MovieId = movie.Id });
            }
            context.SaveChanges();
            //actors.ForEach(a => context.MovieActor.Add(new MovieActor { ActorId = a.Id, MovieId = movie.Id }));

        }

    }
}
