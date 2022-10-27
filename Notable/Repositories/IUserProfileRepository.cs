using Notable.Models;
using System.Collections.Generic;

namespace Notable.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile profile);
        void Delete(int id);
        List<UserProfile> GetAll();
        UserProfile GetById(int id);
        UserProfile GetByFirebaseId(string fbid);
        void Update(UserProfile profile);
    }
}