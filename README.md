 SignNowDotNetSDK
 ===============
A UnitTest Project using REST Endpoints API.

#### About SignNow
SignNow by Barracuda is an eSigning platform that offers a cloud version, a physical appliance and also a virtual appliance. Backed by Barracuda’s industry-leading security infrastructure, SignNow is fully compliant with eSigning laws and encrypts all data in transit. Users can share, manage and access their documents with confidence. It’s never been easier to get legally binding signatures from customers, partners, and employees - in seconds using any device.

#### API Contact Information
If you have questions about the SignNow API, please visit https://techlib.barracuda.com/SignNow/RestEndpointsAPI or email [api@signnow.com](mailto:api@signnow.com).

See additional contact information at the bottom.

Examples
==================

To run the examples you will need an API key. You can get one here [https://signnow.com/l/api/request_information](https://signnow.com/l/api/request_information). 

Before start using the Rest API's using this project configure the Keys in (App.config) configuration file:

-- Client Id and Client Secret are the API keys which can be obtained as explained above.

-- Input Directory and Output Directory have to be created to upload or download a document to/from Sign Now Application.

--Log file will be generated under "SNDotNetSDK\bin\Debug" with the name (log.txt) and the log configuration file (log4.config) is also under this folder and also the config file can be changed for adding/removing Appenders etc. Details on how to modify the config file can be accessed here- (https://logging.apache.org/log4net/release/manual/configuration.html)

##User

To Create and Retrieve the User to/from SigNow Application use the UserServiceTest.

##Token 

To perform Token related operations use the Oauth2TokenServiceTest.

##Document Service

To perform and test the Document specific test operations use DocumentServiceTest.

For a full list of accepted parameters, refer to the SignNow REST Endpoints API guide: [https://techlib.barracuda.com/SignNow/RestEndpointsAPI](https://techlib.barracuda.com/SignNow/RestEndpointsAPI).


# Additional Contact Information

##### SUPPORT
To contact SignNow support, please email [support@signnow.com](mailto:support@signnow.com).

##### SALES
For pricing information, please call (800) 831-2050 or email [sales@signnow.com](mailto:sales@signnow.com).
