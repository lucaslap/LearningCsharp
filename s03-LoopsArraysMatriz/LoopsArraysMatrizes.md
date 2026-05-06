# Loops, Arrays e Matrizes em C#

## 1. Estruturas de Repetição (Loops)

### for
Use quando o **número de repetições é conhecido** com antecedência.

```csharp
// Sintaxe: for (inicialização; condição; incremento)
for (int i = 0; i < 5; i++)
    Console.WriteLine(i); // 0, 1, 2, 3, 4

// Decrescente
for (int i = 10; i >= 1; i--)
    Console.Write($"{i} ");

// Passo diferente de 1
for (int i = 0; i <= 20; i += 2)
    Console.Write($"{i} "); // 0 2 4 6 ... 20
```

### while
Use quando a condição é verificada **antes** de cada execução. Pode não executar nenhuma vez.

```csharp
int n = 1;
while (n < 100)
    n *= 2; // 1 → 2 → 4 → 8 → ... → 128
```

### do-while
Garante **ao menos uma execução** — condição verificada depois do bloco.

```csharp
int opcao;
do
{
    Console.Write("Escolha (1-3): ");
    opcao = int.Parse(Console.ReadLine()!);
} while (opcao < 1 || opcao > 3);
```

### foreach
Use para **percorrer coleções** quando o índice não importa.

```csharp
string[] frutas = { "Maçã", "Banana", "Laranja" };
foreach (string fruta in frutas)
    Console.WriteLine(fruta);

// Também funciona em strings (coleção de chars)
foreach (char c in "C#")
    Console.Write(c);
```

### break e continue

| Palavra-chave | Efeito |
|---|---|
| `break` | Sai do loop imediatamente |
| `continue` | Pula para a próxima iteração |

```csharp
for (int i = 0; i < 10; i++)
{
    if (i == 7) break;      // para em 7
    if (i % 2 == 0) continue; // pula pares
    Console.Write($"{i} "); // imprime: 1 3 5
}
```

### Loops aninhados
O loop interno executa **completo** a cada iteração do externo.

```csharp
for (int i = 1; i <= 3; i++)
    for (int j = 1; j <= 3; j++)
        Console.WriteLine($"{i} × {j} = {i * j}");
// Total de execuções: 3 × 3 = 9
```

---

## 2. Arrays (Vetores Unidimensionais)

### Declaração e inicialização

```csharp
// Tamanho fixo — elementos iniciam com valor padrão (0, null, false)
int[] numeros = new int[5];

// Com valores — tamanho inferido pelo compilador
int[] primos = { 2, 3, 5, 7, 11 };

// new + valores (equivalente)
double[] precos = new double[] { 9.99, 14.50, 3.75 };
```

### Acesso e modificação

```csharp
string[] dias = { "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb", "Dom" };

dias[0]   // "Seg" — primeiro elemento (índice 0)
dias[^1]  // "Dom" — último (C# 8+: ^ conta do fim)
dias[^2]  // "Sáb" — penúltimo

dias[1] = "Terça"; // modifica o elemento no índice 1
```

### Range (fatiamento) — C# 8+

```csharp
int[] seq = { 10, 20, 30, 40, 50, 60, 70 };

seq[2..5]  // { 30, 40, 50 }  — índices 2, 3, 4 (5 não incluído)
seq[^3..]  // { 50, 60, 70 }  — últimos 3
seq[..3]   // { 10, 20, 30 }  — primeiros 3 (0, 1, 2)
```

### Métodos da classe Array

```csharp
int[] v = { 5, 2, 8, 1, 9 };

Array.Sort(v);               // { 1, 2, 5, 8, 9 }       — ordena crescente
Array.Reverse(v);            // { 9, 8, 5, 2, 1 }       — inverte
Array.IndexOf(v, 5);         // 2                        — índice do elemento (-1 se não existir)
Array.Exists(v, n => n > 7); // true                     — existe algum > 7?
Array.Find(v, n => n > 7);   // 9                        — primeiro > 7
Array.FindAll(v, n => n > 4);// { 9, 8, 5 }             — todos > 4
Array.Fill(v, 0);            // { 0, 0, 0, 0, 0 }       — preenche tudo com 0
Array.Copy(origem, dest, n); // copia n elementos        — para outro array

int[] copia = (int[])v.Clone(); // cópia independente (sem Clone, seria mesma referência)
```

> **Atenção:** `Sort` e `Reverse` **modificam o array original**. Use `.Clone()` para preservar o original.

### Strings como arrays

