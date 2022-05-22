using UnityEngine;
using System;

namespace UserManagement.Models
{
	[Serializable]
	public sealed class WaterSensorModel
	{
		public bool Enabled;

		public override string ToString()
		{
			return JsonUtility.ToJson(this, true);
		}
	}
}
