// ============================================================
//  3) RESTRICOES (CONSTRAINTS) - palavra-chave where
// ============================================================
//
//  Por padrao, dentro de um codigo generico sabemos muito pouco sobre T:
//  podemos atribui-lo, compara-lo com null (se for tipo de referencia) e
//  chamar apenas os membros de object. As RESTRICOES informam ao
//  compilador requisitos adicionais sobre T, liberando mais operacoes.
//
//  PRINCIPAIS RESTRICOES:
//    where T : class            -> T deve ser tipo de referencia.
//    where T : struct           -> T deve ser tipo de valor (nao nulavel).
//    where T : new()            -> T deve ter construtor publico sem
//                                  parametros (permite `new T()`).
//    where T : NomeDeClasse     -> T deve ser ou herdar dessa classe.
//    where T : INomeInterface   -> T deve implementar a interface (libera
//                                  os membros da interface dentro do generico).
//    where T : U                -> T deve ser ou derivar de outro tipo U.
//
//  Varias restricoes podem ser combinadas e ha uma clausula `where` por
//  parametro de tipo.
// ============================================================

namespace GenericsLesson;

class ConstraintsDemo
{
    public static void Executar()
    {
        Console.WriteLine("=== 3) Restricoes (constraints) ===\n");

        // Restricao de interface: T precisa implementar IComparable<T>, o que
        // permite chamar CompareTo dentro do metodo Maior.
        Console.WriteLine($"  Maior entre 7 e 3: {Maior(7, 3)}");
        Console.WriteLine($"  Maior entre 'abc' e 'abd': {Maior("abc", "abd")}");

        // Restricao new(): permite instanciar T dentro do metodo de fabrica.
        var produto = Criar<Produto>();
        produto.Nome = "Monitor";
        Console.WriteLine($"  Instancia criada via new T(): {produto.Nome}");

        // Restricao de interface aplicada a uma colecao de entidades.
        var repo = new Repositorio<Produto>();
        repo.Adicionar(new Produto { Id = 1, Nome = "Teclado" });
        repo.Adicionar(new Produto { Id = 2, Nome = "Mouse" });
        Produto? encontrado = repo.BuscarPorId(2);
        Console.WriteLine($"  Buscado por Id=2: {encontrado?.Nome}");

        Console.WriteLine();
    }

    // where T : IComparable<T> -> garante a existencia de CompareTo.
    static T Maior<T>(T primeiro, T segundo) where T : IComparable<T>
    {
        return primeiro.CompareTo(segundo) >= 0 ? primeiro : segundo;
    }

    // where T : new() -> permite construir uma instancia de T.
    static T Criar<T>() where T : new()
    {
        return new T();
    }

    // Entidades possuem Id; a interface formaliza esse contrato.
    public interface IEntidade
    {
        int Id { get; }
    }

    public class Produto : IEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }

    // Repositorio generico que so aceita tipos de referencia que sejam
    // entidades (class + IEntidade), o que viabiliza acessar item.Id.
    public class Repositorio<T> where T : class, IEntidade
    {
        private readonly List<T> _itens = new();

        public void Adicionar(T item) => _itens.Add(item);

        public T? BuscarPorId(int id)
        {
            foreach (T item in _itens)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
