using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Http;
using Sistema.Interfaces;
using Sistema.Models;
using System;
using System.Linq;

namespace Sistema.Repositorios
{
    public class ComentarioRepositorio : IComentario
    {
        private List<ComentarioModel> ComentariosSalvos { get; set; }
        public ComentarioRepositorio()
        {
            if (File.Exists("comentarios.dat"))
            {
                ComentariosSalvos = LerArquivoSerializado();
            }
            else
            {
                ComentariosSalvos = new List<ComentarioModel>();
            }
        }

        public void Cadastrar(ComentarioModel comentario)
        {
            comentario.Id = ComentariosSalvos.Count + 1;
            ComentariosSalvos.Add(comentario);
            EscreverNoArquivo();
        }

        private void EscreverNoArquivo()
        {
            MemoryStream memoria = new MemoryStream();
            BinaryFormatter serializador = new BinaryFormatter();
            serializador.Serialize(memoria, ComentariosSalvos);
            byte[] bytes = memoria.ToArray();
            File.WriteAllBytes("comentarios.dat", bytes);
        }

        public List<ComentarioModel> LerArquivoSerializado()
        {
            byte[] bytesSerializados = File.ReadAllBytes("comentarios.dat");
            MemoryStream memoria = new MemoryStream(bytesSerializados);
            BinaryFormatter serializador = new BinaryFormatter();
            return (List<ComentarioModel>)serializador.Deserialize(memoria);
        }

        public ComentarioModel BuscarPorId(int id)
        {
            foreach (ComentarioModel comentario in ComentariosSalvos)
            {
                if (id == comentario.Id)
                {
                    return comentario;
                }
            }

            return null;
        }

        public void Editar(string newStatus, ComentarioModel newComentario)
        {
            for (int i = 0; i < ComentariosSalvos.Count; i++)
            {
                if (newComentario.Id == ComentariosSalvos[i].Id)
                {
                    ComentariosSalvos[i].Situacao = newStatus;

                    EscreverNoArquivo();

                    break;
                }
            }

        }

        public List<ComentarioModel> ListarComentarios() => ComentariosSalvos;

        public List<ComentarioModel> ListarComentariosEspecifico(string status)
        {
            List<ComentarioModel> comentariosEspecificos = new List<ComentarioModel>();

            foreach (ComentarioModel comentario in ComentariosSalvos)
            {
                if (comentario.Situacao == status)
                {
                    comentariosEspecificos.Add(comentario);
                }
            }

            return comentariosEspecificos.OrderBy(x => x.DataCriacao).Reverse().ToList();
        }

    }
}