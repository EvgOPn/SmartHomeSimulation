using Core.DI;
using Core.UIFramework;

namespace LoaderScene.Controllers
{
    public class ConfirmForm : UIController
    {        
        [Inject] private RegisterForm _registerForm;

        public readonly Observable<bool> ConfirmButtonEnabled = new Observable<bool>(false);

        protected override void OnAwake()
        {
            
        }

        public void CheckPin(string value)
        {
            if (value.Length == 4)
            {
                ConfirmButtonEnabled.Set(true);
            }
        }

        public void Back()
        {
            Hide();
            _registerForm.Show();
        }

        public void ConfirmEmail()
        {
            Hide();            
        }
    }
}