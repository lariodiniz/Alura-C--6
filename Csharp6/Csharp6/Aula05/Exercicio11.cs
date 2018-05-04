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
using System.Collections;

namespace Csharp6.Aula05
{
    class Exercicio11
    {

        public async void Imprimir()
        {
            WriteLine("11. Métodos De Extensão Para Inicializadores De Coleção");

            StreamWriter logAplicacao = new StreamWriter("LogAplicacao.txt");

            try
            {
                await logAplicacao.WriteLineAsync("Aplicação esta iniciando...");

                Aluno11 aluno = new Aluno11("Marty", "McFly", new DateTime(1968, 6, 12))
                {
                    Endereco = "9303 Lyon Drive Hill Valley CA",
                    Telefone = "555-4385"
                };

                await logAplicacao.WriteLineAsync("Aluno Marty criado");
                WriteLine(aluno.NomeCompleto);
                WriteLine("Idade: {0}", aluno.GetIdade());
                WriteLine(aluno.DadosPessoais);

                aluno.AdicionaAvaliacao(new Avaliacao07(1, "GEO", 8));
                aluno.AdicionaAvaliacao(new Avaliacao07(1, "MAT", 7));
                aluno.AdicionaAvaliacao(new Avaliacao07(1, "HIS", 9));
                ImprimirMelhorNota(aluno);

                foreach (var avaliacao in aluno.Avaliacaos)
                {
                    WriteLine(avaliacao.ToString());
                }

                Aluno11 aluno2 = null;
                await logAplicacao.WriteLineAsync("Aluno null foi criado");
                ImprimirMelhorNota(aluno2);

                aluno.PropertyChanged += Aluno_PropertyChanged;

                aluno.Endereco = "Rua Vergueiro, 3185";
                aluno.Telefone = "555-1234";

                Aluno11 aluno3 = new Aluno11("Charles", "Brown");
                await logAplicacao.WriteLineAsync("Aluno Chales criado");

                ListaDeMatricula listaDeMatricula = new ListaDeMatricula
                { aluno, aluno3 };

                foreach (var a in listaDeMatricula)
                {
                    WriteLine(a.DadosPessoais);
                }

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

        private static void ImprimirMelhorNota(Aluno11 aluno)
        {
            WriteLine("Melhor Nota: {0}", aluno?.MelhorAvaliacao?.Nota);
        }
    }

    class Aluno11 : INotifyPropertyChanged
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

        private IList<Avaliacao07> avaliacoes = new List<Avaliacao07>();

        public event PropertyChangedEventHandler PropertyChanged;

        public IReadOnlyCollection<Avaliacao07> Avaliacaos
              => new ReadOnlyCollection<Avaliacao07>(avaliacoes);

        public Avaliacao07 MelhorAvaliacao => avaliacoes.OrderBy(a => a.Nota).LastOrDefault();


        public void AdicionaAvaliacao(Avaliacao07 avaliacao)
        {
            avaliacoes.Add(avaliacao);
        }

        public string DadosPessoais =>
                $"Nome: {NomeCompleto}, Endereço: {Endereco}, Telefone: {Telefone}, Data de Nascimento: {DataNascimento:dd/MM/yyyy}";                    

        public string NomeCompleto => Nome + " " + SobreNome;

        public int GetIdade() => (int)(((Now - DataNascimento).TotalDays) / 365.242199);

        public Aluno11(string nome, string sobreNome)
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

        public Aluno11(string nome, string sobreNome, DateTime dataNascimento) :
            this(nome, sobreNome)
        {
            this.DataNascimento = dataNascimento;
        }


    }

    class Avaliacao07
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

        public Avaliacao07(int bimestre, string codigoMateria, double nota)
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

    class ListaDeMatricula: IEnumerable<Aluno11>
    {
        private List<Aluno11> alunos = new List<Aluno11>();

        public IEnumerator<Aluno11> GetEnumerator()
        {
            return ((IEnumerable<Aluno11>)alunos).GetEnumerator();
        }

        public void Matricular(Aluno11 aluno)
        {
            alunos.Add(aluno);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Aluno11>)alunos).GetEnumerator();
        }
    }

    static class AlunoExtensions
    {
        public static void Add(this ListaDeMatricula lista, Aluno11 aluno)
                => lista.Matricular(aluno);
        
    }
}
