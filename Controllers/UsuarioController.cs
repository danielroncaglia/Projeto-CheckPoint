    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Sistema.Interfaces;
    using Sistema.Models;
    using Sistema.Repositorios;
    using System;

    namespace Sistema.Controllers
    {
        public class UsuarioController : Controller
        {
            //Sistema de controle do usuário
            public IUsuario UsuarioRepositorio { get; set; }

            public UsuarioController()
            {
                UsuarioRepositorio = new UsuarioRepositorio();
            }


            [HttpGet]
            public IActionResult Cadastrar()
            {
                string id = HttpContext.Session.GetString("IdUsuario");

                if (id != null)
                {
                    int idInt = int.Parse(id);

                    UsuarioRepositorio usuarioRep = new UsuarioRepositorio();

                    UsuarioModel usuario = usuarioRep.BuscarPorId(idInt);

                    ViewBag.UsuarioLogado = usuario.Nome;
                    ViewBag.AdminBool = usuario.Administrador;
                }
                else
                {
                    ViewBag.UsuarioLogado = null;
                    ViewBag.UsuarioId = null;
                }
                return View();
            }

            [HttpPost]
            public IActionResult Cadastrar(IFormCollection form)
            {
                UsuarioModel usuario = new UsuarioModel(
                    nome: form["nome"],
                    email: form["email"],
                    senha: form["senha"],
                    administrador: false
                );

                UsuarioRepositorio.Cadastrar(usuario);

                TempData["Mensagem"] = "Usuario cadastrado";

                return RedirectToAction("Login");
            }

            [HttpGet]
            public IActionResult Login()
            {
                string id = HttpContext.Session.GetString("IdUsuario");

                if (id != null)
                {
                    TempData["Mensagem"] = "Faça novo login";

                    return RedirectToAction("Cadastrar");

                }
                else
                {
                    ViewBag.UsuarioLogado = null;
                    ViewBag.UsuarioId = null;
                }

                return View();
            }

            [HttpPost]
            public IActionResult Login(IFormCollection form)
            {
                UsuarioModel usuario = UsuarioRepositorio.Login(form["email"], form["senha"]);

                if (usuario != null)
                {
                    string[] nomes = usuario.Nome.Split(" ");
                    HttpContext.Session.SetString("IdUsuario", usuario.Id.ToString());
                    HttpContext.Session.SetString("nomeUsuario", usuario.Nome.ToString());

                    return RedirectToAction("Index", "Pages");
                }
                else
                {
                    ViewBag.Mensagem = "Algo incorreto";
                    return View();
                }
            }

            [HttpGet]
            public IActionResult Deslogar()
            {
                HttpContext.Session.Clear();

                return RedirectToAction("Index", "Pages");
            }

        }
    }