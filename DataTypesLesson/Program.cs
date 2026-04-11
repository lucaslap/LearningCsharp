// ============================================================
//  C# - Tipos de Dados e Variáveis (Data Types & Variables)
// ============================================================
//
//  Uma VARIÁVEL é um espaço nomeado na memória que armazena um valor.
//  Em C#, toda variável tem um TIPO que define quais valores ela pode guardar.
//
//  Sintaxe básica:
//      tipo nomeDaVariavel = valor;
//
// ============================================================

Console.WriteLine("=== TIPOS DE DADOS EM C# ===\n");

// ============================================================
//  1. TIPOS INTEIROS (números sem casa decimal)
// ============================================================
Console.WriteLine("--- 1. Tipos Inteiros ---");

byte   myByte   = 255;              // 0 a 255                    (8 bits, sem sinal)
sbyte  mySbyte  = -128;             // -128 a 127                 (8 bits, com sinal)
short  myShort  = -32_000;          // -32.768 a 32.767           (16 bits)
ushort myUshort = 65_000;           // 0 a 65.535                 (16 bits, sem sinal)
int    myInt    = -2_000_000;       // ~-2 bilhões a +2 bilhões   (32 bits) ← o mais usado
uint   myUint   = 4_000_000;        // 0 a ~4 bilhões             (32 bits, sem sinal)
long   myLong   = -9_000_000_000L;  // número enorme              (64 bits) ← use 'L'
ulong  myUlong  = 18_000_000_000UL; // positivo enorme            (64 bits) ← use 'UL'

Console.WriteLine($"  byte:   {myByte}");
Console.WriteLine($"  sbyte:  {mySbyte}");
Console.WriteLine($"  short:  {myShort}");
Console.WriteLine($"  ushort: {myUshort}");
Console.WriteLine($"  int:    {myInt}");
Console.WriteLine($"  uint:   {myUint}");
Console.WriteLine($"  long:   {myLong}");
Console.WriteLine($"  ulong:  {myUlong}");

Console.WriteLine($"\n  int mínimo:  {int.MinValue}");
Console.WriteLine($"  int máximo:  {int.MaxValue}");
Console.WriteLine($"  long mínimo: {long.MinValue}");
Console.WriteLine($"  long máximo: {long.MaxValue}");

// ============================================================
//  2. TIPOS DE PONTO FLUTUANTE (números com casa decimal)
// ============================================================
Console.WriteLine("\n--- 2. Ponto Flutuante ---");

float   meuFloat   = 3.14f;       // ~6-7 dígitos de precisão   ← use 'f' no final
double  meuDouble  = 3.141592653; // ~15-16 dígitos de precisão ← padrão em C#
decimal meuDecimal = 19.99m;      // 28-29 dígitos, sem erro de arredondamento ← use 'm'

Console.WriteLine($"  float:   {meuFloat}");
Console.WriteLine($"  double:  {meuDouble}");
Console.WriteLine($"  decimal: {meuDecimal}");

// DICA: Use decimal para valores monetários (dinheiro, preços)
decimal preco      = 9.99m;
decimal desconto   = 0.10m;
decimal precoFinal = preco - (preco * desconto);
Console.WriteLine($"\n  Preço com 10% de desconto: R$ {precoFinal:F2}");

// ============================================================
//  3. TEXTO E CARACTERE
// ============================================================
Console.WriteLine("\n--- 3. Texto e Caractere ---");

char   meuChar   = 'A';           // Um único caractere — aspas SIMPLES
string meuString = "Olá, mundo!"; // Texto — aspas DUPLAS

Console.WriteLine($"  char:   {meuChar}");
Console.WriteLine($"  string: {meuString}");

string nome = "  Lucas  ";
Console.WriteLine($"\n  Original:    '{nome}'");
Console.WriteLine($"  Maiúsculo:   '{nome.ToUpper()}'");
Console.WriteLine($"  Minúsculo:   '{nome.ToLower()}'");
Console.WriteLine($"  Sem espaços: '{nome.Trim()}'");
Console.WriteLine($"  Comprimento: {nome.Length} caracteres (incluindo espaços)");

// Concatenação e interpolação
string sobrenome = "Pereira";
string completo1 = "Olá, " + nome.Trim() + " " + sobrenome + "!"; // concatenação com +
string completo2 = $"Olá, {nome.Trim()} {sobrenome}!"; // interpolação com $
Console.WriteLine($"\n  Concatenação: {completo1}");
Console.WriteLine($"  Interpolação: {completo2}");

// String verbatim — ignora barras invertidas com @
string caminho1 = "C:\\Users\\Lucas\\Documentos"; // forma normal
string caminho2 = @"C:\Users\Lucas\Documentos";   // forma verbatim com @
Console.WriteLine($"\n  Caminho normal:   {caminho1}");
Console.WriteLine($"  Caminho verbatim: {caminho2}");

// ============================================================
//  4. BOOLEANO (verdadeiro ou falso)
// ============================================================
Console.WriteLine("\n--- 4. Booleano ---");

