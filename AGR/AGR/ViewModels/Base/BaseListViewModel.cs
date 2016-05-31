using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AGR.Entidade.Annotations;

namespace AGR.ViewModels.Base
{
    public class BaseListViewModel<T> : INotifyPropertyChanged
        where T : class, new()
    {

        private ObservableCollection<T> _listaEntidades;
        public BaseListViewModel()
        {
            EntidadeAtual = new T();
        }
        private T _entidadeAtual;

        public event PropertyChangedEventHandler PropertyChanged;

        public T EntidadeAtual
        {
            get { return _entidadeAtual; }
            set
            {
                _entidadeAtual = value;
                OnPropertyChanged(nameof(EntidadeAtual));
            }
        }

        public ObservableCollection<T> ListaEntidades
        {
            get { return _listaEntidades; }
            set
            {
                _listaEntidades = value;
                OnPropertyChanged(nameof(ListaEntidades));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}