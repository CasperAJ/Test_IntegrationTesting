using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            var e_handler = new Exercise_Handler();

            Console.WriteLine("\n nr of Twitter users in the database");
            Console.WriteLine("# of users: " + e_handler.UserCount());

            Console.WriteLine("\n what Twitter uer link the most to other users. top 10");
            writeToDoc(e_handler.TopTagger());

            // Console.WriteLine("\n who are the most mentioned user. top 5");
            // could not figure this one out, keep getting error so removed it and proceeded on. I would love to see example on how it suppose to be made if chance comes later on.

            Console.WriteLine("\n who is the most active  user, top 10");
            writeToDoc(e_handler.TopActive());

            Console.WriteLine("\n who is the five grumpiest ,negative, and happiest ,positive, user. top 5 each");
            Console.WriteLine("\n 5 happy tweets, with happy, love, party, hug or kiss");
            writeToDoc(e_handler.Happy());
            Console.WriteLine("\n Top 5 gtumpy tweets, with angry, hate, fuck or kill");
            writeToDoc(e_handler.Grumpy());
        }

        public static void writeToDoc(List<BsonDocument> response)
        {
            int c = 1;
            foreach (var user in response)
            {
                Console.WriteLine(c + user.AsBsonDocument.ToString().Replace("_id", "Username:").Replace("\\", "").Replace("\"", "").Replace("{", "").Replace("}", "").Replace(":", ""));
                c++;
            }
        }
    }

    public class Tweet
    {
        public string polarity { get; set; }
        public string _id { get; set; }
        public DateTime date { get; set; }
        public string query { get; set; }
        public string user { get; set; }
        public string text { get; set; }
    }
}
