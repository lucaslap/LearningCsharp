// ============================================================
//  C# — Regex (Expressoes Regulares)
// ============================================================
//
//  O QUE E REGEX?
//  ──────────────
//  Uma expressao regular e um "molde" (padrao) usado para BUSCAR,
//  VALIDAR, EXTRAIR ou SUBSTITUIR pedaços de texto. Em vez de escrever
//  varios if/for para checar se uma string "parece" um e-mail, um CEP
//  ou uma data, voce descreve o formato uma vez como um padrao.
//
//  Em C#, tudo vive no namespace System.Text.RegularExpressions, na
//  classe `Regex`. (Esse namespace NAO entra nos ImplicitUsings, por
//  isso o `using` explicito abaixo.)
//
//
//  COMO LER UM PADRAO — a "tabela periodica" do regex
//  ──────────────────────────────────────────────────
//  CLASSES DE CARACTERE (que tipo de caractere casar):
//    .        qualquer caractere (menos quebra de linha)
//    \d  \D   digito [0-9]   /  NAO digito
//    \w  \W   "palavra" [A-Za-z0-9_]  /  NAO palavra
//    \s  \S   espaco em branco  /  NAO espaco
//    [abc]    um dos caracteres a, b ou c
//    [a-z]    qualquer letra minuscula (intervalo)
//    [^abc]   QUALQUER um, MENOS a, b ou c (^ dentro de [] = negacao)
//
//  QUANTIFICADORES (quantas vezes repetir o que vem antes):
//    *        0 ou mais        +     1 ou mais
//    ?        0 ou 1 (opcional)
//    {3}      exatamente 3     {2,4} de 2 a 4     {2,} 2 ou mais
//
//  ANCORAS (posicao, nao casam caractere nenhum):
//    ^        inicio da string (ou da linha)
//    $        fim da string (ou da linha)
//    \b       fronteira de palavra (borda entre \w e \W)
//
//  GRUPOS E ALTERNANCIA:
//    (...)    grupo de captura (veja Groups.cs)
//    (a|b)    alternancia: "a" OU "b"
//    \        escapa um metacaractere literal: \.  \?  \(  etc.
//
//
//  DICA DE C#: use string verbatim @"..." para os padroes. Assim a
//  barra invertida do regex (\d, \w...) nao briga com o escape do C#.
//
//
//  Para grupos e extracao de dados, veja Groups.cs.
//  Para substituir e dividir texto, veja ReplaceAndSplit.cs.
//
// ============================================================

using System.Text.RegularExpressions;

namespace RegexLesson;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== 1) IsMatch — o texto BATE com o padrao? (true/false) ===\n");

        // ^...$ ancoram o padrao: a string INTEIRA precisa casar, nao só
        // um pedaço. Aqui: 1+ digitos, do comeco ao fim.
        string padraoSoDigitos = @"^\d+$";

        Console.WriteLine($"\"12345\" e so digitos? {Regex.IsMatch("12345", padraoSoDigitos)}");  // True
        Console.WriteLine($"\"12a45\" e so digitos? {Regex.IsMatch("12a45", padraoSoDigitos)}");  // False (tem letra)
        Console.WriteLine($"\"\"      e so digitos? {Regex.IsMatch("", padraoSoDigitos)}");        // False (+ exige 1)

        Console.WriteLine("\n=== 2) Validacao real: e-mail simples ===\n");

        // Lendo o padrao em partes:
        //   ^                inicio
        //   [\w.-]+          1+ de letra/digito/_/ponto/hifen  (usuario)
        //   @                arroba literal
        //   [\w-]+           1+ de letra/digito/_/hifen         (dominio)
        //   (\.[\w-]+)+      1+ blocos de ".algo"  (.com, .com.br ...)
        //   $                fim
        string padraoEmail = @"^[\w.-]+@[\w-]+(\.[\w-]+)+$";

        string[] candidatos = { "lucas@email.com", "ana.silva@empresa.com.br", "texto sem arroba", "errado@semponto" };
        foreach (string c in candidatos)
        {
            // PadRight so alinha a saida no console.
            Console.WriteLine($"{c,-28} -> {(Regex.IsMatch(c, padraoEmail) ? "valido" : "invalido")}");
        }

        Console.WriteLine("\n=== 3) Match — pega a PRIMEIRA ocorrencia e onde ela esta ===\n");

        string frase = "O pedido 4521 foi entregue no dia 12.";
        // \d+ = uma sequencia de digitos. Match para no primeiro acerto.
        Match m = Regex.Match(frase, @"\d+");

        if (m.Success)
        {
            // .Value = texto casado; .Index = posicao onde comeca.
            Console.WriteLine($"Primeiro numero: \"{m.Value}\" (posicao {m.Index})");  // "4521" (posicao 9)
        }

        Console.WriteLine("\n=== 4) Matches — pega TODAS as ocorrencias ===\n");

        // Matches devolve uma colecao. Otimo para varrer tudo de uma vez.
        MatchCollection todos = Regex.Matches(frase, @"\d+");
        Console.WriteLine($"Quantidade de numeros na frase: {todos.Count}");   // 2
        foreach (Match achado in todos)
        {
            Console.WriteLine($"  achou \"{achado.Value}\" na posicao {achado.Index}");
        }
        // achou "4521" na posicao 9
        // achou "12"   na posicao 34

        Console.WriteLine("\n=== 5) Ancoras e \\b (fronteira de palavra) ===\n");

        string texto = "casa casarao casado casa";
        // Sem \b, "casa" casaria tambem dentro de "casarao"/"casado".
        // Com \bcasa\b exigimos a palavra "casa" inteira e isolada.
        Console.WriteLine($"Ocorrencias de \"casa\" (palavra inteira): {Regex.Matches(texto, @"\bcasa\b").Count}");  // 2
        Console.WriteLine($"Ocorrencias de \"casa\" (qualquer lugar):  {Regex.Matches(texto, @"casa").Count}");      // 4

        // --------------------------------------------------------
        //  Sub-topicos em arquivos separados (padrao hibrido s09):
        // --------------------------------------------------------

        GroupsDemo.Executar();
        ReplaceAndSplitDemo.Executar();

        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }
}
