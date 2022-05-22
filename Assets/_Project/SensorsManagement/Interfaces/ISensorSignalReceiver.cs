using SensorsManagement.Enums;

namespace SensorsManagement.Interfaces
{
	public interface ISensorSignalReceiver
	{
		public void OnSensorSignalReceived(SensorType sensorType);
	}
}
