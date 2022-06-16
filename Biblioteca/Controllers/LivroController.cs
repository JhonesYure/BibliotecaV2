using System;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Biblioteca.Controllers
{
    public class LivroController : Controller
    {
        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificarUsuarioAdmin(this);
            
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Livro l)
        {       
            if(l.Titulo== "" || l.Autor== "" ||l.Ano== 0){

                ViewData["msgErro"] = "Preencha os campos";
                return View();
            }else{


            LivroService livroService = new LivroService();

            if(l.Id == 0)
            {
                livroService.Inserir(l);
            }
            else
            {
                livroService.Atualizar(l);
            }

            
            }
            return RedirectToAction("Listagem");
        }
  public IActionResult Listagem(string q, string ordem,int p = 1) // tipoFiltra == ordem, filtro == q 
        {   
             Autenticacao.CheckLogin(this);
            int quantidadeDeRegistroPorPagina = 10;

            LivroService ls = new LivroService();
            if(q == null){
                q = string.Empty;
            }
            if(ordem == null){
                ordem = "t";
            }
             
             int quantidadeDeRegistros = ls.CountRegistro();
             ViewData["quantpaginas"] = (int)Math.Ceiling((double)quantidadeDeRegistros/quantidadeDeRegistroPorPagina);
             ICollection<Livro> lista = ls.GetLivros(q,ordem,p,quantidadeDeRegistroPorPagina);
            return View(lista);
        }

        
        
      

        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);
            LivroService ls = new LivroService();
            Livro l = ls.ObterPorId(id);
            return View(l);
        }
    }
}