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

		[Inject] private AuthorizationPanel _authorizationPanel;
		[Inject] private ApplicationPanel _applicationPanel;
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
				_applicationPanel.Hide();
				_authorizationPanel.Show();
				return;
			}

			_authorizationPanel.Hide();
			_applicationPanel.Show();
		}
	}
}
