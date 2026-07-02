// ============================================================
//  3) SINCRONIZACAO (RACE CONDITIONS E lock)
// ============================================================
//
//  O PROBLEMA: CONDICAO DE CORRIDA (race condition)
//  ────────────────────────────────────────────────
//  Quando varias threads acessam e modificam o MESMO dado ao mesmo tempo,
//  o resultado depende da ordem imprevisivel de execucao. Uma operacao
//  aparentemente simples como `contador++` NAO e atomica: ela envolve tres
//  passos (ler, somar, escrever). Duas threads podem ler o mesmo valor,
//  somar 1 sobre ele e escrever de volta, perdendo um incremento.
//
//  A SOLUCAO: EXCLUSAO MUTUA
//  ─────────────────────────
//  Garantir que apenas uma thread por vez execute a secao critica (o trecho
//  que mexe no dado compartilhado). Ferramentas em C#:
//
//  - lock (objeto) { ... } : forma mais comum. Enquanto uma thread esta
//    dentro do bloco, as outras esperam. O objeto de trava deve ser um
//    campo private readonly dedicado (nunca `this`, `typeof` ou strings).
//  - Interlocked: operacoes atomicas de baixo custo para incremento,
//    troca e soma em inteiros (mais rapido que lock para esses casos).
//  - Monitor, Mutex, SemaphoreSlim: mecanismos mais avancados.
//
//  REGRA PRATICA: mantenha a secao critica a MENOR possivel e nunca chame
//  codigo demorado (I/O) segurando um lock.
// ============================================================

using System.Threading;

namespace ThreadsLesson;

class SynchronizationDemo
{
    // Objeto dedicado usado apenas como "cadeado". readonly impede que a
    // referencia seja trocada, o que quebraria a exclusao mutua.
    private static readonly object _trava = new object();

    private static int _contadorSemLock;
    private static int _contadorComLock;
    private static int _contadorInterlocked;

    private const int NumThreads = 10;
    private const int IncrementosPorThread = 100_000;

    public static void Executar()
    {
        Console.WriteLine("=== 3) Sincronizacao (race conditions e lock) ===\n");

        int esperado = NumThreads * IncrementosPorThread;

        // --- Sem sincronizacao: demonstra a race condition ---
        _contadorSemLock = 0;
        ExecutarEmParalelo(IncrementarSemLock);
        Console.WriteLine($"  Sem lock:      esperado={esperado}, obtido={_contadorSemLock}");
        Console.WriteLine("    (o valor obtido costuma ser MENOR devido a incrementos perdidos)");

        // --- Com lock: resultado sempre correto ---
        _contadorComLock = 0;
        ExecutarEmParalelo(IncrementarComLock);
        Console.WriteLine($"  Com lock:      esperado={esperado}, obtido={_contadorComLock}");

        // --- Com Interlocked: correto e mais eficiente para inteiros ---
        _contadorInterlocked = 0;
        ExecutarEmParalelo(IncrementarInterlocked);
        Console.WriteLine($"  Interlocked:   esperado={esperado}, obtido={_contadorInterlocked}");

        Console.WriteLine();
    }

    // Cria, inicia e aguarda (Join) um conjunto de threads que executam o
    // metodo informado. Centraliza o padrao usado nos tres cenarios.
    private static void ExecutarEmParalelo(ThreadStart trabalho)
    {
        Thread[] threads = new Thread[NumThreads];
        for (int i = 0; i < NumThreads; i++)
            threads[i] = new Thread(trabalho);

        foreach (Thread t in threads) t.Start();
        foreach (Thread t in threads) t.Join();
    }

    // `contador++` nao e atomico: ler/somar/escrever podem intercalar entre
    // threads, fazendo incrementos se perderem.
    private static void IncrementarSemLock()
    {
        for (int i = 0; i < IncrementosPorThread; i++)
            _contadorSemLock++;
    }

    // O bloco lock garante que apenas uma thread por vez execute o incremento.
    private static void IncrementarComLock()
    {
        for (int i = 0; i < IncrementosPorThread; i++)
        {
            lock (_trava)
            {
                _contadorComLock++;
            }
        }
    }

    // Interlocked.Increment faz o incremento de forma atomica em uma unica
    // instrucao, sem o custo de adquirir um lock.
    private static void IncrementarInterlocked()
    {
        for (int i = 0; i < IncrementosPorThread; i++)
            Interlocked.Increment(ref _contadorInterlocked);
    }
}
