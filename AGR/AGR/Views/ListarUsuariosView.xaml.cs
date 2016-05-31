using AGR.Interfaces;
using AGR.ViewModels;

namespace AGR.Views
{
    public partial class ListarUsuariosView : IMensagens
    {
        public ListarUsuariosView()
        {
            InitializeComponent();

            var vaListarUsuarioViewModel = new ListarUsuarioViewModel {Mensagens = this, Navigation = this.Navigation};
            BindingContext = vaListarUsuarioViewModel;

        }
    }
}
