// ============================================================
//  C# — Delegates (Delegados)
// ============================================================
//
//  O QUE E UM DELEGATE?
//  ────────────────────
//  Um delegate e um TIPO que representa uma referencia a um metodo.
//  Pense nele como um "ponteiro de funcao" com seguranca de tipo:
//  ele guarda QUAL metodo chamar, e voce pode passar isso adiante
//  como se fosse uma variavel.
//
//  Por que isso e util?
//    - Permite passar comportamento como argumento (callbacks).
//    - E a base de eventos, LINQ, lambdas e Func/Action/Predicate.
//    - Desacopla "quem chama" de "o que e chamado".
//
//
//  DECLARACAO
//  ──────────
//  A assinatura do delegate (retorno + parametros) define quais
//  metodos cabem nele:
//
//      delegate int Operacao(int a, int b);
//
//  Qualquer metodo `int Xxx(int, int)` pode ser atribuido a uma
//  variavel do tipo Operacao.
//
//
//  MULTICAST
//  ─────────
//  Um delegate pode apontar para VARIOS metodos ao mesmo tempo
//  (lista de invocacao). Use `+=` para adicionar e `-=` para remover.
//  Ao invocar, todos rodam na ordem em que foram adicionados.
//  Atencao: se o delegate tem retorno, so o valor do ULTIMO metodo
//  e devolvido — por isso multicast combina melhor com `void`.
//
//
//  Para Func/Action/Predicate e lambdas, veja FuncActionPredicate.cs.
//  Para eventos (event), veja Events.cs.
//
// ============================================================

namespace DelegatesAndEvents;

// Delegate customizado: aceita qualquer metodo que receba dois int
// e devolva um int.
delegate int Operacao(int a, int b);

// Delegate sem retorno: ideal para multicast e notificacoes.
delegate void Notificador(string mensagem);

class Program
{
    static void Main()
    {
        Console.WriteLine("=== 1) Delegate apontando para um metodo ===\n");

        // Atribui o metodo Somar (mesma assinatura) ao delegate.
        // Note: usamos o NOME do metodo, sem parenteses — nao estamos
        // chamando, e sim guardando a referencia.
        Operacao op = Somar;
        Console.WriteLine($"op(4, 3) = {op(4, 3)}");   // 7 — chama Somar

        // O mesmo delegate pode apontar para outro metodo compativel.
        op = Multiplicar;
        Console.WriteLine($"op(4, 3) = {op(4, 3)}");   // 12 — agora chama Multiplicar

        Console.WriteLine("\n=== 2) Delegate como parametro (callback) ===\n");

        // Aplicar nao sabe (nem precisa saber) qual conta sera feita.
        // O comportamento chega de fora, via delegate.
        Console.WriteLine($"Aplicar Somar:       {Aplicar(10, 5, Somar)}");        // 15
        Console.WriteLine($"Aplicar Multiplicar: {Aplicar(10, 5, Multiplicar)}");  // 50

        Console.WriteLine("\n=== 3) Multicast: um delegate, varios metodos ===\n");

        // += encadeia metodos na lista de invocacao. Declaramos como
        // Notificador? (anulavel) porque um -= pode esvaziar a lista e
        // deixar o delegate null — por isso invocamos com ?.Invoke.
        Notificador? aviso = LogConsole;
        aviso += LogPrefixado;
        aviso += LogTamanho;

        // Uma unica chamada dispara os tres, na ordem adicionada.
        aviso?.Invoke("Sistema iniciado");
        // Saida esperada:
        //   [console] Sistema iniciado
        //   [LOG] Sistema iniciado
        //   (mensagem com 16 caracteres)

        Console.WriteLine("\n-- removendo um metodo com -= --\n");

        aviso -= LogPrefixado;   // tira LogPrefixado da lista
        aviso?.Invoke("Apenas dois agora");
        // Saida esperada:
        //   [console] Apenas dois agora
        //   (mensagem com 17 caracteres)

        Console.WriteLine("\n=== 4) Delegate pode ser null — proteja a invocacao ===\n");

        Notificador? talvezNulo = null;
        // talvezNulo("boom");   // NullReferenceException se descomentar
        talvezNulo?.Invoke("So roda se houver algum metodo inscrito");
        Console.WriteLine("Invocacao com ?. nao quebra quando o delegate e null.");

        // --------------------------------------------------------
        //  Sub-topicos em arquivos separados (padrao hibrido s09):
        // --------------------------------------------------------

        FuncActionPredicateDemo.Executar();
        EventsDemo.Executar();

        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }

    // ----- Metodos compativeis com o delegate Operacao -----

    static int Somar(int a, int b) => a + b;
    static int Multiplicar(int a, int b) => a * b;

    // Recebe o comportamento como parametro. Quem chama decide a conta.
    static int Aplicar(int a, int b, Operacao op) => op(a, b);

    // ----- Metodos compativeis com o delegate Notificador (void) -----

    static void LogConsole(string msg) => Console.WriteLine($"[console] {msg}");
    static void LogPrefixado(string msg) => Console.WriteLine($"[LOG] {msg}");
    static void LogTamanho(string msg)
        => Console.WriteLine($"(mensagem com {msg.Length} caracteres)");
}
