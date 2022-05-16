using Core.UIFramework;

namespace CommonUI.Controllers
{
	public sealed class ApplicationPanel : UIController
	{
		protected override void OnAwake()
		{
			Visibility.Set(true);
		}
	}
}
