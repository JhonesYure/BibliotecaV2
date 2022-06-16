using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        //INSERIR
        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastro(Usuario novoUsuario)
        {
           
            UsuarioService us = new UsuarioService();
            us.Inserir(novoUsuario);
            ViewData["mensagem"] = "Cadastro realizado!";

           return View();
        }
        //LISTA
        public IActionResult Listagem()
        {
            
            Autenticacao.verificarUsuarioAdmin(this);
            
            return View(new UsuarioService().Listar());
        }
        //EDITAR
        public IActionResult Editar(int id)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificarUsuarioAdmin(this);
            
            return View();
        }
        [HttpPost]
        public IActionResult Editar(Usuario usuario)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificarUsuarioAdmin(this);

            UsuarioService us = new UsuarioService();
            us.EditarUsuario(usuario);
            return RedirectToAction("Lista", "Usuario");
        }
        //EXCLUIR
        public IActionResult Excluir(int id)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificarUsuarioAdmin(this);
            
            return View(new UsuarioService().Listar(id));
        }
        [HttpPost]
        public IActionResult Excluir(string decisao, int id)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificarUsuarioAdmin(this);
            
            if (decisao == "EXCLUIR")
            {
                ViewData["mensagem"] = "Exclusão do usuário"+ new UsuarioService().Listar(id).Nome+ "Realizada com sucesso";
                new UsuarioService().ExcluirUsuario(id);
                return View("ListaDeUsuario", new UsuarioService().Listar());
            }
            else
            {
                ViewData["mensagem"] = "Exclusão cancelada";
                return View("listaDeUsuario", new UsuarioService().Listar());
            }
        }
        //CADASTRO
        /*public IActionResult CadastroRealizado()
        {
            return View();
        }*/
         public IActionResult NeedAdmin()
        {
            Autenticacao.CheckLogin(this);
            
            
            return View();
        }
         //LOGOUT
        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index","Home");
        }
    }
}