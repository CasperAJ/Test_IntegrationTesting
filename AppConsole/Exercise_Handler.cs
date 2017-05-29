using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppConsole
{
    public class Exercise_Handler
    {
        private MongoClient mongo;
        private IMongoDatabase IMD;
        private IMongoCollection<Tweet> tweets;

        public Exercise_Handler()
        {
            mongo = new MongoClient("mongodb://127.0.0.1:27017");
            IMD = mongo.GetDatabase("Social_Network");
            tweets = IMD.GetCollection<Tweet>("tweets");
        }

        public long UserCount()
        {
            var usecount = tweets.AsQueryable().Select(x => x.user).Distinct().Count();
            return usecount;
        }

        public List<BsonDocument> TopTagger()
        {
            var result = tweets.Aggregate(new AggregateOptions { AllowDiskUse = true })
                .Match(new BsonDocument { { "text", BsonRegularExpression.Create(new Regex("@")) } })
                .Group(new BsonDocument { { "_id", "$user" }, { "nrOfTweets", new BsonDocument("$sum", 1) } })
                .Sort(new BsonDocument { { "nrOfTweets", -1 } }).Limit(10);

            return result.ToList();
        }

        public List<BsonDocument> TopActive()
        {
            var result = tweets.Aggregate(new AggregateOptions { AllowDiskUse = true })
                .Group(new BsonDocument { { "_id", "$user" }, { "nrOfTweets", new BsonDocument("$sum", 1) } })
                .Sort(new BsonDocument { { "nrOfTweets", -1 } }).Limit(10);

            return result.ToList();
        }

        public List<BsonDocument> Happy()
        {
            var lookingFor = BsonRegularExpression.Create(new Regex("happy|love|party|hug|kiss"));
            var result = tweets.Aggregate(new AggregateOptions { AllowDiskUse = true })
                .Match(new BsonDocument { { "text", lookingFor } })
                .Group(new BsonDocument { { "_id", "$user" }, { "nrOfTweets", new BsonDocument("$sum", 1) } })
                .Sort(new BsonDocument { { "nrOfTweets", -1 } }).Limit(5);

            return result.ToList();
        }

        public List<BsonDocument> Grumpy()
        {
            var lookingFor = BsonRegularExpression.Create(new Regex("angry|hate|fuck|kill"));
            var result = tweets.Aggregate(new AggregateOptions { AllowDiskUse = true })
                    .Match(new BsonDocument { { "text", lookingFor } })
                    .Group(new BsonDocument { { "_id", "$user" }, { "nrOfTweets", new BsonDocument("$sum", 1) } })
                    .Sort(new BsonDocument { { "nrOfTweets", -1 } }).Limit(5);

            return result.ToList();
        }
    }
}
