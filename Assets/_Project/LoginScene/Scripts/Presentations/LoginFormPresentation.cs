using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Core.UIFramework;
using LoaderScene.Controllers;

namespace LoaderScene.Presentations
{
    public class LoginFormPresentation : UIPresentationModel<LoginForm>
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _enterButton;
        [SerializeField] private Button _recoveryButton;
        [SerializeField] private Toggle _rememberToggle;
        [SerializeField] private TMP_InputField _emailField;
        [SerializeField] private TMP_InputField _passwordField;
        [SerializeField] private TextMeshProUGUI _errorLabel;

        private void OnEnable()
        {
            _backButton.onClick.AddListener(Back);
            _enterButton.onClick.AddListener(LogIn);
            _recoveryButton.onClick.AddListener(ForgotPassword);
            _emailField.onValueChanged.AddListener(Controller.Email.Set);
            _passwordField.onValueChanged.AddListener(Controller.Password.Set);
            _rememberToggle.onValueChanged.AddListener(Controller.Remember.Set);
            
            Controller.Remember.onChanged.AddListener(_rememberToggle.Set);
            Controller.Message.onChanged.AddListener(_errorLabel.Set);
            Controller.Password.onChanged.AddListener(_passwordField.Set);
            Controller.Email.onChanged.AddListener(_emailField.Set);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(Back);
            _enterButton.onClick.RemoveListener(LogIn);
            _recoveryButton.onClick.RemoveListener(ForgotPassword);
            _emailField.onValueChanged.RemoveListener(Controller.Email.Set);
            _passwordField.onValueChanged.RemoveListener(Controller.Password.Set);
            _rememberToggle.onValueChanged.RemoveListener(Controller.Remember.Set);
            
            Controller.Remember.onChanged.RemoveListener(_rememberToggle.Set);
            Controller.Message.onChanged.RemoveListener(_errorLabel.Set);
            Controller.Password.onChanged.RemoveListener(_passwordField.Set);
            Controller.Email.onChanged.RemoveListener(_emailField.Set);
        }
        
        private void Back()
        {
            _uiAnimator.ButtonScale(_backButton, Controller.Back);
        }        
        
        private void LogIn()
        {
            _uiAnimator.ButtonScale(_enterButton, Controller.LogIn);
        }

        private void ForgotPassword()
        {
            _uiAnimator.ButtonScale(_recoveryButton, Controller.RecoveryPassword);
        }
    }
}