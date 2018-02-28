using System.Collections.Generic;
using ObjectConverter;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var listaCarros = new List<Objeto1>();
            listaCarros.Add(new Objeto1(1, "Objeto1"));
            listaCarros.Add(new Objeto1(2, "Objeto1"));
            var classeOrigem = new ClasseOrigem("Teste", "XPTO", listaCarros);
            var objetoConvertido = AutoConverter.ToConvert<ClasseOrigem, ClasseDestino>(classeOrigem);
        }
    }


    class ClasseOrigem
    {
        public ClasseOrigem(string nome, string endereco, IList<Objeto1> carros)
        {
            Nome = nome;
            Endereco = endereco;
            Carros = carros;
        }

        public string Nome { get; set; }

        public string Endereco { get; set; }

        public IList<Objeto1> Carros { get; set; }
    }

    class ClasseDestino
    {
        public ClasseDestino(string nome, string endereco, string telefone)
        {
            Nome = nome;
            Endereco = endereco;
            Telefone = telefone;
        }

        public ClasseDestino() { }

        public string Nome { get; set; }

        public string Endereco { get; set; }

        public string Telefone { get; set; }

        public IList<Objeto2> Carros { get; set; }
    }

    class Objeto1
    {
        public Objeto1(long id, string nome)
        {
            Id = id;
            Nome = nome;
        }
        public long Id { get; set; }

        public string Nome { get; set; }
    }

    class Objeto2
    {
        public long Id { get; set; }

        public string Nome { get; set; }
    }
}
