// ============================================================
//  LINQ - Agregacao
// ============================================================
//
//  Operadores de agregacao percorrem a sequencia e produzem um unico
//  valor. Por executarem a consulta imediatamente, nao participam da
//  execucao adiada.
//
//    Count      quantidade de elementos (opcionalmente com predicado).
//    Sum        soma dos valores de uma chave numerica.
//    Min / Max  menor / maior valor de uma chave.
//    Average    media aritmetica de uma chave numerica.
//    Aggregate  agregacao personalizada: define como combinar os
//               elementos dois a dois a partir de um acumulador.
// ============================================================

namespace LinqLesson;

static class AggregationDemo
{
    public static void Executar(List<Program.Produto> produtos)
    {
        Console.WriteLine("\n=== 9) Count, Sum, Min, Max, Average ===\n");

        // Count aceita um predicado opcional para contar apenas os
        // elementos que satisfazem a condicao.
        int total = produtos.Count();
        int semEstoque = produtos.Count(p => p.Estoque == 0);

        Console.WriteLine($"Total de produtos: {total}");
        Console.WriteLine($"Sem estoque:       {semEstoque}");

        // Sum, Min, Max e Average recebem um seletor que indica qual
        // valor numerico deve ser agregado.
        decimal valorEmEstoque = produtos.Sum(p => p.Preco * p.Estoque);
        decimal maisCaro = produtos.Max(p => p.Preco);
        decimal maisBarato = produtos.Min(p => p.Preco);
        decimal precoMedio = produtos.Average(p => p.Preco);

        Console.WriteLine($"Valor total em estoque: {valorEmEstoque:C}");
        Console.WriteLine($"Preco mais alto:        {maisCaro:C}");
        Console.WriteLine($"Preco mais baixo:       {maisBarato:C}");
        Console.WriteLine($"Preco medio:            {precoMedio:C}");

        Console.WriteLine("\n=== 10) Aggregate - agregacao personalizada ===\n");

        // Aggregate combina os elementos de forma definida por nos.
        // Aqui concatenamos os nomes, comecando de uma string vazia
        // (o acumulador) e adicionando cada nome separado por " | ".
        string listaNomes = produtos
            .Select(p => p.Nome)
            .Aggregate((acumulado, nome) => $"{acumulado} | {nome}");

        Console.WriteLine("Nomes concatenados:");
        Console.WriteLine($"  {listaNomes}");
    }
}
