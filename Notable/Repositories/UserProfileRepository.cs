using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Notable.Models;
using Notable.Utils;
using System;
using System.Collections.Generic;

namespace Notable.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration iconfig) : base(iconfig) { }
        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT Id, Username, Email, FirebaseUserId, CreatedAt
                    FROM UserProfile";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var users = new List<UserProfile>();
                        while (reader.Read())
                        {
                            users.Add(new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Username = DbUtils.GetString(reader, "Username"),
                                Email = DbUtils.GetString(reader, "Email"),
                                FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                                CreatedAt = DbUtils.GetDateTime(reader, "CreatedAt")
                            });
                        }

                        return users;
                    }
                }
            }
        }
        public UserProfile GetByFirebaseId(string fbid)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT Id, Username, Email, FirebaseUserId, CreatedAt
                    FROM UserProfile
                    WHERE FirebaseUserId = @Id";

                    DbUtils.AddParameter(cmd, "@Id", fbid);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserProfile profile = null;
                        if (reader.Read())
                        {
                            profile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Username = DbUtils.GetString(reader, "Username"),
                                Email = DbUtils.GetString(reader, "Email"),
                                FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                                CreatedAt = DbUtils.GetDateTime(reader, "CreatedAt")
                            };
                        }

                        return profile;
                    }
                }
            }
        }
        public UserProfile GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT Id, Username, Email, FirebaseUserId, CreatedAt
                    FROM UserProfile
                    WHERE id = @Id";
                    
                    DbUtils.AddParameter(cmd, "@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        UserProfile profile = null;
                        if (reader.Read())
                        {
                            profile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Username = DbUtils.GetString(reader, "Username"),
                                Email = DbUtils.GetString(reader, "Email"),
                                FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                                CreatedAt = DbUtils.GetDateTime(reader, "CreatedAt")
                            };
                        }

                        return profile;
                    }
                }
            }
        }
        public void Add(UserProfile profile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO UserProfile (Username, Email, FirebaseUserId, CreatedAt)
                    OUTPUT INSERTED.ID
                    VALUES (@username, @email, @firebaseUserId, @createdAt)";
                    
                    DbUtils.AddParameter(cmd, "@username", profile.Username);
                    DbUtils.AddParameter(cmd, "@email", profile.Email);
                    DbUtils.AddParameter(cmd, "@firebaseUserId", profile.FirebaseUserId);
                    DbUtils.AddParameter(cmd, "@createdAt", DateTime.Now);

                    profile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
        public void Update(UserProfile profile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    UPDATE UserProfile 
                        SET Username = @username,
                        Email = @email
                    WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", profile.Id);
                    DbUtils.AddParameter(cmd, "@username", profile.Username);
                    DbUtils.AddParameter(cmd, "@email", profile.Email);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM UserProfile WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
