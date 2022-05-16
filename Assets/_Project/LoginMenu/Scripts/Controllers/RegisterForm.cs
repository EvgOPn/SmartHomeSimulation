using Core.DI;
using UserManagement;
using Core.UIFramework;

namespace LoaderScene.Controllers
{
    public class RegisterForm : UIController
    {
        public readonly Observable<string> Email = new Observable<string>("");
        public readonly Observable<string> Password = new Observable<string>("");
        public readonly Observable<string> Username = new Observable<string>("");
        public readonly Observable<string> Message = new Observable<string>("");
        public readonly Observable<bool> Remember = new Observable<bool>(true);
        
        [Inject] private LoginAPI _loginAPI;
        [Inject] private WelcomeForm _welcomeForm;
        //[Inject] private ConfirmForm _confirmForm;
        [Inject] private UserProfile _userProfile;
        [Inject] private AvatarForm _avatarForm;

        private const float DelayBeforeShowLoginForm = 1.0f;
        
        protected override void OnAwake()
        {
            
        }

        public void Back()
        {
            Hide();
            _welcomeForm.Show();
        }

        public void CreateAccount()
        {
            RememberLoginFormData();
            _loginAPI.RegisterAccount(Email, Password, Username, SuccessfulRegistration, RegistrationError);
        }
        
        private void RememberLoginFormData()
        {
            _userProfile.SaveLoginFormData(string.Empty, string.Empty);
            if (Remember.Get())
            {
                _userProfile.SaveLoginFormData(Email, Password);
            }
        }
        
        private void RegistrationError(string errorMessage)
        {
            Message.Set("<color=red>" + errorMessage + "</color>");
        }

        private void SuccessfulRegistration()
        {
            Message.Set("<color=green>Successful Registration</color>");
            Invoke(nameof(ShowLoginForm), DelayBeforeShowLoginForm);
        }
        
        public void ShowLoginForm()
        {
            Hide();
            //_confirmForm.Show();
            _avatarForm.Show();
        } 
    }
}