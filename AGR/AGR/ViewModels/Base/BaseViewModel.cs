using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AGR.Entidade.Annotations;

namespace AGR.ViewModels.Base
{
    public class BaseViewModel : BaseViewModel<object>
    {

    }

    public class BaseViewModel<T> : INotifyPropertyChanged
        where T : class, new()
    {
        public BaseViewModel()
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

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}