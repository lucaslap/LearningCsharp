// ============================================================
//  C# - Programação Orientada a Objetos (Object-Oriented Programming)
// ============================================================
//
//  POO em uma frase: organizar o programa em OBJETOS que combinam
//  DADOS (campos/propriedades) com COMPORTAMENTO (métodos).
//
//  OS 4 PILARES DA POO
//  ───────────────────
//    1. Abstração     → expor só o essencial; esconder o "como"
//    2. Encapsulamento→ proteger dados internos com private + propriedades
//    3. Herança       → uma classe filha REUTILIZA código da classe mãe
//    4. Polimorfismo  → o mesmo método age diferente em cada classe
//
//  CLASSE vs OBJETO
//  ────────────────
//    • Classe = a PLANTA, o molde, o tipo. Existe uma só.
//    • Objeto = a INSTÂNCIA criada a partir da planta. Pode ter muitas.
//
//      class  Carro       ← a planta (descreve o que TODO carro tem)
//      Carro  meuCarro    ← um objeto específico (com dados próprios)
//      meuCarro = new Carro(); ← cria a instância na memória
//
//  ┌─────────────────────────────────────────────────────────┐
//  │  ANATOMIA DE UMA CLASSE                                 │
//  │                                                         │
//  │  class Pessoa                                           │
//  │  {                                                      │
//  │      private string _nome;        ← CAMPO (estado)      │
//  │      public  int    Idade { get; set; } ← PROPRIEDADE   │
//  │      public  Pessoa(string n) {...}     ← CONSTRUTOR    │
//  │      public  void   Falar() {...}       ← MÉTODO        │
//  │  }                                                      │
//  └─────────────────────────────────────────────────────────┘
//
// ============================================================

