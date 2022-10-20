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

        public List<Note> GetAllByUser(int userId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT Id, UserProfileId, Content, CreatedAt, isPublic
                    FROM Note
                    WHERE UserProfileId = @userId";

                    cmd.Parameters.AddWithValue("@userId",userId);

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
                                CreatedAt = DbUtils.GetDateTime(reader, "CreatedAt"),
                                IsPublic = DbUtils.GetBool(reader, "isPublic")
                            });
                        }
                        return notes;
                    }
                }
            }
        }
        public void AddCategoryNote(CategoryNote categoryNote)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO CategoryNote (CategoryId, NoteId, UserProfileId)
                    OUTPUT INSERTED.ID
                    VALUES (@categoryId, @noteId, @userId)";

                    DbUtils.AddParameter(cmd, "@categoryId", categoryNote.CategoryId);
                    DbUtils.AddParameter(cmd, "@noteId", categoryNote.NoteId);
                    DbUtils.AddParameter(cmd, "@userId", categoryNote.UserProfileId);

                    categoryNote.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
        public void RemoveCategoryNote(int categoryNoteId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM CategoryNote WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", categoryNoteId);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void AddLike(NoteLike noteLike)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO NoteLike (NoteId, UserProfileId)
                    OUTPUT INSERTED.ID
                    VALUES (@noteId, @userId)";

                    DbUtils.AddParameter(cmd, "@noteId", noteLike.NoteId);
                    DbUtils.AddParameter(cmd, "@userId", noteLike.UserProfileId);

                    noteLike.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
        public void RemoveLike(int likeId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM NoteLike WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", likeId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddComment(NoteComment noteComment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO NoteComment (NoteId, UserProfileId, Content, CreatedAt)
                    OUTPUT INSERTED.ID
                    VALUES (@noteId, @userId, @content, @createdAt)";

                    DbUtils.AddParameter(cmd, "@noteId", noteComment.NoteId);
                    DbUtils.AddParameter(cmd, "@userId", noteComment.UserProfileId);
                    DbUtils.AddParameter(cmd, "@content", noteComment.Content);
                    DbUtils.AddParameter(cmd, "@createdAt", DateTime.Now);

                    noteComment.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
        public void RemoveComment(int commentId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Comment WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", commentId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Note> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT Id, UserProfileId, Content, CreatedAt, isPublic
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
                                CreatedAt = DbUtils.GetDateTime(reader, "CreatedAt"),
                                IsPublic = DbUtils.GetBool(reader, "isPublic")
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
                    SELECT Id, UserProfileId, Content, CreatedAt, isPublic
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
                                CreatedAt = DbUtils.GetDateTime(reader, "CreatedAt"),
                                IsPublic = DbUtils.GetBool(reader, "isPublic")
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
                    INSERT INTO Note (UserProfileId, Content, CreatedAt, isPublic)
                    OUTPUT INSERTED.ID
                    VALUES (@userProfileId, @content, @createdAt, @public)";

                    DbUtils.AddParameter(cmd, "@userProfileId", note.UserProfileId);
                    DbUtils.AddParameter(cmd, "@content", note.Content);
                    DbUtils.AddParameter(cmd, "@public", note.IsPublic);
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
                        SET Content = @content, isPublic = @public
                    WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", note.Id);
                    DbUtils.AddParameter(cmd, "@content", note.Content);
                    DbUtils.AddParameter(cmd, "@public", note.IsPublic);
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
