// ============================================================
//  4) INTERFACES GENERICAS E VARIANCIA (in / out)
// ============================================================
//
//  INTERFACES GENERICAS
//  ────────────────────
//  Interfaces tambem podem ser parametrizadas por tipo. Boa parte da BCL
//  se apoia nelas: IEnumerable<T>, IComparable<T>, IEquatable<T>,
//  IDictionary<TKey, TValue>, etc. Implementa-las torna nossos tipos
//  compativeis com toda a infraestrutura que ja conhece essas interfaces
//  (foreach, LINQ, ordenacao...).
//
//  VARIANCIA (covariancia e contravariancia)
//  ─────────────────────────────────────────
//  Por padrao, generics sao INVARIANTES: IList<string> NAO e um
//  IList<object>, mesmo string sendo um object. A variancia, declarada
//  nos parametros de tipo de uma INTERFACE (ou delegate), relaxa isso:
//
//    out T (COVARIANCIA): T so aparece em posicao de SAIDA (retorno).
//      Permite usar um IEnumerable<Derivado> onde se espera
//      IEnumerable<Base>. Ex.: IEnumerable<string> -> IEnumerable<object>.
//
//    in T (CONTRAVARIANCIA): T so aparece em posicao de ENTRADA (parametro).
//      Permite usar um IComparer<Base> onde se espera IComparer<Derivado>.
//      Ex.: Action<object> -> Action<string>.
//
//  Regra mnemonica: `out` = produz (sai), `in` = consome (entra).
// ============================================================

namespace GenericsLesson;

class VarianceDemo
{
    public static void Executar()
    {
        Console.WriteLine("=== 4) Interfaces genericas e variancia ===\n");

        // Interface generica propria implementada por uma classe concreta.
        IRepositorioLeitura<string> fonte = new RepositorioMemoria<string>(
            new[] { "alfa", "beta", "gama" });
        Console.WriteLine($"  Item no indice 1: {fonte.Obter(1)}");
        Console.WriteLine($"  Total de itens: {fonte.Contar()}");

        // COVARIANCIA (out): IProdutor<string> e aceito onde se espera
        // IProdutor<object>, porque T so aparece como retorno.
        IProdutor<string> produtorTexto = new ProdutorDeTexto();
        IProdutor<object> produtorObjeto = produtorTexto; // permitido por 'out'
        Console.WriteLine($"  [Covariancia] produziu: {produtorObjeto.Produzir()}");

        // CONTRAVARIANCIA (in): IConsumidor<object> e aceito onde se espera
        // IConsumidor<string>, porque T so aparece como parametro de entrada.
        IConsumidor<object> consumidorObjeto = new ConsumidorDeObjeto();
        IConsumidor<string> consumidorTexto = consumidorObjeto; // permitido por 'in'
        consumidorTexto.Consumir("mensagem de texto");

        Console.WriteLine();
    }

    // Interface generica invariante (sem in/out): T aparece em entrada e saida.
    public interface IRepositorioLeitura<T>
    {
        T Obter(int indice);
        int Contar();
    }

    public class RepositorioMemoria<T> : IRepositorioLeitura<T>
    {
        private readonly T[] _itens;

        public RepositorioMemoria(T[] itens) => _itens = itens;

        public T Obter(int indice) => _itens[indice];
        public int Contar() => _itens.Length;
    }

    // Covariante: 'out T' so pode ser usado como tipo de RETORNO.
    public interface IProdutor<out T>
    {
        T Produzir();
    }

    public class ProdutorDeTexto : IProdutor<string>
    {
        public string Produzir() => "texto gerado";
    }

    // Contravariante: 'in T' so pode ser usado como tipo de PARAMETRO.
    public interface IConsumidor<in T>
    {
        void Consumir(T item);
    }

    public class ConsumidorDeObjeto : IConsumidor<object>
    {
        public void Consumir(object item) =>
            Console.WriteLine($"  [Contravariancia] consumiu: {item}");
    }
}
