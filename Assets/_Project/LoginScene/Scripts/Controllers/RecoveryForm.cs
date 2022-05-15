using Core.DI;
using UserManagement;
using Core.UIFramework;

namespace LoaderScene.Controllers
{
    public class RecoveryForm : UIController
    {
        public readonly Observable<string> Pin = new Observable<string>("");
        public readonly Observable<string> Email = new Observable<string>("");
        public readonly Observable<string> Password = new Observable<string>("");
        public readonly Observable<string> ErrorMessage = new Observable<string>("");
        
        public readonly Observable<bool> PinDialogEnabled = new Observable<bool>(false);
        public readonly Observable<bool> EmailDialogEnabled = new Observable<bool>(true);
        public readonly Observable<bool> FinishDialogEnabled = new Observable<bool>(false);
        public readonly Observable<bool> PasswordDialogEnabled = new Observable<bool>(false);

        [Inject] private LoginAPI _loginAPI;
        [Inject] private LoginForm _loginForm;

        protected override void OnAwake()
        {
            
        }

        public void ConfirmEmail()
        {            
        }

        public void ConfirmPin()
        {
            PinDialogEnabled.Set(false);
            PasswordDialogEnabled.Set(true);
        }
        
        public void ConfirmPassword()
        {           
        }

        public void Finish()
        {
            BackToLoginForm();
        }

        public void BackToLoginForm()
        {
            Hide();
            _loginForm.Show();
        }
    }
}