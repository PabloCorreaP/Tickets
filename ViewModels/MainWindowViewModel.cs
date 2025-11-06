using PracticaDIA.UI.ViewModels;
namespace PracticaDIA.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting => "Sistema de Gestión - Gestión de Personal";

    public PersonalViewModel PersonalViewModel { get; }

    public MainWindowViewModel()
    {
        PersonalViewModel = new PersonalViewModel();
    }
}
