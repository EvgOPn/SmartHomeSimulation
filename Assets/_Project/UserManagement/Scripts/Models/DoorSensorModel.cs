using UnityEngine;
using System;

namespace UserManagement.Models
{
	[Serializable]
	public sealed class DoorSensorModel
	{
		public bool Enabled;

		public override string ToString()
		{
			return JsonUtility.ToJson(this, true);
		}
	}
}
