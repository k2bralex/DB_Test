using DB_Test.Additional_Classes;

var arguments = Environment.GetCommandLineArgs();

switch (arguments[1])
{
    case "1":
        Run.MyApp1(); break;
    case "2":
        if (arguments[2..].Count() < 5)
        {
            Console.WriteLine("Wrong arguments quantity!");
            break;
        }
        var argRange = arguments[2..];
        Run.MyApp2(argRange);
        break;
    case "3":
        Run.MyApp3(); break;
    case "4":
        Run.MyApp4(); break;
    case "5":
        Run.MyApp5(); break;
    case "6":
        Run.MyApp6(); break;
    default: Console.WriteLine("Wrong argument!"); break;
}





