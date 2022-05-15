using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Core.UIFramework;
using LoaderScene.Controllers;

namespace LoaderScene.Presentations
{
    public class RecoveryFormPresentation : UIPresentationModel<RecoveryForm>
    {
        [SerializeField] private Button _confirmEmailButton;
        [SerializeField] private Button _confirmPinButton;
        [SerializeField] private Button _confirmPasswordButton;
        [SerializeField] private Button _doneButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private TextMeshProUGUI _errorMessage;
        
        [SerializeField] private TMP_InputField _pinField;
        [SerializeField] private TMP_InputField _emailField;
        [SerializeField] private TMP_InputField _passwordField;
        
        [SerializeField] private GameObject _pinDialog;
        [SerializeField] private GameObject _emailDialog;
        [SerializeField] private GameObject _passwordDialog;
        [SerializeField] private GameObject _finishDialog;

        private void Start()
        {
            _pinDialog.SetActive(Controller.PinDialogEnabled.Get());
            _emailDialog.SetActive(Controller.EmailDialogEnabled.Get());
            _finishDialog.SetActive(Controller.FinishDialogEnabled.Get());
            _passwordDialog.SetActive(Controller.PasswordDialogEnabled.Get());
        }
        
        private void OnEnable()
        {
            _backButton.onClick.AddListener(Back);
            _doneButton.onClick.AddListener(Done);
            _confirmPinButton.onClick.AddListener(ConfirmPin);
            _confirmEmailButton.onClick.AddListener(ConfirmEmail);
            _confirmPasswordButton.onClick.AddListener(ConfirmPassword);
            _pinField.onValueChanged.AddListener(Controller.Pin.Set);
            _emailField.onValueChanged.AddListener(Controller.Email.Set);
            _passwordField.onValueChanged.AddListener(Controller.Password.Set);
            Controller.PinDialogEnabled.onChanged.AddListener(_pinDialog.SetActive);
            Controller.EmailDialogEnabled.onChanged.AddListener(_emailDialog.SetActive);
            Controller.FinishDialogEnabled.onChanged.AddListener(_finishDialog.SetActive);
            Controller.PasswordDialogEnabled.onChanged.AddListener(_passwordDialog.SetActive);
            
            Controller.Pin.onChanged.AddListener(_pinField.Set);
            Controller.Email.onChanged.AddListener(_emailField.Set);
            Controller.Password.onChanged.AddListener(_passwordField.Set);
            Controller.ErrorMessage.onChanged.AddListener(_errorMessage.Set);
        }
        
        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(Back);
            _doneButton.onClick.RemoveListener(Done);
            _confirmPinButton.onClick.RemoveListener(ConfirmPin);
            _confirmEmailButton.onClick.RemoveListener(ConfirmEmail);
            _confirmPasswordButton.onClick.RemoveListener(ConfirmPassword);
            _pinField.onValueChanged.RemoveListener(Controller.Pin.Set);
            _emailField.onValueChanged.RemoveListener(Controller.Email.Set);
            _passwordField.onValueChanged.RemoveListener(Controller.Password.Set);
            Controller.PinDialogEnabled.onChanged.RemoveListener(_pinDialog.SetActive);
            Controller.EmailDialogEnabled.onChanged.RemoveListener(_emailDialog.SetActive);
            Controller.FinishDialogEnabled.onChanged.RemoveListener(_finishDialog.SetActive);
            Controller.PasswordDialogEnabled.onChanged.RemoveListener(_passwordDialog.SetActive);
            
            Controller.Pin.onChanged.RemoveListener(_pinField.Set);
            Controller.Email.onChanged.RemoveListener(_emailField.Set);
            Controller.Password.onChanged.RemoveListener(_passwordField.Set);
            Controller.ErrorMessage.onChanged.RemoveListener(_errorMessage.Set);
        }

        private void ConfirmEmail()
        {
            _uiAnimator.ButtonScale(_confirmEmailButton, Controller.ConfirmEmail);
        }
        
        private void ConfirmPin()
        {
            _uiAnimator.ButtonScale(_confirmPinButton, Controller.ConfirmPin);
        }

        private void ConfirmPassword()
        {
            _uiAnimator.ButtonScale(_confirmPasswordButton, Controller.ConfirmPassword);
        }

        private void Done()
        {
            _uiAnimator.ButtonScale(_doneButton, Controller.Finish);
        }

        private void Back()
        {
            _uiAnimator.ButtonScale(_doneButton, Controller.BackToLoginForm);
        }
    }
}