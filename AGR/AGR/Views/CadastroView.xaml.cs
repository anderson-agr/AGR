using AGR.Entidade;
using AGR.Interfaces;
using AGR.ViewModels;

namespace AGR.Views
{
    public partial class CadastroView : IMensagens
    {
        public CadastroView()
        {
            InitializeComponent();
            var vaCadastroViewModel = new CadastroViewModel {Mensagens = this, Navigation = this.Navigation};
            BindingContext = vaCadastroViewModel;
        }
        public CadastroView(Usuario usuario )
        {
            InitializeComponent();
            var vaCadastroViewModel = new CadastroViewModel(usuario) { Mensagens = this, Navigation = this.Navigation};

            BindingContext = vaCadastroViewModel;
        }
    }
}
