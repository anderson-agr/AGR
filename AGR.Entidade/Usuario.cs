using System.ComponentModel;
using System.Runtime.CompilerServices;
using AGR.Entidade.Annotations;
using AGR.Entidade.Base;
using AGR.Entidade.Exceptions;
using SQLite.Net.Attributes;

namespace AGR.Entidade
{
    public class Usuario : EntidadeBase
    {


        private string _nome;
        private string _sobrenome;

        public string Nome
        {
            get { return _nome; }
            set
            {
                _nome = value;
                OnPropertyChanged(nameof(Nome));
            }
        }

        public string Sobrenome
        {
            get { return _sobrenome; }
            set
            {
                _sobrenome = value;
                OnPropertyChanged(nameof(Sobrenome));
            }
        }


        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Nome))
            {
                throw new ObrigatorioException("O campo nome é Obrigatorio");
            }
            if (string.IsNullOrWhiteSpace(Sobrenome))
            {
                throw new ObrigatorioException("O campo sobrenome é Obrigatorio");
            }
        }
    }
}