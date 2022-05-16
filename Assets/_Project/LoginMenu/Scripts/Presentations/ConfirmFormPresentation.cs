using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Core.UIFramework;
using LoaderScene.Controllers;

namespace LoaderScene.Presentations
{
    public class ConfirmFormPresentation : UIPresentationModel<ConfirmForm>
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private TMP_InputField _pinField;

        private void Start()
        {
            _confirmButton.enabled = false;
        }

        private void OnEnable()
        {
            _backButton.onClick.AddListener(Back);
            _confirmButton.onClick.AddListener(ConfirmEmail);
            _pinField.onValueChanged.AddListener(Controller.CheckPin);
            Controller.ConfirmButtonEnabled.onChanged.AddListener(ConfirmButtonEnabledChanged);
        }
        
        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(Back);
            _confirmButton.onClick.RemoveListener(ConfirmEmail);
            _pinField.onValueChanged.RemoveListener(Controller.CheckPin);
            Controller.ConfirmButtonEnabled.onChanged.RemoveListener(ConfirmButtonEnabledChanged);
        }
        
        private void ConfirmButtonEnabledChanged(bool enabled)
        {
            _confirmButton.enabled = enabled;
        }
        
        private void Back()
        {
            _uiAnimator.ButtonScale(_backButton, Controller.Back);
        }
        
        private void ConfirmEmail()
        {
            _uiAnimator.ButtonScale(_confirmButton, Controller.ConfirmEmail);
        }
    }
}