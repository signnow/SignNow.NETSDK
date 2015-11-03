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
        public static void Before(TestContext t)
        {
            Config config = new Config("apiBase", "clientId", "clientSecret");
            cudasign = new CudaSign(config);
        }

        [TestMethod]
        public void CreateUser() {
        String randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.Email = randomEmail;
        user.Password = "fakePassword";
        user.FirstName = "firstName";
        user.LastName = "LastName";

        User resultUser = cudasign.userService.Create(user);
        
        Assert.IsNotNull("No user id from creating user", resultUser.Id);
        
        Console.WriteLine(resultUser.Email + " " + resultUser.Id);
       }

        [TestMethod]
        public void GetUser()
        {
            String randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.Email = randomEmail;
            user.Password = "fakePassword";
            user.FirstName = "firstName";
            user.LastName = "LastName";

            User resultUser = cudasign.userService.Create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.Id);
            resultUser.Password = "fakePassword";

            Oauth2Token requestedToken = cudasign.authenticationService.RequestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.AccessToken);

            resultUser.OAuth2Token = requestedToken;

            User getUser = cudasign.userService.Get(resultUser.OAuth2Token.AccessToken);
            Assert.AreEqual(resultUser.Id, getUser.Id, "Found");
        }
    }
}