class Program
{
    static void Main()
    {
        Console.WriteLine("=== PROGRAMAÇÃO ORIENTADA A OBJETOS EM C# ===\n");

        // ============================================================
        //  1. CLASSE E OBJETO — criação básica
        // ============================================================
        Console.WriteLine("--- 1. Classe e Objeto ---");

        // 'new' aloca memória e chama o construtor.
        // 'p1' é uma REFERÊNCIA (endereço) para o objeto na memória.
        Pessoa p1 = new Pessoa();
        p1.Nome = "Lucas";
        p1.Idade = 30;
        p1.Apresentar();

        // Cada objeto tem seu PRÓPRIO estado, independente dos outros.
        Pessoa p2 = new Pessoa();
        p2.Nome = "Maria";
        p2.Idade = 25;
        p2.Apresentar();

        // ============================================================
        //  2. CAMPOS vs PROPRIEDADES
        // ============================================================
        Console.WriteLine("\n--- 2. Campos vs Propriedades ---");

        // CAMPO    → variável dentro da classe (estado bruto).
        //           Geralmente private para esconder dados.
        // PROPRIEDADE → "wrapper" público com get/set que controla o acesso.
        //           Permite validação, leitura-só, etc.
        var produto = new Produto();
        produto.Nome  = "Notebook";
        produto.Preco = 3500.00m;
        produto.Preco = -10;            // ← rejeitado pelo set (mantém 3500)
        Console.WriteLine($"{produto.Nome} — R$ {produto.Preco:F2}");
        Console.WriteLine($"Preço com 10% imposto: R$ {produto.PrecoComImposto:F2}"); // só getter

        // ============================================================
        //  3. CONSTRUTORES — inicialização do objeto
        // ============================================================
        Console.WriteLine("\n--- 3. Construtores ---");

        // O construtor é chamado AUTOMATICAMENTE pelo 'new'.
        // Usado para garantir que o objeto nasça em estado válido.
        // Pode ter SOBRECARGA (vários construtores com parâmetros diferentes).
        var livro1 = new Livro("1984", "George Orwell");           // 2 parâmetros
        var livro2 = new Livro("Dom Casmurro", "Machado", 256);    // 3 parâmetros
        var livro3 = new Livro();                                  // sem parâmetros
        Console.WriteLine(livro1.Resumo());
        Console.WriteLine(livro2.Resumo());
        Console.WriteLine(livro3.Resumo());

        // ============================================================
        //  4. ENCAPSULAMENTO — esconder os detalhes internos
        // ============================================================
        Console.WriteLine("\n--- 4. Encapsulamento ---");

        // O saldo é PRIVADO: ninguém de fora pode somar/subtrair direto.
        // Só métodos públicos (Depositar/Sacar) controlam a mudança.
        // Isso garante regras de negócio (não permitir saldo negativo, etc).
        var conta = new ContaBancaria("Lucas", 1000);
        conta.Depositar(500);
        conta.Sacar(2000);    // ← bloqueado: saldo insuficiente
        conta.Sacar(300);
        // conta._saldo = 9999999; ← ERRO de compilação: campo é private
        Console.WriteLine(conta.ExibirSaldo());

        // ============================================================
        //  5. HERANÇA — reaproveitar código da classe mãe
        // ============================================================
        Console.WriteLine("\n--- 5. Herança ---");

        // Cachorro HERDA de Animal — ganha Nome, Idade e Dormir() de graça.
        // 'base(...)' chama o construtor da classe mãe.
        var rex = new Cachorro("Rex", 5, "Labrador");
        rex.Dormir();                 // método herdado de Animal
        rex.Latir();                  // método próprio de Cachorro
        Console.WriteLine($"Raça: {rex.Raca}");

        var felix = new Gato("Félix", 3);
        felix.Dormir();
        felix.Ronronar();

        // ============================================================
        //  6. POLIMORFISMO — mesmo método, comportamentos diferentes
        // ============================================================
        Console.WriteLine("\n--- 6. Polimorfismo (virtual / override) ---");

        // 'virtual' permite que a classe filha SUBSTITUA o método.
        // 'override' redefine o comportamento na classe filha.
        // Ao chamar via referência da classe mãe, executa a versão da filha.
        Animal[] animais = { rex, felix, new Animal("Genérico", 1) };
        foreach (var a in animais)
            a.EmitirSom(); // cada um responde do seu jeito

        // ============================================================
        //  7. CLASSE ABSTRATA — molde que NÃO pode ser instanciado
        // ============================================================
        Console.WriteLine("\n--- 7. Classe Abstrata ---");

        // 'abstract' marca a classe como incompleta — só serve como base.
        // Métodos abstratos NÃO têm corpo: as filhas SÃO OBRIGADAS a implementar.
        // Forma[] formas = { new Forma() }; ← ERRO: Forma é abstrata
        Forma[] formas = { new Circulo(5), new Retangulo(4, 6) };
        foreach (var f in formas)
            Console.WriteLine($"{f.GetType().Name}: área = {f.CalcularArea():F2}");

        // ============================================================
        //  8. INTERFACE — contrato puro, sem implementação
        // ============================================================
        Console.WriteLine("\n--- 8. Interface ---");

        // Uma interface só DECLARA o que a classe DEVE fazer (sem dizer como).
        // Uma classe pode implementar VÁRIAS interfaces (ao contrário da herança).
        // Convenção: nome da interface começa com 'I' (IComparable, IDisposable…).
        IPagavel[] pagaveis = {
            new Funcionario("Ana", 5000),
            new Freelancer("Bruno", 80, 60) // 80h × R$60
        };
        foreach (var p in pagaveis)
            Console.WriteLine($"{p.GetType().Name}: pagar R$ {p.CalcularPagamento():F2}");

        // ============================================================
        //  9. CLASSE SELADA (sealed) — proibir herança
        // ============================================================
        Console.WriteLine("\n--- 9. Classe sealed ---");

        // 'sealed' impede que outras classes herdem desta.
        // Útil para classes que NÃO devem ser estendidas (segurança, performance).
        var cpf = new Cpf("123.456.789-00");
        Console.WriteLine($"CPF: {cpf.Numero}");
        // class Outro : Cpf { } ← ERRO: não pode herdar de classe sealed

        // ============================================================
        //  10. CLASSE ESTÁTICA E MEMBROS ESTÁTICOS
        // ============================================================
        Console.WriteLine("\n--- 10. Static ---");

        // Classe static → não tem instância, só métodos utilitários.
        Console.WriteLine($"Calculadora.Somar(10, 5) = {Calculadora.Somar(10, 5)}");
        Console.WriteLine($"Calculadora.Pi          = {Calculadora.Pi}");

        // Campo static → COMPARTILHADO entre TODOS os objetos da classe.
        // Cada 'new Contador()' incrementa o mesmo _total.
        new Contador(); new Contador(); new Contador();
        Console.WriteLine($"Contador.Total = {Contador.Total}");

        // ============================================================
        //  11. const e readonly — valores que não mudam
        // ============================================================
        Console.WriteLine("\n--- 11. const e readonly ---");

        // const    → valor FIXO em tempo de compilação. Implícito static.
        // readonly → atribuído UMA vez (na declaração ou no construtor).
        //            Pode variar por instância; const não.
        Console.WriteLine($"Configuracao.VersaoApp = {Configuracao.VersaoApp}");
        var cfg = new Configuracao("PROD");
        Console.WriteLine($"Ambiente (readonly): {cfg.Ambiente}");

        // ============================================================
        //  12. THIS e BASE — referências especiais
        // ============================================================
        Console.WriteLine("\n--- 12. this e base ---");

        // 'this' refere-se ao OBJETO ATUAL (útil quando há ambiguidade de nomes).
        // 'base' refere-se à CLASSE MÃE (chamar construtor/método herdado).
        var func = new FuncionarioCLT("Carla", 4000);
        func.MostrarDados(); // usa this para distinguir parâmetro de campo

        // ============================================================
        //  RESUMO
        // ============================================================
        Console.WriteLine("\n=== RESUMO ===");
        Console.WriteLine("  class Nome { }                  → define um molde");
        Console.WriteLine("  new Nome()                      → cria instância (objeto)");
        Console.WriteLine("  private/public/protected        → controle de acesso");
        Console.WriteLine("  Nome { get; set; }              → propriedade auto-implementada");
        Console.WriteLine("  Nome(...) : this(...)/base(...) → construtor encadeado");
        Console.WriteLine("  class Filha : Mae               → herança");
        Console.WriteLine("  virtual / override              → polimorfismo");
        Console.WriteLine("  abstract                        → molde obrigatório (sem instância)");
        Console.WriteLine("  interface I... { }              → contrato sem implementação");
        Console.WriteLine("  sealed                          → impede herança");
        Console.WriteLine("  static                          → pertence à classe, não ao objeto");
        Console.WriteLine("  const                           → constante de compilação");
        Console.WriteLine("  readonly                        → atribuído só no construtor");

        Console.ReadKey();
    }
}

