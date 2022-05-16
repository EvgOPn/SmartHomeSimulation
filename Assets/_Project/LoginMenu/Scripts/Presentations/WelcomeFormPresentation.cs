using UnityEngine;
using UnityEngine.UI;
using Core.UIFramework;
using LoaderScene.Controllers;

namespace LoaderScene.Presentations
{
    public class WelcomeFormPresentation : UIPresentationModel<WelcomeForm>
    {
        [SerializeField] private Button _loginButton;
        [SerializeField] private Button _registerButton;

        protected override void OnAwake()
        {
            base.OnAwake();
            Screen.orientation = ScreenOrientation.Portrait;
        }
        
        private void OnEnable()
        {
            _loginButton.onClick.AddListener(ShowLoginForm);
            _registerButton.onClick.AddListener(ShowRegisterForm);
        }
        
        private void OnDisable()
        {
            _loginButton.onClick.RemoveListener(ShowLoginForm);
            _registerButton.onClick.RemoveListener(ShowRegisterForm);
        }

        private void ShowLoginForm()
        {
            _uiAnimator.ButtonScale(_loginButton, Controller.ShowLoginForm);
        }

        private void ShowRegisterForm()
        {
            _uiAnimator.ButtonScale(_registerButton, Controller.ShowRegisterForm);
        }
    }
}