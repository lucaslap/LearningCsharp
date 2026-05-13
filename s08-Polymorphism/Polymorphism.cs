// ============================================================
//  C# — Polimorfismo e Interfaces
// ============================================================
//
//  POLIMORFISMO (do grego: "muitas formas")
//  ────────────────────────────────────────
//  É a capacidade de um MESMO método/objeto se comportar de
//  MANEIRAS DIFERENTES dependendo do tipo concreto em tempo
//  de execução.
//
//  Existem dois "sabores" principais em C#:
//
//    1) Polimorfismo de SOBRECARGA (compile-time)
//       → mesmo nome de método, assinaturas diferentes
//         Ex: Somar(int, int) vs Somar(double, double)
//
//    2) Polimorfismo de SOBRESCRITA (runtime, o que vamos focar)
//       → classe-filha redefine um método da classe-pai usando
//         "virtual" (na pai) + "override" (na filha)
//
//  POR QUE USAR?
//  ─────────────
//  Permite escrever código que trabalha com a CLASSE BASE
//  (ou uma interface) e funciona para QUALQUER filha — sem
//  precisar de "if/switch" gigante checando o tipo.
//
//      Animal[] zoologico = { new Cachorro(), new Gato(), new Vaca() };
//      foreach (var a in zoologico) a.FazerSom();
//      // cada um late, mia ou muge — o RUNTIME decide qual chamar
//
//
//  AS PALAVRAS-CHAVE
//  ─────────────────
//    virtual   → na classe-pai: "este método PODE ser sobrescrito"
//    override  → na classe-filha: "estou redefinindo o método da pai"
//    new       → na classe-filha: "estou ESCONDENDO o método da pai"
//                (raramente é o que você quer — geralmente quer override)
//    abstract  → método SEM corpo, OBRIGA as filhas a implementarem
//    sealed    → impede que outras classes herdem/sobrescrevam
//    base      → referência à classe-pai (chamar base.Metodo())
//
//
//  CLASSE ABSTRATA vs INTERFACE
//  ────────────────────────────
//
//    CLASSE ABSTRATA                    INTERFACE
//    ───────────────                    ─────────
//    abstract class Animal              interface IVoador
//
//    • pode ter CAMPOS                  • só métodos/propriedades (tradicionalmente)
//    • pode ter construtor              • sem construtor
//    • pode ter método com corpo        • só ASSINATURAS (até C# 8, depois pode default)
//    • pode ter método abstract         • todos os membros são "públicos abstratos"
//    • herança SIMPLES (1 só pai)       • uma classe pode implementar VÁRIAS
//    • diz "É UM" (Cachorro É UM Animal) • diz "É CAPAZ DE" (Pato É CAPAZ DE Voar)
//
//  Regra prática:
//    • Tem estado/comportamento comum? → classe abstrata
//    • Só quer garantir um CONTRATO?   → interface
//    • Precisa dos dois?               → use ambos!
//
//
//  CONVENÇÃO DE NOMES
//  ──────────────────
//    Interfaces SEMPRE começam com "I" maiúsculo:
//        IComparable, IDisposable, IEnumerable, IVoador
//
//
//  DEPENDENCY INJECTION (DI) — injeção de dependência
//  ──────────────────────────────────────────────────
//
//  É o "para que serve" das interfaces no dia a dia. A ideia é
//  simples:
//
//    EM VEZ DE a classe CRIAR sozinha as coisas de que precisa,
//    ela RECEBE essas coisas de fora (geralmente pelo construtor).
//
//  Exemplo do problema (acoplamento forte):
//
//      class Pedido
//      {
//          private EmailService _email = new EmailService(); //
//          public void Confirmar() => _email.Enviar(...);
//      }
//
//    → Pedido está PRESO ao EmailService concreto. Não dá pra
//      trocar por SmsService, nem por um FakeEmailService em teste.
//
//  Mesma classe com DI (acoplamento fraco):
//
//      class Pedido
//      {
//          private readonly INotificador _notificador;
//          public Pedido(INotificador notificador)  // recebe de fora
//          {
//              _notificador = notificador;
//          }
//          public void Confirmar() => _notificador.Notificar(...);
//      }
//
//    → Agora Pedido depende só do CONTRATO (INotificador).
//      Posso injetar Email, SMS, WhatsApp, Fake... sem mudar Pedido.
//
//  OS 3 ESTILOS DE INJEÇÃO
//  ───────────────────────
//    • Constructor injection (MAIS COMUM e recomendado)
//        → dependências passadas no construtor; obrigatórias
//    • Property injection
//        → setadas via propriedade pública; opcionais
//    • Method injection
//        → passadas só no método que precisa delas
//
//  POR QUE VALE A PENA?
//  ────────────────────
//    1) Testabilidade → injete um "mock"/"fake" no teste unitário
//    2) Flexibilidade → trocar implementação sem alterar quem usa
//    3) Princípio SOLID — D = Dependency Inversion:
//         "dependa de abstrações, não de implementações concretas"
//    4) Single Responsibility → cada classe foca no seu trabalho;
//       não precisa saber CRIAR suas dependências
//
// ============================================================

