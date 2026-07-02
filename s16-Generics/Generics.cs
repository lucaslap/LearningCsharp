// ============================================================
//  C# - GENERICS (Tipos e Metodos Genericos)
// ============================================================
//
//  O QUE SAO GENERICS?
//  ───────────────────
//  Generics permitem escrever classes, structs, interfaces, metodos e
//  delegates parametrizados por um TIPO que so e definido no momento do
//  uso. O tipo vira um "parametro" (por convencao chamado T) que o
//  compilador substitui pelo tipo real informado pelo chamador.
//
//  Em vez de escrever uma versao do codigo para cada tipo (uma para int,
//  outra para string, outra para Produto...), escrevemos UMA versao
//  generica que funciona para qualquer tipo.
//
//
//  POR QUE USAR?
//  ─────────────
//  - Reutilizacao: uma unica implementacao serve para muitos tipos.
//  - Seguranca de tipo (type safety): os erros sao pegos em tempo de
//    compilacao, nao em tempo de execucao.
//  - Desempenho: evita boxing/unboxing de tipos de valor e o custo de
//    conversoes (cast) que existiriam ao usar `object`.
//
//  O CONTRA-EXEMPLO (sem generics):
//  Antes dos generics, codigo reutilizavel usava `object`. Isso obrigava
//  a fazer cast manual e abria espaco para erros so detectados em runtime:
//
//      object caixa = 42;
//      string s = (string)caixa; // compila, mas QUEBRA em execucao
//
//  Com generics, o tipo e fixado e o compilador impede esse erro.
//
//
//  SUBTOPICOS DESTA SECAO (padrao hibrido s09)
//  ───────────────────────────────────────────
//  1. Metodos genericos              -> abaixo, nesta classe
//  2. Classes genericas              -> GenericClasses.cs
//  3. Restricoes (constraints)       -> Constraints.cs
//  4. Interfaces genericas e variancia (in/out) -> GenericInterfacesAndVariance.cs
// ============================================================

namespace GenericsLesson;

class Program
{
    static void Main()
    {
        Console.WriteLine("============================================");
        Console.WriteLine("  GENERICS EM C#");
        Console.WriteLine("============================================\n");

        Console.WriteLine("=== 1) Metodos genericos ===\n");

        // Um metodo generico declara seus proprios parametros de tipo entre
        // <>. O compilador normalmente INFERE T a partir dos argumentos, sem
        // precisar informa-lo explicitamente.
        int a = 10, b = 20;
        Trocar(ref a, ref b);
        Console.WriteLine($"  Trocar<int>: a={a}, b={b}");

        string x = "primeiro", y = "segundo";
        Trocar(ref x, ref y); // T inferido como string
        Console.WriteLine($"  Trocar<string>: x={x}, y={y}");

        // O mesmo metodo PrimeiroOuPadrao funciona para qualquer tipo.
        int[] numeros = { 5, 6, 7 };
        string[] vazio = Array.Empty<string>();
        Console.WriteLine($"  Primeiro de numeros: {PrimeiroOuPadrao(numeros)}");
        Console.WriteLine($"  Primeiro de vazio (string): '{PrimeiroOuPadrao(vazio)}'");

        // Tambem e possivel informar o tipo explicitamente quando ele nao
        // pode ser inferido dos argumentos.
        var lista = CriarLista<double>(1.5, 2.5, 3.5);
        Console.WriteLine($"  Lista criada: {string.Join(", ", lista)}");
        Console.WriteLine();

        // ----------------------------------------------------------
        //  Subtopicos em arquivos separados:
        // ----------------------------------------------------------
        GenericClassesDemo.Executar();
        ConstraintsDemo.Executar();
        VarianceDemo.Executar();

        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }

    // Metodo generico: T e um parametro de tipo. `ref` permite trocar os
    // valores das variaveis do chamador. Funciona para qualquer tipo.
    static void Trocar<T>(ref T primeiro, ref T segundo)
    {
        T temporario = primeiro;
        primeiro = segundo;
        segundo = temporario;
    }

    // Retorna o primeiro elemento ou o valor padrao do tipo (default(T)):
    // 0 para int, null para tipos de referencia, etc.
    static T? PrimeiroOuPadrao<T>(T[] itens)
    {
        return itens.Length > 0 ? itens[0] : default;
    }

    // `params T[]` aceita um numero variavel de argumentos do mesmo tipo.
    static List<T> CriarLista<T>(params T[] itens)
    {
        return new List<T>(itens);
    }
}
