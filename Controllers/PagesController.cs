            using Microsoft.AspNetCore.Http;
            using Microsoft.AspNetCore.Mvc;
            using Sistema.Models;
            using Sistema.Repositorios;
            using System;

            namespace Sistema.Controllers
            {
                //Código de controle das páginas estáticas
                public class PagesController : Controller
                {
                    //Redireciona para as páginas estáticas: Index (Home)
                    [HttpGet]
                    public IActionResult Index()
                    {
                        string id = HttpContext.Session.GetString("IdUsuario");

                        if (id != null)
                        {
                            int idInt = int.Parse(id);
                            UsuarioRepositorio usuarioRep = new UsuarioRepositorio();
                            UsuarioModel usuario = usuarioRep.BuscarPorId(idInt);
                            string[] nomes = usuario.Nome.Split(" ");
                            ViewBag.UsuarioLogado = nomes[0];
                            ViewBag.UsuarioId = usuario.Id;
                            ViewBag.AdminBool = usuario.Administrador;
                        }
                        else
                        {
                            ViewBag.UsuarioLogado = null;
                            ViewBag.UsuarioId = null;
                        }

                        ComentarioRepositorio comentario = new ComentarioRepositorio();
                        
                        ViewData["ComentariosAceitos"] = comentario.ListarComentariosEspecifico(Tipos.Aceito.ToString());

                        return View ();
                    }

                    //Redireciona para as páginas estáticas: Sobre Carfel
                    [HttpGet]
                    public IActionResult SobreCarfel()
                    {
                        string id = HttpContext.Session.GetString("IdUsuario");

                        if (id != null)
                        {
                            int idInt = int.Parse(id);
                            UsuarioRepositorio usuarioRep = new UsuarioRepositorio();
                            UsuarioModel usuario = usuarioRep.BuscarPorId(idInt);
                            string[] nomes = usuario.Nome.Split(" ");
                            ViewBag.UsuarioLogado = nomes[0];
                            ViewBag.AdminBool = usuario.Administrador;

                        }
                        else
                        {
                            ViewBag.UsuarioLogado = null;
                        }
                        return View();
                    }

                    //Redireciona para as páginas estáticas: Dúvidas
                    [HttpGet]
                    public IActionResult Duvidas()
                    {
                        string id = HttpContext.Session.GetString("IdUsuario");

                        if (id != null)
                        {
                            int idInt = int.Parse(id);
                            UsuarioRepositorio usuarioRep = new UsuarioRepositorio();
                            UsuarioModel usuario = usuarioRep.BuscarPorId(idInt);
                            string[] nomes = usuario.Nome.Split(" ");
                            ViewBag.UsuarioLogado = nomes[0];
                            ViewBag.AdminBool = usuario.Administrador;
                        }
                        else
                        {
                            ViewBag.UsuarioLogado = null;
                        }
                        return View();
                    }
                }
            }
