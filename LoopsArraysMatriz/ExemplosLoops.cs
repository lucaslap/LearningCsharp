// ============================================================
//  LOOPS — Estruturas de Repetição em C#
// ============================================================
//
//  Quando usar cada tipo:
//    for       → número de repetições conhecido com antecedência
//    while     → repetir enquanto uma condição for verdadeira (pode não executar nenhuma vez)
//    do-while  → igual ao while, mas garante pelo menos UMA execução
//    foreach   → percorrer coleções (arrays, listas) sem precisar do índice
//
// ============================================================

static class ExemplosLoops
{
    public static void Executar()
    {
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║          LOOPS — REPETIÇÃO           ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");

        DemonstrarFor();
        DemonstrarWhile();
        DemonstrarDoWhile();
        DemonstrarForeach();
        DemonstrarBreakContinue();
        DemonstrarLoopsAninhados();
    }

    // ──────────────────────────────────────────────────────────
    //  1. FOR — quando o número de repetições é conhecido
    // ──────────────────────────────────────────────────────────
    static void DemonstrarFor()
    {
        Console.WriteLine("--- 1. for ---");

        // Sintaxe:  for (inicialização; condição; incremento)
        //   • inicialização: executada UMA vez antes do loop
        //   • condição:      verificada antes de CADA iteração
        //   • incremento:    executado depois de CADA iteração

        // Contagem crescente (0 a 4)
        Console.Write("  Crescente (0–4):  ");
        for (int i = 0; i < 5; i++)
        {
            Console.Write($"{i} ");
        }
        Console.WriteLine();

        // Contagem decrescente (10 a 1)
        Console.Write("  Decrescente 10–1: ");
        for (int i = 10; i >= 1; i--)
        {
            Console.Write($"{i} ");
        }
        Console.WriteLine();

        // Passo diferente de 1 — de 2 em 2
        Console.Write("  Pares de 0 a 20:  ");
        for (int i = 0; i <= 20; i += 2)
        {
            Console.Write($"{i} ");
        }
        Console.WriteLine();

        // Uso prático: tabuada do 7
        Console.WriteLine("\n  Tabuada do 7:");
        for (int i = 1; i <= 10; i++)
        {
            Console.WriteLine($"    7 × {i,2} = {7 * i,3}"); // ,2 e ,3 alinham à direita
        }

        Console.WriteLine();
    }

    // ──────────────────────────────────────────────────────────
    //  2. WHILE — enquanto a condição for verdadeira
    // ──────────────────────────────────────────────────────────
    static void DemonstrarWhile()
    {
        Console.WriteLine("--- 2. while ---");

        // A condição é testada ANTES de entrar no bloco.
        // Se for falsa desde o início, o bloco NUNCA executa.

        int tentativas = 0;
        int senhaCorreta = 1234;
        int[] tentativasFeitas = { 0000, 9999, 1234 }; // simula entradas do usuário

        Console.WriteLine("  Simulação de login:");
        while (tentativas < tentativasFeitas.Length)
        {
            int entrada = tentativasFeitas[tentativas];
            tentativas++;

            Console.Write($"    Tentativa {tentativas}: {entrada} → ");

            if (entrada == senhaCorreta)
            {
                Console.WriteLine("Acesso liberado!");
                break; // sai do while
            }

            Console.WriteLine("Senha incorreta.");
        }

        // Uso prático: reduzir pela metade até ser < 1
        double valor = 100.0;
        int passos = 0;
        Console.Write("\n  100 dividido por 2 repetidamente: ");
        while (valor >= 1)
        {
            Console.Write($"{valor:F1} ");
            valor /= 2;
            passos++;
        }
        Console.WriteLine($"\n  ({passos} divisões até chegar em < 1)");

        Console.WriteLine();
    }

    // ──────────────────────────────────────────────────────────
    //  3. DO-WHILE — garante ao menos uma execução
    // ──────────────────────────────────────────────────────────
    static void DemonstrarDoWhile()
    {
        Console.WriteLine("--- 3. do-while ---");

        // Diferença-chave: a condição é testada DEPOIS do bloco.
        // Isso garante que o corpo execute pelo menos uma vez.

        // Exemplo: menu que exibe ao menos uma vez
        int[] opcoesSimuladas = { 5, 2 }; // 5 é inválido, 2 é válido
        int indiceSim = 0;
        int opcao;

        Console.WriteLine("  Simulação de menu (mínimo 1 exibição):");
        do
        {
            opcao = opcoesSimuladas[indiceSim++];
            Console.WriteLine($"    Opção escolhida: {opcao}");

            if (opcao < 1 || opcao > 3)
                Console.WriteLine("    → Opção inválida, tente novamente.");

        } while (opcao < 1 || opcao > 3);

        Console.WriteLine($"    → Opção {opcao} aceita!\n");

        // Contraste: while com condição falsa de início — não executa nada
        int contador = 10;
        Console.Write("  while falso desde o início (não imprime nada): ");
        while (contador < 5)          // 10 < 5 é falso → bloco ignorado
        {
            Console.Write($"{contador} ");
            contador++;
        }
        Console.WriteLine("[nada]");

        // Do-while com condição falsa de início — executa UMA vez
        contador = 10;
        Console.Write("  do-while falso desde o início (imprime uma vez): ");
        do
        {
            Console.Write($"{contador} "); // imprime 10
            contador++;
        } while (contador < 5);           // 11 < 5 é falso → para
        Console.WriteLine();

        Console.WriteLine();
    }

