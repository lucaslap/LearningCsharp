// ============================================================
//  2) CLASSES GENERICAS
// ============================================================
//
//  Uma classe generica declara um ou mais parametros de tipo logo apos o
//  nome: `class Caixa<T>`. Esses parametros podem ser usados em campos,
//  propriedades, parametros de metodo e tipos de retorno dentro da classe.
//
//  Ao instanciar, informamos o tipo concreto: `new Caixa<int>()`. A partir
//  dai, todo T daquela instancia significa int, com seguranca de tipo
//  garantida pelo compilador.
//
//  As colecoes da BCL (List<T>, Dictionary<TKey, TValue>, Stack<T> ...)
//  sao exatamente isto: classes genericas. Aqui reimplementamos versoes
//  minimas para entender o mecanismo por dentro.
// ============================================================

namespace GenericsLesson;

class GenericClassesDemo
{
    public static void Executar()
    {
        Console.WriteLine("=== 2) Classes genericas ===\n");

        // A mesma classe Caixa<T> guarda tipos diferentes com seguranca.
        var caixaDeInteiro = new Caixa<int>(42);
        var caixaDeTexto = new Caixa<string>("ola");

        Console.WriteLine($"  Caixa<int>: {caixaDeInteiro.Conteudo}");
        Console.WriteLine($"  Caixa<string>: {caixaDeTexto.Conteudo}");

        // caixaDeInteiro.Conteudo = "texto"; // ERRO de compilacao: type safety.

        // Classe generica com mais de um parametro de tipo.
        var par = new Par<string, int>("idade", 30);
        Console.WriteLine($"  Par<string,int>: {par.Primeiro} = {par.Segundo}");

        // Uma "pilha" generica propria, demonstrando T em metodos e estado.
        var pilha = new PilhaSimples<string>();
        pilha.Empilhar("a");
        pilha.Empilhar("b");
        pilha.Empilhar("c");
        Console.WriteLine($"  Itens na pilha: {pilha.Quantidade}");
        Console.WriteLine($"  Desempilhou: {pilha.Desempilhar()}");
        Console.WriteLine($"  Desempilhou: {pilha.Desempilhar()}");
        Console.WriteLine($"  Itens restantes: {pilha.Quantidade}");

        Console.WriteLine();
    }

    // Classe generica com um parametro de tipo.
    public class Caixa<T>
    {
        public T Conteudo { get; set; }

        public Caixa(T conteudo) => Conteudo = conteudo;
    }

    // Classe generica com dois parametros de tipo independentes.
    public class Par<TPrimeiro, TSegundo>
    {
        public TPrimeiro Primeiro { get; }
        public TSegundo Segundo { get; }

        public Par(TPrimeiro primeiro, TSegundo segundo)
        {
            Primeiro = primeiro;
            Segundo = segundo;
        }
    }

    // Implementacao didatica de uma pilha (LIFO) generica.
    public class PilhaSimples<T>
    {
        private readonly List<T> _itens = new();

        public int Quantidade => _itens.Count;

        public void Empilhar(T item) => _itens.Add(item);

        public T Desempilhar()
        {
            if (_itens.Count == 0)
            {
                throw new InvalidOperationException("A pilha esta vazia.");
            }

            int ultimo = _itens.Count - 1;
            T item = _itens[ultimo];
            _itens.RemoveAt(ultimo);
            return item;
        }
    }
}
