// ============================================================
//  C# - Coleções (Collections)
// ============================================================
//
//  Por que usar coleções em vez de arrays?
//  ─────────────────────────────────────────
//  Arrays têm tamanho FIXO. Coleções são DINÂMICAS: crescem e
//  encolhem em tempo de execução, e já trazem métodos prontos.
//
//  HIERARQUIA DE INTERFACES (leia de baixo para cima)
//  ─────────────────────────────────────────────────────
//  IEnumerable<T>  → suporta foreach (qualquer coleção)
//    └─ ICollection<T> → Count, Add, Remove, Contains, Clear
//         ├─ IList<T>       → índice [i], Insert, RemoveAt
//         └─ ISet<T>        → UnionWith, IntersectWith…
//
//  As interfaces definem o CONTRATO; as classes concretas o implementam:
//    List<T>               → implementa IList<T>
//    Dictionary<TK,TV>     → implementa IDictionary<TK,TV>
//    HashSet<T>            → implementa ISet<T>
//    Queue<T> / Stack<T>   → implementam IEnumerable<T>
//
//  VISÃO GERAL — quando usar cada uma
//  ─────────────────────────────────────
//    List<T>                → lista genérica: ordenada, com duplicatas, por índice
//    Dictionary<TK,TV>      → busca por chave em O(1)
//    HashSet<T>             → elementos únicos; operações de conjuntos
//    Queue<T>               → fila FIFO (primeiro a entrar, primeiro a sair)
//    Stack<T>               → pilha LIFO (último a entrar, primeiro a sair)
//    LinkedList<T>          → inserção/remoção no meio em O(1)
//    SortedDictionary<K,V>  → como Dictionary mas sempre ordenado por chave
//
// ============================================================

