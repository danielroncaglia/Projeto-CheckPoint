using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Sistema.Models;
using System;

namespace Sistema.Interfaces
{    public interface IUsuario
    {
        UsuarioModel Cadastrar(UsuarioModel usuario);
        int ValidaUsuario(IFormCollection form);
        UsuarioModel Login(string email, string senha);
    }
}