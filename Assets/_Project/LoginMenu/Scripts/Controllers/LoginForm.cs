using System;
using Core.DI;
using UserManagement;
using Core.UIFramework;
using CommonUI.Controllers;

namespace LoaderScene.Controllers
{
    public class LoginForm : UIController
    {
        public readonly Observable<string> Email = new Observable<string>("");
        public readonly Observable<string> Password = new Observable<string>("");
        public readonly Observable<string> Message = new Observable<string>("");
        public readonly Observable<bool> Remember = new Observable<bool>(true);
        
        [Inject] private LoginAPI _loginAPI;        
        [Inject] private UserProfile _userProfile;
        [Inject] private WelcomeForm _welcomeForm;
        [Inject] private RecoveryForm _recoveryForm;
        [Inject] private AuthorizationPanel _authorizationPanel;

        protected override void OnAwake()
        {
            
        }
        
        public void Back()
        {
            Message.Set(string.Empty);
            _welcomeForm.Show();
            Hide();
        }
        
        public void LogIn()
        {
            Email.Set(Email.Get().TrimEnd());

            _loginAPI.Login(Email, Password, OnSuccessfulLogin, OnLoginError);
            RememberLoginFormData();
        }
        
        public void RestoreLoginFormData()
        {
            Tuple<string, string> savedLoginData = _userProfile.GetLoginFormData();
            Email.Set(savedLoginData.Item1);
            Password.Set(savedLoginData.Item2);

            bool rememberUser = !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
            Remember.Set(rememberUser);
        }        

        private void Start()
        {
            RestoreLoginFormData();
        }
        
        private void OnSuccessfulLogin()
        {
            Hide();
            _userProfile.IsAuthorized = true;
            _authorizationPanel.LaunchApplicationPanel();
        }
        
        private void OnLoginError(string errorMessage)
        {
            Message.Set("<color=red>" + errorMessage + "</color>");
        }

        private void RememberLoginFormData()
        {
            _userProfile.SaveLoginFormData(string.Empty, string.Empty);
            if (Remember.Get())
            {
                _userProfile.SaveLoginFormData(Email, Password);
            }
        }

        public void RecoveryPassword()
        {
            Hide();
            _recoveryForm.ResetState();
            _recoveryForm.Show();
        }
    }
}