using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Biblioteca.Models
{
    public class UsuarioService
    {
        //LISTAR
        public List<Usuario> Listar()
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.ToList();
            }
        }
        public Usuario Listar(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.Find(id);
            }
        }
        //INCLUIR
        public void Inserir(Usuario novoUsuario)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
               novoUsuario.Senha = Criptografia.txtcripto(novoUsuario.Senha);
               bc.Usuarios.Add(novoUsuario);
               bc.SaveChanges();
            }
        }
        //EDITAR
            public void EditarUsuario(Usuario editaUsuario)
            {
                using(BibliotecaContext bc = new BibliotecaContext())
                {
                    Usuario u = bc.Usuarios.Find(editaUsuario.ID);
                    u.Nome = editaUsuario.Nome;
                    u.Login = editaUsuario.Login;
                    u.Senha = editaUsuario.Senha;
                    u.Tipo = editaUsuario.Tipo;

                    bc.SaveChanges();
                }
            }
            //EXCLUIR
            public void ExcluirUsuario(int id)
            {
                using(BibliotecaContext bc = new BibliotecaContext())
                {
                    bc.Usuarios.Remove(bc.Usuarios.Find(id));

                    bc.SaveChanges();
                }
            }    
    }
}