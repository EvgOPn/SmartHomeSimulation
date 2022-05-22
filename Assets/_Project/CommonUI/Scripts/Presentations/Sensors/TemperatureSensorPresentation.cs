using CommonUI.Controllers.Sensors;
using Core.UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace CommonUI.Presentations.Sensors
{
	public sealed class TemperatureSensorPresentation : UIPresentationModel<TemperatureSensor>
	{
		[SerializeField] private Toggle _enabledToggle;
		[SerializeField] private Button _acceptButton;

		private void OnEnable()
		{
			//_enabledToggle.onValueChanged.AddListener(OnEnabledToggleSwitched);
			_acceptButton.onClick.AddListener(OnAcceptButtonClick);

			Controller.IsEnabled.onChanged.AddListener(InitializeToggle);
		}

		private void OnDisable()
		{
			_enabledToggle.onValueChanged.RemoveListener(OnEnabledToggleSwitched);
			_acceptButton.onClick.RemoveListener(OnAcceptButtonClick);

			Controller.IsEnabled.onChanged.RemoveListener(InitializeToggle);
		}

		private void OnEnabledToggleSwitched(bool enabled)
		{
			_acceptButton.gameObject.SetActive(true);
		}

		private void OnAcceptButtonClick()
		{
			_uiAnimator.ButtonScale(_acceptButton, () =>
			{
				Controller.UpdateSensorState(_enabledToggle.isOn);
				_acceptButton.gameObject.SetActive(false);
			});
		}

		private void InitializeToggle(bool enabled)
		{
			_enabledToggle.Set(enabled);
			_enabledToggle.onValueChanged.AddListener(OnEnabledToggleSwitched);
		}
	}
}
