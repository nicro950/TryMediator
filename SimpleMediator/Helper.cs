#nullable enable
using System;
using System.Linq;

namespace MediatrTry
{
    public static class Helper
    {
        public static void PrintResult(string person, PersonResponse response)
        {
            if (response?.Person == null)
                Console.WriteLine($"Could not find any person for {person}");
            else
                Console.WriteLine($"Found the following information for {person}: {{ {Dump(response.Person)} }}");
        }

        public static string Dump(object resposne)
        {
            return string.Join(", ", resposne.GetType().GetProperties().Select(x => $"{x.Name}: {x.GetValue(resposne)}"));
        }
    }
}
