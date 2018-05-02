using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp6.Aula1
{
    public class Exercicio02
    {

        public void Imprimir()
        {
            Console.WriteLine("1. Propriedades Automáticas Somente-Leitura");

            Aluno02 aluno = new Aluno02("Marty", "McFly", new DateTime(1968, 6, 12));


        }
    }

    class Aluno02
    {

        public string Nome { get; }
        public string SobreNome { get; }
        public DateTime DataNacimento { get; } = new DateTime(1990, 1, 1);
        public string NomeCompleto
        {
            get { return Nome + " " + SobreNome; }
        }


        public Aluno02(string nome, string sobreNome)
        {
            this.Nome = nome;
            this.SobreNome = sobreNome;
        }

        public Aluno02(string nome, string sobreNome, DateTime dataNascimento) : 
            this(nome, sobreNome)
        {
            this.DataNacimento = dataNascimento;
        }


    }
}
