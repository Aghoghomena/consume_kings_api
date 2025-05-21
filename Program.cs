
using project.services;

//call the API

Console.WriteLine("Welcome to Aghogho Olokpa World!");
var callapi = new callApi();
var apiresponse = await callapi.GetData();
if (apiresponse != null)
{


    Console.Write("What is your name: ");
    string? firstname = Console.ReadLine();
    Console.WriteLine($"welcome {firstname}");
    Console.Write("Do you want to continue enter Yes: ");
    string? entered = Console.ReadLine();
    bool isentered = entered.Trim().Equals("Yes", StringComparison.OrdinalIgnoreCase);
    if (isentered)
    {
        Console.WriteLine("Select a question by entering any of the numbers below");
        var options = new Options();
        foreach (var option in options.OptionsList)
        {
            Console.WriteLine($"Enter {option.Key} for {option.Value}");
        }
        Console.WriteLine("0. Exit");
        Console.Write("\nYour choice: ");
        string? enteredvalue = Console.ReadLine();
        Console.WriteLine($"Your entered option is {enteredvalue}");
        if (int.TryParse(enteredvalue, out int parsedvalue))
        {
            Console.WriteLine(options.handleOptions(parsedvalue, apiresponse));
        }


    }

}
else
{
    Console.WriteLine("I am unable to process the Data right now. Try again Later");
}


