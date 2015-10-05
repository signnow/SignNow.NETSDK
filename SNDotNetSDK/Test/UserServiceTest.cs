using Microsoft.VisualStudio.TestTools.UnitTesting;
using SNDotNetSDK.Configuration;
using SNDotNetSDK.Models;
using SNDotNetSDK.Service;
using System;
using System.Configuration;

namespace SNDotNetSDK.Test
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This test class is used to create and retrieve the user to/from the Sign Now Application
     */
    [TestClass]
    public class UserServiceTest
    {
        static CudaSign cudasign;
        [ClassInitialize]
        public static void before(TestContext t)
        {
            Config config = new Config("ApiBAse", "Client-Id", "Client-Secret");
            cudasign = new CudaSign(config);
        }

        [TestMethod]
        public void createUser() {
        String randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";

        User resultUser = cudasign.userService.create(user);
        
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

            User resultUser = cudasign.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = cudasign.authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            resultUser.oauth2Token = requestedToken;

            User getUser = cudasign.userService.get(resultUser.oauth2Token.access_token);
            Assert.AreEqual(resultUser.id, getUser.id, "Found");
        }
    }
}