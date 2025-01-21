using Firebase.Auth;

namespace Project.Services
{
    public  class UserProvider : IProvider<FirebaseUser>
    {
        public FirebaseUser Value { get; set; }
    }
}