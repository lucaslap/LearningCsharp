// ============================================================
//  C# - Funções e Métodos (Functions & Methods)
// ============================================================
//
//  FUNÇÃO vs MÉTODO
//  ─────────────────
//  Em C#, os dois termos descrevem blocos de código reutilizáveis.
//  A diferença é conceitual:
//    • Método   → função declarada dentro de uma classe/struct.
//    • Função   → termo genérico; em C# toda função é um método.
//
//  ANATOMIA DE UM MÉTODO — cada palavra tem um papel:
//
//      public static int Somar(int a, int b)
//      ──────── ────── ─── ─────  ────  ────
//         │       │     │    │      └──── parâmetros
//         │       │     │    └────────── nome do método
//         │       │     └─────────────── tipo de retorno
//         │       └───────────────────── modificador static
//         └───────────────────────────── modificador de acesso
//
//  ┌─────────────────────────────────────────────────────────┐
//  │  MODIFICADORES DE ACESSO                                │
//  │  Controlam quem pode chamar o método.                   │
//  │                                                         │
//  │  public    → qualquer código pode acessar               │
//  │  private   → apenas a própria classe pode acessar       │
//  │  protected → a própria classe e subclasses (herança)    │
//  │  internal  → apenas código do mesmo projeto (.dll)      │
//  └─────────────────────────────────────────────────────────┘
//
//  ┌─────────────────────────────────────────────────────────┐
//  │  static                                                 │
//  │  Sem static → o método pertence ao OBJETO (instância).  │
//  │              Precisa criar um objeto para chamar.        │
//  │              Tem acesso aos campos do objeto (this).     │
//  │                                                         │
//  │  Com static → o método pertence à CLASSE.               │
//  │              Pode chamar sem criar nenhum objeto.        │
//  │              NÃO tem acesso a campos de instância.       │
//  └─────────────────────────────────────────────────────────┘
//
//  ┌─────────────────────────────────────────────────────────┐
//  │  TIPO DE RETORNO                                        │
//  │  void  → o método não devolve nada                      │
//  │  int   → devolve um inteiro                             │
//  │  string, double, bool, etc. → devolve esse tipo         │
//  │  A palavra 'return' entrega o valor e encerra o método. │
//  └─────────────────────────────────────────────────────────┘
//
// ============================================================

