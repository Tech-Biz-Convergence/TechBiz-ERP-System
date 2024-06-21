using BusinessEntities.Identity;
namespace IdentityService.Repository
{
    public class UserRepository:IUserRepository
    {
        public tbm_user_info? GetUser(string username)
        {
                 List<tbm_user_info> users = new List<tbm_user_info>
                {
                new tbm_user_info()
                {
                    user_id = 1,
                    user_name = "game",
                    password = "rn97TKvDs8UE6LHXH8dp8rPC3PcPUhCmI/xJKqwKwhDTMvg0Ewio9A==",
                    user_email = "game@gmail.com",
                    salt = "1DG0ecvn0JmBdU2fiGvxOa+ezns1/cjuRpFBUzj00rauwV8le3mN3A==",
                    isAdmin = true
                },
                new tbm_user_info
                {
                    user_id = 2,
                    user_name = "test@test.com",
                    password = "hashedPassword2",
                    user_email = "test@gmail.com",
                    salt = "salt2",
                    isAdmin = false
                }
            };
            return users.FirstOrDefault(user => user.user_email == username);

        }

        public void InsertUser(tbm_user_info user)
        {

        }
    }
}
