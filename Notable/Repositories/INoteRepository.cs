using Notable.Models;
using System.Collections.Generic;

namespace Notable.Repositories
{
    public interface INoteRepository
    {
        void Add(Note note);
        void Delete(int id);
        List<Note> GetAll();
        Note GetById(int id);
        void Update(Note note);
        List<Note> GetAllByUser(int userId);
        void AddCategoryNote(CategoryNote categoryNote);
    }
}