        using System;

        namespace Sistema.Models
        {
            [Serializable]
            public class ComentarioModel
            {
                //Declaração das variáveis referentes aos comentários
                public int Id { get; set; }
                public UsuarioModel Usuario { get; set; }
                public string Texto { get; set; }
                public DateTime DataCriacao { get; set; }
                public string Situacao { get; set; }

                //Construção de método contrutor dos comentários com ID
                public ComentarioModel(int id, UsuarioModel usuario, string texto, DateTime data, string situacao)
                {
                    this.Id = id;
                    this.Usuario = usuario;
                    this.Texto = texto;
                    this.DataCriacao = data;
                    this.Situacao = situacao;
                }

                //Construção de método contrutor dos comentários sem ID
                public ComentarioModel(UsuarioModel usuario, string texto, DateTime data, string situacao)
                {
                    //this.Id = id;
                    this.Usuario = usuario;
                    this.Texto = texto;
                    this.DataCriacao = data;
                    this.Situacao = situacao;
                }
            }
            public enum Tipos
            {
                Aceito,
                Recusado
            }
        }