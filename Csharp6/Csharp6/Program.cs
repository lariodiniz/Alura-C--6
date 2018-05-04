using Csharp6.Aula02;
using Csharp6.Aula03;
using Csharp6.Aula04;
using Csharp6.Aula1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_atualizacoes
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] menus = new string[] {
                "1. Propriedades Automáticas Somente-Leitura",
                "2. Inicializadores De Propriedade Automática",
                "3. Membros Com Corpo De Expressão",
                "4. Using Static",
                "5. Oparadores Null-Condicionais",
                "6 . Interpolação De Cadeia De Caracteres",
                "7. Expressões nameOf",
                "8. Filtros de Exceção",
                "9. Await Em Blocos Catch e Finally"
            };

            Console.WriteLine("ÍNDICE DE PROGRAMAS");
            Console.WriteLine("===================");

            int programa = 0;
            string line;
            do
            {
                foreach (var menu in menus)
                {
                    Console.WriteLine(menu);
                }

                Console.WriteLine();
                Console.WriteLine("Escolha um programa:");

                line = Console.ReadLine();
                Int32.TryParse(line, out programa);
                switch (programa)
                {
                    case 1:
                        new Exercicio01().Imprimir();
                        break;
                    case 2:
                        new Exercicio02().Imprimir();
                        break;
                    case 3:
                        new Exercicio03().Imprimir();
                        break;
                    case 4:
                        new Exercicio04().Imprimir();
                        break;
                    case 5:
                        new Exercicio05().Imprimir();
                        break;
                    case 6:
                        new Exercicio06().Imprimir();
                        break;
                    case 7:
                        new Exercicio07().Imprimir();
                        break;
                    case 8:
                        new Exercicio08().Imprimir();
                        break;
                    case 9:
                        new Exercicio09().Imprimir();
                        break;
                    default:
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("PRESSIONE UMA TECLA PARA CONTINUAR...");
                Console.ReadKey();
                Console.Clear();
            } while (line.Length > 0);
        }
    }
}
