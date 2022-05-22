using Core.DI;
using Core.UIFramework;
using SensorsManagement;
using SensorsManagement.Enums;
using System;
using System.Collections;
using SensorsManagement.Interfaces;
using UnityEngine;
using UnityEngine.Playables;
using UserManagement;

namespace CommonUI.Controllers.Sensors
{
	public sealed class TemperatureSensor : UIController, ISensorSignalReceiver
	{
		[SerializeField] private PlayableDirector _temperatureSensorTimeline;

		public readonly Observable<bool> IsEnabled = new Observable<bool>(false);

		[Inject] private SensorsSignalReceiver _sensorsSignalReceiver;
		[Inject] private UserProfile _userProfile;

		private SensorType _currentSensorType = SensorType.TemperatureSensore;

		protected override void OnAwake()
		{

		}

		private void OnEnable()
		{
			_sensorsSignalReceiver.AddSensorSignalReceiver(this);
		}

		private void OnDisable()
		{
			_sensorsSignalReceiver.RemoveSensorSignalReceiver(this);
		}

		public override void Show()
		{
			base.Show();
			IsEnabled.Set(_userProfile.UserData.SensorsData.TemperatureHumiditySensorModel.Enabled);
		}

		public void UpdateSensorState(bool enabled)
		{
			_userProfile.UserData.SensorsData.TemperatureHumiditySensorModel.Enabled = enabled;
			_userProfile.UpdateUserDataOnServer();
		}

		public void OnSensorSignalReceived(SensorType sensorType)
		{
			if (!_userProfile.UserData.SensorsData.TemperatureHumiditySensorModel.Enabled)
			{
				return;
			}

			if (sensorType != _currentSensorType)
			{
				return;
			}

			_sensorsSignalReceiver.BlockReceivingSignals();
			_temperatureSensorTimeline.gameObject.SetActive(true);
			StartCoroutine(PlayTimelineRoutine(() =>
			{
				_sensorsSignalReceiver.AllowReceivingSignals();
				_temperatureSensorTimeline.gameObject.SetActive(false);
			}));
		}

		private IEnumerator PlayTimelineRoutine(Action onComplete)
		{
			_temperatureSensorTimeline.Play();
			yield return new WaitForSeconds((float)_temperatureSensorTimeline.duration);
			onComplete();
		}
	}
}
