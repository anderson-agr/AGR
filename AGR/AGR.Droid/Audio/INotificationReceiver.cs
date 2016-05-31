using System.Threading.Tasks;

namespace AGR.Droid.Audio
{
    internal interface INotificationReceiver
    {
        Task StartAsync();

        void Stop();
    }
}