class Program
{
    static void Main()
    {
        Console.WriteLine("=== COLEÇÕES EM C# ===\n");

        // ============================================================
        //  1. List<T> — a coleção mais usada no dia a dia
        // ============================================================
        Console.WriteLine("--- 1. List<T> ---");

        // List<T> é um array dinâmico: cresce conforme você adiciona itens.
        // Acesso por ÍNDICE é O(1); busca sem índice é O(n).
        var carrinho = new List<string>();

        // Adicionar
        carrinho.Add("Maçã");
        carrinho.Add("Banana");
        carrinho.Add("Laranja");
        carrinho.Add("Banana");        // duplicata permitida
        carrinho.AddRange(new[] { "Uva", "Pera" }); // vários de uma vez

        Console.WriteLine($"Itens: [{string.Join(", ", carrinho)}]");
        Console.WriteLine($"Count: {carrinho.Count}");

        // Acesso por índice (igual ao array)
        Console.WriteLine($"Primeiro: {carrinho[0]} | Último: {carrinho[^1]}");

        // Inserir e remover
        carrinho.Insert(1, "Abacaxi"); // insere no índice 1, empurra os outros
        carrinho.Remove("Banana");     // remove a PRIMEIRA ocorrência
        carrinho.RemoveAt(0);          // remove pelo índice
        Console.WriteLine($"Após edições: [{string.Join(", ", carrinho)}]");

        // Busca
        bool temUva   = carrinho.Contains("Uva");
        int  idxPera  = carrinho.IndexOf("Pera");
        var  comA     = carrinho.FindAll(f => f.StartsWith("A")); // LINQ-like
        Console.WriteLine($"Tem Uva? {temUva} | Índice Pera: {idxPera}");
        Console.WriteLine($"Frutas com 'A': [{string.Join(", ", comA)}]");

        // Ordenar (modifica a lista original)
        carrinho.Sort();
        Console.WriteLine($"Ordenado: [{string.Join(", ", carrinho)}]");

        // Converter array → List e List → array
        string[] arrayFrutas = ["Manga", "Caju"];
        var listaDeFrutas    = arrayFrutas.ToList(); // array para List
        string[] voltaArray  = carrinho.ToArray();   // List para array

        // ============================================================
        //  2. Dictionary<TKey, TValue> — busca por chave em O(1)
        // ============================================================
        Console.WriteLine("\n--- 2. Dictionary<TKey, TValue> ---");

        // Armazena pares CHAVE → VALOR. A chave é única; o valor pode repetir.
        // A hashtable interna garante busca, inserção e remoção em O(1) amortizado.
        var estoque = new Dictionary<string, int>
        {
            ["Maçã"]   = 50,   // sintaxe de inicialização com chave
            ["Banana"] = 120,
            ["Uva"]    = 30
        };

        // Adicionar e sobrescrever
        estoque["Laranja"] = 75;    // adiciona (chave nova) ou atualiza (chave existente)
        estoque.Add("Pera", 40);    // Add lança exceção se a chave já existir

        // Leitura segura com TryGetValue (evita KeyNotFoundException)
        if (estoque.TryGetValue("Banana", out int qtdBanana))
            Console.WriteLine($"Estoque Banana: {qtdBanana}");

        // Verificação de chave antes de acessar
        if (estoque.ContainsKey("Abacate"))
            Console.WriteLine("Tem abacate");
        else
            Console.WriteLine("Abacate não cadastrado");

        // Remover
        estoque.Remove("Uva");

        // Iterar — cada elemento é um KeyValuePair<TKey, TValue>
        Console.WriteLine("Estoque atual:");
        foreach (var (produto, quantidade) in estoque) // desestrutura o par
            Console.WriteLine($"  {produto,-10} → {quantidade} unidades");

        // Só chaves ou só valores
        Console.WriteLine($"Chaves:  [{string.Join(", ", estoque.Keys)}]");
        Console.WriteLine($"Valores: [{string.Join(", ", estoque.Values)}]");

        // --- 2a. Valor como objeto ---
        // O valor pode ser qualquer tipo, inclusive classes customizadas.
        var agenda = new Dictionary<string, Contato>
        {
            ["Lucas"] = new Contato("Lucas",  "(11) 9000-0001", "lucas@email.com"),
            ["Maria"] = new Contato("Maria",  "(21) 9000-0002", "maria@email.com"),
            ["João"]  = new Contato("João",   "(31) 9000-0003", "joao@email.com")
        };

        if (agenda.TryGetValue("Maria", out Contato? c))
            Console.WriteLine($"Encontrado: {c.Nome} — {c.Telefone}");

        // Atualizar um campo do objeto (valor é referência, então funciona direto)
        agenda["Lucas"].Telefone = "(11) 9999-9999";
        Console.WriteLine($"Lucas atualizado: {agenda["Lucas"].Telefone}");

        // --- 2b. Dicionário com List como valor — agrupar elementos ---
        // Padrão muito comum: cada chave mapeia para UMA LISTA de valores.
        // Ex: categoria → lista de produtos naquela categoria.
        var categorias = new Dictionary<string, List<string>>();

        void AdicionarProduto(string categoria, string produto)
        {
            // Se a chave não existe ainda, cria a lista antes de adicionar.
            if (!categorias.ContainsKey(categoria))
                categorias[categoria] = new List<string>();
            categorias[categoria].Add(produto);
        }

        AdicionarProduto("Frutas",   "Maçã");
        AdicionarProduto("Frutas",   "Banana");
        AdicionarProduto("Verduras", "Alface");
        AdicionarProduto("Frutas",   "Uva");
        AdicionarProduto("Verduras", "Cenoura");

        Console.WriteLine("Produtos por categoria:");
        foreach (var (cat, produtos) in categorias)
            Console.WriteLine($"  {cat}: [{string.Join(", ", produtos)}]");

        // --- 2c. Converter List para Dictionary com LINQ ---
        // ToDictionary(keySelector, valueSelector) transforma qualquer coleção.
        var contatos = new List<Contato>
        {
            new("Ana",   "(41) 1111-1111", "ana@email.com"),
            new("Bruno", "(51) 2222-2222", "bruno@email.com"),
            new("Carla", "(61) 3333-3333", "carla@email.com"),
        };

        // Chave = nome, valor = objeto inteiro
        Dictionary<string, Contato> contatosPorNome =
            contatos.ToDictionary(c => c.Nome);

        // Chave = nome, valor = só o telefone
        Dictionary<string, string> telefonePorNome =
            contatos.ToDictionary(c => c.Nome, c => c.Telefone);

        Console.WriteLine($"Telefone da Ana (via ToDictionary): {telefonePorNome["Ana"]}");

        // --- 2d. Mesclar dois dicionários ---
        var dict1 = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 };
        var dict2 = new Dictionary<string, int> { ["b"] = 99, ["c"] = 3 }; // "b" conflita

