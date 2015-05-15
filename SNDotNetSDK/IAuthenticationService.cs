using com.signnow.sdk.model;

namespace com.signnow.sdk.service
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This interface is used to perform to OAuth2 specific operations in the SignNow Application.
     */
    public interface IAuthenticationService
    {
        /**
         * Requests an OAuth2 token for the user. User's email, password, and grantType are required. The scope attribute
         * is optional.
         *
         * @param user
         * @return a user with the system generated OAuth2 credentials
         */
        Oauth2Token requestToken(User user);

        /**
        * Verify that an OAuth2 token is still valid for some user.
        *
        * @return
        */
        Oauth2Token verify(Oauth2Token token);
    }
}