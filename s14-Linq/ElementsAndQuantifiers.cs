// ============================================================
//  LINQ - Selecao de Elementos e Quantificadores
// ============================================================
//
//  OPERADORES DE ELEMENTO (retornam um unico elemento)
//  ───────────────────────────────────────────────────
//    First / FirstOrDefault    primeiro elemento (que satisfaca o
//                              predicado, se informado).
//    Last  / LastOrDefault     ultimo elemento.
//    Single / SingleOrDefault  exige que exista EXATAMENTE um elemento
//                              correspondente; lanca excecao se houver
//                              mais de um.
//
//    A variante sem sufixo lanca InvalidOperationException quando nao ha
//    nenhum elemento correspondente. A variante "OrDefault" retorna, nesse
//    caso, o valor padrao do tipo (null para tipos de referencia, 0 para
//    int etc.), o que evita a excecao.
//
//  QUANTIFICADORES (retornam bool)
//  ───────────────────────────────
//    Any   verifica se existe ao menos um elemento (ou um que satisfaca
//          o predicado).
//    All   verifica se TODOS os elementos satisfazem o predicado.
//
//  PARTICIONAMENTO
//  ───────────────
//    Take / Skip    obtem ou ignora os N primeiros elementos.
//    Distinct       remove duplicatas.
// ============================================================

namespace LinqLesson;

static class ElementsAndQuantifiersDemo
{
    public static void Executar(List<Program.Produto> produtos)
    {
        Console.WriteLine("\n=== 11) First / FirstOrDefault ===\n");

        // First retorna o primeiro produto que custa mais de 1000.
        var primeiroCaro = produtos.First(p => p.Preco > 1000m);
        Console.WriteLine($"Primeiro acima de 1000: {primeiroCaro.Nome}");

        // FirstOrDefault evita excecao quando nada corresponde: como
        // Produto e um tipo de referencia, o retorno e null.
        var inexistente = produtos.FirstOrDefault(p => p.Preco > 100000m);
        Console.WriteLine($"Acima de 100000: {(inexistente is null ? "nenhum" : inexistente.Nome)}");

        Console.WriteLine("\n=== 12) Any / All - quantificadores ===\n");

        bool existeSemEstoque = produtos.Any(p => p.Estoque == 0);
        bool todosTemPreco = produtos.All(p => p.Preco > 0m);

        Console.WriteLine($"Existe produto sem estoque? {existeSemEstoque}");
        Console.WriteLine($"Todos tem preco positivo?   {todosTemPreco}");

        Console.WriteLine("\n=== 13) Take / Skip / Distinct ===\n");

        // Os tres produtos mais caros: ordena e pega os 3 primeiros.
        var tresMaisCaros = produtos
            .OrderByDescending(p => p.Preco)
            .Take(3)
            .Select(p => p.Nome);

        Console.WriteLine("Tres mais caros: " + string.Join(", ", tresMaisCaros));

        // Distinct sobre as categorias para listar cada uma uma so vez.
        var categorias = produtos.Select(p => p.Categoria).Distinct();
        Console.WriteLine("Categorias distintas: " + string.Join(", ", categorias));

        // Skip ignora os 2 primeiros (apos ordenar por preco crescente).
        var semOsDoisMaisBaratos = produtos
            .OrderBy(p => p.Preco)
            .Skip(2)
            .Select(p => p.Nome);

        Console.WriteLine("Sem os dois mais baratos: " + string.Join(", ", semOsDoisMaisBaratos));
    }
}
