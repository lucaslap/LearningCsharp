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
//  9. OPERADORES ARITMÉTICOS
// ============================================================
Console.WriteLine("\n--- 9. Operadores Aritméticos ---");

int a = 10, b = 3;

Console.WriteLine($"  a = {a}, b = {b}");
Console.WriteLine($"  a + b  = {a + b}");   // adição
Console.WriteLine($"  a - b  = {a - b}");   // subtração
Console.WriteLine($"  a * b  = {a * b}");   // multiplicação
Console.WriteLine($"  a / b  = {a / b}");   // divisão inteira — trunca o decimal!
Console.WriteLine($"  a % b  = {a % b}");   // módulo (resto da divisão): 10 % 3 = 1

// Divisão com ponto flutuante preserva o decimal (F4 formata para 4 casas decimais)
double divisaoReal = (double)a / b;
Console.WriteLine($"  (double)a / b = {divisaoReal:F4}");

// Módulo é útil para verificar par/ímpar
Console.WriteLine($"\n  {a} é par? {a % 2 == 0}");
Console.WriteLine($"  {b} é par? {b % 2 == 0}");

// Incremento e decremento
int contador = 5;
Console.WriteLine($"\n  contador = {contador}");
Console.WriteLine($"  contador++ (pós): usa {contador++}, depois vira {contador}"); // usa 5, vira 6
Console.WriteLine($"  ++contador (pré): vira {++contador} antes de usar");          // vira 7 e usa 7
Console.WriteLine($"  contador-- (pós): usa {contador--}, depois vira {contador}"); // usa 7, vira 6
Console.WriteLine($"  --contador (pré): vira {--contador} antes de usar");          // vira 5 e usa 5

// ============================================================
//  10. OPERADORES DE COMPARAÇÃO
// ============================================================
Console.WriteLine("\n--- 10. Operadores de Comparação ---");

int x = 7, y = 10;
Console.WriteLine($"  x = {x}, y = {y}");
Console.WriteLine($"  x == y  → {x == y}");  // igual
Console.WriteLine($"  x != y  → {x != y}");  // diferente
Console.WriteLine($"  x >  y  → {x > y}");   // maior que
Console.WriteLine($"  x <  y  → {x < y}");   // menor que
Console.WriteLine($"  x >= y  → {x >= y}");  // maior ou igual
Console.WriteLine($"  x <= y  → {x <= y}");  // menor ou igual

// Exemplo prático: verificar se um valor está em um intervalo
int temperatura = 23;
bool confortavel = temperatura >= 18 && temperatura <= 26;
Console.WriteLine($"\n  Temperatura {temperatura}°C está confortável (18–26)? {confortavel}");

// ============================================================
//  11. OPERADORES LÓGICOS
// ============================================================
Console.WriteLine("\n--- 11. Operadores Lógicos ---");

bool temCNH     = true;
bool maiorIdade = true;
bool sobriedade = false;

// && (E): verdadeiro somente se AMBOS forem verdadeiros
Console.WriteLine($"  temCNH && maiorIdade          → {temCNH && maiorIdade}");
Console.WriteLine($"  temCNH && sobriedade           → {temCNH && sobriedade}");

// || (OU): verdadeiro se PELO MENOS UM for verdadeiro
Console.WriteLine($"  maiorIdade || sobriedade       → {maiorIdade || sobriedade}");
Console.WriteLine($"  sobriedade || !temCNH          → {sobriedade || !temCNH}");

// ! (NÃO): inverte o valor lógico
Console.WriteLine($"  !temCNH                        → {!temCNH}");
Console.WriteLine($"  !sobriedade                    → {!sobriedade}");

// Combinação realista
bool podeConduzir = temCNH && maiorIdade && !sobriedade;
Console.WriteLine($"\n  Pode conduzir (CNH + idade + sóbrio)? {podeConduzir}");

// Curto-circuito: C# para de avaliar assim que o resultado é certo
// Em &&: se o primeiro for false, o segundo NÃO é avaliado
// Em ||: se o primeiro for true,  o segundo NÃO é avaliado

// ============================================================
//  12. OPERADORES DE ATRIBUIÇÃO
// ============================================================
Console.WriteLine("\n--- 12. Operadores de Atribuição ---");

int pontos = 100;
Console.WriteLine($"  pontos = {pontos}");

pontos += 20;  // equivale a: pontos = pontos + 20
Console.WriteLine($"  pontos += 20  → {pontos}");

pontos -= 15;  // equivale a: pontos = pontos - 15
Console.WriteLine($"  pontos -= 15  → {pontos}");

pontos *= 2;   // equivale a: pontos = pontos * 2
Console.WriteLine($"  pontos *= 2   → {pontos}");

