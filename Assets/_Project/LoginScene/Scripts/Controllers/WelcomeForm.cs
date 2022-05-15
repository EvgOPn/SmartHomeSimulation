using Core.DI;
using Core.UIFramework;

namespace LoaderScene.Controllers
{
    public class WelcomeForm : UIController
    {
        [Inject] private LoginForm _loginForm;
        [Inject] private RegisterForm _registerForm;
        
        protected override void OnAwake()
        {
        }

        private void Start()
        {
            Show();
        }

        public void ShowLoginForm()
        {
            Hide();
            _loginForm.Show();
        }

        public void ShowRegisterForm()
        {
            Hide();
            _registerForm.Show();
        }
    }
}