using UnityEngine;
using Uduino;
using Core.DI;

namespace SensorsManagement
{
	[Dependent]
	public sealed class ArduinoSignalOperator : MonoBehaviour
	{
		private const string DoorSensor = "Door";
		private const string HumiditySensor = "Humidity";
		private const string WaterSensor = "Water";
		private const string FlameSensor = "Flame";

		private const string DoorOpened = "1";
		private const double HighHumidity = 60.00;
		private const int HighWater = 50;
		private const int HighFlame = 20;

		[Inject] private SensorsSignalReceiver _sensorsSignalReceiver;

		private void Awake()
		{
			Binder.UpdateDependencies();
		}

		private void Start()
		{
			UduinoManager.Instance.OnDataReceived += ArduinoDataReceived;
		}

		private void Update()
		{
			UduinoDevice myDevice = UduinoManager.Instance.GetBoard("uduinoBoard");
			UduinoManager.Instance.Read(myDevice, "HumiditySensor");
			UduinoManager.Instance.Read(myDevice, "DoorSensor");
			UduinoManager.Instance.Read(myDevice, "WaterSensor");
			UduinoManager.Instance.Read(myDevice, "FlameSensor");
		}

		private void ArduinoDataReceived(string data, UduinoDevice device)
		{
			string[] dataArray = data.Split(',');

			if (dataArray.Length != 2)
			{
				Debug.LogWarning("Non operable data");
				return;
			}

			switch (dataArray[0])
			{
				case DoorSensor:
					{
						CheckDoorSensorData(dataArray[1]);
						break;
					}
				case HumiditySensor:
					{
						CheckHumiditySensorData(double.Parse(dataArray[1], System.Globalization.CultureInfo.InvariantCulture));
						break;
					}
				case WaterSensor:
					{
						CheckWaterSensorData(System.Convert.ToInt32(dataArray[1]));
						break;
					}
				case FlameSensor:
					{
						CheckFlameSensorData(System.Convert.ToInt32(dataArray[1]));
						break;
					}
			}
		}

		private void CheckDoorSensorData(string data)
		{
			if (data == DoorOpened)
			{
				_sensorsSignalReceiver.ReceiveDoorSignal();
				Debug.Log("Door opened: " + data);
			}
		}

		private void CheckHumiditySensorData(double data)
		{
			if (data >= HighHumidity)
			{
				_sensorsSignalReceiver.ReceiveTemperatureSignal();
				Debug.Log("High humidity " + data.ToString());
			}
		}

		private void CheckWaterSensorData(int data)
		{
			if (data >= HighWater)
			{
				_sensorsSignalReceiver.ReceiveWaterSignal();
				Debug.Log("High water " + data.ToString());
			}
		}

		private void CheckFlameSensorData(int data)
		{
			if (data >= HighFlame)
			{
				_sensorsSignalReceiver.ReceiveFireSignal();
				Debug.Log("Fire " + data.ToString());
			}
		}
	}
}
