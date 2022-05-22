using Core.DI;
using Core.UIFramework;
using System.Collections.Generic;
using UnityEngine;
using UserManagement;

namespace CommonUI.Controllers
{
	public sealed class SensorsPanel : UIController
	{
		public Observable<string> UserEmail = new Observable<string>(string.Empty);

		[SerializeField] private List<UIController> _sensorsControllers = new List<UIController>();

		[Inject] private UserProfile _userProfile;

		protected override void OnAwake()
		{

		}

		public override void Show()
		{
			base.Show();
			_sensorsControllers.ForEach(controller => controller.Show());
			UserEmail = _userProfile.UserData.Username;
		}
	}
}