        // Mescla: dict2 sobrescreve chaves de dict1 em caso de conflito
        var mesclado = new Dictionary<string, int>(dict1);
        foreach (var (chave, valor) in dict2)
            mesclado[chave] = valor;  // [chave] sobrescreve, Add lançaria exceção

        Console.WriteLine($"Mesclado: [{string.Join(", ", mesclado.Select(p => $"{p.Key}={p.Value}"))}]");

        // --- 2e. Contador de frequência (padrão clássico) ---
        string texto = "banana abacaxi banana laranja abacaxi banana";
        var frequencia = new Dictionary<string, int>();
        foreach (var palavra in texto.Split(' '))
        {
            // GetValueOrDefault retorna 0 se a chave não existe
            frequencia[palavra] = frequencia.GetValueOrDefault(palavra) + 1;
        }
        Console.WriteLine("Frequência de palavras:");
        foreach (var (palavra, count) in frequencia.OrderByDescending(p => p.Value))
            Console.WriteLine($"  {palavra}: {count}x");

        // --- 2f. Dicionário como cache simples (memoização) ---
        // Evita recalcular resultados pesados: na primeira chamada computa e guarda;
        // nas seguintes retorna o valor já calculado em O(1).
        var cache = new Dictionary<int, long>();
        Console.WriteLine("Fibonacci com cache:");
        for (int i = 0; i <= 10; i++)
            Console.Write($"F({i})={FibCache(i, cache)}  ");
        Console.WriteLine();

        // ============================================================
        //  3. HashSet<T> — elementos únicos + operações de conjuntos
        // ============================================================
        Console.WriteLine("\n--- 3. HashSet<T> ---");

        // HashSet GARANTE unicidade: adicionar um elemento já existente não faz nada.
        // Não tem índice. Busca com Contains é O(1) (mais rápido que List.Contains).
        var visitantesLunes = new HashSet<string> { "Ana", "Bruno", "Carla", "Ana" };
        var visitantesMarte = new HashSet<string> { "Bruno", "Diego", "Carla", "Eva" };

        Console.WriteLine($"Visitantes segunda: [{string.Join(", ", visitantesLunes)}]"); // Ana aparece 1x
        Console.WriteLine($"Visitantes terça:   [{string.Join(", ", visitantesMarte)}]");

        // Operações de conjuntos — cada método MODIFICA o HashSet original
        var uniao        = new HashSet<string>(visitantesLunes);
        uniao.UnionWith(visitantesMarte);               // A ∪ B — todos os elementos
        Console.WriteLine($"União:         [{string.Join(", ", uniao)}]");

        var intersecao   = new HashSet<string>(visitantesLunes);
        intersecao.IntersectWith(visitantesMarte);      // A ∩ B — só os comuns
        Console.WriteLine($"Interseção:    [{string.Join(", ", intersecao)}]");

        var diferenca    = new HashSet<string>(visitantesLunes);
        diferenca.ExceptWith(visitantesMarte);          // A − B — só em A
        Console.WriteLine($"Diferença A-B: [{string.Join(", ", diferenca)}]");

        var difSimetrica = new HashSet<string>(visitantesLunes);
        difSimetrica.SymmetricExceptWith(visitantesMarte); // em A ou em B, mas não em ambos
        Console.WriteLine($"Dif. simétrica:[{string.Join(", ", difSimetrica)}]");

