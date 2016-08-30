using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;
using Core.Persistency;
using Core.Primitives;
using Domain;

namespace InMemory
{
    public class InMemoryRepository : IRepository
    {
        private static readonly IDictionary<Type, IList<IAggregateRoot>> Database = new Dictionary<Type, IList<IAggregateRoot>>();

        static InMemoryRepository()
        {
            var admin = new User("admin", "pass");
            admin.SetClaims(new List<Claim> { Claim.Administrator, Claim.Authenticated });
            Save(admin);
            
            for (var i = 1; i <= 100; i++)
            {
                var user = new User("user" + i, "pass");
                user.SetClaims(new List<Claim> { Claim.Authenticated });
                Save(user);
            }
        }

        public IList<T> List<T>() where T : IAggregateRoot
        {
            if (Database.ContainsKey(typeof(T)) == false)
                return new List<T>();

            var collection = (IList<T>)Database[typeof(T)].Select(x => (T)x).ToList();
            return collection.ToList();
        }

        public IList<T> List<T>(Func<T, bool> predicate) where T : IAggregateRoot
        {
            if (Database.ContainsKey(typeof(T)) == false)
                return new List<T>();

            var collection = (IList<T>)Database[typeof(T)].Select(x => (T)x).ToList();
            return collection.Where(predicate).ToList();
        }

        public T SingleOrDefault<T>() where T : IAggregateRoot
        {
            if (Database.ContainsKey(typeof(T)) == false)
                return default(T);

            var collection = (IList<T>)Database[typeof(T)].Select(x => (T)x).ToList();
            return collection.SingleOrDefault();
        }

        public T SingleOrDefault<T>(Func<T, bool> predicate) where T : IAggregateRoot
        {
            if (Database.ContainsKey(typeof(T)) == false)
                return default(T);

            var collection = (IList<T>)Database[typeof(T)].Select(x => (T)x).ToList();
            return collection.SingleOrDefault(predicate);
        }

        public bool Any<T>(Func<T, bool> predicate)
        {
            if (Database.ContainsKey(typeof(T)) == false)
                return false;

            var collection = (IList<T>)Database[typeof(T)].Select(x => (T)x).ToList();
            return collection.Any(predicate);
        }

        public void SaveOrUpdate<T>(T aggregateRoot) where T : IAggregateRoot
        {
            Save(aggregateRoot);
        }

        public void Remove<T>(T aggregateRoot) where T : IAggregateRoot
        {
            if (Database.ContainsKey(typeof(T)))
            {
                var collection = Database[typeof(T)];

                if (collection.Contains(aggregateRoot))
                    collection.Remove(aggregateRoot);
            }
        }

        public static void Save<T>(T aggregateRoot) where T : IAggregateRoot
        {
            if (Database.ContainsKey(typeof(T)) == false)
            {
                Database.Add(typeof(T), new List<IAggregateRoot> { aggregateRoot });
            }
            else
            {
                var collection = Database[typeof(T)];

                var persistedAggregateRoot = collection.SingleOrDefault(x => x.Id == aggregateRoot.Id);

                if (persistedAggregateRoot != null)
                    collection.Remove(persistedAggregateRoot);

                collection.Add(aggregateRoot);
            }
        }
    }

