// ============================================================
//  1) NOMES SIGNIFICATIVOS
// ============================================================
//
//  O nome de uma variavel, metodo ou classe deve revelar a INTENCAO:
//  o que ele representa, faz ou guarda. Um bom nome dispensa comentario
//  explicativo. Regras praticas:
//
//  - Use nomes pronunciaveis e pesquisaveis (evite abreviacoes obscuras).
//  - Revele a intencao: `diasDesdeUltimoLogin` em vez de `d`.
//  - Evite informacao falsa ou ruido: nao chame de `lista` algo que
//    nao e uma List; evite sufixos vazios como `Info`, `Data`, `Manager`.
//  - Classes e tipos: substantivos (Cliente, Pedido, CalculadoraImposto).
//  - Metodos: verbos ou frases verbais (CalcularTotal, EnviarEmail).
//  - Booleanos: perguntas (estaAtivo, temEstoque, ehValido).
//
//  CONVENCOES DE NOMENCLATURA EM C#:
//  - PascalCase: classes, metodos, propriedades, constantes publicas.
//  - camelCase: variaveis locais e parametros.
//  - _camelCase: campos privados (convencao comum, com underscore).
// ============================================================

namespace CleanCodeLesson;

class NamingDemo
{
    public static void Executar()
    {
        Console.WriteLine("=== 1) Nomes significativos ===\n");

        // ANTES: nomes sem significado exigem que o leitor adivinhe o proposito.
        //   int d;            // o que e "d"? dias? distancia? desconto?
        //   var l = new List<int>();
        //   bool flag;

        // DEPOIS: o nome carrega a intencao, sem necessidade de comentario.
        int diasDesdeUltimaCompra = 45;
        List<string> nomesDeClientes = new() { "Ana", "Bruno", "Carla" };
        bool clienteEstaInativo = diasDesdeUltimaCompra > 30;

        Console.WriteLine($"  Dias desde a ultima compra: {diasDesdeUltimaCompra}");
        Console.WriteLine($"  Clientes cadastrados: {nomesDeClientes.Count}");
        Console.WriteLine($"  Cliente inativo? {clienteEstaInativo}");

        // Numeros magicos viram constantes nomeadas: o "30" acima nao explica
        // nada por si so. Extrair para uma constante revela a regra de negocio.
        const int DiasParaConsiderarInativo = 30;
        bool inativoComConstante = diasDesdeUltimaCompra > DiasParaConsiderarInativo;
        Console.WriteLine($"  Inativo (usando constante nomeada)? {inativoComConstante}");

        // Booleanos como pergunta tornam as condicoes legiveis como frases.
        var produto = new Produto("Teclado", Preco: 150m, QuantidadeEmEstoque: 0);
        if (!produto.TemEstoque)
        {
            Console.WriteLine($"  {produto.Nome} esta indisponivel.");
        }

        Console.WriteLine();
    }

    // Metodos com verbos; propriedade booleana como pergunta (TemEstoque).
    public record Produto(string Nome, decimal Preco, int QuantidadeEmEstoque)
    {
        public bool TemEstoque => QuantidadeEmEstoque > 0;
    }
}
