using com.signnow.sdk.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace com.signnow.sdk.service.impl
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This test class is used to create and retrieve the user to/from the Sign Now Application
     */
    [TestClass]
    public class UserServiceTest
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
        public void createUser() {
        String randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);

        User resultUser = userService.create(user);
        
        Assert.IsNotNull("No user id from creating user", resultUser.id);
        
        Console.WriteLine(resultUser.email + " " + resultUser.id);
       }

        [TestMethod]
        public void getUser()
        {
            String randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
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

            resultUser.oauth2Token = requestedToken;

            User getUser = userService.get(resultUser);
            Assert.AreEqual(resultUser.id, getUser.id, "Found");
        }
    }
}