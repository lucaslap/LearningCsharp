// ============================================================
//  3) COMENTARIOS E FORMATACAO
// ============================================================
//
//  COMENTARIOS
//  ───────────
//  O melhor comentario e aquele que voce nao precisou escrever porque o
//  codigo ja se explica. Comentarios nao corrigem codigo ruim - um nome
//  melhor ou uma funcao extraida costumam ser superiores a um comentario.
//
//  COMENTARIOS RUINS (evitar):
//  - Redundantes: repetem o que o codigo ja diz claramente.
//  - Desatualizados: mentem sobre o que o codigo faz (pior que nenhum).
//  - Codigo comentado: deixe o controle de versao (git) guardar o historico.
//
//  COMENTARIOS UTEIS (manter):
//  - Explicar o PORQUE de uma decisao nao obvia (regra de negocio, workaround).
//  - Avisos sobre consequencias (ex.: ordem importa, efeito colateral).
//  - Documentacao XML (/// <summary>) em APIs publicas.
//
//
//  FORMATACAO
//  ──────────
//  Formatacao consistente reduz a carga cognitiva. Em C#:
//  - Indentacao e chaves consistentes (o padrao .NET usa chaves na linha
//    seguinte - estilo Allman).
//  - Linhas em branco separam blocos com ideias diferentes.
//  - Variaveis declaradas proximo de onde sao usadas.
//  - Uma instrucao por linha; linhas curtas o suficiente para ler sem rolar.
// ============================================================

namespace CleanCodeLesson;

class CommentsAndFormattingDemo
{
    public static void Executar()
    {
        Console.WriteLine("=== 3) Comentarios e formatacao ===\n");

        // ANTES (comentario redundante): o comentario apenas repete o codigo.
        //   // incrementa o contador em 1
        //   contador = contador + 1;

        // DEPOIS: o codigo e claro o bastante; nenhum comentario necessario.
        int totalDeTentativas = 0;
        totalDeTentativas++;

        decimal preco = 100m;
        decimal precoComJuros = AplicarJurosDeAtraso(preco);

        Console.WriteLine($"  Tentativas: {totalDeTentativas}");
        Console.WriteLine($"  Preco com juros de atraso: {precoComJuros:C}");

        // Documentacao XML aparece no IntelliSense de quem consome o metodo.
        Console.WriteLine($"  Dobro de 21: {Dobrar(21)}");

        Console.WriteLine();
    }

    private static decimal AplicarJurosDeAtraso(decimal valor)
    {
        // Comentario UTIL: explica o PORQUE (regra de negocio), nao o "como".
        // Multa fixa de 2% exigida pelo Codigo de Defesa do Consumidor para
        // atraso no pagamento; o percentual nao deve ser alterado sem revisao juridica.
        const decimal MultaPorAtraso = 0.02m;
        return valor + (valor * MultaPorAtraso);
    }

    /// <summary>
    /// Retorna o dobro do numero informado.
    /// </summary>
    /// <param name="numero">Valor a ser multiplicado por dois.</param>
    /// <returns>O valor de <paramref name="numero"/> multiplicado por 2.</returns>
    private static int Dobrar(int numero) => numero * 2;
}
