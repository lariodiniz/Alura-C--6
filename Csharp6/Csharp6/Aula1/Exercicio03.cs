using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp6.Aula1
{
    class Exercicio03
    {

        public void Imprimir()
        {
            Console.WriteLine("3. Membros Com Corpo De Expressão");

            Aluno03 aluno = new Aluno03("Marty", "McFly", new DateTime(1968, 6, 12));

            Console.WriteLine(aluno.NomeCompleto);
            Console.WriteLine("Idade: {0}", aluno.GetIdade());
        }
    }

    class Aluno03
    {

        public string Nome { get; }
        public string SobreNome { get; }

        public DateTime DataNascimento { get; } = new DateTime(1990, 1, 1);

        public string NomeCompleto => Nome + " " + SobreNome;         

        public int GetIdade() => (int)(((DateTime.Now - DataNascimento).TotalDays) / 365.242199);        

        public Aluno03(string nome, string sobreNome)
        {
            this.Nome = nome;
            this.SobreNome = sobreNome;
        }

        public Aluno03(string nome, string sobreNome, DateTime dataNascimento) :
            this(nome, sobreNome)
        {
            this.DataNascimento = dataNascimento;
        }


    }
}
