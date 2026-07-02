// ============================================================
//  C# — Inheritance (Heranca)
// ============================================================
//
//  O QUE E HERANCA?
//  ────────────────
//  Heranca e o mecanismo que permite uma classe (FILHA / derivada)
//  reaproveitar e ESTENDER os membros de outra classe (PAI / base).
//  E uma das tres "patas" da OOP: encapsulamento, heranca, polimorfismo.
//
//  Pense em uma relacao "E UM" (IS-A):
//    Um Cachorro E UM Animal.
//    Um Carro    E UM Veiculo.
//    Um Gerente  E UM Funcionario.
//
//  Se voce nao consegue dizer "X e um Y" sem soar estranho, provavelmente
//  voce quer COMPOSICAO (X TEM UM Y) em vez de heranca.
//
//
//  SINTAXE BASICA
//  ──────────────
//
//      class Animal { ... }              // classe base (pai)
//      class Cachorro : Animal { ... }   // classe derivada (filha)
//
//  O `:` indica que Cachorro HERDA de Animal. A filha recebe:
//    - todos os campos e propriedades (publicos, internal, protected)
//    - todos os metodos (idem)
//  Construtores NAO sao herdados — sao encadeados (ver ConstructorChaining.cs).
//
//
//  REGRAS-CHAVE DA HERANCA EM C#
//  ─────────────────────────────
//    1) Heranca SIMPLES: uma classe so pode ter UMA classe base direta.
//       (Para "varios pais" use interfaces — ver s08-Polymorphism.)
//    2) TODA classe que voce escreve, no fundo, herda de System.Object.
//       Por isso todo objeto tem ToString(), Equals(), GetHashCode(), GetType().
//    3) `sealed class` IMPEDE que alguem herde dessa classe.
//    4) `static class` nao pode ser base de heranca.
//
//
//  MODIFICADORES DE ACESSO E HERANCA
//  ─────────────────────────────────
//    public     — visivel para todos
//    internal   — visivel dentro do mesmo assembly
//    protected  — visivel para a propria classe E para classes filhas
//    private    — visivel SOMENTE dentro da propria classe
//                 (a filha NAO enxerga, mesmo herdando)
//    protected internal — protected OU internal (uniao)
//    private protected  — protected E internal   (intersecao)
//
//  `protected` e a "porta dos fundos" pensada para heranca: deixa um
//  detalhe interno acessivel a filhas sem expor para o mundo.
//
//
//  HERANCA vs COMPOSICAO — quando usar cada uma?
//  ─────────────────────────────────────────────
//  Heranca acopla MUITO filha e pai. Mudar a pai pode quebrar todas
//  as filhas silenciosamente. Por isso a regra moderna e:
//
//      "Prefira composicao a heranca."
//                              (Gang of Four, 1994)
//
//  Use heranca quando:
//    - existe uma relacao IS-A real e estavel
//    - voce quer reaproveitar comportamento E permitir extensao
//    - faz sentido tratar a filha COMO se fosse a pai (polimorfismo)
//
//  Use composicao quando:
//    - voce so quer reaproveitar uma funcionalidade
//    - "X tem um Y" descreve melhor a relacao
//    - voce quer trocar a dependencia (DI, mocks em teste)
//
//
//  UPCAST e DOWNCAST
//  ─────────────────
//  UPCAST (filha -> pai) e seguro e implicito:
//
//      Animal a = new Cachorro("Rex");   // OK, Cachorro E UM Animal
//
//  DOWNCAST (pai -> filha) precisa de cast explicito e pode falhar:
//
//      Cachorro c = (Cachorro)a;         // OK se 'a' for mesmo um Cachorro
//      if (a is Cachorro c2) { ... }     // jeito seguro (pattern matching)
//      var c3 = a as Cachorro;           // retorna null se nao for
//
//
//  OBSERVACAO
//  ──────────
//  Este arquivo cobre os FUNDAMENTOS. Os sub-topicos estao em arquivos
//  separados (padrao hibrido s09):
//
//    ConstructorChaining.cs  — encadeamento de construtores com `: base(...)`
//    BaseAndHiding.cs        — `base.X` para estender comportamento e `new` para esconder
//
//  Para virtual/override/abstract/interfaces, veja s08-Polymorphism.
//
// ============================================================

