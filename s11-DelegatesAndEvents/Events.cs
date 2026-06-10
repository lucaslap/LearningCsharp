// ============================================================
//  C# — Events (Eventos)
// ============================================================
//
//  O QUE E UM EVENTO?
//  ──────────────────
//  Um evento e um delegate com regras de seguranca. Ele implementa o
//  padrao PUBLISHER / SUBSCRIBER (publicador / assinante):
//
//    - O PUBLISHER declara o evento e o "dispara" quando algo acontece.
//    - Os SUBSCRIBERS se inscrevem com += para serem avisados.
//
//  Por que `event` e nao um delegate publico cru?
//    Um delegate publico qualquer um poderia: reatribuir com `=`
//    (apagando todos os outros inscritos) ou invocar de fora.
//    A palavra `event` BLOQUEIA isso: de fora so e permitido += e -=.
//    Quem dispara o evento e somente a propria classe que o declara.
//
//
//  EventHandler / EventHandler<T>
//  ──────────────────────────────
//  A convencao .NET para eventos usa a assinatura:
//
//      void Metodo(object? sender, TEventArgs e)
//
//    - sender -> quem disparou o evento.
//    - e      -> dados do evento (herda de EventArgs).
//
//  Use o delegate pronto `EventHandler<T>` em vez de criar o seu.
//  Para eventos sem dados extras, existe o `EventHandler` simples.
//
//
//  PADRAO DE DISPARO
//  ─────────────────
//  Por convencao, cria-se um metodo protegido `OnXxx(...)` que dispara
//  o evento usando `?.Invoke(this, e)`. O `?.` evita exceção quando
//  ninguem se inscreveu (evento == null).
//
// ============================================================

namespace DelegatesAndEvents;

static class EventsDemo
{
    public static void Executar()
    {
        Console.WriteLine("\n=== 10) Evento: publisher + subscribers ===\n");

        // O publisher: uma conta bancaria que avisa quando o saldo muda.
        ContaBancaria conta = new ContaBancaria("Lucas", 100m);

        // SUBSCRIBERS se inscrevem com +=. Cada um reage do seu jeito,
        // sem a ContaBancaria saber quem sao ou o que fazem.
        conta.SaldoAlterado += EnviarEmail;
        conta.SaldoAlterado += (sender, e) =>
            Console.WriteLine($"   [auditoria] novo saldo: {e.NovoSaldo:C}");

        // Cada operacao DISPARA o evento internamente. Os dois subscribers
        // acima rodam a cada disparo.
        conta.Depositar(50m);    // saldo 100 -> 150
        conta.Sacar(30m);        // saldo 150 -> 120

        Console.WriteLine("\n-- um subscriber se descadastra (-=) --\n");

        conta.SaldoAlterado -= EnviarEmail;   // para de receber e-mail
        conta.Depositar(200m);   // saldo 120 -> 320 (so a auditoria reage)

        Console.WriteLine("\n=== 11) Saque invalido nao dispara o evento ===\n");

        conta.Sacar(99999m);     // recusado: saldo nao muda, evento nao dispara
    }

    // Subscriber nomeado, no formato (object? sender, TEventArgs e).
    static void EnviarEmail(object? sender, SaldoAlteradoEventArgs e)
        => Console.WriteLine($"   [email] Ola! Movimento de {e.Operacao}. Saldo: {e.NovoSaldo:C}");
}

// ------------------------------------------------------------
//  Dados que viajam junto com o evento. Por convencao herda de
//  EventArgs e expoe os dados como propriedades somente-leitura.
// ------------------------------------------------------------
class SaldoAlteradoEventArgs : EventArgs
{
    public decimal NovoSaldo { get; }
    public string Operacao { get; }

    public SaldoAlteradoEventArgs(decimal novoSaldo, string operacao)
    {
        NovoSaldo = novoSaldo;
        Operacao = operacao;
    }
}

// ------------------------------------------------------------
//  PUBLISHER: declara o evento e e o unico que pode dispara-lo.
// ------------------------------------------------------------
class ContaBancaria
{
    public string Titular { get; }
    public decimal Saldo { get; private set; }

    // O evento. De fora, so da pra fazer += e -=. Ninguem reatribui
    // nem dispara este evento a nao ser a propria ContaBancaria.
    public event EventHandler<SaldoAlteradoEventArgs>? SaldoAlterado;

    public ContaBancaria(string titular, decimal saldoInicial)
    {
        Titular = titular;
        Saldo = saldoInicial;
    }

    public void Depositar(decimal valor)
    {
        if (valor <= 0) return;
        Saldo += valor;
        OnSaldoAlterado($"deposito de {valor:C}");
    }

    public void Sacar(decimal valor)
    {
        // Saque invalido: simplesmente nao mexe no saldo nem notifica.
        if (valor <= 0 || valor > Saldo)
        {
            Console.WriteLine($"   [conta] saque de {valor:C} recusado (saldo: {Saldo:C})");
            return;
        }
        Saldo -= valor;
        OnSaldoAlterado($"saque de {valor:C}");
    }

    // Metodo de disparo no padrao On<Evento>. O `?.Invoke` so chama
    // os inscritos se houver pelo menos um (evento != null).
    private void OnSaldoAlterado(string operacao)
    {
        SaldoAlterado?.Invoke(this, new SaldoAlteradoEventArgs(Saldo, operacao));
    }
}