// ============================================================
//  CLASSES AUXILIARES
// ============================================================

// 1. Classe simples com campos e método
class Pessoa
{
    // Propriedades auto-implementadas: o compilador cria o campo de fundo.
    public string Nome  { get; set; } = "";
    public int    Idade { get; set; }

    public void Apresentar()
    {
        Console.WriteLine($"  Olá, sou {Nome} e tenho {Idade} anos.");
    }
}

// 2. Campos vs propriedades + validação no setter
class Produto
{
    private decimal _preco; // CAMPO privado guarda o estado real

    public string Nome { get; set; } = "";

    // Propriedade COM lógica: o set valida antes de aceitar o valor.
    public decimal Preco
    {
        get => _preco;
        set
        {
            if (value >= 0) _preco = value;
            // valores negativos são silenciosamente ignorados
        }
    }

    // Propriedade SOMENTE LEITURA (calculada): só tem 'get'.
    public decimal PrecoComImposto => _preco * 1.10m;
}

// 3. Construtores com sobrecarga
class Livro
{
    public string Titulo { get; }
    public string Autor  { get; }
    public int    Paginas { get; }

    // Construtor sem parâmetros — valores padrão.
    public Livro() : this("Sem título", "Desconhecido", 0)
    {
        // ': this(...)' encadeia para o construtor de baixo, evitando duplicação.
    }

    // Construtor com 2 parâmetros — encadeia para o de 3.
    public Livro(string titulo, string autor) : this(titulo, autor, 0) { }

    // Construtor "principal" com 3 parâmetros.
    public Livro(string titulo, string autor, int paginas)
    {
        Titulo  = titulo;
        Autor   = autor;
        Paginas = paginas;
    }

    public string Resumo() => $"  \"{Titulo}\" — {Autor} ({Paginas} pág.)";
}

// 4. Encapsulamento bem feito
class ContaBancaria
{
    // Campos privados: ninguém de fora mexe direto.
    private readonly string _titular;
    private decimal _saldo;

    public ContaBancaria(string titular, decimal saldoInicial)
    {
        _titular = titular;
        _saldo   = saldoInicial;
    }

    public void Depositar(decimal valor)
    {
        if (valor <= 0) return;
        _saldo += valor;
        Console.WriteLine($"  Depósito de R$ {valor:F2} OK.");
    }

