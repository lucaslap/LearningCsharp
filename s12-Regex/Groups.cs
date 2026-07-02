// ============================================================
//  C# — Regex: Grupos e Extracao de Dados
// ============================================================
//
//  PARA QUE SERVEM OS GRUPOS?
//  ──────────────────────────
//  Validar (true/false) e so metade da historia. Muitas vezes voce
//  quer EXTRAIR pedaços do texto: o dia, o mes e o ano de uma data;
//  o DDD e o numero de um telefone. Para isso usamos GRUPOS.
//
//  Um grupo e a parte do padrao entre parenteses ( ). Cada grupo
//  "guarda" o trecho que casou, e voce acessa depois por:
//    - NUMERO:  m.Groups[1], m.Groups[2] ...   (1 = primeiro parenteses)
//               m.Groups[0] e SEMPRE o match inteiro.
//    - NOME:    com (?<nome>...) voce acessa por m.Groups["nome"].
//               Mais legivel e nao quebra se voce reordenar o padrao.
//
//
//  GRUPO QUE NAO CAPTURA: (?:...)
//  ──────────────────────────────
//  As vezes voce precisa agrupar so para aplicar um quantificador ou
//  uma alternancia, mas NAO quer guardar aquilo. Use (?:...) — agrupa
//  sem ocupar um numero de grupo. Mais rapido e mantem a numeracao limpa.
//
//
//  BACKREFERENCE: \1, \2 ...
//  ─────────────────────────
//  Dentro do MESMO padrao, \1 significa "o mesmo texto que o grupo 1
//  casou". Util para achar repeticoes (ex: palavra duplicada).
//
// ============================================================

using System.Text.RegularExpressions;

namespace RegexLesson;

static class GroupsDemo
{
    public static void Executar()
    {
        Console.WriteLine("\n=== 6) Grupos por NUMERO — separando uma data ===\n");

        string data = "Hoje e 09/06/2026.";
        // Tres grupos: (dia)/(mes)/(ano). Cada () vira um numero de grupo.
        Match m = Regex.Match(data, @"(\d{2})/(\d{2})/(\d{4})");

        if (m.Success)
        {
            Console.WriteLine($"Match inteiro (Groups[0]): {m.Groups[0].Value}");  // 09/06/2026
            Console.WriteLine($"Dia  (Groups[1]): {m.Groups[1].Value}");           // 09
            Console.WriteLine($"Mes  (Groups[2]): {m.Groups[2].Value}");           // 06
            Console.WriteLine($"Ano  (Groups[3]): {m.Groups[3].Value}");           // 2026
        }

        Console.WriteLine("\n=== 7) Grupos NOMEADOS — (?<nome>...) ===\n");

        string telefone = "Ligue para (11) 98765-4321 hoje.";
        // (?<ddd>...) e (?<numero>...) dao nomes aos grupos.
        //   \(  \)   = parenteses LITERAIS (escapados), pois ( ) sozinhos
        //              seriam grupos. \s? = um espaco opcional.
        string padrao = @"\((?<ddd>\d{2})\)\s?(?<numero>\d{4,5}-\d{4})";
        Match t = Regex.Match(telefone, padrao);

        if (t.Success)
        {
            // Acesso por nome: muito mais legivel que Groups[1]/Groups[2].
            Console.WriteLine($"DDD:    {t.Groups["ddd"].Value}");      // 11
            Console.WriteLine($"Numero: {t.Groups["numero"].Value}");  // 98765-4321
        }

        Console.WriteLine("\n=== 8) Grupo SEM captura (?:...) ===\n");

        // Queremos casar "http://" ou "https://" mas NAO guardar o "s".
        // (?:s)? agrupa o "s" opcional sem virar um grupo numerado.
        string url = "https://exemplo.com";
        Match u = Regex.Match(url, @"^https?://(?<host>[\w.-]+)$");
        // Obs: aqui usei "s?" direto; (?:...) brilha quando o trecho
        // agrupado tem mais de um caractere, ex: (?:www\.)?
        if (u.Success)
        {
            Console.WriteLine($"Host extraido: {u.Groups["host"].Value}");  // exemplo.com
        }

        Console.WriteLine("\n=== 9) Backreference \\1 — achar palavra repetida ===\n");

        string comRepeticao = "isso e e um teste de de regex";
        // (\w+)\s+\1  =  uma palavra, espaco(s), e DE NOVO a mesma palavra.
        // \b nas pontas garante palavra inteira.
        MatchCollection repetidas = Regex.Matches(comRepeticao, @"\b(\w+)\s+\1\b");
        foreach (Match r in repetidas)
        {
            Console.WriteLine($"Palavra repetida: \"{r.Groups[1].Value}\"");
        }
        // Palavra repetida: "e"
        // Palavra repetida: "de"
    }
}
