using TamagochiCSharp;

Console.WriteLine("Main!");

try
{
    // Código que pode dar erro
    Criatura bixin = new Criatura("TonTON");

    bixin.ExibirNome();

    bixin.DefinirNome("");
}
catch (ArgumentException ex)
{
    // O que fazer se o erro acontecer
    Console.WriteLine($"Erro de validação: {ex.Message}");
}
catch (Exception ex)
{
    // Captura qualquer outro tipo de erro genérico
    Console.WriteLine($"Erro inesperado: {ex.Message}");
}