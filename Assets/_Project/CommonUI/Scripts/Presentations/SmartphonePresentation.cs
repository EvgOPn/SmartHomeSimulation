using Core.UIFramework;
using CommonUI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace CommonUI.Presentations
{
	public sealed class SmartphonePresentation : UIPresentationModel<Smartphone>
	{
		[SerializeField] private Button _hideButton;

		private void OnEnable()
		{
			_hideButton.onClick.AddListener(OnHideButtonClick);
		}

		private void OnDisable()
		{
			_hideButton.onClick.RemoveListener(OnHideButtonClick);
		}

		private void OnHideButtonClick()
		{
			_uiAnimator.ButtonScale(_hideButton, Controller.HideSmartphone);
		}
	}
}