        // ============================================================
        //  4. Queue<T> — fila FIFO (First In, First Out)
        // ============================================================
        Console.WriteLine("\n--- 4. Queue<T> (FIFO) ---");

        // Fila: entra pelo FINAL, sai pelo INÍCIO.
        // Caso de uso: fila de atendimento, fila de mensagens, BFS em grafos.
        var filaAtendimento = new Queue<string>();

        filaAtendimento.Enqueue("Cliente 1");   // entra no final
        filaAtendimento.Enqueue("Cliente 2");
        filaAtendimento.Enqueue("Cliente 3");

        Console.WriteLine($"Fila: [{string.Join(", ", filaAtendimento)}]");
        Console.WriteLine($"Próximo a atender (Peek): {filaAtendimento.Peek()}"); // só espia, não remove

        while (filaAtendimento.Count > 0)
        {
            string atendido = filaAtendimento.Dequeue(); // remove do início
            Console.WriteLine($"  Atendendo: {atendido} | Restantes: {filaAtendimento.Count}");
        }

        // TryDequeue — versão segura que não lança exceção em fila vazia
        if (!filaAtendimento.TryDequeue(out string? proximo))
            Console.WriteLine("  Fila vazia, nada a desenfileirar.");

        // ============================================================
        //  5. Stack<T> — pilha LIFO (Last In, First Out)
        // ============================================================
        Console.WriteLine("\n--- 5. Stack<T> (LIFO) ---");

        // Pilha: entra pelo TOPO, sai pelo TOPO.
        // Caso de uso: desfazer/refazer (Ctrl+Z), navegação (back/forward),
        //              chamadas de função (call stack do próprio C#), DFS em grafos.
        var historicoDesfazer = new Stack<string>();

        historicoDesfazer.Push("Digitou 'Olá'");       // empilha
        historicoDesfazer.Push("Formatou negrito");
        historicoDesfazer.Push("Inseriu imagem");

        Console.WriteLine($"Pilha (topo → base): [{string.Join(", ", historicoDesfazer)}]");
        Console.WriteLine($"Topo (Peek): {historicoDesfazer.Peek()}"); // espia sem remover

        // Desfazer — retira do topo
        Console.WriteLine("Desfazendo ações:");
        while (historicoDesfazer.Count > 0)
            Console.WriteLine($"  Desfeito: {historicoDesfazer.Pop()}");

        // ============================================================
        //  6. LinkedList<T> — lista duplamente encadeada
        // ============================================================
        Console.WriteLine("\n--- 6. LinkedList<T> ---");

        // Cada nó guarda: valor + referência ao nó anterior + ao próximo.
        // Inserção/remoção em QUALQUER posição é O(1) (dado o nó).
        // Mas BUSCA é O(n) — não tem índice; percorre nó a nó.
        // Caso de uso: playlist de músicas, editor de texto, deque.
        var playlist = new LinkedList<string>();

        playlist.AddLast("Música A");
        playlist.AddLast("Música B");
        playlist.AddLast("Música C");
        playlist.AddFirst("Intro");        // adiciona no início

        // Inserir antes/depois de um nó específico
        LinkedListNode<string>? noB = playlist.Find("Música B");
        if (noB != null)
            playlist.AddBefore(noB, "Interlúdio"); // insere antes de "Música B"

        Console.Write("Playlist: ");
        foreach (var musica in playlist)
            Console.Write($"[{musica}] → ");
        Console.WriteLine("fim");

        // Navegar pelos nós manualmente
        var noAtual = playlist.First;
        Console.WriteLine($"Primeiro: {noAtual?.Value}");
        Console.WriteLine($"Segundo:  {noAtual?.Next?.Value}");
        Console.WriteLine($"Último:   {playlist.Last?.Value}");

        playlist.Remove("Interlúdio");
        playlist.RemoveFirst();
        Console.Write("Após remoções: ");
        foreach (var m in playlist) Console.Write($"[{m}] ");
        Console.WriteLine();

        // ============================================================
        //  7. SortedDictionary<TKey, TValue> — sempre ordenado por chave
        // ============================================================
        Console.WriteLine("\n--- 7. SortedDictionary<TKey, TValue> ---");

