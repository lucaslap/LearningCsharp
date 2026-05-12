// ============================================================
//  C# — Tratamento de Erros (Error Handling / Exceptions)
// ============================================================
//
//  Em uma frase: EXCEÇÕES sinalizam que algo INESPERADO aconteceu
//  durante a execução, e o programa precisa decidir como reagir
//  (mostrar mensagem, tentar de novo, encerrar com elegância...).
//
//  ERRO vs EXCEÇÃO
//  ───────────────
//    • Erro de compilação → o código nem chega a rodar
//        (faltou ponto-e-vírgula, tipo errado, nome inexistente)
//    • Exceção em tempo de execução → o código compilou, mas
//        algo deu errado AGORA: dividir por zero, abrir arquivo
//        que não existe, converter "abc" em número, etc.
//
//  ANATOMIA DE UM try/catch/finally
//  ────────────────────────────────
//
//      try
//      {
//          // código que PODE lançar uma exceção
//      }
//      catch (FormatException ex)
//      {
//          // executa SE uma FormatException for lançada
//      }
//      catch (Exception ex)
//      {
//          // captura qualquer outra exceção (rede de segurança)
//      }
//      finally
//      {
//          // SEMPRE executa, com ou sem exceção
//          // (ótimo para fechar arquivos, conexões, etc.)
//      }
//
//  HIERARQUIA DE EXCEÇÕES (todas herdam de System.Exception)
//  ─────────────────────────────────────────────────────────
//      Exception
//        ├─ SystemException
//        │    ├─ ArithmeticException
//        │    │    ├─ DivideByZeroException
//        │    │    └─ OverflowException
//        │    ├─ ArgumentException
//        │    │    ├─ ArgumentNullException
//        │    │    └─ ArgumentOutOfRangeException
//        │    ├─ FormatException
//        │    ├─ IndexOutOfRangeException
//        │    ├─ InvalidOperationException
//        │    ├─ NullReferenceException
//        │    └─ IO.IOException
//        │         └─ IO.FileNotFoundException
//        └─ ApplicationException  (base sugerida para suas próprias)
//
//  REGRA DE OURO: capture do MAIS específico para o MAIS genérico.
//  Se 'catch (Exception)' vier ANTES de 'catch (FormatException)',
//  o compilador acusa erro — o catch genérico capturaria tudo.
//
// ============================================================

