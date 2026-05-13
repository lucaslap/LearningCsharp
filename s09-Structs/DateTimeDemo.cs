// ============================================================
//  C# — DateTime, TimeSpan e amigos
// ============================================================
//
//  CONTEXTO
//  ────────
//  DateTime e um STRUCT da BCL (mora em System) que representa
//  um instante no tempo — data e hora juntas. Por ser struct,
//  segue todas as regras vistas nos exemplos anteriores:
//  valor, copia, imutavel, sem null (sem '?').
//
//  Junto dele andam outros tipos relacionados:
//
//    DateTime         — data + hora (com "kind": Local/Utc/Unspecified)
//    DateTimeOffset   — data + hora + offset de fuso (recomendado em APIs)
//    TimeSpan         — DURACAO (diferenca entre dois instantes)
//    DateOnly         — so a data, sem horas (C# 10+)
//    TimeOnly         — so o horario do dia (C# 10+)
//
//
//  IMUTABILIDADE
//  ─────────────
//  TODOS os metodos que "alteram" um DateTime na verdade
//  RETORNAM um novo DateTime. O original nunca muda:
//
//      DateTime d = new DateTime(2026, 1, 1);
//      d.AddDays(5);              // sem efeito! valor retornado e descartado
//      DateTime d2 = d.AddDays(5); // jeito certo
//
//
//  CRIANDO UM DateTime
//  ───────────────────
//
//      new DateTime(2026, 5, 13)               // ano, mes, dia
//      new DateTime(2026, 5, 13, 14, 30, 0)    // + hora, minuto, segundo
//      DateTime.Now                            // agora, fuso LOCAL
//      DateTime.UtcNow                         // agora, em UTC
//      DateTime.Today                          // hoje 00:00:00, fuso local
//      DateTime.MinValue / MaxValue            // limites do tipo
//
//
//  Now vs UtcNow — QUAL USAR?
//  ──────────────────────────
//  Regra geral: ARMAZENE e TRANSMITA em UTC. Converta para o
//  fuso do usuario apenas na hora de exibir. Isso evita bugs
//  cabeludos com horario de verao e servidores em fusos diferentes.
//
//      var agoraUtc   = DateTime.UtcNow;
//      var agoraLocal = agoraUtc.ToLocalTime();
//
//  Em APIs publicas, prefira DateTimeOffset — ele guarda o offset
//  junto, entao nunca ha ambiguidade.
//
//
//  PARSE — string → DateTime
//  ─────────────────────────
//
//      DateTime.Parse("2026-05-13")            // joga excecao se invalido
//      DateTime.TryParse("13/05/2026", out var dt)  // retorna bool, sem excecao
//      DateTime.ParseExact("13/05/2026", "dd/MM/yyyy", CultureInfo.InvariantCulture)
//
//  TryParse e quase sempre a escolha certa: entrada do usuario
//  pode falhar e voce nao quer try/catch para fluxo normal.
//
//
//  FORMATACAO — DateTime → string
//  ──────────────────────────────
//  Use ToString com um codigo. Os principais:
//
//      "d"  → 13/05/2026          (data curta, depende da cultura)
//      "D"  → quarta-feira, 13 de maio de 2026  (data longa)
//      "t"  → 14:30                (hora curta)
//      "T"  → 14:30:00             (hora longa)
//      "g"  → 13/05/2026 14:30     (geral curto)
//      "o"  → 2026-05-13T14:30:00.0000000  (ISO 8601 — bom para serializar)
//      "yyyy-MM-dd HH:mm"          (formato customizado)
//
//  Cuidado: maiusculas/minusculas IMPORTAM. "MM" = mes, "mm" = minutos.
//
//
//  TimeSpan — duracoes
//  ───────────────────
//  Subtrair dois DateTime retorna um TimeSpan. Tambem da pra
//  construir TimeSpans diretamente:
//
//      TimeSpan dia = TimeSpan.FromDays(1);
//      TimeSpan meiaHora = TimeSpan.FromMinutes(30);
//      TimeSpan diff = fim - inicio;
//      Console.WriteLine(diff.TotalHours);
//
//
//  ARITMETICA
//  ──────────
//      DateTime + TimeSpan  → DateTime
//      DateTime - DateTime  → TimeSpan
//      DateTime - TimeSpan  → DateTime
//      d.AddYears(1), AddMonths(2), AddDays(7), AddHours(3)...
//
//
//  PEGADINHAS COMUNS
//  ─────────────────
//    • DateTime nao pode ser null (use DateTime? se precisar).
//    • DateTime.MinValue (01/01/0001) e frequentemente usado como
//      "nao preenchido" — prefira DateTime? por clareza.
//    • Cuidado com cultura ao fazer Parse: "01/02/2026" pode ser
//      jan/02 ou fev/01 dependendo da regiao. Use ParseExact ou
//      ISO 8601 ("yyyy-MM-dd") em sistemas.
//    • Comparar com == compara o "tick" mais o Kind — dois DateTime
//      com mesmo instante mas Kind diferentes nao sao iguais.
//
// ============================================================

