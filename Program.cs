/* 
 * Maybe not the most beautiful and elgant code, but I think it does the job.
 * Here is a short description of the constituents:
 * In total two classes; ProductList and Product, and five methods; OptionMode, AddMode, SearchItem, ListItems and Product (constructor).
 * 
 * ProductList class handles the two methods OptionMode and AddMode, which consists of while-loops "listening" to the user. 
 * In optionMode the user choose between three alternatives; search, add or quit (not case sensitive).
 * - If add is selected, the user enters AddMode where new items can be added, for this the constructor Product in the Product class is used
 * This enables product addition to the list (object initialization). Also, sorting based on price occur here. And the user can quit AddMode at any input line (Category, Product, Price)
 * - If search is selected, the two methods SearchItem and ListItems are used, where SearchItem searches for a product based on the product name, 
 * and the ListItems lists the products and if a product was searched and found, the product row of the corresponding product is highlighted. 
 * - If quit is selected the app is terminated (script ran to the end with no return possibility).
 * 
 * The error handlings in this script are as following:  
 * - If the users inputs a different command to OptionMode  
 * - If user fail to give a correct format of price in AddMode. 
 * - If user enter incorrect name/non-existing product to search for
 */

List<Product> products = new List<Product>();
ProductList.AddMode(products);


class ProductList
{
    public static List<Product> OptionMode(List<Product> inputList)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("To enter a new product - enter: \"P\" | To search for a product - enter: \"S\" | To quit app - enter: \"Q\": ");
        Console.ForegroundColor = ConsoleColor.White;       
        string option = Console.ReadLine().ToLower(); ;
        if (option == "s")
        {
            Console.Write("Enter a Product Name: ");
            string inputName = Console.ReadLine();
            int listNumber = Product.SearchItem(inputName, inputList);
            if (listNumber != -1)
            {
                Product.ListItems(inputList, listNumber);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Product name not found. Try again!\n");
            }
            OptionMode(inputList); // Run this mode again
        }
        else if (option == "p")
        {
            inputList = ProductList.AddMode(inputList); // Go back to AddMode
        }
        else if (option == "q")
        {
            Console.WriteLine("Thank you, come again.");
        }
        else //try again
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Something went wrong. Try again!");
            OptionMode(inputList); // Run this mode again
        }
        return inputList;

    }
    public static List<Product> AddMode(List<Product> inputList)
    {
        string category = "", prodName = "", price = "";
        Boolean qEntered = false;
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("To enter a new product - follow the steps | To quit add product mode - enter: \"Q\" (at any step).");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Enter a Category: ");
            category = Console.ReadLine().ToLower();
            if (category == "q")
            {
                break; 
            } 
                Console.Write("Enter a Product Name: ");
                prodName = Console.ReadLine().ToLower();
            if (prodName == "q")
            {
                break;
            }
                Console.Write("Enter a Price: ");
                price = Console.ReadLine().ToLower();

            if (price == "q")
            {
                break;
            }
            try
            {
                inputList.Add(new Product(category, prodName, int.Parse(price)));
                inputList = inputList.OrderBy(product => product.Price).ToList(); //Sort it directly
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("The product was succesfully added! \n");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong. Try again!");
                continue;
            }
        }
        int listNumber = -1;
        Product.ListItems(inputList, listNumber);

        int priceSum = inputList.Sum(product => product.Price);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("".PadRight(20) + "Total amount:".PadRight(20) + priceSum);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("-----------------------------------");
        OptionMode(inputList); //instead of app terminates, user gets option to continue manipulate list (according to output example) 
        
        return inputList;       
    }
}
class Product
{

    // Method
    public static string ListItems(List<Product> inputList, int highlightNbr)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Category".PadRight(20) + "Product".PadRight(20) + "Price");
        int currentIndex = 0;
        foreach (Product product in inputList)
        {
            if (highlightNbr == currentIndex)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine(product.Category.PadRight(20) + product.ProdName.PadRight(20) +
                      product.Price.ToString());
            currentIndex++;
        }
        Console.WriteLine("");
        return null;
    }

    // Method 
    public static int SearchItem(string inputName, List<Product> inputList)
    {
        int listNumber = inputList.FindIndex(product => product.ProdName.Equals(inputName));
        return listNumber;
    }

    // Constructor
    public Product(string category, string prodName, int price)
    {
        Category = category;
        ProdName = prodName;
        Price = price;
    }

    // Properties
    public string Category { get; set; }
    public string ProdName { get; set; }
    public int Price { get; set; }

}