bool estaLogado   = true;
bool maiorDeIdade = false;
bool resultado    = estaLogado && !maiorDeIdade; // && = E lógico, ! = negação

Console.WriteLine($"  estaLogado:   {estaLogado}");
Console.WriteLine($"  maiorDeIdade: {maiorDeIdade}");
Console.WriteLine($"  Logado E não maior de idade: {resultado}");

// ============================================================
//  5. TIPO IMPLÍCITO COM 'var'
// ============================================================
Console.WriteLine("\n--- 5. Tipo Implícito (var) ---");

// 'var' faz o C# inferir o tipo automaticamente em tempo de compilação.
var numero   = 42;      // inferido como int
var pi       = 3.14;    // inferido como double
var mensagem = "Oi!";  // inferido como string
var ativo    = true;    // inferido como bool

Console.WriteLine($"  var numero   = {numero}  → {numero.GetType().Name}");
Console.WriteLine($"  var pi       = {pi}  → {pi.GetType().Name}");
Console.WriteLine($"  var mensagem = \"{mensagem}\" → {mensagem.GetType().Name}");
Console.WriteLine($"  var ativo    = {ativo}  → {ativo.GetType().Name}");

// ============================================================
//  6. TIPOS ANULÁVEIS (Nullable Types)
// ============================================================
Console.WriteLine("\n--- 6. Tipos Anuláveis (Nullable) ---");

// Tipos de valor (int, bool…) não aceitam null por padrão.
// Adicionando '?' você permite que sejam null.
int?    idadeOpcional  = null;
double? alturaOpcional = 1.75;

Console.WriteLine($"  idadeOpcional:  {idadeOpcional ?? 0} (null → usa 0 pelo operador ??)");
Console.WriteLine($"  alturaOpcional: {alturaOpcional}");
Console.WriteLine($"  Tem valor?      {alturaOpcional.HasValue}");

// ============================================================
//  7. CONSTANTES
// ============================================================
Console.WriteLine("\n--- 7. Constantes ---");

// 'const' define um valor que NUNCA muda após a compilação.
const double PI            = 3.14159265358979;
const int    MAX_TENTATIVAS = 3;

Console.WriteLine($"  const PI:             {PI}");
Console.WriteLine($"  const MAX_TENTATIVAS: {MAX_TENTATIVAS}");
// PI = 3; ← isso causaria ERRO de compilação!

// ============================================================
//  8. CONVERSÃO DE TIPOS (Type Casting)
// ============================================================
Console.WriteLine("\n--- 8. Conversão de Tipos ---");

// Implícita — automática, sem perda de dados (menor → maior)
int    inteiro    = 100;
double automatico = inteiro; // int cabe em double sem problema
Console.WriteLine($"  int → double implícito: {automatico}");

// Explícita — cast manual, pode perder dados (maior → menor)
double grande  = 9.99;
int    cortado = (int)grande; // perde a parte decimal!
Console.WriteLine($"  double → int explícito: {grande} vira {cortado} (perde .99)");

// Parse — converte string em número (lança exceção se inválido)
string textoNumero = "123";
int    convertido  = int.Parse(textoNumero);
Console.WriteLine($"  Parse: \"{textoNumero}\" → {convertido + 1}");

// TryParse — conversão segura, não lança exceção se falhar
string textoInvalido = "abc";
bool   sucesso = int.TryParse(textoInvalido, out int resultado2);
Console.WriteLine($"  TryParse \"{textoInvalido}\": sucesso={sucesso}, valor={resultado2}");

// Convert.ToDouble com cultura invariante (evita problema do separador decimal)
string  textoDouble      = "3.14";
double  convertidoDouble = Convert.ToDouble(textoDouble, System.Globalization.CultureInfo.InvariantCulture);
Console.WriteLine($"  Convert: \"{textoDouble}\" → {convertidoDouble}");

// ============================================================
//  RESUMO
// ============================================================
Console.WriteLine("\n=== RESUMO ===");
Console.WriteLine("  Inteiros:   byte, sbyte, short, ushort, int, uint, long, ulong");
Console.WriteLine("  Decimais:   float (f), double, decimal (m)");
Console.WriteLine("  Texto:      char (aspas simples), string (aspas duplas)");
Console.WriteLine("  Lógico:     bool (true / false)");
Console.WriteLine("  Implícito:  var");
Console.WriteLine("  Anulável:   tipo? (ex: int?)");
Console.WriteLine("  Imutável:   const");
Console.WriteLine("\nDica de ouro:");
Console.WriteLine("  • int     → inteiros do dia a dia");
Console.WriteLine("  • double  → decimais gerais");
Console.WriteLine("  • decimal → DINHEIRO (sem erros de arredondamento)");
Console.WriteLine("  • string  → texto");
Console.WriteLine("  • bool    → condições verdadeiro/falso");
Console.ReadKey(); // Espera o usuário pressionar uma tecla antes de fechar o console.