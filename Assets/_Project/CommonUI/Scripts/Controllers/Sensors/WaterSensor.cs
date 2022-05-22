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
	public sealed class WaterSensor : UIController, ISensorSignalReceiver
	{
		[SerializeField] private PlayableDirector _waterSensorTimeline;

		public readonly Observable<bool> IsEnabled = new Observable<bool>(false);

		[Inject] private SensorsSignalReceiver _sensorsSignalReceiver;
		[Inject] private UserProfile _userProfile;

		private SensorType _currentSensorType = SensorType.WaterSensor;

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
			IsEnabled.Set(_userProfile.UserData.SensorsData.WaterSensorModel.Enabled);
		}

		public void UpdateSensorState(bool enabled)
		{
			_userProfile.UserData.SensorsData.WaterSensorModel.Enabled = enabled;
			_userProfile.UpdateUserDataOnServer();
		}

		public void OnSensorSignalReceived(SensorType sensorType)
		{
			if (!_userProfile.UserData.SensorsData.WaterSensorModel.Enabled)
			{
				return;
			}

			if (sensorType != _currentSensorType)
			{
				return;
			}

			_sensorsSignalReceiver.BlockReceivingSignals();
			_waterSensorTimeline.gameObject.SetActive(true);
			StartCoroutine(PlayTimelineRoutine(() =>
			{
				_sensorsSignalReceiver.AllowReceivingSignals();
				_waterSensorTimeline.gameObject.SetActive(false);
			}));
		}

		private IEnumerator PlayTimelineRoutine(Action onComplete)
		{
			_waterSensorTimeline.Play();
			yield return new WaitForSeconds((float)_waterSensorTimeline.duration);
			onComplete();
		}
	}
}
