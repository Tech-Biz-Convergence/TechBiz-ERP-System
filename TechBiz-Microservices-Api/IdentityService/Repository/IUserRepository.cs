using BusinessEntities.Identity;


namespace IdentityService.Repository
{
    public interface IUserRepository
    {
        tbm_user_info? GetUser(string username);
        void InsertUser(tbm_user_info user);
    }
}
