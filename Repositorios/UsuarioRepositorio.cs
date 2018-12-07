using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Http;
using Sistema.Interfaces;
using Sistema.Models;
using System;

namespace Sistema.Repositorios
{
    public class UsuarioRepositorio : IUsuario
    {
        //Lista dos usuários cadastrados
        private List<UsuarioModel> UsuariosSalvos { get; set; }
        public UsuarioRepositorio()
        {
            if (File.Exists("usuarios.dat"))
            {
                UsuariosSalvos = LerArquivoSerializado();
            } else {
                UsuariosSalvos = new List<UsuarioModel>();
                UsuarioModel usuario = new UsuarioModel(
                    id: 1,
                    nome: "administrador",
                    email: "sac@carfel.com",
                    senha: "123456",
                    administrador: true
                );
                UsuariosSalvos.Add(usuario);
                EscreverNoArquivo(); 
                ListarUsuariosConsole(); 
            }
        }

        //cadastramento de usuários
        public UsuarioModel Cadastrar(UsuarioModel usuario)
        {
            usuario.Id = UsuariosSalvos.Count + 1;
            UsuariosSalvos.Add(usuario);
            EscreverNoArquivo();
            return usuario;
        }

        private void EscreverNoArquivo()
        {
            MemoryStream memoria = new MemoryStream();

            BinaryFormatter serializador = new BinaryFormatter();

            serializador.Serialize(memoria, UsuariosSalvos);

            byte[] bytes = memoria.ToArray();

            File.WriteAllBytes("usuarios.dat", bytes);
        }

        public List<UsuarioModel> LerArquivoSerializado()
        {
            byte[] bytesSerializados = File.ReadAllBytes ("usuarios.dat");

            MemoryStream memoria = new MemoryStream (bytesSerializados);

            BinaryFormatter serializador = new BinaryFormatter ();

            return (List<UsuarioModel>) serializador.Deserialize (memoria);
        }
        public int ValidaUsuario(IFormCollection form)
        {
            if (string.IsNullOrEmpty(form["nome"]))
            {
                return 1;
            }

            foreach(UsuarioModel user in UsuariosSalvos)
            {
                if (form["email"] == user.Email)
                {
                    return 2; 
                }
            }

            if (form["senha"] != form["confirmaSenha"])
            {
                return 3; 
            } else {
                string senha = form["senha"];

                if (senha.Length < 6)
                {
                    return 4;
                }
            }

            return 0;
        }
        public UsuarioModel Login(string email, string senha)
        {
            foreach(UsuarioModel usuario in UsuariosSalvos)
            {
                if (usuario.Email == email && usuario.Senha == senha)
                {
                    return usuario;
                }
            }
            return null;
        }

        public UsuarioModel BuscarPorId(int id)
        {
            foreach(UsuarioModel usuario in UsuariosSalvos)
            {
                if (id == usuario.Id)
                {
                    return usuario;
                }
            }

            return null;
        }

        public void ListarUsuariosConsole()
        {
            foreach(UsuarioModel user in UsuariosSalvos)
            {
                System.Console.WriteLine(user);
            }
        }
    }
}