    public static class PaintingsImages
    {
        private static readonly IList<string> Images = new List<string>
        {
            "http://www.worldhistorycharts.com/images/paintings/botticelli-birth-of-venus.jpg",
            "http://www.worldhistorycharts.com/images/paintings/da-vinci-mona-lisa.jpg",
            "http://www.worldhistorycharts.com/images/paintings/michelangelo-sistine-chapel.jpg",
            "http://www.worldhistorycharts.com/images/paintings/raphael-school-of-athens.jpg",
            "http://www.worldhistorycharts.com/images/paintings/bruegel-fall-of-icarus.jpg",
            "http://www.worldhistorycharts.com/images/paintings/velazquez-las-meninas.jpg",
            "http://www.worldhistorycharts.com/images/paintings/rembrandt-night-watch.jpg",
            "http://www.worldhistorycharts.com/images/paintings/vermeer-girl-with-a-pearl-earring.jpg",
            "http://www.worldhistorycharts.com/images/paintings/goya-third-of-may.jpg",
            "http://www.worldhistorycharts.com/images/paintings/whistlers-mother.jpg",
            "http://www.worldhistorycharts.com/images/paintings/cezanne-basket-of-apples.jpg",
            "http://www.worldhistorycharts.com/images/paintings/monet-water-lillies.jpg",
            "http://www.worldhistorycharts.com/images/paintings/renoir-le-moulin-de-la-galette.jpg",
            "http://www.worldhistorycharts.com/images/paintings/van-gogh-starry-night.jpg",
            "http://www.worldhistorycharts.com/images/paintings/seurat-sunday-afternoon.jpg",
            "http://www.worldhistorycharts.com/images/paintings/klimt-the-kiss.jpg",
            "http://www.worldhistorycharts.com/images/paintings/kandinsky-composition-8.jpg",
            "http://www.worldhistorycharts.com/images/paintings/picasso-dora-maar.jpg",
            "http://www.worldhistorycharts.com/images/paintings/wood-american-gothic.jpg",
            "http://www.worldhistorycharts.com/images/paintings/magritte-son-of-man.jpg",
            "http://www.worldhistorycharts.com/images/paintings/dali-persistence-of-memory.jpg",
            "http://www.worldhistorycharts.com/images/paintings/pollock-no-5.jpg"
        };

        private static readonly Random Rand = new Random();

        public static string Generate()
        {
            return Images[Rand.Next(0, 21)];
        }
    }

    public static class FilmImages
    {
        private static readonly IList<string> Images = new List<string>
        {
            "http://ia.media-imdb.com/images/M/MV5BODU4MjU4NjIwNl5BMl5BanBnXkFtZTgwMDU2MjEyMDE@._V1_SX214_AL_.jpg",
		    "http://ia.media-imdb.com/images/M/MV5BMTkxMTA5OTAzMl5BMl5BanBnXkFtZTgwNjA5MDc3NjE@._V1_SX214_AL_.jpg",
		    "http://ia.media-imdb.com/images/M/MV5BMjE4MjA1NTAyMV5BMl5BanBnXkFtZTcwNzM1NDQyMQ@@._V1_SX214_AL_.jpg",
		    "http://ia.media-imdb.com/images/M/MV5BMjIwNTYzMzE1M15BMl5BanBnXkFtZTcwOTE5Mzg3OA@@._V1_SX214_AL_.jpg",
		    "http://ia.media-imdb.com/images/M/MV5BOTQ5NDI3MTI4MF5BMl5BanBnXkFtZTgwNDQ4ODE5MDE@._V1_SX214_AL_.jpg",
		    "http://ia.media-imdb.com/images/M/MV5BMTY2OTE5MzQ3MV5BMl5BanBnXkFtZTgwMTY2NTYxMTE@._V1_SX214_AL_.jpg",
		    "http://ia.media-imdb.com/images/M/MV5BMzI1MjI5MDQyOV5BMl5BanBnXkFtZTcwNzE4Mjg3NA@@._V1_SX214_AL_.jpg",
		    "http://ia.media-imdb.com/images/M/MV5BMTgzMzg4MDkwNF5BMl5BanBnXkFtZTcwODAwNDg3OA@@._V1_SY317_CR4,0,214,317_AL_.jpg",
            "http://ia.media-imdb.com/images/M/MV5BMTkzODMzODYwOF5BMl5BanBnXkFtZTcwODM2NjA2NQ@@._V1_SY317_CR3,0,214,317_AL_.jpg",
            "http://ia.media-imdb.com/images/M/MV5BMTkxNDYxOTA4M15BMl5BanBnXkFtZTgwNTk0NzQxMTE@._V1_SX214_AL_.jpg",
            "http://ia.media-imdb.com/images/M/MV5BMjA0ODEzMTc1Nl5BMl5BanBnXkFtZTcwODM2MjAxNA@@._V1_SX214_AL_.jpg",
            "http://ia.media-imdb.com/images/M/MV5BMTUxMzQyNjA5MF5BMl5BanBnXkFtZTYwOTU2NTY3._V1_SY317_CR0,0,214,317_AL_.jpg",
            "http://ia.media-imdb.com/images/M/MV5BMjA5NTYzMDMyM15BMl5BanBnXkFtZTgwNjU3NDU2MTE@._V1_SX214_AL_.jpg",
            "http://ia.media-imdb.com/images/M/MV5BMjIyNTQ5NjQ1OV5BMl5BanBnXkFtZTcwODg1MDU4OA@@._V1_SX214_AL_.jpg",
            "http://ia.media-imdb.com/images/M/MV5BMTk4ODQzNDY3Ml5BMl5BanBnXkFtZTcwODA0NTM4Nw@@._V1_SX214_AL_.jpg",
            "http://ia.media-imdb.com/images/M/MV5BMTYzNzU5MzQ4OV5BMl5BanBnXkFtZTcwMDcxNDg3OA@@._V1_SY317_CR12,0,214,317_AL_.jpg",
            "http://ia.media-imdb.com/images/M/MV5BMTQxMTAwMDQ3Nl5BMl5BanBnXkFtZTcwODMwNTgzMQ@@._V1_SY317_CR5,0,214,317_AL_.jpg",
            "http://ia.media-imdb.com/images/M/MV5BMTYzNjkxMTczOF5BMl5BanBnXkFtZTgwODg5NDc2MjE@._V1_SY317_CR0,0,214,317_AL_.jpg",
            "http://ia.media-imdb.com/images/M/MV5BMTgwMzQzNTQ1Ml5BMl5BanBnXkFtZTgwMDY2NTYxMTE@._V1_SY317_CR0,0,214,317_AL_.jpg",
            "http://ia.media-imdb.com/images/M/MV5BMTc4MjUxNDAwN15BMl5BanBnXkFtZTcwMDMwNDg3OA@@._V1_SY317_CR12,0,214,317_AL_.jpg",
            "http://ia.media-imdb.com/images/M/MV5BMjA4NDI0MTIxNF5BMl5BanBnXkFtZTYwNTM0MzY2._V1_SX214_AL_.jpg",
            "http://ia.media-imdb.com/images/M/MV5BMTk5NzE0MDQyNl5BMl5BanBnXkFtZTcwNzk4Mjk3OA@@._V1_SY317_CR2,0,214,317_AL_.jpg",
            "http://ia.media-imdb.com/images/M/MV5BMTkzODczMTgwM15BMl5BanBnXkFtZTYwNTAwODI5._V1_SX214_AL_.jpg",
            "http://ia.media-imdb.com/images/M/MV5BMTQxMTUyODg2OF5BMl5BanBnXkFtZTcwNDM2MjAxNA@@._V1_SX214_AL_.jpg",
            "http://ia.media-imdb.com/images/M/MV5BMTk0NjY0Mzg5MF5BMl5BanBnXkFtZTcwNzM1OTM2MQ@@._V1_SY317_CR1,0,214,317_AL_.jpg",
            "http://ia.media-imdb.com/images/M/MV5BMTA4MDQxNTk2NDheQTJeQWpwZ15BbWU3MDE2NjIyODk@._V1_SX214_AL_.jpg"
        };