class Program
{
    static void Main()
    {
        Console.WriteLine("=== FUNÇÕES E MÉTODOS EM C# ===\n");

        // ============================================================
        //  0. ANATOMIA — demonstração visual dos modificadores
        // ============================================================
        Console.WriteLine("--- 0. Anatomia de um método ---\n");

        // Chamando um método ESTÁTICO (static) — sem criar objeto:
        //   NomeDaClasse.NomeDoMetodo(...)  ← quando está em outra classe
        //   NomeDoMetodo(...)               ← quando está na mesma classe
        int resultado = Somar(3, 4);
        Console.WriteLine($"Somar(3, 4) = {resultado}  ← método static chamado sem criar objeto\n");

        // Chamando um método DE INSTÂNCIA — precisa criar o objeto primeiro:
        var demo = new DemoModificadores();
        demo.MetodoPublico();    // public  → acessível de qualquer lugar
        demo.MetodoInterno();    // internal → acessível dentro do projeto
        // demo.MetodoPrivado(); // ← ERRO: private só pode ser chamado de dentro da classe
        // demo.MetodoProtegido();// ← ERRO: protected só é acessível em subclasses

        Console.WriteLine();

        // ============================================================
        //  1. MÉTODO SEM RETORNO E SEM PARÂMETROS (void)
        // ============================================================
        Console.WriteLine("--- 1. Método void sem parâmetros ---");

        // 'void' significa que o método não devolve nenhum valor.
        Saudar();
        Saudar(); // pode chamar quantas vezes quiser

        // ============================================================
        //  2. MÉTODO SEM RETORNO COM PARÂMETROS
        // ============================================================
        Console.WriteLine("\n--- 2. Método void com parâmetros ---");

        // Os parâmetros são variáveis recebidas na assinatura do método.
        // Quem chama passa os ARGUMENTOS; o método declara os PARÂMETROS.
        SaudarPessoa("Lucas");
        SaudarPessoa("Maria");

        // ============================================================
        //  3. MÉTODO COM RETORNO
        // ============================================================
        Console.WriteLine("\n--- 3. Método com retorno ---");

        // O método calcula algo e devolve o resultado com 'return'.
        // O tipo declarado antes do nome deve corresponder ao retornado.
        int soma = Somar(10, 5);
        Console.WriteLine($"Somar(10, 5) = {soma}");

        double area = CalcularAreaRetangulo(4.5, 3.0);
        Console.WriteLine($"Área do retângulo 4.5 × 3.0 = {area:F2}");

        // ============================================================
        //  4. PARÂMETROS OPCIONAIS (valores padrão)
        // ============================================================
        Console.WriteLine("\n--- 4. Parâmetros opcionais (default values) ---");

        // Um parâmetro com valor padrão é opcional na chamada.
        // Parâmetros obrigatórios SEMPRE vêm antes dos opcionais.
        Console.WriteLine(CriarMensagem("Lucas"));                     // usa padrão "Olá"
        Console.WriteLine(CriarMensagem("Maria", "Boa tarde"));        // substitui o padrão
        Console.WriteLine(CriarMensagem("João", "Bem-vindo", "!!")); // substitui os dois

        // ============================================================
        //  5. PARÂMETROS NOMEADOS (named arguments)
        // ============================================================
        Console.WriteLine("\n--- 5. Parâmetros nomeados ---");

        // Você pode passar os argumentos fora de ordem usando o nome do parâmetro.
        // Útil quando há muitos parâmetros opcionais e você quer pular um deles.
        Console.WriteLine(CriarMensagem(nome: "Ana", pontuacao: "..."));
        //                              ↑ pula 'saudacao', que usa o valor padrão "Olá"

        // ============================================================
        //  6. RETORNANDO MÚLTIPLOS VALORES COM TUPLA
        // ============================================================
        Console.WriteLine("\n--- 6. Retorno de múltiplos valores (Tuple) ---");

        // C# não permite dois 'return' mas permite retornar uma Tupla.
        // Sintaxe: (TipoA nomeA, TipoB nomeB) NomeDoMetodo(...)
        var (minimo, maximo) = EncontrarMinMax(new int[] { 3, 7, 1, 9, 4, 2 });
        Console.WriteLine("Array: [3, 7, 1, 9, 4, 2]");
        Console.WriteLine($"  Mínimo: {minimo}");
        Console.WriteLine($"  Máximo: {maximo}");

        // ============================================================
        //  7. PARÂMETRO 'ref' — passa por referência
        // ============================================================
        Console.WriteLine("\n--- 7. Parâmetro ref (passagem por referência) ---");

        // Por padrão, C# passa tipos de valor (int, double…) por CÓPIA.
        // Com 'ref', o método recebe o endereço da variável e pode modificá-la.
        int contador = 0;
        Console.WriteLine($"Antes: contador = {contador}");
        Incrementar(ref contador);
        Incrementar(ref contador);
        Console.WriteLine($"Depois de 2 chamadas: contador = {contador}");

        // ============================================================
        //  8. PARÂMETRO 'out' — retorno via parâmetro de saída
        // ============================================================
        Console.WriteLine("\n--- 8. Parâmetro out ---");

        // 'out' é parecido com 'ref', mas:
        //  • A variável NÃO precisa ser inicializada antes de ser passada.
        //  • O método É OBRIGADO a atribuir um valor antes de retornar.
        string entrada = "42";
        if (int.TryParse(entrada, out int numero))
            Console.WriteLine($"Conversão bem-sucedida: \"{entrada}\" → {numero}");

        if (Dividir(10, 3, out double quociente))
            Console.WriteLine($"10 ÷ 3 = {quociente:F4}");

        if (!Dividir(10, 0, out _)) // '_' descarta o out quando não interessa
            Console.WriteLine("Divisão por zero: operação inválida.");

        // ============================================================
        //  9. SOBRECARGA DE MÉTODOS (Method Overloading)
        // ============================================================
        Console.WriteLine("\n--- 9. Sobrecarga de métodos ---");

        // C# permite vários métodos com o MESMO nome, desde que os PARÂMETROS
        // sejam diferentes (tipo ou quantidade). O compilador escolhe a versão certa.
        Console.WriteLine($"Dobrar(int    5)   = {Dobrar(5)}");
        Console.WriteLine($"Dobrar(double 3.5) = {Dobrar(3.5)}");
        Console.WriteLine($"Dobrar(\"oi\")      = {Dobrar("oi")}");

        // ============================================================
        //  10. EXPRESSÃO LAMBDA (função anônima)
        // ============================================================
        Console.WriteLine("\n--- 10. Expressão Lambda ---");

        // Lambda é uma função sem nome criada inline com o operador =>.
        // Sintaxe: (parâmetros) => expressão
        var nums = new List<int> { 5, 2, 8, 1, 9, 3 };

        var pares     = nums.Where(n => n % 2 == 0).ToList();
        var ordenados = nums.OrderBy(n => n).ToList();
        var quadrados = nums.Select(n => n * n).ToList();

        Console.WriteLine($"Original:  [{string.Join(", ", nums)}]");
        Console.WriteLine($"Pares:     [{string.Join(", ", pares)}]");
        Console.WriteLine($"Ordenados: [{string.Join(", ", ordenados)}]");
        Console.WriteLine($"Quadrados: [{string.Join(", ", quadrados)}]");

        // Lambda atribuída a uma variável usando Func<TEntrada, TSaída>
        Func<double, double> calcularImposto = preco => preco * 0.15;
        Console.WriteLine($"\nImposto 15% sobre R$ 200,00: R$ {calcularImposto(200):F2}");

        // Action<T> — lambda que não retorna nada (equivale a void)
        Action<string> imprimir = msg => Console.WriteLine($"  → {msg}");
        imprimir("Lambda com Action<T> não retorna valor.");

        // ============================================================
        //  11. RECURSÃO
        // ============================================================
        Console.WriteLine("\n--- 11. Recursão ---");

        // Uma função recursiva chama a si mesma para resolver subproblemas.
        // SEMPRE precisa de um CASO BASE para parar; sem ele → loop infinito.
        Console.WriteLine($"Fatorial(0) = {Fatorial(0)}");  // caso base
        Console.WriteLine($"Fatorial(5) = {Fatorial(5)}");  // 5×4×3×2×1 = 120
        Console.WriteLine($"Fibonacci(7) = {Fibonacci(7)}"); // 0 1 1 2 3 5 8 13 → pos.7 = 13

        // ============================================================
        //  12. MÉTODOS DE INSTÂNCIA
        // ============================================================
        Console.WriteLine("\n--- 12. Métodos de instância ---");

        // Métodos sem 'static' pertencem ao OBJETO e operam nos seus dados.
        var conta1 = new ContaBancaria("Lucas", 1000);
        var conta2 = new ContaBancaria("Maria", 500);

        conta1.Depositar(200);
        conta1.Sacar(150);
        conta2.Depositar(800);

        Console.WriteLine(conta1.ExibirSaldo());
        Console.WriteLine(conta2.ExibirSaldo());

        // ============================================================
        //  RESUMO
        // ============================================================
        Console.WriteLine("\n=== RESUMO ===");
        Console.WriteLine("  void Metodo()                      → sem retorno, sem parâmetro");
        Console.WriteLine("  void Metodo(tipo param)            → sem retorno, com parâmetro");
        Console.WriteLine("  tipo Metodo(...)                   → com retorno");
        Console.WriteLine("  tipo param = valor                 → parâmetro opcional");
        Console.WriteLine("  nome: valor na chamada             → argumento nomeado");
        Console.WriteLine("  (TipoA, TipoB) Metodo()           → retorno de tupla");
        Console.WriteLine("  ref tipo param                     → passa por referência");
        Console.WriteLine("  out tipo param                     → retorno via parâmetro de saída");
        Console.WriteLine("  mesmo nome, parâmetros diferentes  → sobrecarga (overloading)");
        Console.WriteLine("  (p) => expressão                   → lambda / função anônima");
        Console.WriteLine("  Func<TIn,TOut> / Action<T>         → tipos de delegate para lambdas");
        Console.WriteLine("  método chama a si mesmo            → recursão (exige caso base!)");
        Console.WriteLine("  static                             → pertence à classe");
        Console.WriteLine("  sem static                         → pertence ao objeto (instância)");

        Console.ReadKey();
    }

