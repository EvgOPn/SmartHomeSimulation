using Core.DI;
using Core.UIFramework;

namespace CommonUI.Controllers
{
	public sealed class ApplicationPanel : UIController
	{
		[Inject] private SensorsPanel _sensorsPanel;

		protected override void OnAwake()
		{			
		}

		public override void Show()
		{
			base.Show();
			_sensorsPanel.Show();
		}
	}
}
