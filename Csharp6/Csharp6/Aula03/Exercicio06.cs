using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.String;
using static System.DateTime;
using System.Collections.ObjectModel;

namespace Csharp6.Aula03
{
    class Exercicio06
    {

        public void Imprimir()
        {
            WriteLine("6 . Interpolação De Cadeia De Caracteres");

            Aluno06 aluno = new Aluno06("Marty", "McFly", new DateTime(1968, 6, 12))
            {
                Endereco = "9303 Lyon Drive Hill Valley CA",
                Telefone = "555-4385"
            };

            WriteLine(aluno.NomeCompleto);
            WriteLine("Idade: {0}", aluno.GetIdade());
            WriteLine(aluno.DadosPessoais);

            aluno.AdicionaAvaliacao(new Avaliacao02(1, "Geografia", 8));
            aluno.AdicionaAvaliacao(new Avaliacao02(1, "Matemática", 7));
            aluno.AdicionaAvaliacao(new Avaliacao02(1, "História", 9));
            ImprimirMelhorNota(aluno);

            Aluno06 aluno2 = null;
            ImprimirMelhorNota(aluno2);
        }

        private static void ImprimirMelhorNota(Aluno06 aluno)
        {
            WriteLine("Melhor Nota: {0}", aluno?.MelhorAvaliacao?.Nota);
        }
    }

    class Aluno06
    {

        public string Nome { get; }
        public string SobreNome { get; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; } = new DateTime(1990, 1, 1);

        private IList<Avaliacao02> avaliacoes = new List<Avaliacao02>();

        public IReadOnlyCollection<Avaliacao02> Avaliacaos
              => new ReadOnlyCollection<Avaliacao02>(avaliacoes);

        public Avaliacao02 MelhorAvaliacao => avaliacoes.OrderBy(a => a.Nota).LastOrDefault();


        public void AdicionaAvaliacao(Avaliacao02 avaliacao)
        {
            avaliacoes.Add(avaliacao);
        }

        public string DadosPessoais =>
                $"Nome: {NomeCompleto}, Endereço: {Endereco}, Telefone: {Telefone}, Data de Nascimento: {DataNascimento:dd/MM/yyyy}";                    

        public string NomeCompleto => Nome + " " + SobreNome;

        public int GetIdade() => (int)(((Now - DataNascimento).TotalDays) / 365.242199);

        public Aluno06(string nome, string sobreNome)
        {
            this.Nome = nome;
            this.SobreNome = sobreNome;
        }

        public Aluno06(string nome, string sobreNome, DateTime dataNascimento) :
            this(nome, sobreNome)
        {
            this.DataNascimento = dataNascimento;
        }


    }

    class Avaliacao02
    {
        public Avaliacao02(int bimestre, string materia, double nota)
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
