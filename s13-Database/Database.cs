// ============================================================
//  C# — Acessando Banco de Dados (SQL Server + ADO.NET)
// ============================================================
//
//  O QUE E ADO.NET?
//  ────────────────
//  ADO.NET e uma forma de conversar com um banco de dados em C#.
//  Voce escreve o SQL voce mesmo e usa um punhado de classes para mandar
//  esse SQL para o servidor e ler a resposta. (O EF Core, que vem depois,
//  esconde tudo isso atras de objetos — mas entender ADO.NET primeiro
//  ajuda a saber o que o ORM faz por baixo.)
//
//  O provider do SQL Server NAO faz parte da BCL: e o pacote NuGet
//  Microsoft.Data.SqlClient (veja o .csproj). Por isso o `using` abaixo.
//
//
//  AS 4 CLASSES QUE USA O TEMPO TODO
//  ──────────────────────────────────────
//    SqlConnection   abre/fecha o "cano" ate o servidor.
//    SqlCommand      carrega um comando SQL (texto) + seus parametros.
//    SqlParameter    um valor passado COM SEGURANCA ao SQL (evita injection).
//    SqlDataReader   le o resultado de um SELECT, linha por linha (so avanca).
//
//
//  CONNECTION STRING — o "endereco" do banco
//  ─────────────────────────────────────────
//  E uma string com pares chave=valor separados por ";". Exemplos comuns:
//    Server=localhost;Database=Loja;Trusted_Connection=True;TrustServerCertificate=True;
//    Server=(localdb)\\MSSQLLocalDB;Database=Loja;Trusted_Connection=True;
//
//    Server                 onde esta o SQL Server (host\instancia).
//    Database               qual banco usar.
//    Trusted_Connection     usa o login do Windows (sem usuario/senha).
//                           Alternativa: User Id=...;Password=...
//    TrustServerCertificate aceita o certificado local em DEV (evita erro de SSL).
//
//
//  REGRA DE OURO: SEMPRE feche a conexao.
//  ──────────────────────────────────────
//  Conexao e recurso caro e limitado. O `using` (visto na s07) garante o
//  Dispose()/Close() mesmo se der excecao. Por isso TODO bloco abaixo
//  usa `using var conn = new SqlConnection(...)`.
//
//
//  Operacoes de escrita/leitura (INSERT/SELECT/UPDATE/DELETE) e o uso de
//  parametros ficam em CrudDemo.cs (padrao hibrido s09).
//
//  OBS: para RODAR de verdade voce precisa de um SQL Server acessivel
//  (LocalDB ja serve) e da connection string certa. O codigo esta
//  envolto em try/catch para nao quebrar caso nao haja servidor.
//
// ============================================================

using Microsoft.Data.SqlClient;

namespace DatabaseLesson;

class Program
{
    // Connection string compartilhada pela secao. Ajuste o Server para o
    // seu ambiente (LocalDB, localhost, nome da instancia, etc.).
    public const string ConnectionString =
        @"Server=(localdb)\MSSQLLocalDB;Database=LojaDemo;Trusted_Connection=True;TrustServerCertificate=True;";

    static void Main()
    {
        Console.WriteLine("=== 1) Abrindo uma conexao ===\n");

        try
        {
            // O `using` fecha a conexao automaticamente no fim do bloco.
            using var conn = new SqlConnection(ConnectionString);
            conn.Open();   // so aqui o "cano" realmente abre ate o servidor.
            Console.WriteLine($"Conectado! Estado da conexao: {conn.State}");  // Open
        }
        catch (SqlException ex)
        {
            // Sem servidor disponivel, caimos aqui. Em estudo, isso e esperado.
            Console.WriteLine($"Nao foi possivel conectar: {ex.Message}");
        }

        Console.WriteLine("\n=== 2) Criando a tabela (ExecuteNonQuery) ===\n");

        // ExecuteNonQuery roda comandos que NAO devolvem linhas
        // (CREATE, INSERT, UPDATE, DELETE). Retorna nº de linhas afetadas.
        string sqlCriarTabela = @"
            IF OBJECT_ID('dbo.Produtos', 'U') IS NULL
            CREATE TABLE dbo.Produtos (
                Id     INT IDENTITY(1,1) PRIMARY KEY,
                Nome   NVARCHAR(100)  NOT NULL,
                Preco  DECIMAL(10,2)  NOT NULL
            );";

        try
        {
            using var conn = new SqlConnection(ConnectionString);
            conn.Open();

            // SqlCommand junta o texto SQL + a conexao por onde ele vai.
            using var cmd = new SqlCommand(sqlCriarTabela, conn);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Tabela 'Produtos' pronta.");
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Falha ao criar tabela: {ex.Message}");
        }

        Console.WriteLine("\n=== 3) Lendo um valor unico (ExecuteScalar) ===\n");

        // ExecuteScalar devolve a 1ª coluna da 1ª linha. Otimo para COUNT,
        // SUM, MAX... ou para pegar um Id recem-inserido.
        try
        {
            using var conn = new SqlConnection(ConnectionString);
            conn.Open();

            using var cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Produtos;", conn);
            object? resultado = cmd.ExecuteScalar();   // vem como object: precisa converter.
            int total = Convert.ToInt32(resultado);
            Console.WriteLine($"Total de produtos cadastrados: {total}");
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Falha ao contar: {ex.Message}");
        }

        // --------------------------------------------------------
        //  Sub-topicos em arquivos separados (padrao hibrido s09):
        // --------------------------------------------------------

        CrudDemo.Executar();

        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }
}
