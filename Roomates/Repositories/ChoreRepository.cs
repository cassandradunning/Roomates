using Microsoft.Data.SqlClient;
using Roommates.Models;
using System;
using System.Collections.Generic;

namespace Roommates.Repositories
{
    public class ChoreRepository : BaseRepository
    {
        public ChoreRepository(string connectionString) : base(connectionString) { }
        public List<Chore> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Select * FROM Chore";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Chore> chores = new();
                        while (reader.Read())
                        {
                            Chore chore = new Chore
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                            };
                            chores.Add(chore);
                        }
                        return chores;
                    }
                }
            }
        }
        public void Insert(Chore chore)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Chore (Name)
                                        OUTPUT INSERTED.id
                                        VALUES (@name)";
                    cmd.Parameters.AddWithValue("@name", chore.Name);

                    int id = (int)cmd.ExecuteScalar();
                    chore.Id = id;
                }
            }
        }
        public Chore GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Name FROM Chore WHERE Id =@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Chore chore = null;
                        if (reader.Read())
                        {
                            chore = new Chore
                            {
                                Id = id,
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                            };
                        }
                        return chore;
                    }
                }
            }
        }

        public List<Chore> GetUnassignedChores()
        {

            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM CHORE " +
                        "LEFT JOIN RoommateChore rc on Chore.Id = rc.ChoreId " +
                        "WHERE rc.RoommateId is null ";


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Chore> unassignedChores = new List<Chore>();

                        while (reader.Read())
                        {
                            Chore chores = new Chore
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                            };
                            unassignedChores.Add(chores);
                        }
                        return unassignedChores;
                    }
                }
            }
        }
        public void AssignChore( int roommateId, int choreId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO RoommateChore (RoommateId, ChoreId) OUTPUT INSERTED.Id VALUES (@roommateId, @choreId) ";
                    cmd.Parameters.AddWithValue("@roommateId", roommateId);
                    cmd.Parameters.AddWithValue("@choreId", choreId);
                    
                    int Id = 0;

                    Id=(int)cmd.ExecuteScalar();

                    if (Id != 0) 
                    {
                            Console.WriteLine("Your chore has been assigned.");
                        
                    }
                    Console.WriteLine("Press any key to continue");
                    Console.ReadLine();
                }
            }

            }

        public void Update(Chore chore)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Chore SET Name = @name WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@name", chore.Name);
                    cmd.Parameters.AddWithValue("@id", chore.Id);

                    // Now that we're using a UPDATE statement in our SQL command, what we're really
                    // telling the database is simply that "We'd like you to execute this SQL code to update a row. .
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        ///  Delete the room with the given id
        /// </summary>
        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // What do you think this code will do if there is a roommate in the room we're deleting???
                    cmd.CommandText = "DELETE FROM Chore WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
