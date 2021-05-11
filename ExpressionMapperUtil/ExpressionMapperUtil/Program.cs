using System;

namespace ExpressionMapperUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var dto = new PersonDto() { Age = 1, Name = "songlin", Addr = "1231" };

            var person = ExpressionMapper.Map<PersonDto, Person>(dto);
            
            Console.ReadLine();
        }
    }
}
