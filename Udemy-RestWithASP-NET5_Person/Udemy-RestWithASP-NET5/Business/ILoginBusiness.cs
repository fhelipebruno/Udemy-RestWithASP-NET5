using Udemy_RestWithASP_NET5.Data.Converter.VO;

namespace Udemy_RestWithASP_NET5.Business {
    public interface ILoginBusiness {
        TokenVO ValidateCredentials(UserVO user);
        TokenVO ValidateCredentials(TokenVO token);
    }
}
