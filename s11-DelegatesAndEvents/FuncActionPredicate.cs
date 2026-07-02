// ============================================================
//  C# — Func, Action, Predicate e Lambdas
// ============================================================
//
//  POR QUE NAO DECLARAR UM DELEGATE TODA VEZ?
//  ──────────────────────────────────────────
//  Na pratica, raramente declaramos `delegate ...` a mao. O .NET ja
//  traz delegates genericos prontos que cobrem quase tudo:
//
//    Action            -> nao recebe nada, retorna void
//    Action<T>         -> recebe T, retorna void
//    Action<T1, T2>    -> recebe varios parametros, retorna void
//    Func<TResult>     -> nao recebe nada, retorna TResult
//    Func<T, TResult>  -> recebe T, retorna TResult
//    Predicate<T>      -> recebe T, retorna bool (atalho de Func<T, bool>)
//
//  Regra mental para Func<...>: o ULTIMO tipo generico e sempre o
//  RETORNO; os anteriores sao os parametros. Ex: Func<int, int, string>
//  recebe (int, int) e devolve string.
//
//
//  LAMBDAS  ( =>  "vai para" )
//  ──────────────────────────
//  Lambda e uma forma curta de escrever um metodo inline:
//
//      x => x * 2                 // recebe x, devolve x*2
//      (a, b) => a + b            // recebe a e b, devolve a soma
//      () => Console.WriteLine()  // sem parametros
//      x => { ...varias linhas...; return y; }   // corpo em bloco
//
//  Metodos anonimos com `delegate (..) { }` sao a forma antiga; a
//  lambda e o estilo moderno e preferido.
//
//
//  CLOSURE (captura de variavel)
//  ─────────────────────────────
//  Uma lambda "lembra" das variaveis do escopo onde foi criada,
//  mesmo depois que esse escopo termina. Isso se chama closure.
//
// ============================================================

namespace DelegatesAndEvents;

static class FuncActionPredicateDemo
{
    public static void Executar()
    {
        Console.WriteLine("\n=== 5) Action — recebe argumentos, nao retorna nada ===\n");

        // Action<string>: um parametro string, retorno void.
        // Aqui o corpo e uma lambda inline, sem precisar de metodo nomeado.
        Action<string> cumprimentar = nome => Console.WriteLine($"Ola, {nome}!");
        cumprimentar("Lucas");   // Ola, Lucas!
        cumprimentar("Ana");     // Ola, Ana!

        Console.WriteLine("\n=== 6) Func — recebe argumentos E retorna valor ===\n");

        // Func<int, int, int>: dois int de entrada, um int de saida.
        Func<int, int, int> somar = (a, b) => a + b;
        Console.WriteLine($"somar(7, 8) = {somar(7, 8)}");   // 15

        // Func<double, double>: um double entra, um double sai.
        Func<double, double> dobro = x => x * 2;
        Console.WriteLine($"dobro(21) = {dobro(21)}");        // 42

        Console.WriteLine("\n=== 7) Predicate — pergunta que devolve true/false ===\n");

        // Predicate<int> e o mesmo que Func<int, bool>: testa uma condicao.
        Predicate<int> ehPar = n => n % 2 == 0;
        Console.WriteLine($"ehPar(10) = {ehPar(10)}");   // True
        Console.WriteLine($"ehPar(7)  = {ehPar(7)}");    // False

        Console.WriteLine("\n=== 8) Passando comportamento para metodos ===\n");

        // List<T>.FindAll, RemoveAll etc. aceitam Predicate<T>;
        // outros metodos de LINQ aceitam Func. Repare como passamos
        // a "regra" como argumento, sem if espalhado pelo codigo.
        List<int> numeros = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        List<int> pares = numeros.FindAll(ehPar);
        Console.WriteLine($"Pares: {string.Join(", ", pares)}");          // 2, 4, 6, 8, 10

        List<int> maioresQue5 = numeros.FindAll(n => n > 5);
        Console.WriteLine($"Maiores que 5: {string.Join(", ", maioresQue5)}");  // 6, 7, 8, 9, 10

        Console.WriteLine("\n=== 9) Closure — a lambda lembra o escopo ===\n");

        // CriarContador devolve uma Func que "captura" a variavel local
        // `total`. Cada chamada da func soma e devolve o acumulado.
        Func<int, int> acumulador = CriarAcumulador();
        Console.WriteLine($"acumulador(10) = {acumulador(10)}");   // 10
        Console.WriteLine($"acumulador(5)  = {acumulador(5)}");    // 15
        Console.WriteLine($"acumulador(3)  = {acumulador(3)}");    // 18
        // `total` continua vivo entre as chamadas porque a lambda o capturou.
    }

    // A variavel `total` vive normalmente so durante esta chamada, mas a
    // lambda retornada a captura — entao ela sobrevive junto com a lambda.
    static Func<int, int> CriarAcumulador()
    {
        int total = 0;
        return valor =>
        {
            total += valor;
            return total;
        };
    }
}
