using Core.DI;
using Core.UIFramework;

namespace CommonUI.Controllers
{
	public sealed class GameMenu : UIController
	{
		[Inject] private PausePanel _pausePanel;
		[Inject] private Smartphone _smartphone;

		protected override void OnAwake()
		{
			Visibility.Set(true);
		}

		public void ShowPauseMenu()
		{
			_pausePanel.Show();
		}

		public void ShowApplicationMenu()
		{
			_smartphone.Show();
		}
	}
}
