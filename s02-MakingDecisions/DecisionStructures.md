# Estruturas de Decisão em C#

## 1. if / else if / else

Executa blocos diferentes dependendo de uma condição.

```csharp
int idade = 20;

if (idade < 13)
{
    Console.WriteLine("Criança");
}
else if (idade < 18)
{
    Console.WriteLine("Adolescente");
}
else if (idade < 60)
{
    Console.WriteLine("Adulto");
}
else
{
    Console.WriteLine("Idoso");
}
```

---

## 2. Operador Ternário

Forma compacta de um `if/else` que retorna um valor: `condição ? valorSeVerdadeiro : valorSeFalso`

```csharp
bool temCarteira = true;
string resultado = temCarteira ? "Pode dirigir" : "Não pode dirigir";
```

---

## 3. switch statement

Compara uma variável contra múltiplos valores constantes. Dois `case` podem compartilhar o mesmo bloco.

```csharp
int diaDaSemana = 3;

switch (diaDaSemana)
{
    case 1: Console.WriteLine("Segunda-feira"); break;
    case 2: Console.WriteLine("Terça-feira");   break;
    case 3: Console.WriteLine("Quarta-feira");  break;
    case 4: Console.WriteLine("Quinta-feira");  break;
    case 5: Console.WriteLine("Sexta-feira");   break;
    case 6:
    case 7: Console.WriteLine("Final de semana"); break;
    default: Console.WriteLine("Dia inválido");   break;
}
```

---

## 4. switch expression (C# 8+)

Versão moderna e compacta do `switch` que **retorna um valor**. O `_` é o caso padrão.

```csharp
string estacao = "inverno";

string descricao = estacao switch
{
    "primavera" => "Flores brotando",
    "verão"     => "Calor intenso",
    "outono"    => "Folhas caindo",
    "inverno"   => "Frio e neve",
    _           => "Estação desconhecida"
};
```

---

## 5. Operadores Lógicos

| Operador | Significado | Verdadeiro quando |
|:---:|---|---|
| `&&` | E | **ambas** as condições são verdadeiras |
| `\|\|` | OU | **ao menos uma** condição é verdadeira |
| `!` | NÃO | a condição é **falsa** |

```csharp
int nota = 75;
bool frequenciaSuficiente = true;

if (nota >= 60 && frequenciaSuficiente)
    Console.WriteLine("Aprovado");
else if (nota >= 40 || frequenciaSuficiente)
    Console.WriteLine("Em recuperação");
else
    Console.WriteLine("Reprovado");
```

---

## 6. Padrões Relacionais no switch expression (C# 9+)

Compara intervalos numéricos diretamente no `switch`, sem `if` encadeado.

```csharp
int pontuacao = 82;

string classificacao = pontuacao switch
{
    >= 90 => "Excelente",
    >= 70 => "Bom",
    >= 50 => "Regular",
    _     => "Insuficiente"
};
```

---

## 7. Padrões Lógicos: and / or / not (C# 9+)

Combina padrões com palavras-chave dentro do `switch`.

```csharp
int temperatura = 22;

string clima = temperatura switch
{
    < 0             => "Congelando",
    >= 0 and < 15   => "Frio",
    >= 15 and < 28  => "Agradável",
    >= 28 and < 35  => "Quente",
    _               => "Muito quente"
};
```

---

## 8. Padrão is — verificação de tipo e nulo (C# 7+ / 9+)

Verifica o tipo de um objeto e já declara a variável convertida na mesma linha. `not null` é um padrão lógico do C# 9.

```csharp
object valor = "Olá, .NET 10!";

if (valor is string texto)
    Console.WriteLine($"É uma string com {texto.Length} caracteres.");

object nulo = null!;

if (nulo is not null)
    Console.WriteLine("Tem valor");
else
    Console.WriteLine("Valor é nulo");
```

---

## 9. Entrada do usuário — case insensitive com ToLower()

`.ToLower()` converte o texto para minúsculo antes de comparar, então `"Sim"`, `"SIM"` e `"sim"` são tratados da mesma forma. O `?.` evita exceção se `ReadLine()` retornar `null`.

```csharp
string? resposta = Console.ReadLine()?.ToLower();

if (resposta == "sim")
    Console.WriteLine("Ótimo, vamos continuar!");
else if (resposta == "não" || resposta == "nao")
    Console.WriteLine("Tudo bem, encerrando.");
else
    Console.WriteLine("Resposta não reconhecida.");
```

---

## 10. switch com ToLower()

Normaliza a entrada antes do `switch` para que todos os `case` fiquem em minúsculo. `?? ""` substitui `null` por string vazia.

```csharp
string linguagem = (Console.ReadLine() ?? "").ToLower();

switch (linguagem)
{
    case "csharp":
    case "c#":
        Console.WriteLine("C# — linguagem da Microsoft, roda no .NET.");
        break;
    case "python":
        Console.WriteLine("Python — famoso por ciência de dados e scripts.");
        break;
    default:
        Console.WriteLine($"Linguagem \"{linguagem}\" não está na lista.");
        break;
}
```

---

## 11. StringComparison.OrdinalIgnoreCase

Alternativa ao `ToLower()`: compara ignorando maiúsculas/minúsculas **sem criar uma string intermediária**. Preferível quando a entrada pode ser `null`.

```csharp
string? cor = Console.ReadLine();

if (string.Equals(cor, "vermelho", StringComparison.OrdinalIgnoreCase))
    Console.WriteLine("Cor quente: vermelho");
else if (string.Equals(cor, "azul", StringComparison.OrdinalIgnoreCase))
    Console.WriteLine("Cor fria: azul");
```

---

## 12. Trim() — removendo espaços acidentais

Sem `Trim()`, `"  sim  "` não é igual a `"sim"`. Com `Trim()`, os espaços são removidos antes da comparação.

```csharp
string? entrada = Console.ReadLine();

// Falha se o usuário digitar com espaços
bool semTrim = entrada == "sim";

// Funciona independente dos espaços
bool comTrim = entrada?.Trim() == "sim";
```

---

## 13. Trim() + ToLower() combinados

Na prática, sempre combine os dois. **Ordem: `Trim()` primeiro, depois `ToLower()`.**

```csharp
string pais = (Console.ReadLine() ?? "").Trim().ToLower();

switch (pais)
{
    case "brasil":   Console.WriteLine("Moeda: Real");  break;
    case "portugal": Console.WriteLine("Moeda: Euro");  break;
    default:         Console.WriteLine("País não encontrado."); break;
}
```

---

## 14. TrimStart() e TrimEnd()

`Trim()` remove dos dois lados. Use as variantes quando precisar remover só de um lado.

| Método | Remove espaços |
|---|---|
| `TrimStart()` | Apenas no início |
| `TrimEnd()` | Apenas no final |
| `Trim()` | Nos dois lados |

```csharp
string? entrada = Console.ReadLine();

Console.WriteLine(entrada?.TrimStart()); // remove só do início
Console.WriteLine(entrada?.TrimEnd());   // remove só do final
Console.WriteLine(entrada?.Trim());      // remove dos dois lados
```
