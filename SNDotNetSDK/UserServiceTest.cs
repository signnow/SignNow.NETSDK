using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.signnow.sdk.model;

namespace com.signnow.sdk.service.impl
{
    [TestClass]
    public class UserServiceTest
    {
        private static IUserService userService;
        private static IAuthenticationService authenticationService;
        private string clientID = "adfb610205ab0b1cc23dddf43de3103d";
        private string clientSecret = "f972dd9947e9fe3b88d9e619eb492d95";
        private string apibase = "signnow.eval";

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