class Program
{
    static void Main()
    {
        Console.WriteLine("=== TRATAMENTO DE ERROS EM C# ===\n");

        // ============================================================
        //  1. try / catch BÁSICO
        // ============================================================
        Console.WriteLine("--- 1. try / catch básico ---");

        try
        {
            int a = 10;
            int b = 0;
            int resultado = a / b;            // lança DivideByZeroException
            Console.WriteLine($"Resultado: {resultado}"); // nunca executa
        }
        catch (DivideByZeroException ex)
        {
            // 'ex' é o objeto da exceção. As propriedades mais úteis são:
            //   • Message    → descrição amigável
            //   • StackTrace → caminho da pilha de chamadas
            //   • InnerException → exceção original (quando há "embrulho")
            Console.WriteLine($"Erro: {ex.Message}");
        }

        Console.WriteLine("Programa continuou normalmente após o erro.\n");

        // ============================================================
        //  2. MÚLTIPLOS catch — do mais específico ao mais genérico
        // ============================================================
        Console.WriteLine("--- 2. Múltiplos catch ---");

        string[] entradas = { "42", "abc", null! };
        foreach (string entrada in entradas)
        {
            try
            {
                // int.Parse pode lançar:
                //   • FormatException        → string sem formato numérico
                //   • ArgumentNullException  → string é null
                //   • OverflowException      → número maior que int.MaxValue
                int valor = int.Parse(entrada);
                Console.WriteLine($"'{entrada}' → {valor}");
            }
            catch (FormatException)
            {
                Console.WriteLine($"'{entrada}' → não é um número válido");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("(null)  → entrada nula");
            }
            catch (Exception ex)
            {
                // Rede de segurança: pega qualquer coisa que escapou.
                Console.WriteLine($"Erro inesperado: {ex.GetType().Name} — {ex.Message}");
            }
        }
        Console.WriteLine();

        // ============================================================
        //  3. finally — código que SEMPRE roda
        // ============================================================
        Console.WriteLine("--- 3. finally ---");

        // 'finally' é executado quer dê erro ou não, quer haja 'return'
        // no meio do try. Ideal para liberar recursos (arquivos, conexões,
        // locks). Veja também a seção 6 (using) que automatiza isso.
        try
        {
            Console.WriteLine("Abrindo recurso...");
            throw new InvalidOperationException("Algo deu errado no meio do trabalho");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Capturado: {ex.Message}");
        }
        finally
        {
            // Sempre fecha o recurso, com ou sem erro.
            Console.WriteLine("Fechando recurso (finally).\n");
        }

        // ============================================================
        //  4. LANÇANDO exceções (throw)
        // ============================================================
        Console.WriteLine("--- 4. throw — lançando exceções ---");

        try
        {
            ValidarIdade(-5);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // nameof(idade) vira a string "idade" em tempo de compilação:
            // se renomear o parâmetro, a mensagem acompanha — sem typo.
            Console.WriteLine($"Validação falhou: {ex.Message}");
        }

        // throw também pode ser uma EXPRESSÃO (C# 7+):
        //   string nome = entrada ?? throw new ArgumentNullException(...);
        string? entradaOpcional = null;
        try
        {
            string nome = entradaOpcional ?? throw new ArgumentNullException(nameof(entradaOpcional));
            Console.WriteLine($"Nome: {nome}");
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Nome obrigatório ausente ({ex.ParamName})\n");
        }

        // ============================================================
        //  5. RELANÇAR exceções — 'throw' vs 'throw ex'
        // ============================================================
        Console.WriteLine("--- 5. Relançar — throw vs throw ex ---");

        // Quando você captura uma exceção mas quer que o chamador também
        // saiba, você RELANÇA. Há duas formas — só uma é correta:
        //
        //   throw;    ✅ preserva a pilha original (onde o erro NASCEU)
        //   throw ex; ❌ reseta a pilha para a linha do 'throw ex'
        //
        // Use 'throw;' para repassar. Use 'throw new Exception("...", ex);'
        // se quiser EMBRULHAR com mais contexto (mantendo a original em
        // InnerException).
        try
        {
            try
            {
                int[] numeros = { 1, 2, 3 };
                Console.WriteLine(numeros[10]);             // IndexOutOfRangeException
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Logando o problema antes de relançar...");
                throw;  // ✅ relança preservando o stack trace original
            }
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"Capturado de novo no nível externo: {ex.Message}\n");
        }

        // ============================================================
        //  6. using — try/finally automático para recursos
        // ============================================================
        Console.WriteLine("--- 6. using (IDisposable) ---");

        // Tudo que implementa IDisposable (arquivos, conexões, streams)
        // deve ser fechado. 'using' garante o Dispose() mesmo se houver
        // exceção — é açúcar para try/finally.
        //
        //   using (var arq = new StreamReader("arq.txt"))
        //   { ... }                       // Dispose() chamado aqui
        //
        // Desde C# 8, há a forma SEM chaves: o Dispose acontece no fim
        // do bloco em que a variável foi declarada (geralmente o método).
        using (var recurso = new RecursoSimulado("conexão BD"))
        {
            recurso.Usar();
        }   // Dispose() é chamado AQUI, mesmo se Usar() lançasse erro
        Console.WriteLine();

        // ============================================================
        //  7. EXCEÇÕES customizadas
        // ============================================================
        Console.WriteLine("--- 7. Exceções customizadas ---");

