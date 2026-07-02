// ============================================================
//  2) FUNCOES PEQUENAS E COESAS
// ============================================================
//
//  Uma funcao deve fazer UMA coisa, faze-la bem e fazer somente isso.
//  Esse e o Principio da Responsabilidade Unica aplicado a metodos.
//
//  DIRETRIZES:
//  - Funcoes pequenas: idealmente cabem na tela sem rolagem.
//  - Um nivel de abstracao por funcao: nao misture detalhes de baixo
//    nivel (formatacao de string) com regras de alto nivel (decisao
//    de negocio) no mesmo metodo.
//  - Poucos parametros: zero, um ou dois sao ideais. Tres ja pedem
//    atencao; mais que isso, considere agrupar em um objeto.
//  - Evite "flag arguments" (parametros booleanos que mudam o
//    comportamento): geralmente indicam que a funcao faz duas coisas.
//  - Prefira retornar cedo (guard clauses) a aninhar varios if/else.
// ============================================================

namespace CleanCodeLesson;

class FunctionsDemo
{
    public static void Executar()
    {
        Console.WriteLine("=== 2) Funcoes pequenas e coesas ===\n");

        var pedido = new Pedido(
            ValorBruto: 1000m,
            Cliente: new Cliente("Ana", EhVip: true),
            Cupom: "PROMO10");

        // O codigo de alto nivel le-se quase como uma descricao do processo,
        // pois cada passo foi extraido para uma funcao com nome claro.
        decimal total = CalcularTotalDoPedido(pedido);
        Console.WriteLine($"  Total do pedido: {total:C}");
        Console.WriteLine();
    }

    // Funcao orquestradora: um unico nivel de abstracao. Ela decide a
    // SEQUENCIA dos passos, delegando os detalhes a funcoes menores.
    private static decimal CalcularTotalDoPedido(Pedido pedido)
    {
        decimal comDesconto = AplicarDescontos(pedido);
        decimal comFrete = AplicarFrete(comDesconto, pedido.Cliente);
        return comFrete;
    }

    private static decimal AplicarDescontos(Pedido pedido)
    {
        decimal valor = pedido.ValorBruto;

        if (pedido.Cliente.EhVip)
        {
            valor -= valor * 0.05m; // 5% para clientes VIP
        }

        if (TemCupomValido(pedido.Cupom))
        {
            valor -= valor * 0.10m; // 10% pelo cupom
        }

        return valor;
    }

    private static decimal AplicarFrete(decimal valor, Cliente cliente)
    {
        // Guard clause: trata o caso especial cedo e sai, evitando aninhamento.
        if (cliente.EhVip)
        {
            return valor; // frete gratis para VIP
        }

        const decimal Frete = 25m;
        return valor + Frete;
    }

    // Pequena funcao com responsabilidade unica e nome que vira a propria
    // documentacao da condicao.
    private static bool TemCupomValido(string? cupom)
    {
        return !string.IsNullOrWhiteSpace(cupom);
    }

    public record Cliente(string Nome, bool EhVip);
    public record Pedido(decimal ValorBruto, Cliente Cliente, string? Cupom);
}
