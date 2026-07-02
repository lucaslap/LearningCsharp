// ============================================================
//  CRUD com ADO.NET — Create, Read, Update, Delete
// ============================================================
//
//  CRUD sao as 4 operacoes basicas de qualquer banco:
//    CREATE  -> INSERT   (inserir linha)
//    READ    -> SELECT   (ler linhas)
//    UPDATE  -> UPDATE    (alterar linha existente)
//    DELETE  -> DELETE    (remover linha)
//
//
//  POR QUE PARAMETROS? (e nao concatenar string)
//  ─────────────────────────────────────────────
//  NUNCA monte SQL grudando o valor do usuario na string:
//      // ERRADO e PERIGOSO:
//      "INSERT INTO Produtos (Nome) VALUES ('" + nome + "')"
//  Se `nome` for  "'); DROP TABLE Produtos;--"  voce acabou de sofrer um
//  SQL INJECTION. A forma certa e usar um PLACEHOLDER (@nome) no texto e
//  passar o valor separado via SqlParameter. O driver trata o valor como
//  DADO, nunca como comando. Bonus: o banco reaproveita o plano de execucao.
//
// ============================================================

using Microsoft.Data.SqlClient;

namespace DatabaseLesson;

class CrudDemo
{
    public static void Executar()
    {
        Console.WriteLine("\n############### CRUD (CrudDemo.cs) ###############");

        try
        {
            // Uma conexao reaproveitada para toda a demo de CRUD.
            using var conn = new SqlConnection(Program.ConnectionString);
            conn.Open();

            Console.WriteLine("\n=== CREATE — INSERT com parametros ===\n");

            string sqlInsert = "INSERT INTO dbo.Produtos (Nome, Preco) VALUES (@nome, @preco);";
            using (var cmd = new SqlCommand(sqlInsert, conn))
            {
                // @nome e @preco no SQL viram parametros aqui. Valor != comando.
                cmd.Parameters.AddWithValue("@nome", "Teclado Mecanico");
                cmd.Parameters.AddWithValue("@preco", 299.90m);
                int linhas = cmd.ExecuteNonQuery();
                Console.WriteLine($"Inserido(s): {linhas} produto(s).");
            }

            Console.WriteLine("\n=== READ — SELECT com SqlDataReader ===\n");

            // O reader le linha a linha, so para frente. .Read() avanca e
            // devolve false quando acabam as linhas.
            using (var cmd = new SqlCommand("SELECT Id, Nome, Preco FROM dbo.Produtos ORDER BY Id;", conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Acesso por nome de coluda + Get tipado (evita boxing/cast solto).
                    int id = reader.GetInt32(reader.GetOrdinal("Id"));
                    string nome = reader.GetString(reader.GetOrdinal("Nome"));
                    decimal preco = reader.GetDecimal(reader.GetOrdinal("Preco"));
                    Console.WriteLine($"  #{id} - {nome,-20} R$ {preco:N2}");
                }
            }

            Console.WriteLine("\n=== UPDATE — alterando o preco ===\n");

            string sqlUpdate = "UPDATE dbo.Produtos SET Preco = @preco WHERE Nome = @nome;";
            using (var cmd = new SqlCommand(sqlUpdate, conn))
            {
                cmd.Parameters.AddWithValue("@preco", 279.90m);
                cmd.Parameters.AddWithValue("@nome", "Teclado Mecanico");
                int linhas = cmd.ExecuteNonQuery();
                Console.WriteLine($"Atualizado(s): {linhas} produto(s).");
            }

            Console.WriteLine("\n=== DELETE — removendo registros ===\n");

            string sqlDelete = "DELETE FROM dbo.Produtos WHERE Nome = @nome;";
            using (var cmd = new SqlCommand(sqlDelete, conn))
            {
                cmd.Parameters.AddWithValue("@nome", "Teclado Mecanico");
                int linhas = cmd.ExecuteNonQuery();
                Console.WriteLine($"Removido(s): {linhas} produto(s).");
            }
        }
        catch (SqlException ex)
        {
            // Sem servidor/banco disponivel, toda a demo cai aqui.
            Console.WriteLine($"Demo de CRUD nao executada (sem banco?): {ex.Message}");
        }
    }
}
