using AGR.ControlRenderer;
using AGR.Views;
using Xamarin.Forms;

namespace AGR
{
    public class AndroidVideoPlayer : ContentPage
    {
        private VideoPlayerView player;

        public AndroidVideoPlayer()
        {
            player = new VideoPlayerView();

            this.ToolbarItems.Add(new ToolbarItem
            {
                Order = ToolbarItemOrder.Secondary,
                Text = "Controller",
                Command = new Command(() => {
                    this.player.VideoPlayer.AddVideoController = !this.player.VideoPlayer.AddVideoController;
                })
            });

            this.ToolbarItems.Add(new ToolbarItem
            {
                Order = ToolbarItemOrder.Secondary,
                Text = "Full Screen",
                Command = new Command(() => {

                    // resize the Content for full screen mode
                    this.player.VideoPlayer.FullScreen = !this.player.VideoPlayer.FullScreen;
                    if (this.player.VideoPlayer.FullScreen)
                    {
                        this.player.HeightRequest = -1;
                        this.Content.VerticalOptions = LayoutOptions.FillAndExpand;
                        player.VideoPlayer.FullScreen = true;
                    }
                    else
                    {
                        this.player.HeightRequest = 200;
                        this.Content.VerticalOptions = LayoutOptions.StartAndExpand;
                        player.VideoPlayer.FullScreen = false;
                    }
                })
            });

            this.ToolbarItems.Add(new ToolbarItem
            {
                Order = ToolbarItemOrder.Secondary,
                Text = "Play",
                Command = new Command(() => {
                    this.player.VideoPlayer.PlayerAction = AGR.Library.VideoState.PLAY;
                })
            });

            this.ToolbarItems.Add(new ToolbarItem
            {
                Order = ToolbarItemOrder.Secondary,
                Text = "Stop",
                Command = new Command(() => {
                    this.player.VideoPlayer.PlayerAction = AGR.Library.VideoState.STOP;
                })
            });

            this.ToolbarItems.Add(new ToolbarItem
            {
                Order = ToolbarItemOrder.Secondary,
                Text = "Pause",
                Command = new Command(() => {
                    this.player.VideoPlayer.PlayerAction = AGR.Library.VideoState.PAUSE;
                })
            });

            this.ToolbarItems.Add(new ToolbarItem
            {
                Order = ToolbarItemOrder.Secondary,
                Text = "Restart",
                Command = new Command(() => {
                    this.player.VideoPlayer.PlayerAction = AGR.Library.VideoState.RESTART;
                })
            });

            // heightRequest must be set it not full screen
            player.HeightRequest = 200;
            player.VideoPlayer.AddVideoController = true;


            // location in Raw folder. no extension
            player.VideoPlayer.FileSource = "sample";

            // autoplay video
            player.VideoPlayer.AutoPlay = true;

            this.Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children =
                {
                    player
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.player.VideoPlayer.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) => {
                if (e.PropertyName == MyVideoPlayer.StateProperty.PropertyName)
                {
                    var s = this.player.VideoPlayer.State;
                    if (s == AGR.Library.VideoState.ENDED)
                    {
                        System.Diagnostics.Debug.WriteLine("State: ENDED");
                    }
                    else if (s == AGR.Library.VideoState.PAUSE)
                    {
                        System.Diagnostics.Debug.WriteLine("State: PAUSE");
                    }
                    else if (s == AGR.Library.VideoState.PLAY)
                    {
                        System.Diagnostics.Debug.WriteLine("State: PLAY");
                    }
                    else if (s == AGR.Library.VideoState.STOP)
                    {
                        System.Diagnostics.Debug.WriteLine("State: STOP");
                    }
                }
                else if (e.PropertyName == MyVideoPlayer.InfoProperty.PropertyName)
                {
                    System.Diagnostics.Debug.WriteLine("Info:\r\n" + this.player.VideoPlayer.Info);
                }
            };
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            this.player.VideoPlayer.ContentHeight = height;
            this.player.VideoPlayer.ContentWidth = width;
            if (width < height)
            {

                this.player.VideoPlayer.Orientation = AGR.ControlRenderer.MyVideoPlayer.ScreenOrientation.PORTRAIT;
            }
            else
            {
                this.player.VideoPlayer.Orientation = AGR.ControlRenderer.MyVideoPlayer.ScreenOrientation.LANDSCAPE;
            }
            this.player.VideoPlayer.OrientationChanged();
            base.OnSizeAllocated(width, height);
        }
    }
}