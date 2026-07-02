// ============================================================
//  4) PRINCIPIOS GERAIS: DRY, KISS, YAGNI e SOLID
// ============================================================
//
//  DRY - Don't Repeat Yourself
//  ───────────────────────────
//  Cada pedaco de conhecimento deve ter uma unica representacao no
//  sistema. Codigo duplicado e dificil de manter: uma correcao precisa
//  ser replicada em todos os lugares, e e facil esquecer um deles.
//  A solucao costuma ser extrair a logica repetida para um metodo.
//
//  KISS - Keep It Simple, Stupid
//  ─────────────────────────────
//  Prefira a solucao mais simples que resolve o problema. Complexidade
//  desnecessaria dificulta a leitura e esconde bugs.
//
//  YAGNI - You Aren't Gonna Need It
//  ────────────────────────────────
//  Nao implemente algo "para o futuro" enquanto nao houver necessidade
//  concreta. Generalizacoes especulativas adicionam codigo que precisa
//  ser mantido sem entregar valor imediato.
//
//  SOLID (introducao)
//  ──────────────────
//  Cinco principios de design orientado a objetos:
//    S - Single Responsibility: uma classe, uma razao para mudar.
//    O - Open/Closed: aberta para extensao, fechada para modificacao.
//    L - Liskov Substitution: subtipos substituem o tipo base sem quebrar.
//    I - Interface Segregation: interfaces pequenas e especificas.
//    D - Dependency Inversion: dependa de abstracoes, nao de implementacoes.
//  Abaixo ilustramos o "S" e o "D", os mais imediatos no dia a dia.
// ============================================================

namespace CleanCodeLesson;

class PrinciplesDemo
{
    public static void Executar()
    {
        Console.WriteLine("=== 4) Principios: DRY, KISS, YAGNI, SOLID ===\n");

        DemonstrarDry();
        DemonstrarSingleResponsibilityEDependencyInversion();

        Console.WriteLine();
    }

    // ---------- DRY ----------
    private static void DemonstrarDry()
    {
        // ANTES: a mesma formula de area do circulo repetida em varios pontos
        // significa varios lugares para corrigir se a regra mudar.
        //   double a1 = 3.14159 * 2 * 2;
        //   double a2 = 3.14159 * 5 * 5;

        // DEPOIS: a regra vive num unico lugar.
        double areaPequena = CalcularAreaDoCirculo(raio: 2);
        double areaGrande = CalcularAreaDoCirculo(raio: 5);

        Console.WriteLine($"  [DRY] Area (raio 2): {areaPequena:F2}");
        Console.WriteLine($"  [DRY] Area (raio 5): {areaGrande:F2}");
    }

    private static double CalcularAreaDoCirculo(double raio) => Math.PI * raio * raio;

    // ---------- SRP + DIP ----------
    private static void DemonstrarSingleResponsibilityEDependencyInversion()
    {
        // O ProcessadorDePedido depende da ABSTRACAO INotificador, nao de uma
        // implementacao concreta (DIP). Trocar o canal de notificacao nao exige
        // alterar o processador (Open/Closed) - basta injetar outra implementacao.
        INotificador porEmail = new NotificadorEmail();
        var processador = new ProcessadorDePedido(porEmail);
        processador.Finalizar("Pedido #1001");

        INotificador porSms = new NotificadorSms();
        var outroProcessador = new ProcessadorDePedido(porSms);
        outroProcessador.Finalizar("Pedido #1002");
    }

    // Abstracao: define O QUE fazer, sem dizer COMO.
    public interface INotificador
    {
        void Notificar(string mensagem);
    }

    // Cada implementacao tem uma unica responsabilidade (SRP).
    public class NotificadorEmail : INotificador
    {
        public void Notificar(string mensagem) =>
            Console.WriteLine($"  [E-mail] {mensagem}");
    }

    public class NotificadorSms : INotificador
    {
        public void Notificar(string mensagem) =>
            Console.WriteLine($"  [SMS] {mensagem}");
    }

    // Responsavel apenas por orquestrar a finalizacao; nao sabe (nem precisa
    // saber) como a notificacao e entregue.
    public class ProcessadorDePedido
    {
        private readonly INotificador _notificador;

        public ProcessadorDePedido(INotificador notificador)
        {
            _notificador = notificador;
        }

        public void Finalizar(string pedido)
        {
            // ... regra de finalizacao do pedido ...
            _notificador.Notificar($"{pedido} finalizado com sucesso.");
        }
    }
}
