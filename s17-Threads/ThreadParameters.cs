// ============================================================
//  2) PASSAGEM DE PARAMETROS PARA THREADS
// ============================================================
//
//  O construtor de Thread aceita dois tipos de delegate:
//
//  - ThreadStart:          void Metodo()          -> sem parametros
//  - ParameterizedThreadStart: void Metodo(object) -> recebe UM object
//
//  Como ParameterizedThreadStart entrega um `object?`, e preciso fazer o
//  cast de volta para o tipo real dentro do metodo. Isso perde seguranca
//  de tipo e so aceita um argumento.
//
//  A forma mais pratica e usar uma EXPRESSAO LAMBDA que "captura" as
//  variaveis locais (closure). Assim passamos quantos argumentos quisermos,
//  com tipos fortes e sem casts.
//
//  ATENCAO A UM ARMADILHA CLASSICA (captura de variavel de laco):
//  ao criar varias threads dentro de um for, capturar a variavel do laco
//  diretamente pode fazer todas lerem o MESMO valor final. A solucao e
//  copiar o valor para uma variavel local dentro do laco.
// ============================================================

using System.Threading;

namespace ThreadsLesson;

class ThreadParametersDemo
{
    public static void Executar()
    {
        Console.WriteLine("=== 2) Passagem de parametros para threads ===\n");

        // Abordagem 1: ParameterizedThreadStart. O argumento chega como
        // object? e precisa de cast. Passamos o valor no Start().
        Thread t1 = new Thread(ImprimirMensagem);
        t1.Start("Ola de uma thread parametrizada");
        t1.Join();

        // Abordagem 2 (recomendada): lambda com closure. Tipos fortes e
        // multiplos argumentos, sem casts.
        string usuario = "Lucas";
        int repeticoes = 3;
        Thread t2 = new Thread(() => Saudar(usuario, repeticoes));
        t2.Start();
        t2.Join();

        Console.WriteLine();

        // A armadilha da captura da variavel de laco.
        // ERRADO: capturar `i` diretamente faria as threads lerem o valor
        // que `i` tiver no momento da execucao (frequentemente o valor final).
        // CERTO: copiar para uma variavel local `indice` a cada iteracao.
        Console.WriteLine("  Criando 3 threads com indice capturado corretamente:");
        Thread[] threads = new Thread[3];
        for (int i = 0; i < threads.Length; i++)
        {
            int indice = i; // copia local: cada thread captura seu proprio valor
            threads[i] = new Thread(() =>
                Console.WriteLine($"    Thread do indice {indice}"));
        }
        foreach (Thread t in threads) t.Start();
        foreach (Thread t in threads) t.Join();

        Console.WriteLine();
    }

    // Assinatura compativel com ParameterizedThreadStart: recebe object?.
    static void ImprimirMensagem(object? argumento)
    {
        // Cast de volta para o tipo real. O padrao `is` valida antes de usar.
        if (argumento is string texto)
            Console.WriteLine($"  [t1] {texto}");
    }

    static void Saudar(string nome, int vezes)
    {
        for (int i = 0; i < vezes; i++)
            Console.WriteLine($"  [t2] Ola, {nome}! ({i + 1})");
    }
}
