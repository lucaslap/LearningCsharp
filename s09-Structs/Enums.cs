// ============================================================
//  C# — Enums (Enumeracoes)
// ============================================================
//
//  O QUE E UM ENUM?
//  ────────────────
//  Um enum e um TIPO DE VALOR (igual struct) que define um
//  conjunto FECHADO de constantes NOMEADAS. Em vez de espalhar
//  numeros magicos pelo codigo (0 = pendente, 1 = pago, 2 = ...),
//  voce nomeia cada caso e o compilador passa a te ajudar.
//
//  Por baixo dos panos, o valor armazenado e um inteiro
//  (int por padrao). O enum so adiciona NOMES e SEGURANCA DE TIPO.
//
//
//  POR QUE USAR?
//  ─────────────
//    1) Legibilidade — `if (status == StatusPedido.Pago)` e muito
//       mais claro do que `if (status == 1)`.
//    2) Seguranca — voce nao pode passar um int qualquer para um
//       parametro do tipo enum (sem cast explicito).
//    3) Autocompletar — o IDE lista todos os valores possiveis.
//    4) Refatoracao — renomear um caso atualiza tudo de uma vez.
//
//
//  SINTAXE BASICA
//  ──────────────
//
//      public enum StatusPedido
//      {
//          Pendente,   // 0  (default)
//          Pago,       // 1
//          Enviado,    // 2
//          Entregue,   // 3
//          Cancelado   // 4
//      }
//
//  Os valores comecam em 0 e incrementam de 1 em 1, mas voce
//  pode atribuir explicitamente:
//
//      public enum CodigoHttp
//      {
//          Ok = 200,
//          Criado = 201,
//          NaoEncontrado = 404,
//          ErroInterno = 500
//      }
//
//
//  TIPO SUBJACENTE
//  ───────────────
//  Por padrao um enum e baseado em int. Voce pode trocar:
//
//      public enum Tamanho : byte { P, M, G, GG }
//
//  Use isso para economizar memoria quando ha muitos valores
//  guardados (em arrays, structs, mensagens de rede, etc).
//
//
//  ENUM "FLAGS" — combinando valores com bits
//  ──────────────────────────────────────────
//  As vezes voce quer COMBINAR varias opcoes ao mesmo tempo:
//  "este usuario pode Ler E Escrever, mas nao Excluir".
//  Para isso existe o atributo [Flags] e potencias de 2:
//
//      [Flags]
//      public enum Permissao
//      {
//          Nenhuma  = 0,
//          Ler      = 1 << 0,  // 1
//          Escrever = 1 << 1,  // 2
//          Excluir  = 1 << 2,  // 4
//          Admin    = Ler | Escrever | Excluir  // 7
//      }
//
//  Operacoes:
//      var p = Permissao.Ler | Permissao.Escrever;   // combinar
//      bool podeLer = p.HasFlag(Permissao.Ler);      // checar
//      p &= ~Permissao.Ler;                          // remover
//
//
//  CONVERSOES
//  ──────────
//    enum → int:   (int)StatusPedido.Pago      → 1
//    int  → enum:  (StatusPedido)2             → StatusPedido.Enviado
//    enum → string:  status.ToString()         → "Pago"
//    string → enum:  Enum.Parse / TryParse
//
//  Atencao: o cast int → enum NAO valida! Voce pode criar um
//  valor que nao existe na lista — o compilador deixa passar:
//
//      var x = (StatusPedido)99;   // compila, mas e lixo
//
//  Para validar use `Enum.IsDefined`.
//
//
//  ENUM EM SWITCH
//  ──────────────
//  Combina muito bem com switch / switch expression:
//
//      string Mensagem(StatusPedido s) => s switch
//      {
//          StatusPedido.Pendente  => "Aguardando pagamento",
//          StatusPedido.Pago      => "Pagamento confirmado",
//          StatusPedido.Enviado   => "A caminho",
//          StatusPedido.Entregue  => "Entregue ao cliente",
//          StatusPedido.Cancelado => "Pedido cancelado",
//          _ => "Status desconhecido"
//      };
//
//
//  QUANDO NAO USAR ENUM
//  ────────────────────
//    • Quando os valores podem MUDAR/CRESCER em runtime
//      (ex: lista de categorias vinda do banco) → use string/id.
//    • Quando cada caso precisa de COMPORTAMENTO proprio
//      (metodos, dados extras) → use classes/polimorfismo
//      ou o padrao "smart enum".
//
// ============================================================

namespace Structs;

// Enums sao normalmente declarados fora de qualquer classe
// (no namespace) para serem reutilizaveis em todo o projeto.

public enum StatusPedido
{
    Pendente,
    Pago,
    Enviado,
    Entregue,
    Cancelado
}

