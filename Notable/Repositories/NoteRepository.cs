using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Notable.Models;
using Notable.Utils;
using System;
using System.Collections.Generic;

namespace Notable.Repositories
{
    public class NoteRepository : BaseRepository, INoteRepository
    {
        public NoteRepository(IConfiguration iconfig) : base(iconfig) { }
        public List<Note> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT Id, UserProfileId, Content, CreatedAt
                    FROM Note";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var notes = new List<Note>();
                        while (reader.Read())
                        {
                            notes.Add(new Note()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                Content = DbUtils.GetString(reader, "Content"),
                                CreatedAt = DbUtils.GetDateTime(reader, "CreatedAt")
                            });
                        }
                        return notes;
                    }
                }
            }
        }
        public Note GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT Id, UserProfileId, Content, CreatedAt
                    FROM Note
                    WHERE id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Note note = null;
                        if (reader.Read())
                        {
                            note = new Note()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                Content = DbUtils.GetString(reader, "Content"),
                                CreatedAt = DbUtils.GetDateTime(reader, "CreatedAt")
                            };
                        }
                        return note;
                    }
                }
            }
        }
        public void Add(Note note)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Note (UserProfileId, Content, CreatedAt)
                    OUTPUT INSERTED.ID
                    VALUES (@userProfileId, @content, @createdAt)";

                    DbUtils.AddParameter(cmd, "@userProfileId", note.UserProfileId);
                    DbUtils.AddParameter(cmd, "@content", note.Content);
                    DbUtils.AddParameter(cmd, "@createdAt", DateTime.Now);

                    note.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
        public void Update(Note note)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    UPDATE Note 
                        SET Content = @content
                    WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", note.Id);
                    DbUtils.AddParameter(cmd, "@content", note.Content);
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
                    cmd.CommandText = "DELETE FROM Note WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
