using System.Text.Json;
using ClosedXML.Excel;
using ConsultaPreco.ImportaDados.Models;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Importar Dados");
            Console.Write("Digite S para importar os dados:");
            string inputKey = Console.ReadLine();
            
            if (inputKey.ToUpper() == "S")
            {
                // Defina o caminho do arquivo Excel
                string excelFilePath = @"..\pesquisaPreco.xlsx";

                // Carregue os dados do Excel
                var produtos = ReadExcel(excelFilePath);

                // Chame a API para cada produto
                await SendProductsToApi(produtos);

                Console.WriteLine("Dados enviados para a API de produtos com sucesso.");
            }
            
            Console.Read();
        }

        static List<Produto> ReadExcel(string filePath)
        {
            List<Produto> produtos = new List<Produto>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); // Lê a primeira planilha

                var rows = worksheet.RowsUsed();
                foreach (var row in rows)
                {
                    var codigoBarras = row.Cell(1).Value.ToString();
                    var nome = row.Cell(3).Value.ToString();

                    if ((!string.IsNullOrEmpty(codigoBarras) || !string.IsNullOrEmpty(nome)) && (!codigoBarras.Equals("codigo")))
                    {
                        produtos.Add(new Produto { CodigoBarras = codigoBarras, Nome = nome });
                    }
                }
            }

            return produtos;
        }


        static async Task SendProductsToApi(List<Produto> produtos)
        {
            using (var httpClient = new HttpClient())
            {
                foreach (var produto in produtos)
                {
                    var json = JsonSerializer.Serialize(produto);
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync("https://localhost:7177/api/produtos", content);

                    if (!response.IsSuccessStatusCode)
                        Console.WriteLine($"Erro ao enviar o produto {produto.Nome} para a API: {response.StatusCode}");
                    else
                        Console.WriteLine($"Produto {produto.Nome} cadastrado com sucesso!");
                }
            }
        }
    }
}
