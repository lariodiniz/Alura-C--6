using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.String;
using static System.DateTime;
using System.Collections.ObjectModel;

namespace Csharp6.Aula02
{
    class Exercicio05
    {

        public void Imprimir()
        {
            WriteLine("5. Oparadores Null-Condicionais");

            Aluno05 aluno = new Aluno05("Marty", "McFly", new DateTime(1968, 6, 12))
            {
                Endereco = "9303 Lyon Drive Hill Valley CA",
                Telefone = "555-4385"
            };

            WriteLine(aluno.NomeCompleto);
            WriteLine("Idade: {0}", aluno.GetIdade());
            WriteLine(aluno.DadosPessoais);

            aluno.AdicionaAvaliacao(new Avaliacao(1, "Geografia", 8));
            aluno.AdicionaAvaliacao(new Avaliacao(1, "Matemática", 7));
            aluno.AdicionaAvaliacao(new Avaliacao(1, "História", 9));
            ImprimirMelhorNota(aluno);

            Aluno05 aluno2 = null;
            ImprimirMelhorNota(aluno2);
        }

        private static void ImprimirMelhorNota(Aluno05 aluno)
        {
            WriteLine("Melhor Nota: {0}", aluno?.MelhorAvaliacao?.Nota);
        }
    }

    class Aluno05
    {

        public string Nome { get; }
        public string SobreNome { get; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; } = new DateTime(1990, 1, 1);

        private IList<Avaliacao> avaliacoes = new List<Avaliacao>();

        public IReadOnlyCollection<Avaliacao> Avaliacaos
              => new ReadOnlyCollection<Avaliacao>(avaliacoes);

        public Avaliacao MelhorAvaliacao => avaliacoes.OrderBy(a => a.Nota).LastOrDefault();


        public void AdicionaAvaliacao(Avaliacao avaliacao)
        {
            avaliacoes.Add(avaliacao);
        }

        public string DadosPessoais => Format("{0}, {1}, {2}", NomeCompleto, Endereco, Telefone);

        public string NomeCompleto => Nome + " " + SobreNome;

        public int GetIdade() => (int)(((Now - DataNascimento).TotalDays) / 365.242199);

        public Aluno05(string nome, string sobreNome)
        {
            this.Nome = nome;
            this.SobreNome = sobreNome;
        }

        public Aluno05(string nome, string sobreNome, DateTime dataNascimento) :
            this(nome, sobreNome)
        {
            this.DataNascimento = dataNascimento;
        }


    }

    class Avaliacao
    {
        public Avaliacao(int bimestre, string materia, double nota)
        {
            Bimestre = bimestre;
            Materia = materia;
            Nota = nota;
        }

        public int Bimestre { get; }
        public string Materia { get; }
        public double Nota { get; }
    }
}
