using System.ComponentModel;
using System.Runtime.CompilerServices;
using AGR.Entidade.Annotations;
using SQLite.Net.Attributes;

namespace AGR.Entidade.Base
{
   
    public abstract class EntidadeBase : INotifyPropertyChanged

    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public abstract void Validate();
    }
}