        // Por que criar uma classe própria? Para que o CHAMADOR consiga
        // diferenciar (catch específico) o erro de domínio do projeto
        // dos erros genéricos do .NET.
        var conta = new ContaBancaria(saldoInicial: 100);
        try
        {
            conta.Sacar(150);
        }
        catch (SaldoInsuficienteException ex)
        {
            // Acessamos propriedades que NÓS adicionamos à exceção.
            Console.WriteLine($"Saque negado!");
            Console.WriteLine($"  Saldo atual:  {ex.SaldoDisponivel:C}");
            Console.WriteLine($"  Tentou sacar: {ex.ValorSolicitado:C}");
            Console.WriteLine($"  Faltou:       {ex.ValorSolicitado - ex.SaldoDisponivel:C}\n");
        }

        // ============================================================
        //  8. EMBRULHANDO exceções (InnerException)
        // ============================================================
        Console.WriteLine("--- 8. InnerException (embrulho) ---");

        // Útil quando o erro de baixo nível não faz sentido para o
        // chamador. Você lança um erro de alto nível e GUARDA o original
        // como 'InnerException' para fins de log/depuração.
        try
        {
            CarregarConfiguracao("config-inexistente.json");
        }
        catch (ConfiguracaoInvalidaException ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
            Console.WriteLine($"Causa raiz: {ex.InnerException?.GetType().Name} — {ex.InnerException?.Message}\n");
        }

        // ============================================================
        //  9. FILTROS de exceção (catch ... when ...)
        // ============================================================
        Console.WriteLine("--- 9. Exception filters (when) ---");

        // O bloco 'when' decide se o catch DEVE PEGAR a exceção.
        // Vantagem sobre 'if' dentro do catch: se o when for falso, a
        // exceção continua subindo SEM perder a pilha original.
        try
        {
            ProcessarTransacao(valor: -50);
        }
        catch (ArgumentException ex) when (ex.ParamName == "valor")
        {
            Console.WriteLine($"Erro de valor: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Outro ArgumentException: {ex.Message}");
        }
        Console.WriteLine();

        // ============================================================
        //  10. TRY-PATTERN — evitando exceção para fluxo normal
        // ============================================================
        Console.WriteLine("--- 10. TryParse — alternativa sem exceção ---");

        // Exceções são CARAS (custo de criar o objeto, capturar stack...).
        // Para casos esperados — usuário digitando — prefira métodos Try*
        // que retornam bool e devolvem o resultado por 'out'.
        //
        //   int.Parse("abc")     → lança FormatException
        //   int.TryParse("abc", out int n) → retorna false, n = 0
        //
        // Regra: use try/catch para o EXCEPCIONAL; TryParse para o ESPERADO.
        string[] testes = { "123", "abc", "-99", "3.14" };
        foreach (var t in testes)
        {
            if (int.TryParse(t, out int n))
                Console.WriteLine($"  '{t}' → {n} ✓");
            else
                Console.WriteLine($"  '{t}' → falhou (sem lançar exceção)");
        }
        Console.WriteLine();

        // ============================================================
        //  RESUMO — Boas práticas
        // ============================================================
        Console.WriteLine("=== RESUMO — boas práticas ===");
        Console.WriteLine("  1. Capture exceções ESPECÍFICAS antes de genéricas.");
        Console.WriteLine("  2. Não 'engula' exceções com catch { } vazio — log ou relança.");
        Console.WriteLine("  3. Use 'throw;' (sem o 'ex') para preservar o stack trace.");
        Console.WriteLine("  4. Use 'using' para recursos descartáveis (arquivos, conexões).");
        Console.WriteLine("  5. Crie exceções customizadas para erros de DOMÍNIO do projeto.");
        Console.WriteLine("  6. Prefira TryParse/TryGetValue ao try/catch para fluxo esperado.");
        Console.WriteLine("  7. Valide nas FRONTEIRAS (entrada do usuário, APIs) com mensagens claras.");
        Console.WriteLine("  8. Mensagens de exceção devem ajudar quem vai LER o log, não o usuário final.");

