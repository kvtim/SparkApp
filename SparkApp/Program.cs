using Microsoft.Spark.Sql;
using System.Diagnostics.Metrics;
using System.IO;
using static Microsoft.Spark.Sql.Functions;
using static System.Net.Mime.MediaTypeNames;

namespace MySparkApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Spark session
            SparkSession spark =
                SparkSession
                    .Builder()
                    .AppName("word_count_sample")
                    .GetOrCreate();

            // Create initial DataFrame
            string filePath = args[0];
            DataFrame dataFrame = spark.Read().Text(filePath);

            //Count words
            DataFrame words =
                dataFrame
                    .Select(Split(Col("value"), " ").Alias("words"))
                    .Select(Explode(Col("words")).Alias("word"))
                    .GroupBy("word")
                    .Count()
                    .OrderBy(Col("count").Desc());

            // Display results
            words.Show();

            var min = words.Agg(Min(words.Col("count"))).Head().Get(0);
            var max = words.Agg(Max(words.Col("count"))).Head().Get(0);

            using (StreamWriter writer = new StreamWriter("../../../../output.txt", false))
            {
                writer.WriteLine($"Min: {min} \nMin: {max}");
            }
            // Stop Spark session
            spark.Stop();
        }
    }
}