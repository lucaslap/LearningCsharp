// ============================================================
//  C# — Regex: Replace, Split e RegexOptions
// ============================================================
//
//  ALEM DE BUSCAR, REGEX TRANSFORMA TEXTO
//  ──────────────────────────────────────
//  Regex.Replace -> troca tudo que casa por outra coisa.
//  Regex.Split   -> quebra a string usando o padrao como separador.
//
//
//  SUBSTITUICAO COM REFERENCIA A GRUPOS
//  ────────────────────────────────────
//  No texto de troca voce pode reaproveitar o que foi casado:
//    $1, $2 ...   -> conteudo do grupo numero N
//    ${nome}      -> conteudo do grupo nomeado
//    $&           -> o match inteiro
//  Assim da pra REORDENAR ou REFORMATAR (ex: dd/mm -> mm/dd).
//
//
//  SUBSTITUICAO COM LOGICA (MatchEvaluator)
//  ────────────────────────────────────────
//  Quando a troca depende de um calculo, passe uma funcao (lambda) que
//  recebe cada Match e devolve o texto substituto. Da pra censurar,
//  mascarar, converter caixa, etc.
//
//
//  REGEXOPTIONS — modificadores
//  ────────────────────────────
//    IgnoreCase       ignora maiusculas/minusculas
//    Multiline        ^ e $ passam a valer por LINHA, nao so na string
//    Singleline       o . passa a casar tambem quebra de linha
//    IgnorePatternWhitespace  permite espacos/comentarios no padrao
//
// ============================================================

using System.Text.RegularExpressions;

namespace RegexLesson;

static class ReplaceAndSplitDemo
{
    public static void Executar()
    {
        Console.WriteLine("\n=== 10) Replace simples — mascarar digitos ===\n");

        string texto = "Meu cartao e 1234 5678 9012 3456.";
        // Troca todo digito por '*'. \d casa cada digito; o resto fica igual.
        string mascarado = Regex.Replace(texto, @"\d", "*");
        Console.WriteLine(mascarado);  // Meu cartao e **** **** **** ****.

        Console.WriteLine("\n=== 11) Replace usando grupos — reformatar data ===\n");

        string data = "09/06/2026";
        // Captura dia/mes/ano e remonta como ano-mes-dia (padrao ISO).
        // No texto de troca, $1 $2 $3 sao os grupos na ordem capturada.
        string iso = Regex.Replace(data, @"(\d{2})/(\d{2})/(\d{4})", "$3-$2-$1");
        Console.WriteLine($"{data}  ->  {iso}");  // 09/06/2026  ->  2026-06-09

        Console.WriteLine("\n=== 12) Replace com grupo NOMEADO ${nome} ===\n");

        string nomeCompleto = "Silva, Lucas";
        // Inverte "Sobrenome, Nome" para "Nome Sobrenome".
        string invertido = Regex.Replace(
            nomeCompleto,
            @"(?<sobrenome>\w+),\s*(?<nome>\w+)",
            "${nome} ${sobrenome}");
        Console.WriteLine($"{nomeCompleto}  ->  {invertido}");  // Silva, Lucas  ->  Lucas Silva

        Console.WriteLine("\n=== 13) Replace com lambda (MatchEvaluator) ===\n");

        string precos = "Itens: 10, 25 e 100 reais.";
        // Cada numero casado e dobrado por uma funcao. O Match chega como
        // parametro; devolvemos a string que entra no lugar.
        string dobrados = Regex.Replace(precos, @"\d+", match =>
        {
            int valor = int.Parse(match.Value);
            return (valor * 2).ToString();
        });
        Console.WriteLine(dobrados);  // Itens: 20, 50 e 200 reais.

        Console.WriteLine("\n=== 14) Split — quebrar por um separador flexivel ===\n");

        // Separador "bagunçado": virgulas e/ou espacos, em qualquer quantidade.
        // [,\s]+ = um ou mais caracteres que sejam virgula ou espaco.
        string bagunca = "maca,  banana ,laranja,uva";
        string[] frutas = Regex.Split(bagunca, @"\s*,\s*");
        Console.WriteLine($"{frutas.Length} frutas: {string.Join(" | ", frutas)}");
        // 4 frutas: maca | banana | laranja | uva

        Console.WriteLine("\n=== 15) RegexOptions.IgnoreCase ===\n");

        string txt = "C# e c# e C-Sharp";
        // Sem IgnoreCase, "C#" e "c#" seriam tratados como diferentes.
        int qtd = Regex.Matches(txt, @"c#", RegexOptions.IgnoreCase).Count;
        Console.WriteLine($"Ocorrencias de \"c#\" (ignorando caixa): {qtd}");  // 2
    }
}