    // ============================================================
    //  IMPLEMENTAÇÕES DOS MÉTODOS
    // ============================================================

    // 1. void sem parâmetros
    static void Saudar()
    {
        Console.WriteLine("Olá! Bem-vindo ao estudo de funções em C#.");
    }

    // 2. void com parâmetro
    static void SaudarPessoa(string nome)
    {
        Console.WriteLine($"Olá, {nome}! Seja bem-vindo(a).");
    }

    // 3. com retorno
    static int Somar(int a, int b)
    {
        return a + b;
    }

    static double CalcularAreaRetangulo(double largura, double altura)
    {
        return largura * altura;
    }

    // 4 e 5. parâmetros opcionais e nomeados
    static string CriarMensagem(string nome, string saudacao = "Olá", string pontuacao = "!")
    {
        return $"{saudacao}, {nome}{pontuacao}";
    }

    // 6. retorno de tupla
    static (int Min, int Max) EncontrarMinMax(int[] numeros)
    {
        int min = numeros[0];
        int max = numeros[0];
        foreach (int n in numeros)
        {
            if (n < min) min = n;
            if (n > max) max = n;
        }
        return (min, max);
    }

    // 7. ref
    static void Incrementar(ref int valor)
    {
        valor++; // modifica a variável original, não uma cópia
    }

