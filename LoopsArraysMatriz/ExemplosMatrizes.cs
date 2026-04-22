// ============================================================
//  MATRIZES — Arrays Multidimensionais em C#
// ============================================================
//
//  C# oferece dois tipos de arrays multidimensionais:
//
//  1. Array Retangular (int[,])
//     • Todas as linhas têm o mesmo número de colunas
//     • Memória contígua — mais eficiente
//     • Acesso: matriz[linha, coluna]
//
//  2. Jagged Array / Array Denteado (int[][])
//     • Cada linha pode ter um número DIFERENTE de colunas
//     • Array de arrays — mais flexível
//     • Acesso: matriz[linha][coluna]
//
// ============================================================

static class ExemplosMatrizes
{
    public static void Executar()
    {
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║       MATRIZES — MULTIDIMENSIONAL    ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");

        DeclararMatriz2D();
        PercorrerMatriz();
        OperacoesMatriz();
        MatrizTridimensional();
        JaggedArrays();
        ExemploPratico();
    }

    // ──────────────────────────────────────────────────────────
    //  1. DECLARAR E INICIALIZAR MATRIZES 2D
    // ──────────────────────────────────────────────────────────
    static void DeclararMatriz2D()
    {
        Console.WriteLine("--- 1. Declaração de Matrizes 2D ---");

        // Forma 1: tamanho definido — todos elementos começam em 0
        // Cria uma matriz de 3 linhas × 4 colunas
        int[,] vazia = new int[3, 4];
        Console.WriteLine($"  new int[3,4]: {vazia.GetLength(0)} linhas, {vazia.GetLength(1)} colunas");
        Console.WriteLine($"  Total de elementos: {vazia.Length}");

        // Forma 2: inicialização com valores
        // Cada {} interna representa uma linha
        int[,] matriz = {
            { 1, 2, 3 },    // linha 0
            { 4, 5, 6 },    // linha 1
            { 7, 8, 9 }     // linha 2
        };

        Console.WriteLine("\n  Matriz 3×3:");
        ImprimirMatriz(matriz);

        // GetLength(dimensão) — tamanho em cada dimensão
        // Rank — número de dimensões
        Console.WriteLine($"\n  GetLength(0) = {matriz.GetLength(0)}  ← número de linhas");
        Console.WriteLine($"  GetLength(1) = {matriz.GetLength(1)}  ← número de colunas");
        Console.WriteLine($"  Length       = {matriz.Length}  ← total de elementos");
        Console.WriteLine($"  Rank         = {matriz.Rank}   ← número de dimensões");

        // Acesso por [linha, coluna]
        Console.WriteLine($"\n  matriz[0, 0] = {matriz[0, 0]}  (linha 0, coluna 0 — canto superior esquerdo)");
        Console.WriteLine($"  matriz[1, 1] = {matriz[1, 1]}  (linha 1, coluna 1 — centro)");
        Console.WriteLine($"  matriz[2, 2] = {matriz[2, 2]}  (linha 2, coluna 2 — canto inferior direito)");

        Console.WriteLine();
    }

    // ──────────────────────────────────────────────────────────
    //  2. PERCORRER MATRIZ COM LOOPS ANINHADOS
    // ──────────────────────────────────────────────────────────
    static void PercorrerMatriz()
    {
        Console.WriteLine("--- 2. Percorrer Matriz ---");

        int[,] notas = {
            { 8, 7, 9 },    // aluno 0: notas das 3 provas
            { 6, 5, 7 },    // aluno 1
            { 9, 10, 8 },   // aluno 2
            { 4, 6, 5 }     // aluno 3
        };

        int linhas = notas.GetLength(0);   // 4 alunos
        int colunas = notas.GetLength(1);  // 3 provas

        Console.WriteLine($"  Notas ({linhas} alunos × {colunas} provas):\n");
        Console.WriteLine("  Aluno    P1   P2   P3   Média   Resultado");
        Console.WriteLine("  ──────────────────────────────────────────");

        for (int aluno = 0; aluno < linhas; aluno++)
        {
            double soma = 0;
            Console.Write($"  Aluno {aluno}  ");

            for (int prova = 0; prova < colunas; prova++)
            {
                Console.Write($"{notas[aluno, prova],4} ");
                soma += notas[aluno, prova];
            }

            double media = soma / colunas;
            string resultado = media >= 6.0 ? "Aprovado" : "Reprovado";
            Console.WriteLine($"  {media,5:F1}   {resultado}");
        }

        // Foreach em array 2D — percorre todos os elementos, linha por linha
        Console.Write("\n  foreach (todos os valores): ");
        foreach (int nota in notas)
        {
            Console.Write($"{nota} ");
        }
        Console.WriteLine("  ← linha 0, depois linha 1, etc.");

        Console.WriteLine();
    }

