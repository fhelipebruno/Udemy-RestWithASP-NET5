using Udemy_RestWithASP_NET5.Data.Converter.VO;
using Udemy_RestWithASP_NET5.Model;

namespace Udemy_RestWithASP_NET5.Repository {
    public interface IUserRepository {
        User ValidateCredentials(UserVO user);
        User ValidateCredentials(string user);
        User RefreshUserInfo(User user);
    }
}