public enum CodigoHttp
{
    Ok = 200,
    Criado = 201,
    NaoEncontrado = 404,
    ErroInterno = 500
}

public enum Tamanho : byte
{
    P, M, G, GG
}

[Flags]
public enum Permissao
{
    Nenhuma  = 0,
    Ler      = 1 << 0,   // 1
    Escrever = 1 << 1,   // 2
    Excluir  = 1 << 2,   // 4
    Admin    = Ler | Escrever | Excluir  // 7
}


public static class EnumsDemo
{
    public static void Executar()
    {
        Console.WriteLine("\n============================================");
        Console.WriteLine("              ENUMS — EXEMPLOS");
        Console.WriteLine("============================================\n");

        Console.WriteLine("=== 1) Enum basico e valor inteiro ===\n");

        StatusPedido s = StatusPedido.Pago;
        Console.WriteLine($"Status: {s}");           // "Pago"
        Console.WriteLine($"Valor inteiro: {(int)s}"); // 1

        // Default de enum e SEMPRE 0 — cuidado: pode nao existir
        // um nome para 0! Por isso e bom ter um caso "Nenhum/Default".
        StatusPedido padrao = default;
        Console.WriteLine($"default(StatusPedido) = {padrao}");  // "Pendente"

        Console.WriteLine("\n=== 2) Enum com valores explicitos (HTTP) ===\n");

        CodigoHttp c = CodigoHttp.NaoEncontrado;
        Console.WriteLine($"{c} = {(int)c}");        // "NaoEncontrado = 404"

        Console.WriteLine("\n=== 3) Tipo subjacente customizado (byte) ===\n");

        Tamanho t = Tamanho.GG;
        Console.WriteLine($"Tamanho.GG ocupa {sizeof(byte)} byte(s)");
        Console.WriteLine($"Valor: {t} = {(byte)t}");

        Console.WriteLine("\n=== 4) Enum em switch expression ===\n");

        foreach (StatusPedido status in Enum.GetValues<StatusPedido>())
        {
            Console.WriteLine($"{status,-10} → {DescreverStatus(status)}");
        }

        Console.WriteLine("\n=== 5) Conversoes string ↔ enum ===\n");

        string entrada = "Enviado";
        if (Enum.TryParse<StatusPedido>(entrada, out var parsed))
            Console.WriteLine($"Parse de \"{entrada}\" deu certo: {parsed}");
        else
            Console.WriteLine($"Nao consegui converter \"{entrada}\"");

        // Validando um cast suspeito (int para enum)
        int valorVindoDeFora = 99;
        StatusPedido suspeito = (StatusPedido)valorVindoDeFora;
        Console.WriteLine($"(StatusPedido)99 = {suspeito}");
        Console.WriteLine($"Esse valor existe no enum? {Enum.IsDefined(suspeito)}");

        Console.WriteLine("\n=== 6) Enum com [Flags] — combinando permissoes ===\n");

        Permissao usuario = Permissao.Ler | Permissao.Escrever;
        Console.WriteLine($"Permissoes do usuario: {usuario}");      // "Ler, Escrever"
        Console.WriteLine($"Valor numerico: {(int)usuario}");        // 3

        Console.WriteLine($"Pode ler?    {usuario.HasFlag(Permissao.Ler)}");
        Console.WriteLine($"Pode excluir? {usuario.HasFlag(Permissao.Excluir)}");

        // Adicionando uma flag
        usuario |= Permissao.Excluir;
        Console.WriteLine($"\nApos adicionar Excluir: {usuario}");

        // Removendo uma flag (AND com o complemento)
        usuario &= ~Permissao.Escrever;
        Console.WriteLine($"Apos remover Escrever: {usuario}");

        // Admin ja vem com todas
        Permissao adm = Permissao.Admin;
        Console.WriteLine($"\nAdmin tem tudo? {adm.HasFlag(Permissao.Ler) && adm.HasFlag(Permissao.Escrever) && adm.HasFlag(Permissao.Excluir)}");

        Console.WriteLine("\n=== 7) Iterando sobre todos os valores ===\n");

        Console.WriteLine("Codigos HTTP cadastrados:");
        foreach (CodigoHttp cod in Enum.GetValues<CodigoHttp>())
        {
            Console.WriteLine($"  {(int)cod,3} {cod}");
        }
    }

    private static string DescreverStatus(StatusPedido s) => s switch
    {
        StatusPedido.Pendente  => "Aguardando pagamento",
        StatusPedido.Pago      => "Pagamento confirmado",
        StatusPedido.Enviado   => "A caminho",
        StatusPedido.Entregue  => "Entregue ao cliente",
        StatusPedido.Cancelado => "Pedido cancelado",
        _ => "Status desconhecido"
    };
}
