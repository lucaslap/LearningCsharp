// ============================================================
//  ARRAYS — Vetores Unidimensionais em C#
// ============================================================
//
//  Um array é uma coleção de elementos do mesmo tipo,
//  armazenados em posições consecutivas de memória.
//
//  Características importantes:
//    • Tamanho FIXO — definido na criação, não pode mudar
//    • Índice começa em 0 (primeiro) e vai até Length-1 (último)
//    • Acesso por índice é O(1) — instantâneo independente do tamanho
//    • Tipo seguro — só aceita elementos do tipo declarado
//
// ============================================================

static class ExemplosArrays
{
    public static void Executar()
    {
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║         ARRAYS — VETORES             ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");

        DeclararEInicializar();
        AcessarEModificar();
        PercorrerArrays();
        MetodosUteis();
        ArraysMultidimensionaisBasico();
    }

    // ──────────────────────────────────────────────────────────
    //  1. DECLARAÇÃO E INICIALIZAÇÃO
    // ──────────────────────────────────────────────────────────
    static void DeclararEInicializar()
    {
        Console.WriteLine("--- 1. Declaração e Inicialização ---");

        // Forma 1: declarar o tamanho — todos os elementos começam com o valor padrão
        //   int → 0,  double → 0.0,  bool → false,  string → null
        int[] numeros = new int[5];
        Console.WriteLine($"  new int[5]: [{string.Join(", ", numeros)}]  ← todos zero por padrão");

        // Forma 2: declarar com valores iniciais (tamanho inferido pelo compilador)
        int[] primos = { 2, 3, 5, 7, 11, 13 };
        Console.WriteLine($"  primos:     [{string.Join(", ", primos)}]");

        // Forma 3: new + valores explícitos (equivalente à forma 2)
        double[] precos = new double[] { 9.99, 14.50, 3.75, 29.90 };
        Console.WriteLine($"  precos:     [{string.Join(", ", precos)}]");

        // Forma 4: var (tipo inferido pelo compilador)
        var nomes = new string[] { "Ana", "Bruno", "Carla" };
        Console.WriteLine($"  nomes:      [{string.Join(", ", nomes)}]");

        // Propriedade Length — número de elementos
        Console.WriteLine($"\n  primos.Length = {primos.Length}  (índices válidos: 0 a {primos.Length - 1})");

        Console.WriteLine();
    }

    // ──────────────────────────────────────────────────────────
    //  2. ACESSAR E MODIFICAR ELEMENTOS
    // ──────────────────────────────────────────────────────────
    static void AcessarEModificar()
    {
        Console.WriteLine("--- 2. Acessar e Modificar ---");

        string[] dias = { "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado", "Domingo" };

        // Acesso por índice positivo (começa em 0)
        Console.WriteLine($"  dias[0] (primeiro):       {dias[0]}");
        Console.WriteLine($"  dias[4] (quinto):         {dias[4]}");
        Console.WriteLine($"  dias[^1] (último):        {dias[^1]}");  // índice do fim com ^
        Console.WriteLine($"  dias[^2] (penúltimo):     {dias[^2]}");  // C# 8+ — ^ conta do fim

        // Modificar elemento existente
        string[] frutas = { "Maçã", "Banana", "Cereja" };
        Console.WriteLine($"\n  Antes: [{string.Join(", ", frutas)}]");
        frutas[1] = "Morango"; // substitui "Banana" por "Morango"
        Console.WriteLine($"  Após frutas[1] = \"Morango\": [{string.Join(", ", frutas)}]");

        // Range — fatia do array (C# 8+)
        int[] sequencia = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
        int[] fatia = sequencia[2..5];     // elementos nos índices 2, 3, 4 (5 não incluído)
        int[] doFim = sequencia[^3..];     // últimos 3 elementos
        int[] doInicio = sequencia[..4];   // primeiros 4 elementos (0, 1, 2, 3)

        Console.WriteLine($"\n  sequencia:              [{string.Join(", ", sequencia)}]");
        Console.WriteLine($"  sequencia[2..5]:        [{string.Join(", ", fatia)}]   ← índices 2,3,4");
        Console.WriteLine($"  sequencia[^3..]:        [{string.Join(", ", doFim)}]        ← últimos 3");
        Console.WriteLine($"  sequencia[..4]:         [{string.Join(", ", doInicio)}]  ← primeiros 4");

        // IndexOutOfRangeException — erro ao acessar índice inválido
        // dias[7] ← ERRO! índice máximo é 6 (Length-1)
        Console.WriteLine("\n  ⚠ dias[7] causaria IndexOutOfRangeException (índice máximo = 6)");

        Console.WriteLine();
    }

