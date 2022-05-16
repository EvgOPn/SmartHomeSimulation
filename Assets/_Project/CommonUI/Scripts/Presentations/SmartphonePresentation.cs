using Core.UIFramework;
using CommonUI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace CommonUI.Presentations
{
	public sealed class SmartphonePresentation : UIPresentationModel<Smartphone>
	{
		[SerializeField] private GameObject _authorizationPanel;
		[SerializeField] private GameObject _applicationPanel;

		[SerializeField] private Button _hideButton;

		private void OnEnable()
		{
			_hideButton.onClick.AddListener(OnHideButtonClick);

			Controller.AuthorizationPanelEnabled.onChanged.AddListener(_authorizationPanel.SetActive);
			Controller.ApplicationPanelEnabled.onChanged.AddListener(_applicationPanel.SetActive);
		}

		private void OnDisable()
		{
			_hideButton.onClick.RemoveListener(OnHideButtonClick);

			Controller.AuthorizationPanelEnabled.onChanged.RemoveListener(_authorizationPanel.SetActive);
			Controller.ApplicationPanelEnabled.onChanged.RemoveListener(_applicationPanel.SetActive);
		}

		private void OnHideButtonClick()
		{
			_uiAnimator.ButtonScale(_hideButton, Controller.HideSmartphone);
		}
	}
}
