# TDD - Test-Driven Development em C#

Anotacoes da secao sobre desenvolvimento guiado por testes.

## O que e

TDD e uma disciplina de desenvolvimento em que o **teste e escrito antes** do
codigo de producao. Em vez de escrever a implementacao e depois testa-la, o
teste define o comportamento desejado e conduz o design do codigo.

## O ciclo Red-Green-Refactor

1. **Red** - escreva um teste para o proximo comportamento desejado. Ele falha,
   pois o codigo ainda nao existe (ou nao atende ao requisito).
2. **Green** - escreva o minimo de codigo necessario para o teste passar. Sem
   otimizacoes prematuras; o objetivo e apenas ficar verde.
3. **Refactor** - com o teste passando, melhore o codigo (nomes, duplicacao,
   estrutura) mantendo todos os testes verdes.

Repita para cada requisito. Os testes acumulados viram uma rede de seguranca
que protege contra regressoes durante futuras mudancas.

## Beneficios

- Design guiado pelo uso real da API (voce escreve como gostaria de chamar o
  codigo antes de implementa-lo).
- Cobertura de testes construida naturalmente, nao como tarefa posterior.
- Refatoracao com confianca: se algo quebrar, um teste avisa.
- Documentacao viva: os testes descrevem o comportamento esperado.

## Estrutura de um teste: AAA

- **Arrange** - prepara dados e o objeto sob teste.
- **Act** - executa a acao a ser verificada.
- **Assert** - confirma que o resultado corresponde ao esperado.

## xUnit - atributos usados

| Atributo | Uso |
|---|---|
| `[Fact]` | Teste sem parametros; roda sempre da mesma forma. |
| `[Theory]` + `[InlineData]` | Teste parametrizado; executa uma vez por conjunto de dados. |
| `Assert.Equal` | Compara valor esperado x obtido. |
| `Assert.Throws<T>` | Verifica que a excecao esperada foi lancada. |
| `Assert.Contains` | Verifica que um trecho existe em texto/colecao. |

O xUnit cria uma **nova instancia** da classe de teste para cada metodo,
garantindo isolamento entre os casos.

## Nomenclatura de testes

Padrao usado nesta secao: `NomeDoMetodo_Cenario_ResultadoEsperado`
(ex.: `Add_ComNegativo_LancaArgumentException`). O nome descreve o
comportamento, nao detalhes de implementacao.

## A kata String Calculator

O exemplo desta secao e a kata **String Calculator** (de Roy Osherove),
construida requisito a requisito seguindo o ciclo Red-Green-Refactor. Cada
regra da classe `StringCalculator` nasceu de um teste que falhou primeiro.

## Como executar

Esta secao usa um **projeto de teste** (xUnit), diferente das demais que sao
aplicacoes de console. Portanto rode com `dotnet test`, nao `dotnet run`:

```bash
dotnet test
```

A partir da raiz do repositorio:

```bash
dotnet test s18-TDD
```
