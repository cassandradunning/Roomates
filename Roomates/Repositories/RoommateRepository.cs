using Microsoft.Data.SqlClient;
using Roommates.Models;
using Roommates.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace Roommates.Repositories
{
    public class RoommateRepository : BaseRepository
    {
        public RoommateRepository(string connectionString) : base(connectionString) { }

        public Roommate GetById(int id)
        {
            // using signifies that we are opening the SQL Connection resource (reps a class in ADO.net)
            // CONNECTION
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                // allows us to write SQL in our app 
                // INSTRUCTION
                using (SqlCommand cmd = Connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT rm.FirstName, rm.RentPortion as 'RoomId', Room.* From Roommate rm" +
                                        "JOIN Room r ON r.Id = rm.RoomId" +
                                        "WHERE rm.Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    // READS what comes back
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Roommate roommate = null;

                        if (reader.Read())
                        {
                            roommate = new Roommate
                            {
                                // id comes to the parameter in public Roommate GetById(int id)
                                Id = id,
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                RentPortion = reader.GetInt32(reader.GetOrdinal("RoomId")),
                                // has a room obj inside of roommate to access 
                                Room = new Room
                                {

                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    MaxOccupancy = reader.GetInt32(reader.GetOrdinal("MaxOccupancy"))

                                },
                            };
                        }
                        // returns within the same scope
                        return roommate;
                    }
                }
            }
            // when we close this, we say we no longer need it, it uses garbage collection automatically.
            // and the garbage collection comes by and frees up memory space.
            // if we dont, it can create a MEMORY LEAK and it slows down and kills your server
            // if you have the using, reader.Closing() isnt needed
        }
        public List<Roommate> GetAll()
            {
               using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM Roommate ";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<Roommate> roommates = new List<Roommate>();

                            while (reader.Read())
                            {
                                Roommate roommate = new Roommate
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                                    MovedInDate = reader.GetDateTime(reader.GetOrdinal("MoveInDate")),

                                };
                                roommates.Add(roommate);
                            }
                            return roommates;
                        }
                    }
                }
            }
    }
}