namespace Polymorphism;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== 1) Polimorfismo com virtual/override ===\n");

        // Note o tipo da variável: Animal (classe-pai), mas o objeto
        // real é Cachorro, Gato, etc. O método chamado é o da CLASSE
        // CONCRETA — isso é polimorfismo de sobrescrita em ação.
        Animal[] zoologico =
        {
            new Cachorro("Rex"),
            new Gato("Mimi"),
            new Vaca("Mimosa"),
            new Animal("Genérico") // a pai também funciona
        };

        foreach (Animal a in zoologico)
        {
            a.Apresentar();   // método comum (não-virtual)
            a.FazerSom();     // virtual → cada filha decide o som
            Console.WriteLine();
        }

        // ────────────────────────────────────────────────────────
        Console.WriteLine("=== 2) Classe ABSTRATA ===\n");

        // Não dá pra fazer: new Forma();  → erro de compilação
        // (classe abstrata não pode ser instanciada diretamente)
        Forma[] formas =
        {
            new Circulo(5),
            new Retangulo(4, 6),
            new Triangulo(3, 8)
        };

        foreach (Forma f in formas)
        {
            // CalcularArea() é abstract → cada filha É OBRIGADA a implementar
            Console.WriteLine($"{f.Nome,-10} → área = {f.CalcularArea():F2}");
        }

        // ────────────────────────────────────────────────────────
        Console.WriteLine("\n=== 3) INTERFACES ===\n");

        // Pato implementa DUAS interfaces: IVoador E INadador.
        // Posso tratar o mesmo objeto por qualquer um dos contratos.
        Pato donald = new("Donald");

        IVoador comoVoador = donald;
        INadador comoNadador = donald;

        comoVoador.Voar();      // só enxerga Voar()
        comoNadador.Nadar();    // só enxerga Nadar()
        donald.Grasnar();       // método próprio do Pato

        // Aviao implementa só IVoador (não nada)
        IVoador aviao = new Aviao("Boeing 747");
        aviao.Voar();

        // Posso ter uma lista de TUDO que voa, sem se importar com o tipo
        Console.WriteLine("\n-- Tudo que voa --");
        IVoador[] coisasQueVoam = { donald, aviao, new Passaro("Bem-te-vi") };
        foreach (IVoador v in coisasQueVoam) v.Voar();

        // ────────────────────────────────────────────────────────
        Console.WriteLine("\n=== 4) Verificação de tipo: is / as ===\n");

        foreach (Animal a in zoologico)
        {
            // "is" → testa o tipo (e opcionalmente faz cast com pattern)
            if (a is Cachorro c)
            {
                c.AbanarRabo(); // método exclusivo de Cachorro
            }

            // "as" → tenta o cast, retorna null se falhar
            Gato? talvezGato = a as Gato;
            talvezGato?.Ronronar();
        }

        // ────────────────────────────────────────────────────────
        Console.WriteLine("\n=== 5) base.Metodo() — reusar a implementação da pai ===\n");

        new GerenteDeVendas("Ana", 5000m, 0.20m).MostrarSalario();

        // ────────────────────────────────────────────────────────
        Console.WriteLine("\n=== 6) DEPENDENCY INJECTION ===\n");

        // Mesma classe Pedido, três comportamentos diferentes —
        // sem alterar UMA LINHA de Pedido. Só troco a implementação
        // de INotificador que passo no construtor.
        var pedidoPorEmail   = new Pedido(123, new EmailNotificador());
        var pedidoPorSms     = new Pedido(124, new SmsNotificador());
        var pedidoSilencioso = new Pedido(125, new FakeNotificador()); // útil em teste

        pedidoPorEmail.Confirmar();
        pedidoPorSms.Confirmar();
        pedidoSilencioso.Confirmar();

        Console.WriteLine($"\nNotificações registradas no fake: {FakeNotificador.Registros.Count}");

        // Bônus: a MESMA classe pode receber VÁRIAS dependências.
        // RelatorioVendas precisa de um repositório E de um logger:
        var relatorio = new RelatorioVendas(
            repo:   new RepositorioEmMemoria(),
            logger: new ConsoleLogger());
        relatorio.Gerar();

        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }
}

