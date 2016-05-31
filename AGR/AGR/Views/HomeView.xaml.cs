using AGR.Interfaces;
using AGR.ViewModels;
using Xamarin.Forms;

namespace AGR.Views
{
    public partial class HomeView : IMensagens
    {
        public HomeView()
        {
            InitializeComponent();

            var homeViewModel = new HomeViewModel { Navigation = Navigation, Mensagens = this };
            BindingContext = homeViewModel;
            Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);

            
        }

        private void Slider_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            var red = SliderRed.Value;
            var green = SliderGreen.Value;
            var blue = SliderBlue.Value;


            btnCadastrar.BackgroundColor = Color.FromRgb(red, green, blue);
            btnListar.BackgroundColor = Color.FromRgb(red, green, blue);
            btnGravaAudio.BackgroundColor = Color.FromRgb(red, green, blue);
            btnVideo.BackgroundColor = Color.FromRgb(red, green, blue);

        }
    }
}
