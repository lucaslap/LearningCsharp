// ============================================================
//  TESTES (xUnit) - GUIANDO O DESENVOLVIMENTO
// ============================================================
//
//  O QUE E TDD?
//  ────────────
//  Test-Driven Development e escrever o TESTE ANTES do codigo de producao.
//  O desenvolvimento avanca em pequenos ciclos chamados Red-Green-Refactor:
//
//    1. RED     - escreva um teste que descreve o proximo comportamento
//                 desejado. Ele FALHA (o codigo ainda nao existe).
//    2. GREEN   - escreva o MINIMO de codigo para o teste passar.
//    3. REFACTOR- melhore o codigo (nomes, duplicacao, design) mantendo
//                 todos os testes verdes.
//
//  Repita o ciclo para cada novo requisito. Os testes acumulados formam uma
//  rede de seguranca que permite refatorar sem medo de quebrar o que ja
//  funcionava (teste de regressao).
//
//  ESTRUTURA DE UM TESTE: AAA (Arrange-Act-Assert)
//  ───────────────────────────────────────────────
//    Arrange - prepara os dados e o objeto sob teste.
//    Act     - executa a acao que se quer verificar.
//    Assert  - verifica se o resultado e o esperado.
//
//  xUnit - ATRIBUTOS PRINCIPAIS
//  ────────────────────────────
//  - [Fact]   : um teste sem parametros; sempre roda igual.
//  - [Theory] : um teste parametrizado, executado uma vez para cada
//               conjunto de dados fornecido por [InlineData].
//
//  Boa pratica de nomenclatura: NomeDoMetodo_Cenario_ResultadoEsperado,
//  descrevendo o comportamento em vez de detalhes de implementacao.
// ============================================================

namespace TddLesson;

public class StringCalculatorTests
{
    // Objeto sob teste. Reinstanciado a cada teste porque o xUnit cria uma
    // NOVA instancia da classe de teste para cada metodo, garantindo
    // isolamento entre os casos.
    private readonly StringCalculator _calculadora = new StringCalculator();

    // Requisito 1 (RED inicial): string vazia deve retornar 0.
    [Fact]
    public void Add_StringVazia_RetornaZero()
    {
        // Arrange
        string entrada = "";

        // Act
        int resultado = _calculadora.Add(entrada);

        // Assert
        Assert.Equal(0, resultado);
    }

    // Requisito 2: um unico numero retorna ele mesmo.
    // [Theory] evita repetir o mesmo teste para varios valores; cada
    // [InlineData] vira uma execucao independente com aqueles argumentos.
    [Theory]
    [InlineData("0", 0)]
    [InlineData("1", 1)]
    [InlineData("42", 42)]
    public void Add_UmNumero_RetornaOProprioNumero(string entrada, int esperado)
    {
        int resultado = _calculadora.Add(entrada);
        Assert.Equal(esperado, resultado);
    }

    // Requisito 3: dois numeros separados por virgula sao somados.
    [Fact]
    public void Add_DoisNumeros_RetornaASoma()
    {
        int resultado = _calculadora.Add("1,2");
        Assert.Equal(3, resultado);
    }

    // Requisito 4: qualquer quantidade de numeros.
    [Theory]
    [InlineData("1,2,3", 6)]
    [InlineData("10,20,30,40", 100)]
    public void Add_VariosNumeros_RetornaASoma(string entrada, int esperado)
    {
        int resultado = _calculadora.Add(entrada);
        Assert.Equal(esperado, resultado);
    }

    // Requisito 5: quebra de linha tambem separa numeros.
    [Fact]
    public void Add_ComQuebraDeLinha_TrataComoSeparador()
    {
        int resultado = _calculadora.Add("1\n2,3");
        Assert.Equal(6, resultado);
    }

    // Requisito 6: numeros negativos lancam ArgumentException.
    // Assert.Throws verifica que a excecao esperada foi lancada e devolve a
    // instancia, permitindo inspecionar a mensagem.
    [Fact]
    public void Add_ComNegativo_LancaArgumentException()
    {
        ArgumentException excecao =
            Assert.Throws<ArgumentException>(() => _calculadora.Add("1,-2,3"));

        Assert.Contains("-2", excecao.Message);
    }

    // A mensagem deve listar TODOS os negativos, nao apenas o primeiro.
    [Fact]
    public void Add_ComVariosNegativos_ListaTodosNaMensagem()
    {
        ArgumentException excecao =
            Assert.Throws<ArgumentException>(() => _calculadora.Add("-1,2,-3"));

        Assert.Contains("-1", excecao.Message);
        Assert.Contains("-3", excecao.Message);
    }
}
