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
        static CopyClient copyclient;

        [ClassInitialize]
        public static void before(TestContext t)
        {
            Config config = new Config("ApiBAse", "Client-Id", "Client-Secret");
            copyclient = new CopyClient(config);
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

            User resultUser = copyclient.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = copyclient.authenticationService.requestToken(resultUser);
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

            User resultUser = copyclient.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = copyclient.authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            Oauth2Token verifiedToken = copyclient.authenticationService.verify(requestedToken.access_token);

            Assert.IsNotNull("Verify Token", verifiedToken.access_token);

            Assert.AreEqual(requestedToken.access_token, verifiedToken.access_token, "Verified");

            Assert.AreNotSame(requestedToken.access_token, verifiedToken.refresh_token, "Refresh Token");
        }
    }
}