using System;
using System.Windows.Input;
using AGR.Entidade;
using AGR.Entidade.Exceptions;
using AGR.Interfaces;
using AGR.Repository;
using AGR.ViewModels.Base;
using AGR.Views;
using Xamarin.Forms;

namespace AGR.ViewModels
{
    public class CadastroViewModel : BaseViewModel<Usuario>
    {

        private bool _salvarVisibilidade;
        private bool _alterarVisibilidade;
        private bool _excluirVisibilidade;

        public ICommand SalvarCommand { get; set; }
        public ICommand AlterarCommand { get; set; }
        public ICommand ExcluirCommand { get; set; }

        public IMensagens Mensagens { get; set; }
        public INavigation Navigation { get; set; }

        public bool ExcluirVisibilidade
        {
            get { return _excluirVisibilidade; }
            set
            {
                _excluirVisibilidade = value;
                OnPropertyChanged(nameof(ExcluirVisibilidade));
            }
        }

        public bool SalvarVisibilidade
        {
            get { return _salvarVisibilidade; }
            set
            {
                _salvarVisibilidade = value;
                OnPropertyChanged(nameof(SalvarVisibilidade));
            }
        }

        public bool AlterarVisibilidade
        {
            get { return _alterarVisibilidade; }
            set
            {
                _alterarVisibilidade = value;
                OnPropertyChanged(nameof(AlterarVisibilidade));
            }
        }

        public void IsVisivel(bool visivel)
        {
            ExcluirVisibilidade = !visivel;
            SalvarVisibilidade = visivel;
            AlterarVisibilidade = !visivel;

        }

        public CadastroViewModel()
        {
            IsVisivel(EntidadeAtual.Id == 0);
            SalvarCommand = new Command(() =>
            {
                try
                {

                    EntidadeAtual.Validate();

                    (new UsuarioRepository()).Salvar(EntidadeAtual);
                    Mensagens.DisplayAlert("Mensagem", "Salvo com sucesso", "ok");
                    Navigation.PushAsync(new ListarUsuariosView());

                }
                catch (ObrigatorioException obrigatorio)
                {
                    Mensagens.DisplayAlert("Error", obrigatorio.Message, "Ok");
                }
                catch (Exception e)
                {
                    Mensagens.DisplayAlert("Error", $"Erro ao salvar o registro. '{e.Message}'", "Ok");
                }

            });


        }

        public CadastroViewModel(Usuario usuario)
        {

            EntidadeAtual = usuario;
            IsVisivel(EntidadeAtual.Id == 0);

            AlterarCommand = new Command(() =>
            {
                try
                {

                    EntidadeAtual.Validate();

                    (new UsuarioRepository()).Alterarusuario(EntidadeAtual);
                    Mensagens.DisplayAlert("Mensagem", "Alterado com sucesso", "ok");
                    Navigation.PushAsync(new ListarUsuariosView());

                }
                catch (ObrigatorioException obrigatorio)
                {
                    Mensagens.DisplayAlert("Error", obrigatorio.Message, "Ok");
                }
                catch (Exception)
                {
                    Mensagens.DisplayAlert("Error", "Erro ao Alterar o registro", "Ok");
                }

            });

            ExcluirCommand = new Command(async () =>
            {
                try
                {


                    if (await Mensagens.DisplayAlert("Mensagem", "Deseja Exlcluir esse registro", "ok", "Cancelar"))
                    {
                        (new UsuarioRepository()).ExcluirUsuario(EntidadeAtual);
                        await Mensagens.DisplayAlert("Mensagem", "Exlcluido com sucesso", "ok");
                        await Navigation.PushAsync(new ListarUsuariosView());
                    }
                }
                catch (ObrigatorioException obrigatorio)
                {
                    await Mensagens.DisplayAlert("Error", obrigatorio.Message, "Ok");
                }
                catch (Exception)
                {
                    await Mensagens.DisplayAlert("Error", "Erro ao Alterar o registro", "Ok");
                }
            });
        }
    }
}