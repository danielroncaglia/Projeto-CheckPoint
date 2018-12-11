using System;

namespace Sistema.Models
{
    [Serializable]
    public class UsuarioModel
    {
        //Declaração das variáveis sobre os usuários
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Administrador { get; set; }


        //Construção de métodos contrutores dos usuarios sem ID
        public UsuarioModel(string nome, string email, string senha, bool administrador)
        {
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.Administrador = administrador;
        }

        //Construção de métodos contrutores dos usuarios sem ID
        public UsuarioModel(int id, string nome, string email, string senha, bool administrador)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.Administrador = administrador;
        }

    }
}