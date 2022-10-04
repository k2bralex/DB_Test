using System.Diagnostics;
using DB_Test.DbContext;
using DB_Test.Entity;

namespace DB_Test.Additional_Classes;

public static class Run
{
    public static void MyApp1()
    { 
        var db = new Context.ApplicationContext();
        db.SaveChanges();
    }

    public static void MyApp2(string[] arr)
    { 
        using (var db = new Context.ApplicationContext())
        {
            /*Console.WriteLine("Enter FisrtName: ");
            var firstName = Console.ReadLine() ?? "None";
            Console.WriteLine("Enter SecondName: ");
            var secondName = Console.ReadLine() ?? "None";
            Console.WriteLine("Enter MiddleName: ");
            var middleName = Console.ReadLine() ?? "None";
            Console.WriteLine("Enter Birth Date: ");
            var birthDate = DateOnly.TryParse(Console.ReadLine(), out var date);
            Console.WriteLine("Enter sex (Male - 0, Female - 1): ");
            var sex = int.TryParse(Console.ReadLine(), out var s);*/
            var firstName = arr[0];
            var secondName = arr[1];
            var middleName = arr[2];
            var birthDate = DateOnly.TryParse(arr[3], out var date);
            var sex = int.TryParse(arr[4], out var s);
            

            db.Add(new Person
            {
                FirstName = firstName, SecondName = secondName, MiddleName = middleName,
                BirthDate = birthDate ? date : DateOnly.MinValue, Sex = sex && s == 1
            });

            db.SaveChanges();
            
            Console.WriteLine("Row added.");
        }
    }

    public static void MyApp3()
    { 
        using (var db = new Context.ApplicationContext())
        {
            var unique = db.Persons.Select(p => new { p.Id, key = p.FirstName + p.SecondName + p.MiddleName + p.BirthDate })
                .ToList()
                .DistinctBy(i => i.key)
                .Select(i => i.Id);

            var result = db.Persons.Where(p => unique.Contains(p.Id))
                .OrderBy(p => p.FirstName);
            /*Console.WriteLine(db.Persons.Count());
            Console.WriteLine(result.Count());*/
            foreach (var person in result)
                Console.WriteLine(
                    $"{person.FirstName} {person.SecondName} {person.MiddleName} {person.BirthDate} {FullYears(person.BirthDate)} {(person.Sex ? "Female" : "Male")}");
        }

        static int FullYears(DateOnly date)
        {
            var now = DateOnly.FromDateTime(DateTime.Now);
            if (now.Year <= date.Year)
                return 0;
            var fullYears = now.Year - date.Year;
            if (date.DayOfYear < now.DayNumber) fullYears--;
            return fullYears;
        }
    }

    public static void MyApp4()
    { 
        using (var db = new Context.ApplicationContext())
        {
            var personList = new List<Person>();
            for (var i = 0; i < 1000000; i++)
                personList.Add(new Person
                {
                    FirstName = Randomizer.RandomName(),
                    SecondName = Randomizer.RandomName(),
                    MiddleName = Randomizer.RandomName(),
                    BirthDate = Randomizer.RandomDatebirth(),
                    Sex = Randomizer.RandomSex()
                });
            db.AddRange(personList);
            db.SaveChanges();
        }
    }

    public static void MyApp5()
    { 
        using (var db = new Context.ApplicationContext())
        {
            var watch = Stopwatch.StartNew();
            watch.Start();
            var result = db.Persons.Where(p => p.FirstName!.Substring(0, 1) == "F" && !p.Sex);
            watch.Stop();

            /*foreach (var person in result)
            {
                Console.WriteLine($"{person.FirstName} {person.SecondName} {person.MiddleName} {person.BirthDate}  {(person.Sex ? "Female" : "Male")}");
            }*/

            Console.WriteLine($"Working time, ms: {watch.ElapsedMilliseconds}");
        }
    }

    public static void MyApp6()
    { 
        Console.WriteLine("We need to add multi-column index (first_name && sex) columns to speed up the query\n" +
                          "modelBuilder.Entity<Person>().HasIndex(p => new{ p.FirstName, p.Sex }).HasDatabaseName(\"first_name_index\");");

        using (var db = new Context.ApplicationContext())
        {
            var watch = Stopwatch.StartNew();
            watch.Start();
            var result = db.Persons.Where(p => p.FirstName!.Substring(0, 1) == "F" && !p.Sex);
            watch.Stop();

            Console.WriteLine($"Working time, ms: {watch.ElapsedMilliseconds}");
        }
    }
}