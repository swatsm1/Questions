using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Solution_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Processing LOC");

            var edi = ProcessEDIFACT("EDIFACT.txt");

            foreach (var s in edi)
            {
                Console.WriteLine("2nd Segment LOC " + s.segment2 + " 3nd Segment LOC " + s.segment3);
            }

        }

        private static List<LOC> ProcessEDIFACT(string v)
        {

            var query =
                   File.ReadAllLines(v)
                       .Where(l => l.Contains("LOC"))
                       .Select(l =>
                       {
                           var columns = l.Split('+');
                           

                               return new LOC
                               {
                                   segment2 = columns[1],
                                   segment3 = columns[2],

                               };
                          
                       });
            return query.ToList();


        }
    }
}
