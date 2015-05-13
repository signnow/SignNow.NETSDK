using com.signnow.sdk.model;

namespace com.signnow.sdk.service
{
    public interface IAuthenticationService
    {
        Oauth2Token requestToken(User user);

        Oauth2Token verify(Oauth2Token token);
    }
}
