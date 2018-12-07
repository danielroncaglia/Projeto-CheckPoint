using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema.Interfaces;
using Sistema.Models;
using Sistema.Repositorios;
using static Sistema.Models.ComentarioModel;

namespace Sistema.Controllers
{
    public class ComentarioController : Controller
    {
        //Controlador do sistemas de comentários
        private IComentario ComentarioRepositorio { get; set; }
        public ComentarioController()
        {
            ComentarioRepositorio = new ComentarioRepositorio();
        }

        [HttpPost]
        public IActionResult Cadastrar(IFormCollection form)
        {
            //Identificação do usuário que fez comentário
            UsuarioRepositorio usuarioRep = new UsuarioRepositorio();
            UsuarioModel usuario = usuarioRep.BuscarPorId(int.Parse(form["id"]));

            //Capitação do comentário na página
            ComentarioModel comentarioModel = new ComentarioModel(
                usuario: usuario,
                texto: form["texto"],
                data: DateTime.Now,
                situacao: Tipos.Recusado.ToString()
            );

            ComentarioRepositorio.Cadastrar(comentarioModel);

            TempData["Mensagem"] = "Comentário feito com sucesso!";

            return RedirectToAction("Index", "Pages");
        }




        [HttpGet]
        //Aprovação de comentários
        public IActionResult Aprovar()
        {
            UsuarioRepositorio usuarioRep = new UsuarioRepositorio();

            if (HttpContext.Session.GetString("IdUsuario") != null)
            {
                int id = int.Parse(HttpContext.Session.GetString("IdUsuario"));

                UsuarioModel usuario = usuarioRep.BuscarPorId(id);

                if (usuario.Administrador)
                {

                    ComentarioRepositorio comentarioRep = new ComentarioRepositorio();

                    ViewData["ComentariosEmEspera"] = comentarioRep.ListarComentariosEspecifico(Tipos.Recusado.ToString());

                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Pages");
                }
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }

        }

        [HttpPost]
        //Publicação do comentário
        public IActionResult Aprovar(IFormCollection form)
        {
            int CommentId = int.Parse(form["commentId"]);

            ComentarioRepositorio comentarioRep = new ComentarioRepositorio();

            ComentarioModel comentarioModel = comentarioRep.BuscarPorId(CommentId);

            if (form["choice"] == "aceito")
            {
                comentarioRep.Editar(Tipos.Aceito.ToString(), comentarioModel);
            }
            else
            {
                if (form["choice"] == "recusado")
                {
                    comentarioRep.Editar(Tipos.Recusado.ToString(), comentarioModel);
                }
                else
                {
                    ViewBag.Mensagem = "Inválido";
                    return View();
                }
            }

            ViewBag.Mensagem = $"Status do comentário de Id '{comentarioModel.Id}' foi alterado com sucesso!";

            return RedirectToAction("Aprovar");
        }

        [HttpGet]
        public IActionResult Listar () {
            string id = HttpContext.Session.GetString ("IdUsuario");

            if (id != null) {
                int idInt = int.Parse (id);

                UsuarioRepositorio usuarioRep = new UsuarioRepositorio ();

                UsuarioModel usuario = usuarioRep.BuscarPorId (idInt);

                string[] nomes = usuario.Nome.Split (" ");

                ViewBag.UsuarioLogado = nomes[0];
                
            } else {
                ViewBag.UsuarioLogado = null;
            }
            return View ();
        }
    }
}