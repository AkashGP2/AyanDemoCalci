using CalculatonDAL;
using CalculatonDAL.Models;

namespace CalculationTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CalculationLogContext())
            {

                CalculationItem item = new CalculationItem()
                {
                    NumberOne = 1,
                    NumberTwo = 2,
                    OperationCode = "Add",
                    Result_Value = 3
                };

                context.CalculationItems.Add(item);
                context.SaveChanges();

                var calculations = context.CalculationItems.ToArray();

                Console.WriteLine($"Count of items {calculations.Length}");
            }
        }
    }
}