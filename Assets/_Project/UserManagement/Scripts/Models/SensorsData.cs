using UnityEngine;
using System;

namespace UserManagement.Models
{
	[Serializable]
	public sealed class SensorsData
	{
		public FireSensorModel FireSensorModel;
		public WaterSensorModel WaterSensorModel;
		public TemperatureHumiditySensorModel TemperatureHumiditySensorModel;
		public DoorSensorModel DoorSensorModel;

		public override string ToString()
		{
			return JsonUtility.ToJson(this, true);
		}
	}
}