```csharp
string csv = "maçã,banana,laranja";
string[] frutas = csv.Split(',');        // divide string em array
string unida = string.Join(", ", frutas); // junta array em string
bool temBanana = frutas.Contains("banana"); // LINQ — verifica existência
```

---

## 3. Matrizes (Arrays Multidimensionais)

### Array Retangular `int[,]`
Todas as linhas têm o **mesmo** número de colunas. Memória contígua.

```csharp
// Declaração e inicialização
int[,] matriz = {
    { 1, 2, 3 },  // linha 0
    { 4, 5, 6 },  // linha 1
    { 7, 8, 9 }   // linha 2
};

// Dimensões
matriz.GetLength(0);  // 3 — número de linhas
matriz.GetLength(1);  // 3 — número de colunas
matriz.Length;        // 9 — total de elementos
matriz.Rank;          // 2 — número de dimensões

// Acesso: [linha, coluna]
matriz[0, 0]  // 1 — canto superior esquerdo
matriz[1, 1]  // 5 — centro
matriz[2, 2]  // 9 — canto inferior direito
```

### Percorrer com loops aninhados

```csharp
for (int i = 0; i < matriz.GetLength(0); i++)      // linhas
{
    for (int j = 0; j < matriz.GetLength(1); j++)   // colunas
    {
        Console.Write($"{matriz[i, j],4}");
    }
    Console.WriteLine();
}
```

### Operações comuns

```csharp
// Transposta: trocar linhas por colunas
int[,] transposta = new int[colunas, linhas];
for (int i = 0; i < linhas; i++)
    for (int j = 0; j < colunas; j++)
        transposta[j, i] = original[i, j];

// Matriz identidade
int[,] id = new int[n, n];
for (int i = 0; i < n; i++)
    id[i, i] = 1; // 1 na diagonal principal, 0 no restante
```

### Array Tridimensional `int[,,]`

```csharp
// Pense como: andares × linhas × colunas
int[,,] cubo = new int[2, 3, 3];  // 2 andares, 3 linhas, 3 colunas
cubo[0, 1, 2]  // andar 0, linha 1, coluna 2

// Percorrer com 3 loops aninhados
for (int z = 0; z < cubo.GetLength(0); z++)    // andares
    for (int i = 0; i < cubo.GetLength(1); i++) // linhas
        for (int j = 0; j < cubo.GetLength(2); j++) // colunas
            Console.Write(cubo[z, i, j]);
```

### Jagged Array `int[][]`
Cada linha pode ter tamanho **diferente**. É um array de arrays.

```csharp
// Declaração
int[][] jagged = new int[3][];
jagged[0] = new int[] { 1 };
jagged[1] = new int[] { 2, 3 };
jagged[2] = new int[] { 4, 5, 6 };

// Inicialização direta
string[][] turmas = {
    new string[] { "Ana" },
    new string[] { "Bruno", "Carla" },
    new string[] { "Diego", "Eva", "Fábio" }
};

// Acesso: [linha][coluna]  — note os [] separados
turmas[1][0]   // "Bruno"
jagged[2][2]   // 6

// Percorrer
for (int i = 0; i < jagged.Length; i++)
    for (int j = 0; j < jagged[i].Length; j++) // .Length por linha!
        Console.Write(jagged[i][j]);
```

### Comparativo: `int[,]` vs `int[][]`

| | `int[,]` (retangular) | `int[][]` (jagged) |
|---|---|---|
| Linhas | Mesmo tamanho obrigatório | Tamanhos diferentes |
| Memória | Contígua (mais eficiente) | Arrays separados |
| Acesso | `m[i, j]` | `m[i][j]` |
| Quando usar | Matrizes matemáticas, tabelas | Dados irregulares, triângulos |

---

## Armadilhas Comuns

```csharp
// ❌ IndexOutOfRangeException — índice fora do range
int[] v = new int[5];
v[5] = 10; // erro! índices válidos: 0 a 4 (Length-1)

// ❌ Cópia por referência (ambas as variáveis apontam pro mesmo array)
int[] a = { 1, 2, 3 };
int[] b = a;         // b É o mesmo array — não é uma cópia!
b[0] = 99;           // muda a[0] também!

// ✅ Cópia independente
int[] c = (int[])a.Clone();  // c é uma cópia independente

// ❌ Modificar coleção durante foreach
foreach (var item in lista)
    lista.Remove(item); // InvalidOperationException!
// ✅ Use for reverso ou crie uma cópia para iterar
```