    // ──────────────────────────────────────────────────────────
    //  4. FOREACH — percorre coleções sem precisar do índice
    // ──────────────────────────────────────────────────────────
    static void DemonstrarForeach()
    {
        Console.WriteLine("--- 4. foreach ---");

        // Sintaxe: foreach (tipo variavel in coleção)
        // A cada iteração, 'variavel' recebe o próximo elemento.
        // Não é possível modificar a coleção durante o foreach.

        string[] frutas = { "Maçã", "Banana", "Laranja", "Uva", "Morango" };

        Console.Write("  Frutas: ");
        foreach (string fruta in frutas)
        {
            Console.Write($"{fruta}  ");
        }
        Console.WriteLine();

        // Foreach com string (string é uma coleção de chars)
        string palavra = "C#Sharp";
        Console.Write($"\n  Caracteres de \"{palavra}\": ");
        foreach (char letra in palavra)
        {
            Console.Write($"[{letra}] ");
        }
        Console.WriteLine();

        // Uso prático: somar todos os elementos
        int[] numeros = { 4, 7, 2, 9, 1, 5, 8, 3, 6 };
        int soma = 0;
        int maior = numeros[0];
        int menor = numeros[0];

        foreach (int n in numeros)
        {
            soma += n;
            if (n > maior) maior = n;
            if (n < menor) menor = n;
        }

        Console.WriteLine($"\n  Array: {{ {string.Join(", ", numeros)} }}");
        Console.WriteLine($"    Soma:  {soma}");
        Console.WriteLine($"    Média: {(double)soma / numeros.Length:F2}");
        Console.WriteLine($"    Maior: {maior}");
        Console.WriteLine($"    Menor: {menor}");

        Console.WriteLine();
    }

    // ──────────────────────────────────────────────────────────
    //  5. BREAK E CONTINUE — controle de fluxo dentro do loop
    // ──────────────────────────────────────────────────────────
    static void DemonstrarBreakContinue()
    {
        Console.WriteLine("--- 5. break e continue ---");

        // BREAK: interrompe o loop imediatamente e sai dele
        Console.Write("  break ao encontrar o 5: ");
        for (int i = 0; i <= 10; i++)
        {
            if (i == 5)
            {
                Console.Write("[parou aqui] ");
                break; // sai do for
            }
            Console.Write($"{i} ");
        }
        Console.WriteLine();

        // CONTINUE: pula para a próxima iteração sem executar o restante do bloco
        Console.Write("  continue (pula números pares): ");
        for (int i = 0; i <= 10; i++)
        {
            if (i % 2 == 0) continue; // se par, vai direto para o próximo i
            Console.Write($"{i} "); // só imprime ímpares
        }
        Console.WriteLine();

        // Uso prático: buscar o primeiro número divisível por 7 no intervalo
        Console.Write("\n  Primeiro múltiplo de 7 entre 20 e 100: ");
        for (int i = 20; i <= 100; i++)
        {
            if (i % 7 == 0)
            {
                Console.WriteLine(i);
                break;
            }
        }

        // Uso prático: pular strings vazias numa lista
        string[] nomes = { "Ana", "", "Bruno", " ", "Carla", "", "Diego" };
        Console.Write("  Nomes não vazios: ");
        foreach (string nome in nomes)
        {
            if (string.IsNullOrWhiteSpace(nome)) continue;
            Console.Write($"{nome}  ");
        }
        Console.WriteLine();

        Console.WriteLine();
    }

    // ──────────────────────────────────────────────────────────
    //  6. LOOPS ANINHADOS — um loop dentro do outro
    // ──────────────────────────────────────────────────────────
    static void DemonstrarLoopsAninhados()
    {
        Console.WriteLine("--- 6. Loops Aninhados ---");

        // Para cada iteração do loop externo, o loop interno executa COMPLETO.
        // Total de execuções = iterações_externo × iterações_interno

        // Exemplo 1: tabela de multiplicação 1–5
        Console.WriteLine("\n  Tabela de multiplicação (1–5):");
        Console.Write("       ");
        for (int col = 1; col <= 5; col++) Console.Write($"  ×{col} ");
        Console.WriteLine();

        for (int linha = 1; linha <= 5; linha++)
        {
            Console.Write($"  [{linha}]  ");
            for (int col = 1; col <= 5; col++)
            {
                Console.Write($"{linha * col,4} "); // ,4 = largura mínima 4, alinhado à direita
            }
            Console.WriteLine();
        }

        // Exemplo 2: padrão de asteriscos (triângulo)
        Console.WriteLine("\n  Triângulo de asteriscos:");
        for (int linha = 1; linha <= 5; linha++)
        {
            Console.Write("    ");
            for (int col = 1; col <= linha; col++)
            {
                Console.Write("* ");
            }
            Console.WriteLine();
        }

        // Exemplo 3: padrão invertido
        Console.WriteLine("\n  Triângulo invertido:");
        for (int linha = 5; linha >= 1; linha--)
        {
            Console.Write("    ");
            for (int col = 1; col <= linha; col++)
            {
                Console.Write("* ");
            }
            Console.WriteLine();
        }

        Console.WriteLine();
    }
}
