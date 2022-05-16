using Core.DI;
using Core.UIFramework;
using UnityEngine;
using UserManagement;

namespace CommonUI.Controllers
{
	public sealed class Smartphone : UIController
	{
		public readonly Observable<bool> AuthorizationPanelEnabled = new Observable<bool>(false);
		public readonly Observable<bool> ApplicationPanelEnabled = new Observable<bool>(false);

		[Inject] private UserProfile _userProfile;

		protected override void OnAwake()
		{

		}

		public override void Show()
		{
			base.Show();
			CheckAuthorization();
		}

		public void HideSmartphone()
		{
			Hide();
		}

		private void CheckAuthorization()
		{
			if (_userProfile == null)
			{
				Debug.LogError("User profilereference is null");
				return;
			}

			if (!_userProfile.IsAuthorized)
			{
				ApplicationPanelEnabled.Set(false);
				AuthorizationPanelEnabled.Set(true);
				return;
			}

			AuthorizationPanelEnabled.Set(false);
			ApplicationPanelEnabled.Set(true);
		}
	}
}
