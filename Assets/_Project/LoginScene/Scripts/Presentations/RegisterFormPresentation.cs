using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Core.UIFramework;
using LoaderScene.Controllers;

namespace LoaderScene.Presentations
{
    public class RegisterFormPresentation : UIPresentationModel<RegisterForm>
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _createButton;
        [SerializeField] private Toggle _rememberToggle;
        [SerializeField] private TMP_InputField _emailField;
        [SerializeField] private TMP_InputField _passwordField;
        [SerializeField] private TMP_InputField _usernameField;
        [SerializeField] private TextMeshProUGUI _messageLabel;

        private void OnEnable()
        {
            _backButton.onClick.AddListener(Back);
            _createButton.onClick.AddListener(CreateAccount);
            _emailField.onValueChanged.AddListener(Controller.Email.Set);
            _passwordField.onValueChanged.AddListener(Controller.Password.Set);
            _usernameField.onValueChanged.AddListener(Controller.Username.Set);
            _rememberToggle.onValueChanged.AddListener(Controller.Remember.Set);
            
            Controller.Message.onChanged.AddListener(_messageLabel.Set);
        }
        
        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(Back);
            _createButton.onClick.RemoveListener(CreateAccount);
            _emailField.onValueChanged.RemoveListener(Controller.Email.Set);
            _passwordField.onValueChanged.RemoveListener(Controller.Password.Set);
            _usernameField.onValueChanged.RemoveListener(Controller.Username.Set);
            _rememberToggle.onValueChanged.RemoveListener(Controller.Remember.Set);
            
            Controller.Message.onChanged.RemoveListener(_messageLabel.Set);
        }
        
        private void CreateAccount()
        {
            _uiAnimator.ButtonScale(_createButton, Controller.CreateAccount);
        }

        private void Back()
        {
            _uiAnimator.ButtonScale(_backButton, Controller.Back);
        }
    }
}