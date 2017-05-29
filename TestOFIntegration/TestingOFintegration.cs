using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoApp;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MongoDB.Bson;
using AppConsole;

namespace IntegrationTests
{
    [TestClass]
    public class TestingOFintegration
    {
        [TestMethod]
        public void CheckSort()
        {
            Exercise_Handler MDBEH = new Exercise_Handler();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(AppConsole.Tweet));


            var tw = BtS(MDBEH.TopTagger());
            Assert.AreEqual(10, tw.Count); CD(tw);
            tw = BtS(MDBEH.TopActive());
            Assert.AreEqual(10, tw.Count); CD(tw);
            tw = BtS(MDBEH.Happy());
            Assert.AreEqual(5, tw.Count); CD(tw);
            tw = BtS(MDBEH.Grumpy());
            Assert.AreEqual(5, tw.Count); CD(tw);
        }

        public void CD(List<string> jsl)
        {
            long pc = 0;
            foreach (var tweet in jsl)
            {
                var split = tweet.Replace("_id", "Username:").Replace("\\", "").Replace("\"", "").Replace("{", "").Replace("}", "").Replace(":", "").Split(' ');

                long c = long.Parse(split[split.Length - 2]);
                if (pc == 0){
                    pc = c;
                }
                else if (pc < c){
                    Assert.Fail("bad sorr");
                }
            }
        }

        public List<string> BtS(List<BsonDocument> bsl)
        {
            var tweetlist = new List<string>();
            foreach (var tweet in bsl)
            {
                tweetlist.Add(tweet.AsBsonDocument.ToString());
            }
            return tweetlist;
        }
    }
}
