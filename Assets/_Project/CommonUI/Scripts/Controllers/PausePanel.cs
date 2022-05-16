using Core.UIFramework;
using UnityEngine;

namespace CommonUI.Controllers
{
	public sealed class PausePanel : UIController
	{
		protected override void OnAwake()
		{
		}

		public void HidePauseMenu()
		{
			Hide();
		}

		public void ExitFromApplication()
		{
			Application.Quit();
		}
	}
}