    // 8. out
    static bool Dividir(double dividendo, double divisor, out double resultado)
    {
        if (divisor == 0)
        {
            resultado = 0; // obrigado a atribuir mesmo no caso de erro
            return false;
        }
        resultado = dividendo / divisor;
        return true;
    }

    // 9. sobrecarga — mesmo nome, parâmetros diferentes
    static int    Dobrar(int x)        => x * 2;
    static double Dobrar(double x)     => x * 2;
    static string Dobrar(string texto) => texto + texto;

    // 11. recursão
    static long Fatorial(int n)
    {
        if (n <= 1) return 1;       // caso base
        return n * Fatorial(n - 1); // chamada recursiva
    }

    static int Fibonacci(int n)
    {
        if (n <= 1) return n;                       // casos base: F(0)=0, F(1)=1
        return Fibonacci(n - 1) + Fibonacci(n - 2); // chamada recursiva
    }
}

// ============================================================
//  CLASSES AUXILIARES
// ============================================================

// 0. demonstração de modificadores de acesso
class DemoModificadores
{
    // public → qualquer código fora da classe pode chamar
    public void MetodoPublico()
    {
        Console.WriteLine("  public    → acessível de qualquer lugar");
        MetodoPrivado(); // a própria classe pode chamar seus métodos private
    }

    // internal → padrão quando nenhum modificador é escrito; acessível no mesmo projeto
    internal void MetodoInterno()
    {
        Console.WriteLine("  internal  → acessível apenas dentro deste projeto (.dll)");
    }

    // private → só pode ser chamado de dentro desta classe
    private void MetodoPrivado()
    {
        Console.WriteLine("  private   → chamado internamente por MetodoPublico()");
    }

    // protected → acessível nesta classe e em subclasses (herança)
    protected void MetodoProtegido()
    {
        Console.WriteLine("  protected → acessível apenas em subclasses");
    }
}

// 12. métodos de instância
class ContaBancaria
{
    private string _titular;
    private double _saldo;

    public ContaBancaria(string titular, double saldoInicial)
    {
        _titular = titular;
        _saldo   = saldoInicial;
    }

    public void Depositar(double valor)
    {
        if (valor > 0) _saldo += valor;
    }

    public bool Sacar(double valor)
    {
        if (valor > _saldo) return false; // saldo insuficiente
        _saldo -= valor;
        return true;
    }

    public string ExibirSaldo() => $"Conta de {_titular}: R$ {_saldo:F2}";
}
