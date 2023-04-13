//using System.Reflection;

//namespace reflection_linq
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            int[] numbers = new int[1000];
//            for (int i=0; i<numbers.Length; i++)
//            {
//                numbers[i] = Random.Shared.Next(42, 556);
//            }

//            //var results = from number in numbers
//            //              where number % 3 == 0
//            //              orderby number ascending
//            //              select number;

//            //LAMBDA
//            var results = numbers
//                .Where(number => number % 3 == 0)
//                .OrderBy(number => number);

//            foreach (var result in results)
//            {
//                Console.WriteLine(result);
//            }



//        }
//    }
//}