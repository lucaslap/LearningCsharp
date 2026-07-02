// ============================================================
//  C# - THREADS (Programacao Concorrente)
// ============================================================
//
//  O QUE E UMA THREAD?
//  ───────────────────
//  Uma thread (linha de execucao) e a menor unidade de processamento que
//  o sistema operacional agenda. Todo programa comeca com uma unica thread
//  (a thread principal, que executa o Main). Criar threads adicionais
//  permite executar varias tarefas de forma CONCORRENTE.
//
//  Concorrencia vs. paralelismo:
//  - Concorrencia: varias tarefas progridem intercaladas (o SO alterna
//    entre elas rapidamente, dando a impressao de simultaneidade).
//  - Paralelismo: varias tarefas executam LITERALMENTE ao mesmo tempo,
//    em nucleos (cores) diferentes do processador.
//
//
//  POR QUE USAR THREADS?
//  ─────────────────────
//  - Manter a interface responsiva enquanto um trabalho longo roda ao fundo.
//  - Aproveitar processadores com varios nucleos.
//  - Executar operacoes de I/O (rede, disco) sem travar o resto do programa.
//
//
//  CUSTOS E RISCOS
//  ───────────────
//  - Criar threads tem custo de memoria e de troca de contexto.
//  - Acesso concorrente a dados compartilhados gera CONDICOES DE CORRIDA
//    (race conditions) e bugs dificeis de reproduzir. Ver Synchronization.cs.
//  - A ordem de execucao entre threads NAO e garantida.
//
//
//  A CLASSE Thread (System.Threading)
//  ──────────────────────────────────
//  - new Thread(metodo): cria uma thread que executara o metodo informado.
//  - Start():  coloca a thread para rodar.
//  - Join():   bloqueia a thread atual ate a outra terminar.
//  - Sleep(ms): pausa a thread atual pelo tempo informado.
//  - IsBackground: threads de background nao impedem o processo de encerrar.
//
//
//  SUBTOPICOS DESTA SECAO (padrao hibrido s09)
//  ───────────────────────────────────────────
//  1. Threads basicas (criar, Start, Join, Sleep) -> abaixo, nesta classe
//  2. Passagem de parametros para threads          -> ThreadParameters.cs
//  3. Sincronizacao (race conditions, lock)        -> Synchronization.cs
//  4. Thread Pool e Task (abordagem moderna)        -> TasksAndAsync.cs
// ============================================================

using System.Threading;

namespace ThreadsLesson;

class Program
{
    static void Main()
    {
        Console.WriteLine("============================================");
        Console.WriteLine("  THREADS EM C#");
        Console.WriteLine("============================================\n");

        Console.WriteLine("=== 1) Threads basicas ===\n");

        // A thread que executa este Main e a thread PRINCIPAL. Toda aplicacao
        // comeca com ela. Podemos inspecionar a thread atual.
        Thread principal = Thread.CurrentThread;
        principal.Name = "Principal";
        Console.WriteLine($"  Thread atual: {principal.Name} (Id={Environment.CurrentManagedThreadId})");

        // Criamos uma nova thread apontando para um metodo. A thread so comeca
        // a rodar quando chamamos Start(). Ate la, ela existe mas esta parada.
        Thread trabalhadora = new Thread(ContarAteTres);
        trabalhadora.Name = "Trabalhadora";
        trabalhadora.Start();

        // IMPORTANTE: o codigo abaixo roda na thread PRINCIPAL, concorrentemente
        // com a thread trabalhadora. A ordem das mensagens pode variar a cada
        // execucao, pois nao ha garantia de qual thread imprime primeiro.
        for (int i = 1; i <= 3; i++)
        {
            Console.WriteLine($"  [Principal] passo {i}");
            Thread.Sleep(100); // pausa a thread principal por 100 ms
        }

        // Join() bloqueia a thread principal ate a trabalhadora terminar.
        // Sem isso, o Main poderia chegar ao fim antes da thread concluir.
        trabalhadora.Join();
        Console.WriteLine("  Thread trabalhadora finalizada (apos Join).\n");

        // Threads de background nao seguram o encerramento do processo:
        // quando todas as threads de foreground terminam, o programa encerra
        // mesmo que ainda existam threads de background em execucao.
        Thread background = new Thread(() =>
        {
            Thread.Sleep(50);
            Console.WriteLine("  [Background] executei antes do processo encerrar.");
        });
        background.IsBackground = true;
        background.Start();
        background.Join(); // aguardamos aqui apenas para garantir a impressao
        Console.WriteLine();

        // ----------------------------------------------------------
        //  Subtopicos em arquivos separados:
        // ----------------------------------------------------------
        ThreadParametersDemo.Executar();
        SynchronizationDemo.Executar();
        TasksAndAsyncDemo.Executar();

        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }

    // Metodo executado pela thread trabalhadora. Cada iteracao imprime e
    // dorme 100 ms, permitindo que a thread principal intercale suas
    // proprias mensagens durante a espera.
    static void ContarAteTres()
    {
        for (int i = 1; i <= 3; i++)
        {
            Console.WriteLine($"  [Trabalhadora] contagem {i}");
            Thread.Sleep(100);
        }
    }
}