    // ──────────────────────────────────────────────────────────
    //  3. PERCORRER ARRAYS
    // ──────────────────────────────────────────────────────────
    static void PercorrerArrays()
    {
        Console.WriteLine("--- 3. Percorrer Arrays ---");

        double[] notas = { 8.5, 6.0, 9.2, 7.8, 5.5, 10.0, 4.3 };

        // Com for — use quando precisar do ÍNDICE
        Console.WriteLine("  Com for (mostrando índice + valor):");
        for (int i = 0; i < notas.Length; i++)
        {
            string status = notas[i] >= 6.0 ? "✓" : "✗";
            Console.WriteLine($"    notas[{i}] = {notas[i]:F1}  {status}");
        }

        // Com foreach — mais limpo quando o índice não importa
        double soma = 0;
        int aprovados = 0;
        foreach (double nota in notas)
        {
            soma += nota;
            if (nota >= 6.0) aprovados++;
        }

        Console.WriteLine($"\n  Com foreach (resumo):");
        Console.WriteLine($"    Total de alunos: {notas.Length}");
        Console.WriteLine($"    Aprovados:       {aprovados}");
        Console.WriteLine($"    Reprovados:      {notas.Length - aprovados}");
        Console.WriteLine($"    Média da turma:  {soma / notas.Length:F2}");

        // Percorrer de trás para frente com for
        Console.Write("\n  Ordem reversa com for: ");
        for (int i = notas.Length - 1; i >= 0; i--)
        {
            Console.Write($"{notas[i]:F1} ");
        }
        Console.WriteLine();

        Console.WriteLine();
    }

