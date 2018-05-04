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
using System.IO;

namespace Csharp6.Aula05
{
    class Exercicio10
    {

        public async void Imprimir()
        {
            WriteLine("10. Inicializadores De Índice");

            StreamWriter logAplicacao = new StreamWriter("LogAplicacao.txt");


            try
            {
                await logAplicacao.WriteLineAsync("Aplicação esta iniciando...");

                Aluno10 aluno = new Aluno10("Marty", "McFly", new DateTime(1968, 6, 12))
                {
                    Endereco = "9303 Lyon Drive Hill Valley CA",
                    Telefone = "555-4385"
                };

                await logAplicacao.WriteLineAsync("Aluno Marty criado");
                WriteLine(aluno.NomeCompleto);
                WriteLine("Idade: {0}", aluno.GetIdade());
                WriteLine(aluno.DadosPessoais);

                aluno.AdicionaAvaliacao(new Avaliacao06(1, "GEO", 8));
                aluno.AdicionaAvaliacao(new Avaliacao06(1, "MAT", 7));
                aluno.AdicionaAvaliacao(new Avaliacao06(1, "HIS", 9));
                ImprimirMelhorNota(aluno);

                foreach (var avaliacao in aluno.Avaliacaos)
                {
                    WriteLine(avaliacao.ToString());
                }

                Aluno10 aluno2 = null;
                await logAplicacao.WriteLineAsync("Aluno null foi criado");
                ImprimirMelhorNota(aluno2);

                aluno.PropertyChanged += Aluno_PropertyChanged;

                aluno.Endereco = "Rua Vergueiro, 3185";
                aluno.Telefone = "555-1234";

                Aluno10 aluno3 = new Aluno10("Charles", "");
                await logAplicacao.WriteLineAsync("Aluno Chales criado");
            }
            catch (ArgumentException exc) when (exc.Message.Contains("não informado"))
            {

                
                string msg = $"Parâmetro {exc.ParamName} não foi informado";
                await logAplicacao.WriteLineAsync(msg);
                WriteLine(msg);
            }
            catch (ArgumentException exc)
            {
                const string msg = "parametro com Problema!";
                await logAplicacao.WriteLineAsync(msg);
                WriteLine(msg);                
            }
            catch (Exception exc)
            {
                await logAplicacao.WriteLineAsync(exc.ToString());
                WriteLine(exc.ToString());
                
            }
            finally
            {
                await logAplicacao.WriteLineAsync("Aplicação Terminou!");
                logAplicacao.Dispose();
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

        private static void ImprimirMelhorNota(Aluno10 aluno)
        {
            WriteLine("Melhor Nota: {0}", aluno?.MelhorAvaliacao?.Nota);
        }
    }

    class Aluno10 : INotifyPropertyChanged
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

        private IList<Avaliacao06> avaliacoes = new List<Avaliacao06>();

        public event PropertyChangedEventHandler PropertyChanged;

        public IReadOnlyCollection<Avaliacao06> Avaliacaos
              => new ReadOnlyCollection<Avaliacao06>(avaliacoes);

        public Avaliacao06 MelhorAvaliacao => avaliacoes.OrderBy(a => a.Nota).LastOrDefault();


        public void AdicionaAvaliacao(Avaliacao06 avaliacao)
        {
            avaliacoes.Add(avaliacao);
        }

        public string DadosPessoais =>
                $"Nome: {NomeCompleto}, Endereço: {Endereco}, Telefone: {Telefone}, Data de Nascimento: {DataNascimento:dd/MM/yyyy}";                    

        public string NomeCompleto => Nome + " " + SobreNome;

        public int GetIdade() => (int)(((Now - DataNascimento).TotalDays) / 365.242199);

        public Aluno10(string nome, string sobreNome)
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

        public Aluno10(string nome, string sobreNome, DateTime dataNascimento) :
            this(nome, sobreNome)
        {
            this.DataNascimento = dataNascimento;
        }


    }

    class Avaliacao06
    {
        Dictionary<string, string> materias = new Dictionary<string, string>()
        {
            ["MAT"] = "Matemática",
            ["LPL"] = "Língua Portuguesa",
            ["FIS"] = "Física",
            ["HIS"] = "História",
            ["GEO"] = "Geografia",
            ["QUI"] = "Quimica",
            ["BIO"] = "Biologia"
        };

        public Avaliacao06(int bimestre, string codigoMateria, double nota)
        {
            Bimestre = bimestre;
            CodigoMateria = codigoMateria;
            Nota = nota;            

        }

        public int Bimestre { get; }
        public string CodigoMateria { get; }
        public double Nota { get; }

        public override string ToString()
        {
            return $"Bimestre: {Bimestre}, Materia: {materias[CodigoMateria]}, Nota: {Nota}";
        }
    }
}
