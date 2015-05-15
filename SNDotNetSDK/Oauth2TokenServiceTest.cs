using com.signnow.sdk.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace com.signnow.sdk.service.impl
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This test class is used to perform and test Token related operations.
     */
    [TestClass]
    public class Oauth2TokenServiceTest
    {
        private static IUserService userService;
        private static IAuthenticationService authenticationService;
        private string clientID = ConfigurationManager.AppSettings.Get("clientID");
        private string clientSecret = ConfigurationManager.AppSettings.Get("clientSecret");
        private string apibase = ConfigurationManager.AppSettings.Get("apibase");

        [ClassInitialize]
        public static void before(TestContext t)
        {
            userService = new UserService();
            authenticationService = new OAuth2TokenService();
        }

        [TestMethod]
        public void requestToken()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.email = randomEmail;
            user.password = "fakePassword";
            user.first_name = "firstName";
            user.last_name = "LastName";
            Config config = new Config(apibase, clientID, clientSecret);

            User resultUser = userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);
        }

        [TestMethod]
        public void verifyToken()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.email = randomEmail;
            user.password = "fakePassword";
            user.first_name = "firstName";
            user.last_name = "LastName";
            Config config = new Config(apibase, clientID, clientSecret);

            User resultUser = userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            Oauth2Token verifiedToken = authenticationService.verify(requestedToken);

            Assert.IsNotNull("Verify Token", verifiedToken.access_token);

            Assert.AreEqual(requestedToken.access_token, verifiedToken.access_token, "Verified");

            Assert.AreNotSame(requestedToken.access_token, verifiedToken.refresh_token, "Refresh Token");
        }
    }
}