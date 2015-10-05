using SNDotNetSDK.Configuration;
using SNDotNetSDK.Service;
using SNDotNetSDK.ServiceImpl;

namespace SNDotNetSDK
{
    public class CudaSign
    {
        private Config config;

        public CudaSign(IConfig config)
        {
            this.config = (Config)config;
            InitServices();
        }

        private void InitServices()
        {
            userService = new UserService(config);
            authenticationService = new OAuth2TokenService(config);
            documentService = new DocumentService(config);
        }

        public IUserService userService { get; private set; }

        public IAuthenticationService authenticationService { get; private set; }

        public IDocumentService documentService { get; private set; }

    }
}
