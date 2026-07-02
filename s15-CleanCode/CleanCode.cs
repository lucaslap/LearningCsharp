// ============================================================
//  C# - CLEAN CODE (Codigo Limpo)
// ============================================================
//
//  O QUE E CLEAN CODE?
//  ───────────────────
//  Clean Code e um conjunto de praticas e principios que tornam o
//  codigo facil de LER, ENTENDER e MANTER. O termo foi popularizado
//  por Robert C. Martin (Uncle Bob) no livro "Clean Code: A Handbook
//  of Agile Software Craftsmanship".
//
//  A ideia central: codigo e lido muitas mais vezes do que e escrito.
//  Portanto, otimizar para a leitura por outras pessoas (e por voce
//  mesmo no futuro) traz mais valor do que economizar alguns segundos
//  na escrita. Codigo limpo nao e sobre o compilador - o compilador
//  aceita qualquer codigo valido. E sobre o ser humano que vai mantê-lo.
//
//
//  POR QUE IMPORTA?
//  ────────────────
//  - Reduz o tempo gasto para entender o que o codigo faz.
//  - Diminui a chance de introduzir bugs ao modificar.
//  - Facilita a colaboracao em equipe.
//  - Diminui o custo de manutencao ao longo do tempo.
//
//
//  PRINCIPIOS COBERTOS NESTA SECAO
//  ───────────────────────────────
//  1. Nomes significativos        -> Naming.cs
//  2. Funcoes pequenas e coesas   -> Functions.cs
//  3. Comentarios e formatacao    -> CommentsAndFormatting.cs
//  4. Principios gerais           -> Principles.cs
//     (DRY, KISS, YAGNI e uma introducao ao SOLID)
//
//  Cada subtopico esta em um arquivo separado expondo Executar(),
//  seguindo o padrao hibrido adotado a partir da secao s09.
//
//  IMPORTANTE: muitos exemplos mostram o par "ANTES" (codigo ruim) e
//  "DEPOIS" (codigo limpo) lado a lado, para deixar o contraste claro.
//  Os trechos "ANTES" existem apenas para fins didaticos.
// ============================================================

namespace CleanCodeLesson;

class Program
{
    static void Main()
    {
        Console.WriteLine("============================================");
        Console.WriteLine("  CLEAN CODE EM C#");
        Console.WriteLine("============================================\n");

        NamingDemo.Executar();
        FunctionsDemo.Executar();
        CommentsAndFormattingDemo.Executar();
        PrinciplesDemo.Executar();

        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }
}
