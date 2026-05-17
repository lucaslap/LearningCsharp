// ============================================================
//  C# — `base.X` (estender) vs `new` (esconder)
// ============================================================
//
//  CONTEXTO
//  ────────
//  Quando uma filha quer reagir a um metodo da pai, ela tem 3 caminhos:
//
//    1) USAR como esta:           herda e pronto, nao precisa redeclarar.
//    2) ESTENDER o comportamento: declara `override` e chama `base.Metodo()`.
//    3) ESCONDER (hide) o membro: declara com `new` — assinatura igual,
//       mas e um metodo SEPARADO. Sem polimorfismo.
//
//  Este arquivo cobre os caminhos (2) e (3). Para fundamentos de
//  virtual/override (caminho 2 puro), veja s08-Polymorphism.
//
//
//  base.Metodo() — REUSAR o que a pai faz
//  ──────────────────────────────────────
//  Em uma override, `base.X()` chama a versao da PAI do membro X.
//  Padrao classico:
//
//      public override void Salvar()
//      {
//          base.Salvar();              // faz o que a pai ja faz
//          Console.WriteLine("...");   // adiciona algo a mais
//      }
//
//  Isso evita reimplementar do zero e respeita o contrato da pai.
//
//
//  `new` — ESCONDER um membro herdado
//  ──────────────────────────────────
//  Se voce declara na filha um membro com a MESMA assinatura de um
//  da pai, sem `override`, o compilador avisa:
//
//      "warning CS0108: ... use the new keyword if hiding was intended"
//
//  Adicionar `new` silencia o aviso e declara: "estou escondendo, nao
//  sobrescrevendo". A diferenca pratica e enorme:
//
//      Pai p = new Filha();
//      p.Metodo();      // override -> chama o da Filha (polimorfismo)
//                       // new      -> chama o da Pai   (sem polimorfismo!)
//
//  Quando usar `new`? Quase NUNCA. Casos legitimos:
//    - voce nao controla a pai (vem de uma lib) e precisa de uma versao
//      diferente para chamadas que ja sao do tipo Filha
//    - voce quer trocar o TIPO de retorno em uma propriedade especifica
//
//  Em codigo novo, prefira override + virtual/abstract na pai.
//
// ============================================================

namespace Inheritance;

static class BaseAndHidingDemo
{
    public static void Executar()
    {
        Console.WriteLine("\n=== base.X — estendendo o comportamento da pai ===\n");

        DocumentoAuditado doc = new DocumentoAuditado("contrato.pdf", "Ana");
        doc.Salvar();
        // Saida esperada:
        //   "Documento 'contrato.pdf' salvo."        (vem do base.Salvar())
        //   "Auditoria: salvo por Ana em <data>."   (extensao da filha)

        Console.WriteLine("\n=== `new` — escondendo um membro (cuidado) ===\n");

        Logger normal = new Logger();
        LoggerColorido colorido = new LoggerColorido();

        normal.Log("oi do normal");
        colorido.Log("oi do colorido");

        // O detalhe importante: pelo tipo da PAI, qual versao roda?
        Logger comoPai = colorido;          // upcast
        comoPai.Log("chamado pelo tipo Logger");
        // Como LoggerColorido usa `new` (nao override), a chamada acima
        // executa Logger.Log — NAO a versao colorida. Isso surpreende
        // muita gente e e a principal razao para evitar `new` em metodos.

        Console.WriteLine("\n-- Compare com override (Documento) --");
        Documento docComoPai = doc;          // upcast
        docComoPai.Salvar();
        // Aqui, como DocumentoAuditado usa OVERRIDE, mesmo chamando pelo
        // tipo Documento o runtime executa a versao da Filha + base.Salvar().
    }
}

// ──────────── Exemplo 1: base.X para ESTENDER ────────────

class Documento
{
    public string NomeArquivo { get; }

    public Documento(string nomeArquivo)
    {
        NomeArquivo = nomeArquivo;
    }

    // `virtual` permite que filhas sobrescrevam.
    public virtual void Salvar()
    {
        Console.WriteLine($"Documento '{NomeArquivo}' salvo.");
    }
}

class DocumentoAuditado : Documento
{
    public string Usuario { get; }

    public DocumentoAuditado(string nomeArquivo, string usuario)
        : base(nomeArquivo)
    {
        Usuario = usuario;
    }

    // Override que CHAMA base.Salvar() antes de adicionar a auditoria.
    // Padrao "fazer o que a pai faz + algo a mais".
    public override void Salvar()
    {
        base.Salvar();
        Console.WriteLine($"Auditoria: salvo por {Usuario} em {DateTime.Now:dd/MM HH:mm}.");
    }
}

// ──────────── Exemplo 2: `new` para ESCONDER ────────────

class Logger
{
    // Note: NAO e virtual. Por isso a filha nao pode usar override.
    public void Log(string mensagem)
    {
        Console.WriteLine($"[LOG] {mensagem}");
    }
}

class LoggerColorido : Logger
{
    // `new` declara: "estou ESCONDENDO o Log da pai, nao sobrescrevendo".
    // Sem o `new`, sairia warning CS0108.
    public new void Log(string mensagem)
    {
        var antes = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[LOG-COLORIDO] {mensagem}");
        Console.ForegroundColor = antes;
    }
}
