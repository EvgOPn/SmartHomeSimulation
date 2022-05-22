using Core.DI;
using SensorsManagement.Enums;
using SensorsManagement.Interfaces;
using System.Collections.Generic;

namespace SensorsManagement
{

	public sealed class SensorsSignalReceiver : DependentMonoBehaviour
	{
		private List<ISensorSignalReceiver> _sensorSignalReceivers = new List<ISensorSignalReceiver>();

		private bool _sensorSignalReceivingAllowed = true;

		protected override void OnAwake()
		{
		}

		public void AllowReceivingSignals()
		{
			_sensorSignalReceivingAllowed = true;
		}

		public void BlockReceivingSignals()
		{
			_sensorSignalReceivingAllowed = false;
		}

		public void AddSensorSignalReceiver(ISensorSignalReceiver receiver)
		{
			_sensorSignalReceivers.Add(receiver);
		}

		public void RemoveSensorSignalReceiver(ISensorSignalReceiver receiver)
		{
			_sensorSignalReceivers.Remove(receiver);
		}

		private void SensorSignalReceived(SensorType sensorType)
		{
			if (_sensorSignalReceivingAllowed)
			{
				_sensorSignalReceivers.ForEach(receiver => receiver.OnSensorSignalReceived(sensorType));
			}
		}

		#region Debug
		public void ReceiveFireSignal()
		{
			SensorSignalReceived(SensorType.FireSensor);
		}

		public void ReceiveWaterSignal()
		{
			SensorSignalReceived(SensorType.WaterSensor);
		}

		public void ReceiveDoorSignal()
		{
			SensorSignalReceived(SensorType.DoorSensor);
		}

		public void ReceiveTemperatureSignal()
		{
			SensorSignalReceived(SensorType.TemperatureSensore);
		}
		#endregion
	}
}
