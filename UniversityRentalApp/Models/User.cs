using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityRentalApp.Models
{
    public enum UserType{
    Student=1,
    Employee=2
    }

    public class User { 
    public Guid Id { get; private set; }
    public String FirstName {  get; private set; }
    public string LastName { get; private set; }
    public UserType Type { get; private set; }

        public User(String firstName, String lastName, UserType type) { 
        Id=Guid.NewGuid();
        FirstName=firstName;
        LastName=lastName;
        Type=type;
       
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName} ({Type}) - ID: {Id}";
        }

    }

}
