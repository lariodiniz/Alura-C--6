using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.String;
using static System.DateTime;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Csharp6.Aula04
{
    class Exercicio08
    {

        public void Imprimir()
        {
            WriteLine("8. Filtros de Exceção");

            try
            {
                Aluno08 aluno = new Aluno08("Marty", "McFly", new DateTime(1968, 6, 12))
                {
                    Endereco = "9303 Lyon Drive Hill Valley CA",
                    Telefone = "555-4385"
                };

                WriteLine(aluno.NomeCompleto);
                WriteLine("Idade: {0}", aluno.GetIdade());
                WriteLine(aluno.DadosPessoais);

                aluno.AdicionaAvaliacao(new Avaliacao04(1, "Geografia", 8));
                aluno.AdicionaAvaliacao(new Avaliacao04(1, "Matemática", 7));
                aluno.AdicionaAvaliacao(new Avaliacao04(1, "História", 9));
                ImprimirMelhorNota(aluno);

                Aluno08 aluno2 = null;
                ImprimirMelhorNota(aluno2);

                aluno.PropertyChanged += Aluno_PropertyChanged;

                aluno.Endereco = "Rua Vergueiro, 3185";
                aluno.Telefone = "555-1234";

                Aluno08 aluno3 = new Aluno08("Charles", "");
            }
            catch (ArgumentException exc) when (exc.Message.Contains("não informado"))
            {
                Console.WriteLine($"Parâmetro {exc.ParamName} não foi informado");
            }
            catch (ArgumentException exc)
            {
                WriteLine("parametro com Problema!");                
            }
            catch (Exception exc)
            {
                WriteLine(exc.ToString());
                
            }

            /* filtro de exceção web
             public static async Task<string> FazerRequisicao()
{ 
    var cliente = new System.Net.Http.HttpClient();
    var streamTask = cliente.GetStringAsync("https://localHost:10000");
    try 
    {
        var resposta = await streamTask;
        return resposta;
    } 
    catch (System.Net.Http.HttpRequestException e) when (e.Message.Contains("404"))
    {
        return "Página não encontrada";
    } 
    catch (System.Net.Http.HttpRequestException e) when (e.Message.Contains("301"))
    {
        return "Site Mudou de Endereço";
    } 
    catch (System.Net.Http.HttpRequestException e) when (e.Message.Contains("304"))
    {
        return "Usar o Cache";
    }
    catch (System.Exception e)
    {
        return "Ocorreu uma exceção";
    }
}
             * */
        }

        private void Aluno_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            WriteLine($"Propriedade {e.PropertyName} foi alterada!");
        }

        private static void ImprimirMelhorNota(Aluno08 aluno)
        {
            WriteLine("Melhor Nota: {0}", aluno?.MelhorAvaliacao?.Nota);
        }
    }

    class Aluno08 : INotifyPropertyChanged
    {
        public string Nome { get; }
        public string SobreNome { get; }
        //public string Endereco { get; set; }
        //public string Telefone { get; set; }

        private string endereco;

        public string Endereco
        {
            get { return endereco; }
            set
            {
                if (endereco != value)
                {
                    endereco = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DadosPessoais));
                }                
            }
        }

        private string telefone;

        public string Telefone
        {
            get { return telefone; }
            set
            {
                if (telefone != value)
                {
                    telefone = value;                    
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DadosPessoais));
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DateTime DataNascimento { get; } = new DateTime(1990, 1, 1);

        private IList<Avaliacao04> avaliacoes = new List<Avaliacao04>();

        public event PropertyChangedEventHandler PropertyChanged;

        public IReadOnlyCollection<Avaliacao04> Avaliacaos
              => new ReadOnlyCollection<Avaliacao04>(avaliacoes);

        public Avaliacao04 MelhorAvaliacao => avaliacoes.OrderBy(a => a.Nota).LastOrDefault();


        public void AdicionaAvaliacao(Avaliacao04 avaliacao)
        {
            avaliacoes.Add(avaliacao);
        }

        public string DadosPessoais =>
                $"Nome: {NomeCompleto}, Endereço: {Endereco}, Telefone: {Telefone}, Data de Nascimento: {DataNascimento:dd/MM/yyyy}";                    

        public string NomeCompleto => Nome + " " + SobreNome;

        public int GetIdade() => (int)(((Now - DataNascimento).TotalDays) / 365.242199);

        public Aluno08(string nome, string sobreNome)
        {
            VerificarParametroPreenchido(nome, nameof(nome));
            this.Nome = nome;

            VerificarParametroPreenchido(sobreNome, nameof(sobreNome));
            this.SobreNome = sobreNome;
        }

        private static void VerificarParametroPreenchido(string valorParametro, string nomeParametro)
        {
            if (IsNullOrEmpty(valorParametro))
            {
                throw new ArgumentException("Parametro não informado!", nameof(nomeParametro));
            }
        }

        public Aluno08(string nome, string sobreNome, DateTime dataNascimento) :
            this(nome, sobreNome)
        {
            this.DataNascimento = dataNascimento;
        }


    }

    class Avaliacao04
    {
        public Avaliacao04(int bimestre, string materia, double nota)
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
