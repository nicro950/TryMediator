#nullable enable
using System.Collections.Generic;

namespace MediatrTry
{
    class Program
    {
        static void Main(string[] args)
        {
            AMediator aMediator = new AMediator();
            RegisterPersonHandler(aMediator);

            //TODO: Request Bob

            Helper.PrintResult("Bob", null /*TODO: Insert result object*/);

            //TODO: Request Peter

            Helper.PrintResult("Peter", null /*TODO: Insert result object*/);

        }

        private static void RegisterPersonHandler(AMediator amediator)
        {
            Dictionary<string, Person> allPersons = new Dictionary<string, Person>
            {
                ["Bob"] = new Person() { Name = "Bob", Email = "bob@bob.com", Age = 35 },
            };

            amediator.RegisterHandler((PersonRequest x) =>
            {
                // TODO: Implement handler logic
                return new PersonResponse(null);
            });
        }
    }

    public class PersonRequest : IRequest<PersonResponse>
    {
        public PersonRequest(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    public class PersonResponse
    {
        public PersonResponse(Person? person)
        {
            Person = person;
        }

        public Person? Person { get; }
    }

    public class Person
    {
#nullable disable
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
#nullable enable
    }
}
