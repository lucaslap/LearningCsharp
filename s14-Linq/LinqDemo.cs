// ============================================================
//  C# - LINQ (Language Integrated Query)
// ============================================================
//
//  O QUE E LINQ?
//  ────────────
//  LINQ e um conjunto de recursos da linguagem que permite consultar
//  colecoes de dados (listas, arrays, dicionarios, resultados de banco,
//  XML etc.) usando uma sintaxe unica e declarativa. Em vez de descrever
//  PASSO A PASSO como percorrer e filtrar os dados com for/foreach e
//  variaveis auxiliares, descrevemos O QUE queremos obter.
//
//  Os operadores de consulta vivem no namespace System.Linq. Ele faz
//  parte dos ImplicitUsings deste projeto, portanto nao e necessario
//  declarar o `using` explicitamente.
//
//
//  DUAS SINTAXES EQUIVALENTES
//  ──────────────────────────
//  LINQ oferece duas formas de escrever a mesma consulta:
//
//  1. Sintaxe de metodo (method syntax) - encadeamento de metodos de
//     extensao (Where, Select, OrderBy ...) com expressoes lambda.
//     E a forma mais comum e cobre todos os operadores.
//
//  2. Sintaxe de consulta (query syntax) - parecida com SQL, usando as
//     palavras-chave from, where, select, orderby, group.
//     O compilador a traduz internamente para a sintaxe de metodo.
//
//  Ambas produzem o mesmo resultado; a escolha e questao de legibilidade.
//
//
//  EXECUCAO ADIADA (deferred execution)
//  ────────────────────────────────────
//  A maioria dos operadores LINQ NAO executa a consulta no momento em
//  que ela e definida. A consulta so e avaliada quando seus resultados
//  sao efetivamente percorridos (por exemplo, em um foreach) ou quando
//  e materializada com ToList(), ToArray(), Count() etc.
//  Isso significa que, se a fonte de dados mudar depois de definida a
//  consulta, o resultado refletira o estado mais recente da fonte.
//
//
//  TIPOS DE RETORNO
//  ────────────────
//  Operadores que filtram ou projetam (Where, Select, OrderBy ...)
//  retornam IEnumerable<T> e participam da execucao adiada.
//  Operadores que produzem um valor unico (Count, Sum, First, Any ...)
//  executam a consulta imediatamente.
// ============================================================

namespace LinqLesson;

class Program
{
    // Modelo simples usado nos exemplos desta secao.
    public record Produto(string Nome, string Categoria, decimal Preco, int Estoque);

    static void Main()
    
        // Fonte de dados compartilhada pelos exemplos.
        List<Produto> produtos = new()
        {
            new("Teclado",   "Perifericos", 150.00m, 30),
            new("Mouse",     "Perifericos",  90.00m,  0),
            new("Monitor",   "Telas",       950.00m, 12),
            new("Notebook",  "Computadores", 4200.00m, 5),
            new("Webcam",    "Perifericos", 220.00m,  8),
            new("Headset",   "Perifericos", 310.00m,  0),
            new("Desktop",   "Computadores", 3100.00m, 3),
        };

        Console.WriteLine("=== 1) Where - filtrar elementos por uma condicao ===\n");

        // Where recebe um predicado (funcao que retorna bool) e mantem
        // apenas os elementos para os quais ele e verdadeiro.
        IEnumerable<Produto> emEstoque = produtos.Where(p => p.Estoque > 0);

        foreach (Produto p in emEstoque)
        {
            Console.WriteLine($"  {p.Nome,-10} estoque: {p.Estoque}");
        }

        Console.WriteLine("\n=== 2) Select - projetar (transformar) cada elemento ===\n");

        // Select transforma cada elemento em outra coisa. Aqui extraimos
        // apenas o nome, produzindo uma sequencia de strings.
        IEnumerable<string> nomes = produtos.Select(p => p.Nome);
        Console.WriteLine("Nomes: " + string.Join(", ", nomes));

        // Tambem e possivel projetar para um tipo anonimo com varios campos.
        var resumo = produtos.Select(p => new { p.Nome, ComImposto = p.Preco * 1.1m });
        foreach (var item in resumo)
        {
            Console.WriteLine($"  {item.Nome,-10} com imposto: {item.ComImposto:C}");
        }

        Console.WriteLine("\n=== 3) Where + Select encadeados ===\n");

        // Os operadores podem ser encadeados. A consulta abaixo seleciona
        // os nomes dos produtos da categoria Perifericos com preco abaixo
        // de 200.
        IEnumerable<string> perifericosBaratos = produtos
            .Where(p => p.Categoria == "Perifericos")
            .Where(p => p.Preco < 200m)
            .Select(p => p.Nome);

        Console.WriteLine("Perifericos abaixo de 200: " + string.Join(", ", perifericosBaratos));

        Console.WriteLine("\n=== 4) Sintaxe de consulta (query syntax) ===\n");

        // A mesma logica do exemplo anterior, escrita na sintaxe de
        // consulta. O compilador a traduz para chamadas de Where/Select.
        IEnumerable<string> perifericosBaratosQuery =
            from p in produtos
            where p.Categoria == "Perifericos" && p.Preco < 200m
            select p.Nome;

        Console.WriteLine("Mesmo resultado: " + string.Join(", ", perifericosBaratosQuery));

        Console.WriteLine("\n=== 5) Execucao adiada na pratica ===\n");

        // A consulta e definida aqui, mas ainda nao executada.
        var caros = produtos.Where(p => p.Preco > 1000m).Select(p => p.Nome);

        // Adicionamos um item DEPOIS de definir a consulta.
        produtos.Add(new("Servidor", "Computadores", 8000.00m, 2));

        // Ao percorrer agora, o novo item ja aparece, pois a consulta so
        // e avaliada neste momento.
        Console.WriteLine("Produtos acima de 1000 (inclui o adicionado depois):");
        Console.WriteLine("  " + string.Join(", ", caros));

        // ----------------------------------------------------------
        //  Sub-topicos em arquivos separados (padrao hibrido s09):
        // ----------------------------------------------------------

        OrderingAndGroupingDemo.Executar(produtos);
        AggregationDemo.Executar(produtos);
        ElementsAndQuantifiersDemo.Executar(produtos);

        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }
}
