using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Notable.Models;
using Notable.Utils;
using System;
using System.Collections.Generic;

namespace Notable.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IConfiguration iconfig) : base(iconfig) { }

        public List<Note> GetNotes(int categoryId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT n.Id, n.UserProfileId, n.Content, n.CreatedAt, n.isPublic
                    FROM CategoryNote cn
                    JOIN Note n ON cn.NoteId = n.Id
                    WHERE cn.CategoryId = @id";

                    cmd.Parameters.AddWithValue("@id", categoryId);

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

        public List<Category> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT Id, [Name], UserProfileId
                    FROM Category";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var categories = new List<Category>();
                        while (reader.Read())
                        {
                            categories.Add(new Category()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId")
                            });
                        }
                        return categories;
                    }
                }
            }
        }
        public Category GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT Id, [Name], UserProfileId
                    FROM Category
                    WHERE id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Category category = null;
                        if (reader.Read())
                        {
                            category = new Category()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId")
                            };
                        }
                        return category;
                    }
                }
            }
        }
        public void Add(Category category)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Category (Name, UserProfileId)
                    OUTPUT INSERTED.ID
                    VALUES (@name, @userProfileId)";

                    DbUtils.AddParameter(cmd, "@name", category.Name);
                    DbUtils.AddParameter(cmd, "@userProfileId", category.UserProfileId);

                    category.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
        public void Update(Category category)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    UPDATE Category
                        SET Name = @name
                    WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", category.Id);
                    DbUtils.AddParameter(cmd, "@name", category.Name);
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
                    cmd.CommandText = "DELETE FROM Category WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
