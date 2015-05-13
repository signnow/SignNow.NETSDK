using com.signnow.sdk.model;

namespace com.signnow.sdk.service
{
    public interface IUserService
    {
        User create(User user);

        User get(User user);
    }
}