using System.Globalization;

namespace Structs;

public static class DateTimeDemo
{
    public static void Executar()
    {
        Console.WriteLine("\n============================================");
        Console.WriteLine("          DATETIME — EXEMPLOS");
        Console.WriteLine("============================================\n");

        Console.WriteLine("=== 1) Criando DateTimes ===\n");

        DateTime agora      = DateTime.Now;
        DateTime agoraUtc   = DateTime.UtcNow;
        DateTime hoje       = DateTime.Today;          // 00:00:00 de hoje
        DateTime aniversario = new DateTime(1995, 7, 20, 8, 30, 0);

        Console.WriteLine($"Now        = {agora}");
        Console.WriteLine($"UtcNow     = {agoraUtc}");
        Console.WriteLine($"Today      = {hoje}");
        Console.WriteLine($"Aniversario = {aniversario}");

        Console.WriteLine($"\nKind de Now: {agora.Kind}");        // Local
        Console.WriteLine($"Kind de UtcNow: {agoraUtc.Kind}");    // Utc

        Console.WriteLine("\n=== 2) Imutabilidade — AddDays retorna NOVO valor ===\n");

        DateTime d = new DateTime(2026, 1, 1);
        d.AddDays(5);   // SEM EFEITO — valor retornado e descartado
        Console.WriteLine($"Apos d.AddDays(5) ignorado: {d:d}");  // 01/01/2026

        DateTime d2 = d.AddDays(5);
        Console.WriteLine($"d2 = d.AddDays(5): {d2:d}");          // 06/01/2026
        Console.WriteLine($"d original intacto: {d:d}");

        Console.WriteLine("\n=== 3) Componentes individuais ===\n");

        Console.WriteLine($"Ano: {agora.Year}");
        Console.WriteLine($"Mes: {agora.Month}");
        Console.WriteLine($"Dia: {agora.Day}");
        Console.WriteLine($"Hora: {agora.Hour}");
        Console.WriteLine($"Dia da semana: {agora.DayOfWeek}");
        Console.WriteLine($"Dia do ano: {agora.DayOfYear}");

        Console.WriteLine("\n=== 4) Aritmetica com datas ===\n");

        DateTime inicio = new DateTime(2026, 5, 13, 9, 0, 0);
        DateTime fim    = new DateTime(2026, 5, 13, 17, 30, 0);
        TimeSpan jornada = fim - inicio;

        Console.WriteLine($"Jornada: {jornada}");                 // 08:30:00
        Console.WriteLine($"Em horas: {jornada.TotalHours}");
        Console.WriteLine($"Em minutos: {jornada.TotalMinutes}");

        DateTime entrega = inicio.AddDays(3).AddHours(2);
        Console.WriteLine($"Entrega: {entrega}");

        // Idade aproximada (cuidado: nao trata bem 29/02!)
        int idade = CalcularIdade(new DateTime(1995, 7, 20));
        Console.WriteLine($"Idade aproximada: {idade} anos");

        Console.WriteLine("\n=== 5) Formatacao (ToString com codigos) ===\n");

        DateTime amostra = new DateTime(2026, 5, 13, 14, 30, 45);

        Console.WriteLine($"d (curta):       {amostra:d}");
        Console.WriteLine($"D (longa):       {amostra:D}");
        Console.WriteLine($"t (hora curta):  {amostra:t}");
        Console.WriteLine($"T (hora longa):  {amostra:T}");
        Console.WriteLine($"g (geral):       {amostra:g}");
        Console.WriteLine($"o (ISO 8601):    {amostra:o}");
        Console.WriteLine($"custom:          {amostra:dd/MM/yyyy HH:mm}");

        Console.WriteLine("\n=== 6) Parse e TryParse ===\n");

        // TryParse — sem excecao, retorna bool
        if (DateTime.TryParse("2026-05-13 10:00", out var dt1))
            Console.WriteLine($"OK: {dt1}");

        // ParseExact — voce diz o formato exato (mais seguro)
        DateTime dt2 = DateTime.ParseExact(
            "13/05/2026",
            "dd/MM/yyyy",
            CultureInfo.InvariantCulture);
        Console.WriteLine($"ParseExact: {dt2:D}");

        // Falha controlada
        if (!DateTime.TryParse("texto qualquer", out _))
            Console.WriteLine("\"texto qualquer\" nao e uma data valida.");

        Console.WriteLine("\n=== 7) UTC ↔ Local ===\n");

        DateTime utc = DateTime.UtcNow;
        DateTime local = utc.ToLocalTime();
        Console.WriteLine($"UTC:   {utc:o}");
        Console.WriteLine($"Local: {local:o}");

        Console.WriteLine("\n=== 8) DateOnly e TimeOnly (C# 10+) ===\n");

        DateOnly nascimento = new DateOnly(1995, 7, 20);
        TimeOnly abertura   = new TimeOnly(9, 0);
        TimeOnly fechamento = new TimeOnly(18, 0);

        Console.WriteLine($"Nascimento: {nascimento}");
        Console.WriteLine($"Funcionamento: {abertura} as {fechamento}");
        Console.WriteLine($"Duracao do expediente: {fechamento - abertura}");

        Console.WriteLine("\n=== 9) DateTimeOffset — datas com fuso ===\n");

        // Recomendado em APIs/banco: guarda o offset junto.
        DateTimeOffset evento = new DateTimeOffset(2026, 5, 13, 14, 0, 0,
            TimeSpan.FromHours(-3));  // Brasilia
        Console.WriteLine($"Evento: {evento}");
        Console.WriteLine($"Em UTC: {evento.UtcDateTime:o}");

        Console.WriteLine("\n=== 10) Comparacao ===\n");

        DateTime a = new DateTime(2026, 5, 13);
        DateTime b = new DateTime(2026, 5, 14);

        Console.WriteLine($"a < b? {a < b}");
        Console.WriteLine($"a == b? {a == b}");
        Console.WriteLine($"Compare: {DateTime.Compare(a, b)}");  // -1, 0 ou 1

        if (DateTime.Today.DayOfWeek == DayOfWeek.Friday)
            Console.WriteLine("Hoje e sexta!");
        else
            Console.WriteLine($"Hoje e {DateTime.Today.DayOfWeek}.");
    }

    private static int CalcularIdade(DateTime nascimento)
    {
        DateTime hoje = DateTime.Today;
        int idade = hoje.Year - nascimento.Year;
        if (nascimento.Date > hoje.AddYears(-idade))
            idade--;
        return idade;
    }
}
