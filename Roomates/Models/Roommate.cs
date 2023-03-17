using System;


namespace Roommates.Models
{
    // C# representation of the Roommate table
    public class Roommate
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RentPortion { get; set; }
        public DateTime MovedInDate { get; set; }
        // has the property of Room, thats why we can access it 
        public Room Room { get; set; }
    }
}