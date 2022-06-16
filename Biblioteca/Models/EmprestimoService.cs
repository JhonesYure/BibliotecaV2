using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models
{
    public class EmprestimoService 
    {
        public void Inserir(Emprestimo e)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Emprestimos.Add(e);
                bc.SaveChanges();
            }
        }

        public void Atualizar(Emprestimo e)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Emprestimo emprestimo = bc.Emprestimos.Find(e.Id);
                emprestimo.NomeUsuario = e.NomeUsuario;
                emprestimo.Telefone = e.Telefone;
                emprestimo.LivroId = e.LivroId;
                emprestimo.DataEmprestimo = e.DataEmprestimo;
                emprestimo.DataDevolucao = e.DataDevolucao;
                emprestimo.Devolvido = e.Devolvido;
                

                bc.SaveChanges();
            }
        }

        public ICollection<Emprestimo> ListarTodos(FiltrosEmprestimos filtro)
        {   

            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Emprestimo Emprestimos = new Emprestimo();
                IQueryable<Emprestimo> query;
                
                if(filtro != null)
                {
                    
                    switch(filtro.TipoFiltro)
                    {
                        
                        case "Usuario":
                            query = bc.Emprestimos.Where(e => e.NomeUsuario.Contains(filtro.Filtro));
                           
                        break;

                        case "Livro":
                           
                            List<Livro> LivroFiltrado = bc.Livros.Where(l => l.Titulo.Contains(filtro.Filtro)).ToList();
                            List<int>LivroIds = new List<int>();

                            for (int i = 0; i < LivroFiltrado.Count; i++)
                            {
                                LivroIds.Add(LivroFiltrado[i].Id);
                            }
                            
                            query = bc.Emprestimos.Where(e => LivroIds.Contains(e.LivroId));
                            var debug = query.ToList();
                        break;

                        default:
                            query = bc.Emprestimos;
                        break;
                    }
                }
                else
                {
                    
                    query = bc.Emprestimos;
                }

                List<Emprestimo> ListaConsulta = query.OrderByDescending(e => e.DataDevolucao).ToList();

                for (int i = 0; i < ListaConsulta.Count; i++)
                {
                    ListaConsulta[i].Livro = bc.Livros.Find(ListaConsulta[i].LivroId);
                }
                return ListaConsulta;
                
               
            }
        }

        public Emprestimo ObterPorId(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Emprestimos.Find(id);
            }
        }
    }
}