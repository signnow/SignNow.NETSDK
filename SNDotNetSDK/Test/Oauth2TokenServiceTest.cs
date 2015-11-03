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
     * This test class is used to perform and test Token related operations.
     */
    [TestClass]
    public class Oauth2TokenServiceTest
    {
        static CudaSign cudasign;

        [ClassInitialize]
        public static void Before(TestContext t)
        {
            Config config = new Config("apiBase", "clientId", "clientSecret");
            cudasign = new CudaSign(config);
        }

        [TestMethod]
        public void RequestToken()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
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
        }

        [TestMethod]
        public void VerifyToken()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
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

            Oauth2Token verifiedToken = cudasign.authenticationService.Verify(requestedToken.AccessToken);

            Assert.IsNotNull("Verify Token", verifiedToken.AccessToken);

            Assert.AreEqual(requestedToken.AccessToken, verifiedToken.AccessToken, "Verified");

            Assert.AreNotSame(requestedToken.AccessToken, verifiedToken.RefreshToken, "Refresh Token");
        }
    }
}