namespace Inheritance;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== 1) Heranca basica: campos e metodos herdados ===\n");

        Cachorro rex = new Cachorro("Rex", 5);
        rex.Apresentar();         // metodo herdado de Animal
        rex.Latir();              // metodo proprio de Cachorro
        // Saida: "Ola, sou Rex e tenho 5 anos." + "Au au!"

        Console.WriteLine("\n=== 2) `protected` — classe filha acessa, o resto não ===\n");

        Gato mia = new Gato("Mia", 3);
        mia.DescreverHumor();
        // DescreverHumor usa o campo `protected energia` herdado de Animal.

        Console.WriteLine("\n=== 3) Toda classe herda de System.Object ===\n");

        object obj = rex;             // upcast ate a raiz da hierarquia
        Console.WriteLine(obj.GetType().Name);  // "Cachorro" — o tipo real nao se perde
        Console.WriteLine(obj.ToString());      // usa o ToString herdado/sobrescrito

        Console.WriteLine("\n=== 4) Upcast e downcast ===\n");

        Animal a = new Cachorro("Toto", 2);   // upcast implicito
        // a.Latir();  // ERRO de compilacao — pelo tipo Animal nao da pra ver Latir

        if (a is Cachorro c)                  // downcast seguro
        {
            c.Latir();                        // agora sim
        }

        Animal outro = new Gato("Felix", 4);
        Cachorro? talvez = outro as Cachorro; // 'as' devolve null se nao casar
        Console.WriteLine($"outro como Cachorro? {(talvez is null ? "nao" : "sim")}");

        Console.WriteLine("\n=== 5) `sealed` — barrar novas heranças ===\n");

        Poodle poodle = new Poodle("Bibi", 1);
        poodle.Latir();
        // class XPTO : Poodle { }  // ERRO — Poodle e sealed

        // --------------------------------------------------------
        //  Sub-topicos em arquivos separados (padrao hibrido s09):
        // --------------------------------------------------------

        ConstructorChainingDemo.Executar();
        BaseAndHidingDemo.Executar();

        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }
}

// ============================================================
//  Hierarquia de exemplo: Animal -> Cachorro / Gato / Poodle
// ============================================================

// Classe BASE. Note os modificadores de acesso pensados para heranca:
//   public    — Nome e visivel a todos
//   protected — energia e visivel a filhas, mas nao ao mundo
//   (`private` tambem existe: ficaria visivel SOMENTE dentro de Animal,
//    nem a filha enxergaria. Veja os comentarios do topo do arquivo.)
class Animal
{
    public string Nome { get; }
    public int Idade { get; }
    protected int energia;            // classe filha enxerga

    public Animal(string nome, int idade)
    {
        Nome = nome;
        Idade = idade;
        energia = 100;
    }

    public void Apresentar()
    {
        Console.WriteLine($"Ola, sou {Nome} e tenho {Idade} anos.");
    }

    // ToString herdado de object. Sobrescrever (override) ajuda no Console.WriteLine.
    public override string ToString() => $"{GetType().Name}({Nome})";
}

// Classe DERIVADA. O `: Animal` diz "Cachorro herda de Animal".
// O `: base(nome, idade)` repassa argumentos para o construtor da pai
// (detalhe explorado em ConstructorChaining.cs).
class Cachorro : Animal
{
    public Cachorro(string nome, int idade) : base(nome, idade) { }

    public void Latir()
    {
        Console.WriteLine("Au au!");
    }
}

class Gato : Animal
{
    public Gato(string nome, int idade) : base(nome, idade) { }

    public void DescreverHumor()
    {
        // `energia` so e acessivel aqui porque e protected na pai.
        string humor = energia > 50 ? "tranquilo" : "irritado";
        Console.WriteLine($"{Nome} esta {humor} (energia {energia}).");
    }
}

// `sealed` bloqueia que outra classe herde de Poodle. Util para
// "fechar" um ponto da hierarquia que ja nao deve ser extendido.
sealed class Poodle : Cachorro
{
    public Poodle(string nome, int idade) : base(nome, idade) { }
}