    public bool Sacar(decimal valor)
    {
        if (valor > _saldo)
        {
            Console.WriteLine($"  Saque de R$ {valor:F2} negado: saldo insuficiente.");
            return false;
        }
        _saldo -= valor;
        Console.WriteLine($"  Saque de R$ {valor:F2} OK.");
        return true;
    }

    public string ExibirSaldo() => $"  Conta de {_titular}: R$ {_saldo:F2}";
}

// 5 e 6. Herança + polimorfismo (virtual / override)
class Animal
{
    public string Nome  { get; }
    public int    Idade { get; }

    public Animal(string nome, int idade)
    {
        Nome  = nome;
        Idade = idade;
    }

    public void Dormir() => Console.WriteLine($"  {Nome} está dormindo... 💤");

    // 'virtual' = filhas PODEM substituir este comportamento.
    public virtual void EmitirSom() => Console.WriteLine($"  {Nome} faz um som genérico.");
}

class Cachorro : Animal
{
    public string Raca { get; }

    // 'base(nome, idade)' chama o construtor da classe Animal.
    public Cachorro(string nome, int idade, string raca) : base(nome, idade)
    {
        Raca = raca;
    }

    public void Latir() => Console.WriteLine($"  {Nome} late: Au! Au!");

    // 'override' substitui a versão virtual da classe mãe.
    public override void EmitirSom() => Console.WriteLine($"  {Nome} (cão): Au au!");
}

class Gato : Animal
{
    public Gato(string nome, int idade) : base(nome, idade) { }

    public void Ronronar() => Console.WriteLine($"  {Nome} ronrona: prrrr...");

    public override void EmitirSom() => Console.WriteLine($"  {Nome} (gato): Miau!");
}

// 7. Classe abstrata
abstract class Forma
{
    // Método abstrato: SEM corpo. Filhas obrigadas a implementar.
    public abstract double CalcularArea();
}

class Circulo : Forma
{
    private readonly double _raio;
    public Circulo(double raio) => _raio = raio;
    public override double CalcularArea() => Math.PI * _raio * _raio;
}

class Retangulo : Forma
{
    private readonly double _largura;
    private readonly double _altura;
    public Retangulo(double largura, double altura)
    {
        _largura = largura;
        _altura  = altura;
    }
    public override double CalcularArea() => _largura * _altura;
}

// 8. Interface
interface IPagavel
{
    // Interface só DECLARA — sem corpo, sem campos privados.
    decimal CalcularPagamento();
}

class Funcionario : IPagavel
{
    public string  Nome    { get; }
    public decimal Salario { get; }

    public Funcionario(string nome, decimal salario)
    {
        Nome    = nome;
        Salario = salario;
    }

    public decimal CalcularPagamento() => Salario;
}

class Freelancer : IPagavel
{
    public string  Nome      { get; }
    public decimal ValorHora { get; }
    public int     Horas     { get; }

    public Freelancer(string nome, int horas, decimal valorHora)
    {
        Nome      = nome;
        Horas     = horas;
        ValorHora = valorHora;
    }

    public decimal CalcularPagamento() => Horas * ValorHora;
}

// 9. Classe sealed (não pode ser herdada)
sealed class Cpf
{
    public string Numero { get; }
    public Cpf(string numero) => Numero = numero;
}

// 10. Classe estática (utilitários)
static class Calculadora
{
    public const double Pi = 3.14159;

    public static int Somar(int a, int b) => a + b;
    public static int Subtrair(int a, int b) => a - b;
}

// Membro static compartilhado entre instâncias
class Contador
{
    public static int Total { get; private set; } // só esta classe altera

    public Contador() => Total++; // a cada 'new', incrementa o contador da classe
}

// 11. const e readonly
class Configuracao
{
    // const: valor fixo, conhecido em tempo de compilação. Implícito static.
    public const string VersaoApp = "1.0.0";

    // readonly: pode ser atribuído na declaração ou no construtor; depois trava.
    public readonly string Ambiente;

    public Configuracao(string ambiente)
    {
        Ambiente = ambiente; // OK aqui — última chance de atribuir
    }
}

// 12. this para resolver ambiguidade entre parâmetro e campo
class FuncionarioCLT
{
    private readonly string nome;
    private readonly decimal salario;

    public FuncionarioCLT(string nome, decimal salario)
    {
        // Parâmetro e campo têm o mesmo nome — 'this' diferencia.
        this.nome    = nome;
        this.salario = salario;
    }

    public void MostrarDados() =>
        Console.WriteLine($"  CLT: {nome} — R$ {salario:F2}");
}