        // Como Dictionary, mas a árvore binária interna mantém as chaves ORDENADAS.
        // Busca/inserção/remoção: O(log n) — mais lento que Dictionary (O(1)),
        // mas a iteração sempre vem em ordem crescente de chave.
        var ranking = new SortedDictionary<int, string>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
        // Comparer customizado: ordena decrescente (maior pontuação primeiro)
        ranking[95]  = "Alice";
        ranking[78]  = "Bruno";
        ranking[100] = "Carla";
        ranking[88]  = "Diego";

        Console.WriteLine("Ranking (decrescente por pontuação):");
        int posicao = 1;
        foreach (var (pontos, nome) in ranking)
            Console.WriteLine($"  {posicao++}º {nome,-10} — {pontos} pts");

        // ============================================================
        //  8. Interfaces de coleção — codifique para interfaces, não classes
        // ============================================================
        Console.WriteLine("\n--- 8. Interfaces de coleção ---");

        // Declarar a VARIÁVEL como interface é boa prática:
        // o código chamador não depende de List<T> especificamente,
        // então você pode trocar a implementação sem quebrar nada.
        IList<int>       lista   = new List<int> { 3, 1, 4, 1, 5, 9 };
        ICollection<int> colecao = lista;  // visão mais restrita (sem índice)
        IEnumerable<int> enumeravel = lista;  // visão mínima (só foreach)

        // IList<T> expõe: Count, Add, Remove, Contains, Insert, RemoveAt, [índice]
        lista.Insert(0, 0);
        Console.WriteLine($"IList (com índice):     [{string.Join(", ", lista)}]");

        // ICollection<T> expõe: Count, Add, Remove, Contains, Clear
        Console.WriteLine($"ICollection.Count:      {colecao.Count}");

        // IEnumerable<T> só garante foreach (e LINQ encima disso)
        int soma = enumeravel.Sum();
        Console.WriteLine($"IEnumerable.Sum():      {soma}");

        // ============================================================
        //  RESUMO COMPARATIVO
        // ============================================================
        Console.WriteLine("\n=== RESUMO — quando usar cada coleção ===");
        Console.WriteLine("  List<T>                 → lista geral com duplicatas e acesso por índice");
        Console.WriteLine("  Dictionary<TKey,TValue> → lookup O(1) por chave; chave única");
        Console.WriteLine("  HashSet<T>              → elementos únicos; Contains O(1); operações de conjunto");
        Console.WriteLine("  Queue<T>                → FIFO: processamento em ordem de chegada");
        Console.WriteLine("  Stack<T>                → LIFO: desfazer, navegação, chamadas recursivas");
        Console.WriteLine("  LinkedList<T>           → inserção/remoção O(1) no meio (dado o nó)");
        Console.WriteLine("  SortedDictionary<K,V>   → como Dictionary, mas chaves sempre ordenadas");
        Console.WriteLine();
        Console.WriteLine("  Declare variáveis como IList<T>, ICollection<T> ou IEnumerable<T>");
        Console.WriteLine("  quando o código chamador não precisa saber a implementação concreta.");

        Console.ReadKey();
    }

    // Fibonacci com memoização: cache evita recomputar subproblemas já resolvidos.
    // Sem cache, Fibonacci recursivo é O(2^n); com cache é O(n).
    static long FibCache(int n, Dictionary<int, long> cache)
    {
        if (n <= 1) return n;
        if (cache.TryGetValue(n, out long resultado)) return resultado; // já calculado
        cache[n] = FibCache(n - 1, cache) + FibCache(n - 2, cache);
        return cache[n];
    }
}

// Classe auxiliar usada como valor no Dictionary (seção 2a e 2c)
class Contato
{
    public string Nome     { get; }
    public string Telefone { get; set; }
    public string Email    { get; }

    public Contato(string nome, string telefone, string email)
    {
        Nome     = nome;
        Telefone = telefone;
        Email    = email;
    }
}
