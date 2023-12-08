using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023
{

    public abstract class BaseSolution
    {
        public List<string> Lines =>
            this.GetInputDataLines();

        /// <summary>
        /// Invokable method that will work with other classes to solve the puzzles.
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// Invokes the Run method, but also inserts the time it took to run at the end.
        /// </summary>
        public void RunWithTimer()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            this.Run();

            stopwatch.Stop();
            Console.WriteLine($"This solution took {stopwatch.ElapsedMilliseconds} milliseconds to run!");
        }


        /// <summary>
        /// Returns the displayable name set in the Display attribute on each child.
        /// </summary>
        public string GetDisplayableName()
        {
            Type type = this.GetType();
            bool hasDisplayAttribute = Attribute.IsDefined(type, typeof(DisplayAttribute));

            if (!hasDisplayAttribute)
            {
                return type.FullName;
            }

            var attribute = (DisplayAttribute)Attribute.GetCustomAttribute(type, typeof(DisplayAttribute));
            return attribute.Name;
        }


        /// <summary>
        /// Returns all text in the local input.in file, with seperated lists.
        /// </summary>
        /// <returns></returns>
        public List<string> GetInputDataLines()
        {
            string inputdata = this.GetInputData();
            return inputdata.Split("\n", StringSplitOptions.TrimEntries).ToList();
        }

        /// <summary>
        /// Returns all text in the local input.in file.
        /// </summary>
        public string GetInputData()
        {
            string filepath = this.GetFilePath();

            try
            {
                using (var reader = new StreamReader(filepath))
                {
                    string content = reader.ReadToEnd();
                    return content;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("An error occured while attempting to read the file");
                return string.Empty;
            }
        }

        /// <summary>
        /// Method used for determening what file is used.
        /// In case of divergent input type, this can be overwritten.
        /// </summary>
        /// <returns>filepath (string)</returns>
        protected virtual string GetFilePath()
        {
            string pattern = @"\.Day(\d+)";
            var type = this.GetType();

            Regex regex = new(pattern);
            Match match = regex.Match(type.FullName);

            if (!match.Success)
            {
                throw new Exception("Could not create full filepath");
            }

            string dayNumber = match.Groups[1].Value;

            return $"../../../Day{dayNumber}/input.in";
        }
    }
}
