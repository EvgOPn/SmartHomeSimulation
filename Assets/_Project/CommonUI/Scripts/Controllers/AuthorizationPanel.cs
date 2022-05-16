using Core.DI;
using Core.UIFramework;
using UserManagement;

namespace CommonUI.Controllers
{
	public sealed class AuthorizationPanel : UIController
	{
		[Inject] private UserProfile _userProfile;
		[Inject] private ApplicationPanel _applicationPanel;

		protected override void OnAwake()
		{
			Visibility.Set(true);
		}	

		public void LaunchApplicationPanel()
		{
			Hide();
			_applicationPanel.Show();
		}
	}
}
