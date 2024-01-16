using System.Security.Claims;

namespace Udemy_RestWithASP_NET5.Services {
    public interface ITokenService {
        string GenerateAcessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
