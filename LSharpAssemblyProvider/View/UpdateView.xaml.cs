using LSharpAssemblyProvider.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace LSharpAssemblyProvider.View
{
	/// <summary>
	/// Interaction logic for UpdateView.xaml
	/// </summary>
	public partial class UpdateView
	{
		public UpdateView()
		{
			this.InitializeComponent();
		    ServiceLocator.Current.GetInstance<MainViewModel>().UpdateGrid = UpdateGrid;
		}
	}
}