    // ──────────────────────────────────────────────────────────
    //  4. MÉTODOS ÚTEIS DA CLASSE Array
    // ──────────────────────────────────────────────────────────
    static void MetodosUteis()
    {
        Console.WriteLine("--- 4. Métodos Úteis (classe Array) ---");

        int[] numeros = { 5, 2, 8, 1, 9, 3, 7, 4, 6 };
        Console.WriteLine($"  Original:    [{string.Join(", ", numeros)}]");

        // Array.Sort — ordena em ordem crescente (modifica o array original!)
        int[] copia = (int[])numeros.Clone(); // Clone cria uma cópia independente
        Array.Sort(copia);
        Console.WriteLine($"  Sort:        [{string.Join(", ", copia)}]");

        // Array.Reverse — inverte a ordem (também modifica o original)
        Array.Reverse(copia);
        Console.WriteLine($"  Reverse:     [{string.Join(", ", copia)}]");

        // Array.Sort + Array.Reverse = ordem decrescente
        int[] decrescente = (int[])numeros.Clone();
        Array.Sort(decrescente);
        Array.Reverse(decrescente);
        Console.WriteLine($"  Decrescente: [{string.Join(", ", decrescente)}]");

        // Array.IndexOf — retorna o índice da primeira ocorrência (-1 se não encontrar)
        int indice7 = Array.IndexOf(numeros, 7);
        int indice99 = Array.IndexOf(numeros, 99);
        Console.WriteLine($"\n  IndexOf(7):   índice {indice7}  → numeros[{indice7}] = {numeros[indice7]}");
        Console.WriteLine($"  IndexOf(99):  índice {indice99}  → não encontrado (retorna -1)");

        // Array.Exists — verifica se algum elemento satisfaz uma condição (usa lambda)
        bool temPar = Array.Exists(numeros, n => n % 2 == 0);
        bool temNegativo = Array.Exists(numeros, n => n < 0);
        Console.WriteLine($"\n  Exists (par):      {temPar}");
        Console.WriteLine($"  Exists (negativo): {temNegativo}");

        // Array.Find — retorna o PRIMEIRO elemento que satisfaz a condição (0 se não achar)
        int primeiroPar = Array.Find(numeros, n => n % 2 == 0);
        Console.WriteLine($"  Find (primeiro par): {primeiroPar}");

        // Array.FindAll — retorna TODOS os elementos que satisfazem a condição
        int[] maioresQue5 = Array.FindAll(numeros, n => n > 5);
        Console.WriteLine($"  FindAll (> 5): [{string.Join(", ", maioresQue5)}]");

        // Array.Fill — preenche todos os elementos com um valor
        int[] zeros = new int[5];
        Array.Fill(zeros, 42);
        Console.WriteLine($"\n  Fill(42):    [{string.Join(", ", zeros)}]");

        // Array.Copy — copia elementos de um array para outro
        int[] destino = new int[5];
        Array.Copy(numeros, destino, 5); // copia os 5 primeiros elementos
        Console.WriteLine($"  Copy (5):    [{string.Join(", ", destino)}]  ← primeiros 5 de numeros");

        // Array.Clear — zera elementos (int → 0, string → null, bool → false)
        Array.Clear(destino, 1, 3); // limpa 3 elementos a partir do índice 1
        Console.WriteLine($"  Clear [1,3]: [{string.Join(", ", destino)}]  ← índices 1,2,3 zerados");

        Console.WriteLine();
    }

    // ──────────────────────────────────────────────────────────
    //  5. ARRAYS DE STRINGS — métodos extras
    // ──────────────────────────────────────────────────────────
    static void ArraysMultidimensionaisBasico()
    {
        Console.WriteLine("--- 5. Arrays de Strings — Exemplos Práticos ---");

        string[] linguagens = { "C#", "Python", "Java", "JavaScript", "Go", "Rust" };

        // Contains — verificar se um valor existe (LINQ — using System.Linq implícito)
        bool temPython = linguagens.Contains("Python");
        bool temRuby = linguagens.Contains("Ruby");
        Console.WriteLine($"  Contains(\"Python\"): {temPython}");
        Console.WriteLine($"  Contains(\"Ruby\"):   {temRuby}");

        // Ordenar alfabeticamente
        string[] copia = (string[])linguagens.Clone();
        Array.Sort(copia);
        Console.WriteLine($"\n  Ordem alfabética: [{string.Join(", ", copia)}]");

        // string.Join — juntar elementos com separador
        string lista1 = string.Join(", ", linguagens);
        string lista2 = string.Join(" | ", linguagens);
        Console.WriteLine($"\n  Join(\", \"):  {lista1}");
        Console.WriteLine($"  Join(\" | \"): {lista2}");

        // string.Split — separar uma string em array (operação inversa)
        string csv = "maçã,banana,laranja,uva,melão";
        string[] frutas = csv.Split(',');
        Console.WriteLine($"\n  Split(',') em \"{csv}\":");
        Console.WriteLine($"    → [{string.Join(", ", frutas)}]  (Length = {frutas.Length})");

        // Uso prático: contar linguagens com mais de 4 caracteres
        int longas = 0;
        foreach (string lang in linguagens)
        {
            if (lang.Length > 4) longas++;
        }
        Console.WriteLine($"\n  Linguagens com mais de 4 letras: {longas} de {linguagens.Length}");

        Console.WriteLine();
    }
}
