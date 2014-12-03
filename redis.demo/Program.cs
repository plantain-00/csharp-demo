using StackExchange.Redis;

namespace redis.demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var redis = ConnectionMultiplexer.Connect("localhost");
            var database = redis.GetDatabase();
            database.StringSet("test", "test2");
            var result = database.StringGet("test");
            redis.Close();
        }
    }
}