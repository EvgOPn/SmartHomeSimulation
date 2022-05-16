using Core.UIFramework;
using CommonUI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace CommonUI.Presentations
{
	public sealed class PausePanelPresentation : UIPresentationModel<PausePanel>
	{
		[SerializeField] private Button _backButton;
		[SerializeField] private Button _exitButton;

		private void OnEnable()
		{
			_backButton.onClick.AddListener(OnBackeButtonClick);
			_exitButton.onClick.AddListener(OnExitButtonClick);
		}

		private void OnDisable()
		{
			_backButton.onClick.RemoveListener(OnBackeButtonClick);
			_exitButton.onClick.RemoveListener(OnExitButtonClick);
		}

		private void OnBackeButtonClick()
		{
			_uiAnimator.ButtonScale(_backButton, Controller.HidePauseMenu);
		}

		private void OnExitButtonClick()
		{
			_uiAnimator.ButtonScale(_exitButton, Controller.ExitFromApplication);
		}
	}
}
