// ============================================================
//  C# — Structs (Estruturas)
// ============================================================
//
//  O QUE É UM STRUCT?
//  ──────────────────
//  Um struct é um TIPO DE VALOR (value type) que agrupa
//  variaveis relacionadas em uma unica unidade — parecido com
//  uma classe, mas com diferencas fundamentais de comportamento
//  e de armazenamento na memoria.
//
//  Pense em um struct como um "registro leve" para representar
//  algo SIMPLES e IMUTAVEL: um ponto (X, Y), uma cor (R, G, B),
//  uma data, um dinheiro (valor + moeda), uma coordenada GPS.
//
//
//  CLASS vs STRUCT — a diferenca mais importante
//  ─────────────────────────────────────────────
//
//    CLASS (referencia)                STRUCT (valor)
//    ──────────────────                ──────────────
//    • alocada no HEAP                 • alocada na STACK (geralmente)
//    • variavel guarda PONTEIRO        • variavel guarda o DADO em si
//    • copia = mesma referencia        • copia = NOVA copia independente
//    • pode ser null                   • NAO pode ser null (sem '?')
//    • herda de outras classes         • NAO pode herdar de outro struct/class
//    • coletada pelo Garbage Collector • liberada com o escopo (sem GC)
//    • construtor sem parametros OK    • em versoes antigas, sem ctor vazio
//    • default = null                  • default = todos os campos zerados
//
//  Exemplo do efeito de copia:
//
//      PontoStruct a = new PontoStruct(1, 2);
//      PontoStruct b = a;     // COPIA — b e a sao independentes
//      b.X = 99;
//      // a.X continua 1, b.X = 99
//
//      PontoClass  c = new PontoClass(1, 2);
//      PontoClass  d = c;     // REFERENCIA — c e d apontam para o MESMO objeto
//      d.X = 99;
//      // c.X tambem virou 99!
//
//
//  QUANDO USAR STRUCT (regra pratica da Microsoft)
//  ───────────────────────────────────────────────
//    Use struct se TODAS as condicoes forem verdadeiras:
//      1) Representa logicamente UM unico valor (como int, DateTime)
//      2) E pequeno (geralmente <= 16 bytes)
//      3) E IMUTAVEL (nao muda apos criado)
//      4) Nao sera "boxed" frequentemente (convertido para object)
//
//    Em caso de duvida → use CLASS. Structs sao otimizacao,
//    nao a primeira escolha.
//
//
//  EXEMPLOS DE STRUCTS NA BCL (.NET)
//  ─────────────────────────────────
//    int, double, bool, char        — tipos primitivos sao structs!
//    DateTime, TimeSpan, Guid
//    decimal
//    System.Numerics.Vector3
//    KeyValuePair<TKey, TValue>
//
//
//  SINTAXE BASICA
//  ──────────────
//
//      public struct Ponto
//      {
//          public int X;            // campo
//          public int Y;
//
//          public Ponto(int x, int y)   // construtor
//          {
//              X = x;
//              Y = y;
//          }
//
//          public double DistanciaOrigem() =>
//              Math.Sqrt(X * X + Y * Y);
//      }
//
//
//  IMUTABILIDADE — "readonly struct"
//  ─────────────────────────────────
//  Marcar o struct como readonly garante que NENHUM campo pode
//  mudar apos a criacao. Isso evita bugs sutis com copias e
//  permite ao compilador otimizar.
//
//      public readonly struct Moeda
//      {
//          public decimal Valor { get; }
//          public string Codigo { get; }
//          public Moeda(decimal v, string c) { Valor = v; Codigo = c; }
//      }
//
//
//  BOXING e UNBOXING — o "pecado" dos structs
//  ──────────────────────────────────────────
//  Quando um struct e convertido para `object` (ou para uma
//  interface que ele implementa), o runtime copia o valor para
//  o HEAP — isso e BOXING. Voltar para o struct e UNBOXING.
//  Ambos sao LENTOS e geram lixo para o GC.
//
//      int n = 42;
//      object o = n;      // boxing  (n e copiado para o heap)
//      int m = (int)o;    // unboxing
//
//  Por isso: evite usar structs em colecoes nao-genericas (ArrayList)
//  ou em comparacoes via interfaces sem cuidado.
//
//
//  ENUM — primo do struct
//  ──────────────────────
//  enum tambem e um tipo de valor, usado para nomear constantes
//  inteiras. Esta fora deste arquivo mas vale lembrar que existe.
//
// ============================================================

