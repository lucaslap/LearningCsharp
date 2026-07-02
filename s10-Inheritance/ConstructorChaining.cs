// ============================================================
//  C# — Encadeamento de construtores (`: base(...)`)
// ============================================================
//
//  REGRA DE OURO
//  ─────────────
//  Construtores NAO sao herdados. Toda classe filha precisa declarar
//  os proprios construtores. Antes do corpo da filha rodar, o
//  construtor da PAI precisa rodar primeiro — sempre.
//
//  Se voce nao escolher qual construtor da pai chamar, o compilador
//  chama implicitamente o construtor SEM PARAMETROS da pai. Se a
//  pai nao tiver um, voce e obrigado a usar `: base(...)` explicito.
//
//
//  SINTAXE
//  ───────
//
//      class Pai
//      {
//          public Pai(string nome) { ... }
//      }
//
//      class Filha : Pai
//      {
//          public Filha(string nome, int x) : base(nome)   // chama Pai(nome)
//          {
//              // corpo da Filha so roda APOS o construtor da Pai
//          }
//      }
//
//
//  ORDEM DE EXECUCAO
//  ─────────────────
//  Para `new Filha(...)`, a ordem real e:
//
//    1) Inicializadores de campo da Filha (ex: `private int x = 10;`)
//    2) Construtor da PAI (que, por sua vez, executa 1+2 dele primeiro)
//    3) Corpo do construtor da Filha
//
//  Isso garante que, dentro do construtor da pai, os campos da
//  pai ja existem — mas os da filha ainda nao foram tocados pelo
//  construtor da filha.
//
//
//  `: this(...)` — encadear DENTRO da mesma classe
//  ───────────────────────────────────────────────
//  Use `: this(...)` para um construtor chamar OUTRO da mesma classe.
//  Util para evitar codigo duplicado entre overloads de construtor:
//
//      public Pessoa(string nome) : this(nome, 0) { }
//      public Pessoa(string nome, int idade) { ... }
//
// ============================================================

namespace Inheritance;

static class ConstructorChainingDemo
{
    public static void Executar()
    {
        Console.WriteLine("\n=== Encadeamento de construtores ===\n");

        Console.WriteLine("-- new Gerente(\"Ana\", 8000, \"Vendas\") --");
        Gerente g = new Gerente("Ana", 8000m, "Vendas");
        Console.WriteLine($"Resultado: {g.Resumo()}");
        // Ordem esperada na saida:
        //   "Funcionario(nome): inicio"
        //   "Funcionario(nome, salario): inicio"
        //   "Funcionario(nome, salario): fim"
        //   "Gerente(nome, salario, depto): inicio"
        //   "Gerente(nome, salario, depto): fim"

        Console.WriteLine("\n-- new Funcionario(\"Bob\") (so um arg) --");
        Funcionario f = new Funcionario("Bob");
        Console.WriteLine($"Resultado: {f.Resumo()}");
        // Mostra que `: this(...)` evita repetir logica entre construtores
        // da MESMA classe.
    }
}

class Funcionario
{
    public string Nome { get; }
    public decimal Salario { get; }

    // Construtor 1: recebe so o nome. Repassa para o construtor 2 da
    // PROPRIA classe usando `: this(...)`, com salario default.
    public Funcionario(string nome) : this(nome, 0m)
    {
        Console.WriteLine("Funcionario(nome): inicio");
        // O corpo deste construtor so roda APOS o construtor 2 terminar.
    }

    // Construtor 2: o "completo". E ele que realmente atribui os campos.
    public Funcionario(string nome, decimal salario)
    {
        Console.WriteLine("Funcionario(nome, salario): inicio");
        Nome = nome;
        Salario = salario;
        Console.WriteLine("Funcionario(nome, salario): fim");
    }

    public virtual string Resumo() => $"{Nome} — salario {Salario:C}";
}

class Gerente : Funcionario
{
    public string Departamento { get; }

    // `: base(nome, salario)` escolhe explicitamente qual construtor
    // da PAI chamar. Sem isso, o compilador tentaria chamar um
    // Funcionario() sem parametros — que nao existe — e daria erro.
    public Gerente(string nome, decimal salario, string departamento)
        : base(nome, salario)
    {
        Console.WriteLine("Gerente(nome, salario, depto): inicio");
        Departamento = departamento;
        Console.WriteLine("Gerente(nome, salario, depto): fim");
    }

    public override string Resumo()
        => $"{base.Resumo()} (gerente de {Departamento})";
}
