using System.Collections.Generic;
using AGR.Entidade;

namespace AGR.Repository
{
    public class UsuarioRepository : Base.Repository
    {
        public void SalvarUsuario(Usuario usuario)
        {
            Salvar(usuario);
        }

        public void Alterarusuario( Usuario usuario)
        {
            Alterar(usuario);
        }

        public void ExcluirUsuario(Usuario usuario)
        {
            Deletar(usuario);
        }
        public Usuario GetUsuario(int id)
        {
           return GetEntity<Usuario>(id);
        }

        public List<Usuario> TodosUsuarios()
        {
            return Todos<Usuario>();
        }
    }
}