using System.Windows.Input;
using AGR.Interfaces;
using AGR.ViewModels.Base;
using AGR.Views;
using Xamarin.Forms;

namespace AGR.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        public ICommand CarregaCommand { get; set; }
        public ICommand CadastrarCommand { get; set; }
        public ICommand GravarCommand { get; set; }
        public ICommand VideosCommand { get; set; }

        public IMensagens Mensagens { get; set; }
        public INavigation Navigation { get; set; }

        public HomeViewModel()
        {
            CadastrarCommand = new Command(() =>
            {
                Navigation.PushAsync(new CadastroView());
            });

            CarregaCommand = new Command(() =>
            {
                Navigation.PushAsync(new ListarUsuariosView());
            });

            GravarCommand = new Command(() =>
            {
                Navigation.PushAsync(new GravaAudioView());
            });
            VideosCommand = new Command(() =>
            {
                if (Device.OS == TargetPlatform.Android)
                {
                    Navigation.PushAsync(new AndroidVideoPlayer());
                }
                else
                {
                    Navigation.PushAsync(new iOSVideoPlayer());
                }
            });
        }
    }
}