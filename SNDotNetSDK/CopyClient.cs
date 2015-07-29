using SNDotNetSDK.Configuration;
using SNDotNetSDK.Service;
using SNDotNetSDK.ServiceImpl;

namespace SNDotNetSDK
{
    public class CopyClient
    {
        private Config copyConfig;

        public CopyClient(IConfig copyConfig)
        {
            this.copyConfig = (Config) copyConfig;
            InitServices();
        }

        private void InitServices()
        {
            userService = new UserService(copyConfig);
            authenticationService = new OAuth2TokenService(copyConfig);
            documentService = new DocumentService(copyConfig);
        }

        public IUserService userService { get; private set; }

        public IAuthenticationService authenticationService { get; private set; }

        public IDocumentService documentService { get; private set; }

    }
}
