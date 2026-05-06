Console.WriteLine("=== Simple Calculator ===");
Console.WriteLine("Enter the operation \n" +
    "1 - Addition\n" +
    "2 - Subtraction\n" +
    "3 - Multiplication\n" +
    "4 - Division");

int menu;
menu = Convert.ToInt32(Console.ReadLine());

if(menu == 1)
{
    Console.WriteLine("You chosen Addition");
}
    else if(menu == 2)
{
    Console.WriteLine("You chosen Subtraction");

}
    else if(menu == 3)
{
    Console.WriteLine("You chosen Multiplication");
}
    else if(menu == 4)
{
    Console.WriteLine("You chosen Division");
}
    else
    {
        Console.WriteLine("Invalid operation selected.");
    return;
    }

double number1;
double number2;
double result = 0;

Console.WriteLine("Enter the first number:");
number1 = Convert.ToDouble(Console.ReadLine());


Console.WriteLine("Enter the second number:");
number2 = Convert.ToDouble(Console.ReadLine());


switch (menu)
{
    case 1:
        result = number1 + number2;
        break;
    case 2:
        result = number1 - number2;
        break;
    case 3:
        result = number1 * number2;
        break;
    case 4:
        if (number2 == 0)
        {
            Console.WriteLine("Error: division by zero.");
            return;
        }
        result = number1 / number2;
        break;
}

Console.WriteLine($"Result: {result:F4}");

Console.ReadKey();