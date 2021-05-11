
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionMapperUtil
{
    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }

        [MapSourceProperty("Addr")]
        public string Address { get; set; }
    }

    public class PersonDto
    {
        public int Age { get; set; }
        public string Name { get; set; }

        public string Addr { get; set; }
    }
}
