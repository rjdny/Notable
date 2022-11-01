using Notable.Models;
using System.Collections.Generic;

namespace Notable.Repositories
{
    public interface ICategoryRepository
    {
        List<Note> GetNotes(int categoryId);
        public List<Category> GetCategories(int noteId);
        void Add(Category category);
        void Delete(int id);
        List<Category> GetAll(int userId);
        Category GetById(int id);
        void Update(Category category);

    }
}