namespace Structs;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== 1) Struct basico: Ponto ===\n");

        Ponto p1 = new Ponto(3, 4);
        Console.WriteLine($"p1 = ({p1.X}, {p1.Y})");
        Console.WriteLine($"Distancia da origem: {p1.DistanciaOrigem()}");
        // Saida esperada: 5  (triangulo 3-4-5)

        Console.WriteLine("\n=== 2) Semantica de COPIA (a grande diferenca) ===\n");

        Ponto a = new Ponto(1, 2);
        Ponto b = a;          // copia: b e independente de a
        b.X = 99;

        Console.WriteLine($"a = ({a.X}, {a.Y})");  // ainda (1, 2)
        Console.WriteLine($"b = ({b.X}, {b.Y})");  // (99, 2)
        // Se Ponto fosse class, a.X tambem teria virado 99.

        Console.WriteLine("\n=== 3) default(struct) — todos os campos zerados ===\n");

        Ponto zero = default;     // X = 0, Y = 0
        Console.WriteLine($"default(Ponto) = ({zero.X}, {zero.Y})");

        Console.WriteLine("\n=== 4) readonly struct — Moeda imutavel ===\n");

        Moeda preco = new Moeda(199.90m, "BRL");
        Console.WriteLine($"Preco: {preco}");
        // preco.Valor = 0;   // ERRO de compilacao — readonly

        // Como mudar? Crie um NOVO struct (estilo "with"):
        Moeda comDesconto = preco with { Valor = 149.90m };
        Console.WriteLine($"Com desconto: {comDesconto}");

        Console.WriteLine("\n=== 5) Struct como parametro — passagem por valor ===\n");

        Ponto original = new Ponto(10, 20);
        Console.WriteLine($"Antes:  ({original.X}, {original.Y})");

        TentarMover(original);    // recebe COPIA — nao afeta o original
        Console.WriteLine($"Depois de TentarMover:  ({original.X}, {original.Y})");

        MoverPorRef(ref original); // passa REFERENCIA — agora altera
        Console.WriteLine($"Depois de MoverPorRef: ({original.X}, {original.Y})");

        Console.WriteLine("\n=== 6) Struct implementando interface ===\n");

        IDescritivel d = new Moeda(50m, "USD"); // cuidado: BOXING aqui
        Console.WriteLine(d.Descrever());

        Console.WriteLine("\n=== 7) Igualdade de structs ===\n");

        // Em record struct, == ja compara campo-a-campo automaticamente.
        Moeda m1 = new Moeda(10m, "BRL");
        Moeda m2 = new Moeda(10m, "BRL");
        Console.WriteLine($"m1 == m2? {m1 == m2}");  // true (record struct)
        Console.WriteLine($"m1.Equals(m2)? {m1.Equals(m2)}");

        Console.WriteLine("\n=== 8) Exemplo pratico: agregando structs ===\n");

        Retangulo r = new Retangulo(new Ponto(0, 0), new Ponto(5, 3));
        Console.WriteLine($"Area do retangulo: {r.Area()}");
        // Saida esperada: 15

        // Enums — primo do struct (tambem value type).
        // Exemplos em Enums.cs.
        EnumsDemo.Executar();

        // DateTime — struct da BCL para data e hora.
        // Exemplos em DateTimeDemo.cs.
        DateTimeDemo.Executar();

        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }

    // Recebe POR VALOR — qualquer alteracao se perde ao retornar.
    static void TentarMover(Ponto p)
    {
        p.X += 100;
        p.Y += 100;
    }

    // 'ref' faz passar a REFERENCIA do struct — agora altera de verdade.
    static void MoverPorRef(ref Ponto p)
    {
        p.X += 100;
        p.Y += 100;
    }
}


// ────────────────────────────────────────────────────────────
//  Struct simples e MUTAVEL (didatico — em codigo real prefira
//  readonly struct).
// ────────────────────────────────────────────────────────────
public struct Ponto
{
    public int X;
    public int Y;

    public Ponto(int x, int y)
    {
        X = x;
        Y = y;
    }

    public double DistanciaOrigem() => Math.Sqrt(X * X + Y * Y);
}


// ────────────────────────────────────────────────────────────
//  'record struct' = struct + igualdade por valor + 'with'
//  'readonly' = imutavel (nenhum campo muda apos criado)
//  Implementa IDescritivel para mostrar boxing na pratica.
// ────────────────────────────────────────────────────────────
public readonly record struct Moeda(decimal Valor, string Codigo) : IDescritivel
{
    public string Descrever() => $"{Codigo} {Valor:N2}";

    // ToString customizado para o exemplo 4.
    public override string ToString() => Descrever();
}


public interface IDescritivel
{
    string Descrever();
}


// ────────────────────────────────────────────────────────────
//  Struct composto por outros structs — sem alocacao no heap.
//  Toda a estrutura mora "junta" na pilha (ou inline no objeto
//  que a contem).
// ────────────────────────────────────────────────────────────
public readonly struct Retangulo
{
    public Ponto CantoInferior { get; }
    public Ponto CantoSuperior { get; }

    public Retangulo(Ponto inferior, Ponto superior)
    {
        CantoInferior = inferior;
        CantoSuperior = superior;
    }

    public int Largura => CantoSuperior.X - CantoInferior.X;
    public int Altura  => CantoSuperior.Y - CantoInferior.Y;
    public int Area()  => Largura * Altura;
}