        private static readonly Random Rand = new Random();

        public static string Generate()
        {
            return Images[Rand.Next(0, 25)];
        }
    }

    public static class LoremIpsum
    {
        private static readonly Random Rand = new Random();

        public static string Generate(int minWords=5, int maxWords=10, int minSentences=3, int maxSentences=3, int numLines=3)
        {
            var words = new[] { "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat" };
            
            int numSentences = Rand.Next(maxSentences - minSentences)
                + minSentences;
            int numWords = Rand.Next(maxWords - minWords) + minWords;

            var sb = new StringBuilder();
            for (int p = 0; p < numLines; p++)
            {
                for (int s = 0; s < numSentences; s++)
                {
                    for (int w = 0; w < numWords; w++)
                    {
                        if (w > 0) { sb.Append(" "); }
                        string word = words[Rand.Next(words.Length)];
                        if (w == 0) { word = word.Substring(0, 1).Trim().ToUpper() + word.Substring(1); }
                        sb.Append(word);
                    }
                    sb.Append(". ");
                }
                if (p < numLines - 1) sb.AppendLine();
            }
            return sb.ToString();
        }
    }

    public static class ISBN
    {
        private static readonly Random Random = new Random(0);

        public static string Generate()
        {
            return string.Format("{0}-{1}-{2}-{3}-0", GetNumberWith(3), GetNumberWith(1), GetNumberWith(2), GetNumberWith(6));
        }

        private static string GetNumberWith(double digits)
        {
            return Random.Next(int.Parse(Math.Pow(10, digits).ToString())).ToString("D" + digits);
        }
    }
}