using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axxes.Carpool.Contracts.Models;

public class EventRegistration
{

    public Person Person;
    public bool CanDrive;
    public bool OpenToCarpool;

    public EventRegistration(Person person, bool canDrive, bool openToCarpool)
    {
        Person = person;
        CanDrive = canDrive;
        OpenToCarpool = openToCarpool;
    }

}