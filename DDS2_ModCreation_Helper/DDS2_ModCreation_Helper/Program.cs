namespace DDS2_ModCreation_Helper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("(1) JSON Field Multiplication");
            Console.WriteLine("(2) WIP");
            Console.WriteLine("(3) WIP");
            string? result = Console.ReadLine();

            if (result == "1")
            {
                JsonFieldMulti.MultipliField();
            }
        }
    }
}
