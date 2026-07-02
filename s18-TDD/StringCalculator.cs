// ============================================================
//  C# - TDD (Test-Driven Development)
// ============================================================
//
//  CODIGO SOB TESTE (System Under Test - SUT)
//  ──────────────────────────────────────────
//  Este arquivo contem a classe StringCalculator, o CODIGO DE PRODUCAO que
//  esta sendo desenvolvido guiado por testes. Ele foi construido de forma
//  incremental: cada regra abaixo so passou a existir DEPOIS que um teste
//  que a exigia foi escrito e falhou (ver StringCalculatorTests.cs).
//
//  A kata "String Calculator" (de Roy Osherove) e um exercicio classico de
//  TDD justamente porque cada requisito e pequeno e se encaixa bem no ciclo
//  Red-Green-Refactor.
//
//  REQUISITOS IMPLEMENTADOS (cada um nasceu de um teste):
//  1. String vazia retorna 0.
//  2. Um unico numero retorna o proprio numero.
//  3. Dois numeros separados por virgula sao somados.
//  4. Quantidade qualquer de numeros e somada.
//  5. Quebra de linha (\n) tambem funciona como separador.
//  6. Numeros negativos lancam excecao informando quais foram.
// ============================================================

namespace TddLesson;

public class StringCalculator
{
    // Separadores aceitos entre os numeros. Note que so adicionamos o '\n'
    // aqui quando um teste passou a exigir esse comportamento (requisito 5).
    private static readonly char[] Separadores = { ',', '\n' };

    // Recebe uma string com numeros e devolve a soma deles.
    public int Add(string numeros)
    {
        // Requisito 1: string vazia (ou nula) resulta em 0. Esse foi o
        // PRIMEIRO teste a passar; comecar pelo caso mais simples e uma
        // pratica central do TDD.
        if (string.IsNullOrEmpty(numeros))
            return 0;

        // Requisitos 2 a 5: quebramos a entrada pelos separadores e
        // convertemos cada pedaco para inteiro.
        int[] valores = numeros
            .Split(Separadores)
            .Select(int.Parse)
            .ToArray();

        // Requisito 6: numeros negativos nao sao permitidos. Coletamos TODOS
        // os negativos e os reportamos juntos na mensagem da excecao, em vez
        // de falhar no primeiro encontrado.
        int[] negativos = valores.Where(v => v < 0).ToArray();
        if (negativos.Length > 0)
        {
            throw new ArgumentException(
                $"Negativos nao sao permitidos: {string.Join(", ", negativos)}");
        }

        return valores.Sum();
    }
}
