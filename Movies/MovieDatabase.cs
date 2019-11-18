using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public class MovieDatabase
    {
        private List<Movie> movies = new List<Movie>();

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        public MovieDatabase() {
            
            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }
        }

        public List<Movie> All { get { return movies; } }

        public List<Movie> Search(string search)
        {
            if (search == null) return All;

            List<Movie> results = new List<Movie>();

            foreach (Movie movie in movies)
            {
                if (movie.Title != null 
                    && movie.Title.Contains(search, StringComparison.InvariantCultureIgnoreCase))
                {
                    results.Add(movie);
                }
            }

            return results;
        }

        public List<Movie> SearchAndFilter(string search, List<string> rating)
        {
            if (rating.Count == 0) return Search(search);
            
            List<Movie> results = new List<Movie>();

            foreach (Movie movie in movies)
            {
                if (search != null && rating.Count > 0 && movie.Title != null 
                    && movie.Title.Contains(search, StringComparison.InvariantCultureIgnoreCase) 
                    && rating.Contains(movie.MPAA_Rating))
                {
                        results.Add(movie);
                }
                else if (search == null && rating.Contains(movie.MPAA_Rating))
                {
                    results.Add(movie);
                }
            }

            return results;
        }
    }
}
