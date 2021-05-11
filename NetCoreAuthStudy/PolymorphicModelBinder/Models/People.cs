
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[JsonConverter(typeof(PolymorphicJsonCreationConverter))]
public class People
{
    public string Name { get; set; }
}

class Student : People
{
    public int Grade { get; set; }
}

class Worker : People
{
    public string Job { get; set; }
}

