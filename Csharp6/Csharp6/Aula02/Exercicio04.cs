using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.String;
using static System.DateTime;

namespace Csharp6.Aula02
{
    class Exercicio04
    {

        public void Imprimir()
        {
            WriteLine("4. Using Static");

            Aluno04 aluno = new Aluno04("Marty", "McFly", new DateTime(1968, 6, 12))
            {
                Endereco = "9303 Lyon Drive Hill Valley CA",
                Telefone = "555-4385"
            };

            WriteLine(aluno.NomeCompleto);
            WriteLine("Idade: {0}", aluno.GetIdade());
            WriteLine(aluno.DadosPessoais);
        }
    }

    class Aluno04
    {

        public string Nome { get; }
        public string SobreNome { get; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; } = new DateTime(1990, 1, 1);

        public string DadosPessoais => Format("{0}, {1}, {2}", NomeCompleto, Endereco, Telefone);
        
        public string NomeCompleto => Nome + " " + SobreNome;

        public int GetIdade() => (int)(((Now - DataNascimento).TotalDays) / 365.242199);

        public Aluno04(string nome, string sobreNome)
        {
            this.Nome = nome;
            this.SobreNome = sobreNome;
        }

        public Aluno04(string nome, string sobreNome, DateTime dataNascimento) :
            this(nome, sobreNome)
        {
            this.DataNascimento = dataNascimento;
        }


    }
}
