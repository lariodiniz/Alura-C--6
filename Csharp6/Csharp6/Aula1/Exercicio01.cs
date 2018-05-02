using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp6.Aula1
{
    public class Exercicio01
    {
        
        public void Imprimir()
        {
            Console.WriteLine("1. Propriedades Automáticas Somente-Leitura");

            Aluno01 aluno = new Aluno01("Marty", "McFly", new DateTime(1968, 6, 12));

            Console.WriteLine(aluno.ToString());
        }       
    }

    class Aluno01
    {
        
        public string Nome { get;}        
        public string SobreNome { get; }
        public DateTime DataNascimento { get;}

        public Aluno01(string nome, string sobreNome, DateTime dataNascimento)
        {
            this.Nome = nome;
            this.SobreNome = sobreNome;
            this.DataNascimento = dataNascimento;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Nome, SobreNome);
        }
    }
}