    // ──────────────────────────────────────────────────────────
    //  3. OPERAÇÕES COMUNS COM MATRIZES
    // ──────────────────────────────────────────────────────────
    static void OperacoesMatriz()
    {
        Console.WriteLine("--- 3. Operações com Matrizes ---");

        // Transposta: trocar linhas por colunas
        // Matriz original A[m,n] → Transposta T[n,m]
        int[,] original = {
            { 1, 2, 3 },
            { 4, 5, 6 }
        };

        int linhas = original.GetLength(0);
        int colunas = original.GetLength(1);
        int[,] transposta = new int[colunas, linhas]; // dimensões invertidas

        for (int i = 0; i < linhas; i++)
            for (int j = 0; j < colunas; j++)
                transposta[j, i] = original[i, j]; // linha vira coluna

        Console.WriteLine("  Original (2×3):     Transposta (3×2):");
        ImprimirDuasMatrizesSideBySide(original, transposta);

        // Matriz identidade — 1s na diagonal principal, 0s em todo o resto
        int tamanho = 4;
        int[,] identidade = new int[tamanho, tamanho];
        for (int i = 0; i < tamanho; i++)
            identidade[i, i] = 1; // só preenche a diagonal (i == j)

        Console.WriteLine($"\n  Matriz Identidade {tamanho}×{tamanho}:");
        ImprimirMatriz(identidade);

        // Soma de duas matrizes (elemento a elemento)
        int[,] a = { { 1, 2 }, { 3, 4 } };
        int[,] b = { { 5, 6 }, { 7, 8 } };
        int[,] soma = new int[2, 2];

        for (int i = 0; i < 2; i++)
            for (int j = 0; j < 2; j++)
                soma[i, j] = a[i, j] + b[i, j];

        Console.WriteLine("\n  Soma de matrizes A + B:");
        ImprimirMatriz(soma);

        Console.WriteLine();
    }

    // ──────────────────────────────────────────────────────────
    //  4. ARRAY 3D — três dimensões
    // ──────────────────────────────────────────────────────────
    static void MatrizTridimensional()
    {
        Console.WriteLine("--- 4. Array Tridimensional (3D) ---");

        // Pense como: andar × linha × coluna  (prédio com andares)
        // Dimensão 0 = andares, Dimensão 1 = linhas, Dimensão 2 = colunas

        int[,,] predio = new int[2, 3, 3]; // 2 andares, 3 linhas, 3 colunas

        // Preencher com valores para identificar posição
        for (int andar = 0; andar < 2; andar++)
            for (int linha = 0; linha < 3; linha++)
                for (int col = 0; col < 3; col++)
                    predio[andar, linha, col] = (andar + 1) * 100 + (linha + 1) * 10 + (col + 1);

        Console.WriteLine($"  Prédio 3D: {predio.GetLength(0)} andares × " +
                          $"{predio.GetLength(1)} linhas × {predio.GetLength(2)} colunas\n");

        for (int andar = 0; andar < predio.GetLength(0); andar++)
        {
            Console.WriteLine($"  Andar {andar + 1}:");
            for (int linha = 0; linha < predio.GetLength(1); linha++)
            {
                Console.Write("    ");
                for (int col = 0; col < predio.GetLength(2); col++)
                {
                    Console.Write($"{predio[andar, linha, col]} ");
                }
                Console.WriteLine();
            }
        }

        Console.WriteLine($"\n  Total de elementos: {predio.Length}  ({predio.Rank} dimensões)");
        Console.WriteLine();
    }

    // ──────────────────────────────────────────────────────────
    //  5. JAGGED ARRAYS — linhas com tamanhos diferentes
    // ──────────────────────────────────────────────────────────
    static void JaggedArrays()
    {
        Console.WriteLine("--- 5. Jagged Arrays (Array de Arrays) ---");

        // Declaração: int[][] em vez de int[,]
        // Cada elemento do array externo é um array independente

        // Forma 1: declarar o array externo e depois cada linha
        int[][] triangulo = new int[5][];
        for (int i = 0; i < triangulo.Length; i++)
        {
            triangulo[i] = new int[i + 1]; // linha i tem i+1 elementos
            for (int j = 0; j <= i; j++)
                triangulo[i][j] = j + 1;
        }

        Console.WriteLine("  Triângulo (jagged — cada linha tem tamanho diferente):");
        for (int i = 0; i < triangulo.Length; i++)
        {
            Console.Write("    ");
            for (int j = 0; j < triangulo[i].Length; j++) // .Length por linha
            {
                Console.Write($"{triangulo[i][j]} ");
            }
            Console.WriteLine();
        }

        // Forma 2: inicialização direta
        string[][] turmas = {
            new string[] { "Ana", "Bruno" },
            new string[] { "Carla", "Diego", "Eduardo" },
            new string[] { "Fábio" }
        };

        Console.WriteLine("\n  Turmas (jagged com strings):");
        for (int t = 0; t < turmas.Length; t++)
        {
            Console.Write($"    Turma {t + 1} ({turmas[t].Length} aluno(s)): ");
            Console.WriteLine(string.Join(", ", turmas[t]));
        }

        // Diferença importante entre int[,] e int[][]
        Console.WriteLine("\n  int[,]  → retangular: todas as linhas TÊM de ter o mesmo tamanho");
        Console.WriteLine("  int[][] → jagged:     cada linha PODE ter tamanho diferente");

        Console.WriteLine();
    }