pontos /= 3;   // equivale a: pontos = pontos / 3
Console.WriteLine($"  pontos /= 3   → {pontos}");

pontos %= 7;   // equivale a: pontos = pontos % 7
Console.WriteLine($"  pontos %= 7   → {pontos}");

// ??= — atribui somente se o valor for null (null-coalescing assignment)
string? apelido = null;
apelido ??= "Sem apelido"; // atribui porque é null
Console.WriteLine($"\n  apelido ??= \"Sem apelido\" → \"{apelido}\"");

apelido ??= "Outro valor"; // NÃO atribui porque já tem valor
Console.WriteLine($"  apelido ??= \"Outro valor\"  → \"{apelido}\" (não mudou)");

// ============================================================
//  13. OPERADOR TERNÁRIO
// ============================================================
Console.WriteLine("\n--- 13. Operador Ternário ---");

// Sintaxe: condição ? valorSeVerdadeiro : valorSeFalso
// Substitui um if/else simples em uma única linha

int idade = 20;
string acesso = idade >= 18 ? "Permitido" : "Negado";
Console.WriteLine($"  idade = {idade} → acesso: {acesso}");

double saldo = -50.0;
string situacao = saldo >= 0 ? "Positivo" : "Negativo";
Console.WriteLine($"  saldo = {saldo} → situação: {situacao}");

// Ternários podem ser encadeados, mas prejudicam a legibilidade
int nota = 75;
string conceito = nota >= 90 ? "A" : nota >= 70 ? "B" : nota >= 50 ? "C" : "F";
Console.WriteLine($"  nota = {nota} → conceito: {conceito}");

// ============================================================
//  14. ORDEM DE AVALIAÇÃO (PRECEDÊNCIA DE OPERADORES)
// ============================================================
Console.WriteLine("\n--- 14. Ordem de Avaliação (Precedência) ---");

// Precedência (do mais alto ao mais baixo):
//  1. ()                        ← parênteses — força a ordem
//  2. ++ -- (pós-fixo), !       ← unários
//  3. * / %                     ← multiplicativos
//  4. + -                       ← aditivos
//  5. < > <= >=                 ← relacionais
//  6. == !=                     ← igualdade
//  7. &&                        ← E lógico
//  8. ||                        ← OU lógico
//  9. ??                        ← null-coalescing
// 10. ?:                        ← ternário
// 11. = += -= *= /= %= ??=      ← atribuição (da direita para a esquerda)

// Exemplo clássico de pegadinha:
int semParenteses  = 2 + 3 * 4;   // 3*4=12, depois 2+12 = 14  (NÃO 20!)
int comParenteses  = (2 + 3) * 4; // 2+3=5,  depois 5*4  = 20
Console.WriteLine($"  2 + 3 * 4   = {semParenteses}  ← * tem precedência sobre +");
Console.WriteLine($"  (2 + 3) * 4 = {comParenteses}  ← () forçam a soma primeiro");

// Combinação de lógicos e comparações
int v = 5;
bool r1 = v > 3 && v < 10;  // (v>3) && (v<10) → true && true  → true
bool r2 = v > 3 || v > 10;  // (v>3) || (v>10) → true || false → true
bool r3 = v > 3 && v > 10;  // (v>3) && (v>10) → true && false → false
Console.WriteLine($"\n  v = {v}");
Console.WriteLine($"  v > 3 && v < 10  → {r1}");
Console.WriteLine($"  v > 3 || v > 10  → {r2}");
Console.WriteLine($"  v > 3 && v > 10  → {r3}");

// Associatividade da esquerda para a direita nos aditivos
int assoc = 10 - 3 - 2; // (10-3)-2 = 7-2 = 5  (NÃO 10-(3-2) = 9)
Console.WriteLine($"\n  10 - 3 - 2 = {assoc}  ← subtração associa da esquerda para a direita");

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
Console.WriteLine("  Aritméticos:  + - * / % ++ --");
Console.WriteLine("  Comparação:   == != > < >= <=");
Console.WriteLine("  Lógicos:      && || !");
Console.WriteLine("  Atribuição:   = += -= *= /= %= ??=");
Console.WriteLine("  Ternário:     condição ? a : b");
Console.WriteLine("  Precedência:  * / antes de + -  →  use () para forçar a ordem");
Console.WriteLine("\nDica de ouro:");
Console.WriteLine("  • int     → inteiros do dia a dia");
Console.WriteLine("  • double  → decimais gerais");
Console.WriteLine("  • decimal → DINHEIRO (sem erros de arredondamento)");
Console.WriteLine("  • string  → texto");
Console.WriteLine("  • bool    → condições verdadeiro/falso");
Console.ReadKey(); // Espera o usuário pressionar uma tecla antes de fechar o console.