// ============================================================
//  1) HIERARQUIA Animal — virtual / override
// ============================================================
class Animal
{
    public string Nome { get; }

    public Animal(string nome) => Nome = nome;

    // Método COMUM (não-virtual) — todas as filhas herdam IGUAL
    public void Apresentar() => Console.WriteLine($"Olá, eu sou {Nome} ({GetType().Name}).");

    // "virtual" → posso ser sobrescrito; tem implementação padrão
    public virtual void FazerSom() => Console.WriteLine($"{Nome} faz um som genérico.");
}

class Cachorro : Animal
{
    public Cachorro(string nome) : base(nome) { } // chama o construtor da pai

    // "override" → estou redefinindo o método virtual da pai
    public override void FazerSom() => Console.WriteLine($"{Nome} late: AU AU!");

    // Método EXCLUSIVO de Cachorro (não existe em Animal)
    public void AbanarRabo() => Console.WriteLine($"{Nome} abana o rabo feliz.");
}

class Gato : Animal
{
    public Gato(string nome) : base(nome) { }
    public override void FazerSom() => Console.WriteLine($"{Nome} mia: Miauuu.");
    public void Ronronar() => Console.WriteLine($"{Nome} ronrona suavemente.");
}

class Vaca : Animal
{
    public Vaca(string nome) : base(nome) { }
    public override void FazerSom() => Console.WriteLine($"{Nome} muge: Muuuu!");
}

// ============================================================
//  2) Classe ABSTRATA — Forma
// ============================================================
//  "abstract" na classe = não pode ser instanciada com "new".
//  "abstract" no método = sem corpo, OBRIGA filhas a implementarem.
abstract class Forma
{
    public abstract string Nome { get; }     // propriedade abstrata
    public abstract double CalcularArea();   // método abstrato

    // Pode ter membros concretos também — a filha herda
    public void Descrever() => Console.WriteLine($"Sou uma forma: {Nome}");
}

class Circulo : Forma
{
    private readonly double _raio;
    public Circulo(double raio) => _raio = raio;

    public override string Nome => "Círculo";
    public override double CalcularArea() => Math.PI * _raio * _raio;
}

class Retangulo : Forma
{
    private readonly double _largura;
    private readonly double _altura;
    public Retangulo(double largura, double altura)
    {
        _largura = largura;
        _altura = altura;
    }

    public override string Nome => "Retângulo";
    public override double CalcularArea() => _largura * _altura;
}

class Triangulo : Forma
{
    private readonly double _base;
    private readonly double _altura;
    public Triangulo(double b, double altura)
    {
        _base = b;
        _altura = altura;
    }

    public override string Nome => "Triângulo";
    public override double CalcularArea() => (_base * _altura) / 2.0;
}

// ============================================================
//  3) INTERFACES — contratos
// ============================================================
//  Interface = "lista de habilidades" que uma classe PROMETE ter.
//  Quem implementa É OBRIGADO a fornecer todos os membros.
interface IVoador
{
    void Voar(); // sem corpo — só a assinatura
}

interface INadador
{
    void Nadar();
}

// Pato implementa as DUAS interfaces (separadas por vírgula).
// Em C# uma classe só pode herdar de UMA classe, mas pode
// implementar QUANTAS interfaces quiser.
class Pato : Animal, IVoador, INadador
{
    public Pato(string nome) : base(nome) { }

    public override void FazerSom() => Console.WriteLine($"{Nome} faz: Quack!");
    public void Voar()   => Console.WriteLine($"{Nome} (pato) bate as asas e voa baixinho.");
    public void Nadar()  => Console.WriteLine($"{Nome} (pato) nada na lagoa.");
    public void Grasnar() => Console.WriteLine($"{Nome} grasna alto.");
}

class Aviao : IVoador // não herda de Animal, e tudo bem
{
    private readonly string _modelo;
    public Aviao(string modelo) => _modelo = modelo;
    public void Voar() => Console.WriteLine($"Avião {_modelo} decola pela pista.");
}

