using Core.UIFramework;
using CommonUI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace CommonUI.Presentations
{
	public sealed class GameMenuPresentation : UIPresentationModel<GameMenu>
	{
		[SerializeField] private Button _pauseButton;
		[SerializeField] private Button _applicationButton;

		private void OnEnable()
		{
			_pauseButton.onClick.AddListener(OnPauseButtonClick);
			_applicationButton.onClick.AddListener(OnApplicationButtonClick);
		}

		private void OnDisable()
		{
			_pauseButton.onClick.RemoveListener(OnPauseButtonClick);
			_applicationButton.onClick.RemoveListener(OnApplicationButtonClick);
		}

		private void OnPauseButtonClick()
		{
			_uiAnimator.ButtonScale(_pauseButton, Controller.ShowPauseMenu);
		}

		private void OnApplicationButtonClick()
		{
			_uiAnimator.ButtonScale(_applicationButton, Controller.ShowApplicationMenu);
		}
	}
}
