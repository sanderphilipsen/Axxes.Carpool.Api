using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axxes.Carpool.Contracts.Models;

public class Person
{
    public string Name;
    public Person(string name)
    {
        Name = name;
    }
}