// ============================================================
//  LINQ - Ordenacao e Agrupamento
// ============================================================
//
//  ORDENACAO
//  ─────────
//    OrderBy            ordena de forma crescente por uma chave.
//    OrderByDescending  ordena de forma decrescente.
//    ThenBy /           criterios de desempate, aplicados quando a
//    ThenByDescending   chave anterior produz valores iguais.
//
//  AGRUPAMENTO
//  ───────────
//    GroupBy organiza os elementos em grupos a partir de uma chave.
//    O resultado e uma sequencia de grupos; cada grupo expoe a
//    propriedade Key (o valor da chave) e e, ele proprio, uma sequencia
//    dos elementos que compartilham aquela chave.
// ============================================================

namespace LinqLesson;

static class OrderingAndGroupingDemo
{
    public static void Executar(List<Program.Produto> produtos)
    {
        Console.WriteLine("\n=== 6) OrderBy / ThenBy - ordenacao com desempate ===\n");

        // Ordena por categoria (crescente) e, dentro de cada categoria,
        // por preco decrescente.
        var ordenados = produtos
            .OrderBy(p => p.Categoria)
            .ThenByDescending(p => p.Preco);

        foreach (var p in ordenados)
        {
            Console.WriteLine($"  {p.Categoria,-13} {p.Nome,-10} {p.Preco,10:C}");
        }

        Console.WriteLine("\n=== 7) GroupBy - agrupar por categoria ===\n");

        // Agrupa os produtos por categoria. Cada grupo tem uma Key
        // (a categoria) e os elementos correspondentes.
        var porCategoria = produtos.GroupBy(p => p.Categoria);

        foreach (var grupo in porCategoria)
        {
            Console.WriteLine($"Categoria: {grupo.Key} ({grupo.Count()} itens)");
            foreach (var p in grupo)
            {
                Console.WriteLine($"    - {p.Nome}");
            }
        }

        Console.WriteLine("\n=== 8) GroupBy combinado com agregacao ===\n");

        // Para cada categoria, calcula o preco medio dos produtos.
        var mediaPorCategoria = produtos
            .GroupBy(p => p.Categoria)
            .Select(g => new { Categoria = g.Key, PrecoMedio = g.Average(p => p.Preco) });

        foreach (var item in mediaPorCategoria)
        {
            Console.WriteLine($"  {item.Categoria,-13} preco medio: {item.PrecoMedio:C}");
        }
    }
}