        Console.ReadKey();
    }

    // ============================================================
    //  Métodos de apoio aos exemplos acima
    // ============================================================

    // Valida idade lançando exceção quando inválida.
    // Usar 'ArgumentOutOfRangeException' é mais semântico que 'Exception'.
    static void ValidarIdade(int idade)
    {
        if (idade < 0 || idade > 150)
        {
            // O segundo argumento é o nome do parâmetro (vai para ex.ParamName).
            throw new ArgumentOutOfRangeException(
                nameof(idade),
                idade,
                "Idade deve estar entre 0 e 150."
            );
        }
        Console.WriteLine($"Idade {idade} válida.");
    }

    // Demonstra embrulho: o método externo lança ConfiguracaoInvalidaException
    // mas mantém o IOException original em InnerException.
    static void CarregarConfiguracao(string caminho)
    {
        try
        {
            // Simulação: a versão real seria File.ReadAllText(caminho)
            throw new System.IO.FileNotFoundException($"Arquivo '{caminho}' não existe.");
        }
        catch (System.IO.IOException ex)
        {
            // Erro de baixo nível (IO) é embrulhado em um erro de domínio.
            throw new ConfiguracaoInvalidaException(
                $"Falha ao carregar configuração de '{caminho}'.",
                ex
            );
        }
    }

    // Demonstra filtro de exceção (when) em conjunto com nameof.
    static void ProcessarTransacao(decimal valor)
    {
        if (valor <= 0)
        {
            throw new ArgumentException(
                "O valor deve ser positivo.",
                nameof(valor)
            );
        }
        Console.WriteLine($"Processando {valor:C}");
    }
}

// ============================================================
//  Classes auxiliares
// ============================================================

// Simula um recurso descartável (arquivo, conexão BD, socket...).
// IDisposable é o contrato do 'using': implementar Dispose() corretamente.
class RecursoSimulado : IDisposable
{
    private readonly string _nome;
    private bool _liberado;

    public RecursoSimulado(string nome)
    {
        _nome = nome;
        Console.WriteLine($"  [abrir]  {_nome}");
    }

    public void Usar()
    {
        if (_liberado)
            throw new ObjectDisposedException(_nome);
        Console.WriteLine($"  [usar]   {_nome}");
    }

    public void Dispose()
    {
        if (_liberado) return;   // idempotente: chamar 2x não dá problema
        Console.WriteLine($"  [fechar] {_nome}");
        _liberado = true;
    }
}

// Exceção customizada com dados extras úteis para quem capturar.
// Convenção: nome termina em "Exception" e a classe herda de Exception.
class SaldoInsuficienteException : Exception
{
    public decimal SaldoDisponivel { get; }
    public decimal ValorSolicitado { get; }

    public SaldoInsuficienteException(decimal saldo, decimal valor)
        : base($"Saldo de {saldo:C} insuficiente para sacar {valor:C}.")
    {
        SaldoDisponivel = saldo;
        ValorSolicitado = valor;
    }
}

// Exceção customizada que aceita InnerException — padrão "embrulho".
// Os três construtores abaixo são a forma canônica de definir uma
// exceção em C# (mensagem, mensagem + causa, sem argumentos).
class ConfiguracaoInvalidaException : Exception
{
    public ConfiguracaoInvalidaException() { }

    public ConfiguracaoInvalidaException(string mensagem)
        : base(mensagem) { }

    public ConfiguracaoInvalidaException(string mensagem, Exception causa)
        : base(mensagem, causa) { }
}

// Classe que LANÇA a exceção customizada acima.
class ContaBancaria
{
    public decimal Saldo { get; private set; }

    public ContaBancaria(decimal saldoInicial)
    {
        if (saldoInicial < 0)
            throw new ArgumentOutOfRangeException(
                nameof(saldoInicial),
                "Saldo inicial não pode ser negativo."
            );
        Saldo = saldoInicial;
    }

    public void Sacar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentOutOfRangeException(nameof(valor), "Valor deve ser positivo.");

        if (valor > Saldo)
            throw new SaldoInsuficienteException(Saldo, valor);

        Saldo -= valor;
    }
}
