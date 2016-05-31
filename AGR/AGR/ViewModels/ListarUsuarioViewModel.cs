using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using AGR.Entidade;
using AGR.Interfaces;
using AGR.Repository;
using AGR.ViewModels.Base;
using AGR.Views;
using Xamarin.Forms;

namespace AGR.ViewModels
{
    public class ListarUsuarioViewModel : BaseListViewModel<Usuario>
    {

        private Usuario _currentUsuario;
        public ICommand VoltarCommand { get; set; }

        public IMensagens Mensagens { get; set; }
        public INavigation Navigation { get; set; }

        public Usuario CurrentUsuario
        {
            get { return _currentUsuario; }
            set
            {
                _currentUsuario = value;
                OnPropertyChanged(nameof(CurrentUsuario));
                if (_currentUsuario != null)
                {
                    Debug.WriteLine($"{CurrentUsuario.Nome} {CurrentUsuario.Sobrenome}"  );
                    Navigation.PushAsync(new CadastroView(CurrentUsuario));
                }
            }
        }

        public ListarUsuarioViewModel()
        {
            ListaEntidades = new ObservableCollection<Usuario>((new UsuarioRepository()).TodosUsuarios().OrderBy(x => x.Nome));
           VoltarCommand = new Command(() =>
           {
               Navigation.PushAsync(new HomeView());
           });
        }
    }
}