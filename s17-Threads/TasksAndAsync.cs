// ============================================================
//  4) THREAD POOL E TASK (A ABORDAGEM MODERNA)
// ============================================================
//
//  Criar um `new Thread` para cada trabalho e caro: cada thread consome
//  memoria e ha custo de criacao/destruicao. Para tarefas curtas e
//  numerosas, o .NET oferece o THREAD POOL: um conjunto de threads
//  reutilizaveis gerenciado pelo runtime.
//
//  A classe Task (System.Threading.Tasks) e a forma recomendada hoje:
//  - Usa o thread pool por padrao.
//  - Representa uma operacao assincrona que pode retornar um resultado
//    (Task<T>) ou nao (Task).
//  - Integra-se com async/await, tornando o codigo concorrente legivel
//    como se fosse sequencial.
//
//  Task vs. Thread (resumo):
//  - Thread: controle de baixo nivel, uma linha de execucao dedicada.
//  - Task:   abstracao de alto nivel sobre uma "operacao"; o runtime
//            decide como executa-la (normalmente no thread pool).
//
//  async/await:
//  - `async` marca um metodo que pode usar `await`.
//  - `await` suspende o metodo ate a Task concluir, SEM bloquear a thread;
//    ela fica livre para outro trabalho enquanto a operacao termina.
// ============================================================

using System.Threading;
using System.Threading.Tasks;

namespace ThreadsLesson;

class TasksAndAsyncDemo
{
    public static void Executar()
    {
        Console.WriteLine("=== 4) Thread Pool e Task ===\n");

        // Task.Run agenda o trabalho no thread pool e devolve uma Task<T>.
        // Aqui iniciamos duas tarefas que rodam concorrentemente.
        Task<int> tarefaA = Task.Run(() => SomarIntervalo(1, 1000));
        Task<int> tarefaB = Task.Run(() => SomarIntervalo(1001, 2000));

        // Enquanto as tarefas rodam ao fundo, a thread atual segue livre.
        // Task.WhenAll cria uma Task que conclui quando ambas terminarem.
        // Como Executar() nao e async, usamos .Result apenas para fins
        // didaticos (em codigo real, prefira await, ver abaixo).
        int[] resultados = Task.WhenAll(tarefaA, tarefaB).Result;
        Console.WriteLine($"  Soma parcial A: {resultados[0]}");
        Console.WriteLine($"  Soma parcial B: {resultados[1]}");
        Console.WriteLine($"  Soma total (1..2000): {resultados[0] + resultados[1]}");

        // Chamamos um metodo async e aguardamos sua conclusao. GetAwaiter()
        // .GetResult() bloqueia sem envolver a mensagem de AggregateException.
        Console.WriteLine("\n  Executando fluxo async/await:");
        ProcessarComAwaitAsync().GetAwaiter().GetResult();

        Console.WriteLine();
    }

    // Trabalho puramente computacional (CPU-bound) usado dentro de Task.Run.
    private static int SomarIntervalo(int inicio, int fim)
    {
        int soma = 0;
        for (int i = inicio; i <= fim; i++)
            soma += i;
        return soma;
    }

    // Metodo assincrono. `await` suspende a execucao ate a Task interna
    // concluir, mas nao bloqueia a thread durante a espera.
    private static async Task ProcessarComAwaitAsync()
    {
        Console.WriteLine("    Antes do await (trabalho iniciado).");

        // Task.Delay simula uma operacao demorada (ex.: I/O) de forma
        // assincrona, sem prender uma thread parada como faria Thread.Sleep.
        await Task.Delay(100);

        int valor = await Task.Run(() => SomarIntervalo(1, 100));
        Console.WriteLine($"    Depois do await: soma 1..100 = {valor}");
    }
}