    // ──────────────────────────────────────────────────────────
    //  6. EXEMPLO PRÁTICO — Planilha de vendas
    // ──────────────────────────────────────────────────────────
    static void ExemploPratico()
    {
        Console.WriteLine("--- 6. Exemplo Prático — Planilha de Vendas ---");

        // Vendas por produto (linha) e mês (coluna)
        // Linhas: 0=Notebook, 1=Mouse, 2=Teclado, 3=Monitor
        // Colunas: 0=Jan, 1=Fev, 2=Mar

        string[] produtos = { "Notebook", "Mouse   ", "Teclado ", "Monitor " };
        string[] meses = { "Jan", "Fev", "Mar" };

        int[,] vendas = {
            { 12, 8,  15 },   // Notebook
            { 45, 52, 38 },   // Mouse
            { 30, 27, 33 },   // Teclado
            { 7,  11, 9  }    // Monitor
        };

        // Cabeçalho
        Console.WriteLine("\n  Produto       Jan   Fev   Mar   TOTAL");
        Console.WriteLine("  ─────────────────────────────────────");

        int totalGeral = 0;

        for (int p = 0; p < vendas.GetLength(0); p++)
        {
            int totalProduto = 0;
            Console.Write($"  {produtos[p]}  ");

            for (int m = 0; m < vendas.GetLength(1); m++)
            {
                Console.Write($"{vendas[p, m],5} ");
                totalProduto += vendas[p, m];
            }

            Console.WriteLine($"  {totalProduto,5}");
            totalGeral += totalProduto;
        }

        // Total por mês (percorre colunas)
        Console.WriteLine("  ─────────────────────────────────────");
        Console.Write("  TOTAL         ");
        for (int m = 0; m < vendas.GetLength(1); m++)
        {
            int totalMes = 0;
            for (int p = 0; p < vendas.GetLength(0); p++)
                totalMes += vendas[p, m];
            Console.Write($"{totalMes,5} ");
        }
        Console.WriteLine($"  {totalGeral,5}");

        // Produto mais vendido (usando busca em array 2D)
        int maxVendas = 0;
        int melhorProduto = 0;
        for (int p = 0; p < vendas.GetLength(0); p++)
        {
            int totalP = 0;
            for (int m = 0; m < vendas.GetLength(1); m++)
                totalP += vendas[p, m];
            if (totalP > maxVendas)
            {
                maxVendas = totalP;
                melhorProduto = p;
            }
        }

        Console.WriteLine($"\n  Produto mais vendido: {produtos[melhorProduto].Trim()} ({maxVendas} unidades)");

        Console.WriteLine();
    }

    // ──────────────────────────────────────────────────────────
    //  Auxiliares de impressão
    // ──────────────────────────────────────────────────────────
    static void ImprimirMatriz(int[,] m)
    {
        for (int i = 0; i < m.GetLength(0); i++)
        {
            Console.Write("    [ ");
            for (int j = 0; j < m.GetLength(1); j++)
            {
                Console.Write($"{m[i, j],3} ");
            }
            Console.WriteLine("]");
        }
    }

    static void ImprimirDuasMatrizesSideBySide(int[,] a, int[,] b)
    {
        int linhasA = a.GetLength(0), colsA = a.GetLength(1);
        int linhasB = b.GetLength(0), colsB = b.GetLength(1);
        int maxLinhas = Math.Max(linhasA, linhasB);

        for (int i = 0; i < maxLinhas; i++)
        {
            Console.Write("    ");
            if (i < linhasA)
            {
                Console.Write("[ ");
                for (int j = 0; j < colsA; j++) Console.Write($"{a[i, j],2} ");
                Console.Write("]");
            }
            else
            {
                Console.Write(new string(' ', colsA * 3 + 3));
            }

            Console.Write("    ");

            if (i < linhasB)
            {
                Console.Write("[ ");
                for (int j = 0; j < colsB; j++) Console.Write($"{b[i, j],2} ");
                Console.Write("]");
            }
            Console.WriteLine();
        }
    }
}