class Passaro : Animal, IVoador
{
    public Passaro(string nome) : base(nome) { }
    public override void FazerSom() => Console.WriteLine($"{Nome} canta.");
    public void Voar() => Console.WriteLine($"{Nome} voa entre as árvores.");
}

// ============================================================
//  5) Usando base.Metodo() para REAPROVEITAR a pai
// ============================================================
class Funcionario
{
    public string Nome { get; }
    public decimal SalarioBase { get; }

    public Funcionario(string nome, decimal salarioBase)
    {
        Nome = nome;
        SalarioBase = salarioBase;
    }

    public virtual void MostrarSalario()
        => Console.WriteLine($"{Nome}: R$ {SalarioBase:N2}");
}

class GerenteDeVendas : Funcionario
{
    public decimal Comissao { get; } // 0.20 = 20%

    public GerenteDeVendas(string nome, decimal salarioBase, decimal comissao)
        : base(nome, salarioBase)
    {
        Comissao = comissao;
    }

    public override void MostrarSalario()
    {
        // Chamo PRIMEIRO o comportamento da pai (mostra o salário base)
        base.MostrarSalario();
        // E ACRESCENTO o que é específico desta classe
        decimal bonus = SalarioBase * Comissao;
        Console.WriteLine($"  + comissão ({Comissao:P0}) = R$ {bonus:N2}");
        Console.WriteLine($"  → total = R$ {SalarioBase + bonus:N2}");
    }
}

// ============================================================
//  6) DEPENDENCY INJECTION — exemplo prático
// ============================================================
//
//  A "abstração" da qual Pedido depende. Pedido NÃO sabe NEM
//  se importa COMO a mensagem é enviada — só que existe um
//  método Notificar(string).
interface INotificador
{
    void Notificar(string mensagem);
}

// Implementações concretas — cada uma faz à sua maneira
class EmailNotificador : INotificador
{
    public void Notificar(string mensagem)
        => Console.WriteLine($"[EMAIL] {mensagem}");
}

class SmsNotificador : INotificador
{
    public void Notificar(string mensagem)
        => Console.WriteLine($"[SMS]   {mensagem}");
}

// Fake/Mock — útil para TESTES. Não envia nada,
// só guarda o que "seria" enviado pra eu verificar depois.
class FakeNotificador : INotificador
{
    public static readonly List<string> Registros = new();

    public void Notificar(string mensagem)
    {
        Registros.Add(mensagem);
        Console.WriteLine($"[FAKE]  (registrado, nada enviado)");
    }
}

// A classe que DEPENDE de INotificador.
// Note: ela só conhece o CONTRATO, nunca um tipo concreto.
class Pedido
{
    private readonly int _numero;
    private readonly INotificador _notificador;

    // Constructor injection: a dependência chega de fora.
    public Pedido(int numero, INotificador notificador)
    {
        _numero = numero;
        _notificador = notificador;
    }

    public void Confirmar()
    {
        // foco no que IMPORTA pra esta classe (a regra de negócio)
        _notificador.Notificar($"Pedido #{_numero} foi confirmado!");
    }
}

// ─── Exemplo com MÚLTIPLAS dependências ────────────────────
//
//  RelatorioVendas precisa de duas coisas: um lugar de onde
//  ler os dados (IRepositorio) e um lugar pra registrar logs
//  (ILogger). As duas chegam pelo construtor.

interface IRepositorio
{
    IEnumerable<string> ObterVendas();
}

interface ILogger
{
    void Log(string mensagem);
}

class RepositorioEmMemoria : IRepositorio
{
    public IEnumerable<string> ObterVendas()
        => new[] { "Notebook R$ 4.500", "Mouse R$ 120", "Teclado R$ 350" };
}

class ConsoleLogger : ILogger
{
    public void Log(string mensagem)
        => Console.WriteLine($"   [LOG {DateTime.Now:HH:mm:ss}] {mensagem}");
}

class RelatorioVendas
{
    private readonly IRepositorio _repo;
    private readonly ILogger _logger;

    public RelatorioVendas(IRepositorio repo, ILogger logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public void Gerar()
    {
        _logger.Log("Iniciando geração do relatório...");
        Console.WriteLine("--- Relatório de Vendas ---");
        foreach (string venda in _repo.ObterVendas())
            Console.WriteLine($"  • {venda}");
        _logger.Log("Relatório concluído.");
    }
}
