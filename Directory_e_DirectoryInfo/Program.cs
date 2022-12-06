using System.ComponentModel;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;

namespace Directory_e_DirectoryInfo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Este código mostra como recuperar todos os arquivos de texto de um diretório e
            // movê - los para um novo diretório. Depois que os arquivos são movidos, eles não existem mais no diretório original.


            //Variáveis do tipo string (origem, destino) que recebem os caminhos (valores) de cada pasta.
            //O sistema operacional Windows não é Case Sensitive

            string origem = @"c:\temp\origem";
            string destino = @"c:\temp\destino";

            try
            {
                //Classe Directory -  utiliza métodos estáticos para criar, mover e enumerar em
                //diretórios e subdiretórios.Essa classe não pode ser herdada.

                //EnumerateFiles - Método estático da classe Directory - Retorna uma coleção enumerável de nomes completos de arquivo que
                // correspondem a um caminho especifico, ou seja, nome completos de arquivos da pasta origem. Nota-se que o respectivo
                // método recepe como parâmetro a varíavel que carrega o caminho da pasta "origem".

                var arquivostxt = Directory.EnumerateFiles(origem);

                foreach (string arquivo in arquivostxt)
                {
                    string nomeArquivo = arquivo.Substring(origem.Length + 1);

                    //O método -Paht.Combine- destina-se a concatenar cadeias de caracteres individuais
                    //em uma única cadeia de caracteres que representa um caminho de arquivo

                    Directory.Move(arquivo, Path.Combine(destino, nomeArquivo));

                    //O Método -Directory.Move- move um arquivo ou um diretório e seu conteúdo para um novo local (De origem para destino).
                }
            }
            catch (Exception e)  // Classe Exception - Representa erros que ocorrem durante a execução do aplicativo.
            {
                //Impressão na tela de exceção de código.
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("------------------------------------------------------------------------------------------");

            try
            {
                // A Expressão from é uma expressão de Consulta
                //A cláusula from especifica o seguinte:
                // - A fonte de dados na qual a consulta ou subconsulta será executada.
                // - Uma variável de intervalo local que representa cada elemento na sequência de origem.

                // A cláusula where em uma definição genérica especifica restrições sobre os tipos que são usados
                // como argumentos para parâmetros de tipo em um tipo genérico,
                // método, delegado ou função local.

                //a cláusula select especifica o tipo de valores que serão produzidos quando a consulta é executada.
                //O resultado é baseado na avaliação de todas as cláusulas anteriores e em quaisquer expressões na cláusula select em si.
                //Uma expressão de consulta deve terminar com uma cláusula select

                // Método ToLower - Retorna uma cópia dessa cadeia de caracteres convertida em minúsculas.
                // Método Contains - Determina se uma sequência contém um elemento especificado usando o comparador de igualdade padrão.

                var arquivos = from arquivo in Directory.EnumerateFiles(@"c:\temp\servidorArquivos")
                               where arquivo.ToLower().Contains("brasil")
                               select arquivo;

                foreach (var item in arquivos)
                {
                    Console.WriteLine("{0}", item);
                }

                //Método .Count - Retorna o número de elementos em uma sequência (número de elementos dentro da pasta arquivo).
                Console.WriteLine("{0} Arquivos encontrados.", arquivos.Count<string>().ToString());
            }
            catch (UnauthorizedAccessException UAEx)
            {
                // UnauthorizedAccessException -- A exceção que é gerada quando o sistema operacional nega acesso devido a um erro de E/S (Entrada e Saída)
                //ou de um tipo específico de erro de segurança.

                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                // PathTooLongException -- A exceção gerada quando um caminho ou nome de arquivo totalmente qualificado é maior
                // que o tamanho máximo definido pelo sistema.
                Console.WriteLine(PathEx.Message);
            }

            Console.WriteLine("------------------------------------------------------------------------------------------");

            try
            {

                //A cadeia de caracteres de pesquisa para correspondência com os nomes dos arquivos em
                //path.Esse parâmetro pode conter uma combinação de caracteres curinga(*e ?) e 
                //caminho de literal, mas não dá suporte a expressões regulares. Os especificadores curinga a seguir
                //são permitidos em searchPattern.

                // (*) - Zero ou mais caracteres nessa posição.
                // (?) -   Exatamente um caracteres nessa posição.  

                var arquivos = Directory.EnumerateFiles(origem, "*.txt");

                foreach (string arquivoAtual in arquivos)
                {
                    string nomeArquivo = arquivoAtual.Substring(origem.Length + 1);
                    Directory.Move(arquivoAtual, Path.Combine(destino, nomeArquivo));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("------------------------------------------------------------------------------------------");

            try
            {
                //O Código a seguir enumera os arquivos no diretório especificado que têm uma extensão
                // ".txt", lê cada linha do arquivo e exibe a linha se ela contiver a cadeia de caracteres “Brasil".

                var arquivos = from arquivo in Directory.EnumerateFiles(@"c:\temp\servidorArquivos", "*.txt")
                               where arquivo.ToLower().Contains("brasil")
                               select arquivo;

                foreach (var item in arquivos)
                {
                    Console.WriteLine("{0}", item);
                }

                Console.WriteLine("{0} Arquivos encontrados.", arquivos.Count<string>().ToString());
            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }

        }
    }
}