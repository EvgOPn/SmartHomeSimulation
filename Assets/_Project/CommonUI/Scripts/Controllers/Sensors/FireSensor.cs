using Core.DI;
using Core.UIFramework;
using SensorsManagement;
using SensorsManagement.Enums;
using SensorsManagement.Interfaces;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UserManagement;

namespace CommonUI.Controllers.Sensors
{
	public sealed class FireSensor : UIController, ISensorSignalReceiver
	{
		[SerializeField] private PlayableDirector _fireSensorTimeline;

		public readonly Observable<bool> IsEnabled = new Observable<bool>(false);

		[Inject] private SensorsSignalReceiver _sensorsSignalReceiver;
		[Inject] private UserProfile _userProfile;

		private SensorType _currentSensorType = SensorType.FireSensor;

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
			IsEnabled.Set(_userProfile.UserData.SensorsData.FireSensorModel.Enabled);
		}

		public void UpdateSensorState(bool enabled)
		{
			_userProfile.UserData.SensorsData.FireSensorModel.Enabled = enabled;
			_userProfile.UpdateUserDataOnServer();
		}

		public void OnSensorSignalReceived(SensorType sensorType)
		{
			if (!_userProfile.UserData.SensorsData.FireSensorModel.Enabled)
			{
				return;
			}

			if (sensorType != _currentSensorType)
			{
				return;
			}

			_sensorsSignalReceiver.BlockReceivingSignals();
			_fireSensorTimeline.gameObject.SetActive(true);
			StartCoroutine(PlayTimelineRoutine(() =>
			{
				_sensorsSignalReceiver.AllowReceivingSignals();
				_fireSensorTimeline.gameObject.SetActive(false);
			}));
		}

		private IEnumerator PlayTimelineRoutine(Action onComplete)
		{			
			_fireSensorTimeline.Play();
			yield return new WaitForSeconds((float)_fireSensorTimeline.duration);			
			onComplete();
		}
	}
}
