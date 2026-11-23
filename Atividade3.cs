using System.Data.SqlTypes;
using System.Globalization;
using System.Text;

public class LoteProducao
{
    public int numeroLote {get; set;}
    public string tipoLiga {get; set;} = string.Empty;
    public decimal pesoTotal{get; set;}
    public decimal temperaturaFundicao {get; set;}
    public string statusQualidade {get; set;} = string.Empty;

    public override string ToString()
    {
        return $"Lote #{numeroLote} | Liga: {tipoLiga,-16} | Peso: {pesoTotal,8:F2} kg | " +
               $"Temp: {temperaturaFundicao,6:F1} ºC | Status: {statusQualidade}";
    }

    public static void Executar()
    {
        string caminhoArquivo = "producao_turno.csv";

        // Lista com 5 objetos
        List<LoteProducao> lotesOriginais = new List<LoteProducao>
        {
            new LoteProducao { numeroLote = 1001, tipoLiga = "Aço Inox 304", pesoTotal = 1250.50m, temperaturaFundicao = 1450.0m, statusQualidade = "Aprovado" },
            new LoteProducao { numeroLote = 1002, tipoLiga = "Alumínio 6061", pesoTotal = 840.25m, temperaturaFundicao = 660.5m, statusQualidade = "Aprovado" },
            new LoteProducao { numeroLote = 1003, tipoLiga = "Ferro Fundido", pesoTotal = 2100.00m, temperaturaFundicao = 1210.8m, statusQualidade = "Em Análise" },
            new LoteProducao { numeroLote = 1004, tipoLiga = "Bronze Fosforoso", pesoTotal = 615.75m, temperaturaFundicao = 1020.4m, statusQualidade = "Aprovado" },
            new LoteProducao { numeroLote = 1005, tipoLiga = "Aço Carbono 1045", pesoTotal = 1780.90m, temperaturaFundicao = 1510.2m, statusQualidade = "Reprovado" },
        };

        // serialização para CSV
        List<string> linhasCSV = new List<string>
        {
            "numeroLote;tipoLiga;pesoTotal;temperaturaFundicao;statusQualidade"
        };

        foreach (LoteProducao lote in lotesOriginais)
        {
            string linha = string.Join(";",
            lote.numeroLote,
            lote.tipoLiga,
            lote.pesoTotal.ToString(CultureInfo.CurrentCulture),
            lote.temperaturaFundicao.ToString(CultureInfo.CurrentCulture),
            lote.statusQualidade
            );
            linhasCSV.Add(linha);
        }

        File.WriteAllLines(caminhoArquivo, linhasCSV, Encoding.UTF8);

        Console.WriteLine("Serialização CSV concluída! Conteúdo salvo em 'pruducao_turno.csv':\n");
        Console.WriteLine(File.ReadAllText(caminhoArquivo, Encoding.UTF8));
        Console.WriteLine(new string('-', 80));

        // Leitura do arquivo CSV, reconstrução dos objetos e cálculo de média de temperatura
        List<LoteProducao> lotesReconstruidos = new List<LoteProducao>();
        string[] linhasLidas = File.ReadAllLines(caminhoArquivo, Encoding.UTF8);

        for (int i = 1; i < linhasLidas.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(linhasLidas[i])) continue;

            string[] colunas = linhasLidas[i].Split(';');

            LoteProducao lote = new LoteProducao
            {
                numeroLote = int.Parse(colunas[0]),
                tipoLiga = colunas[1],
                pesoTotal = decimal.Parse(colunas[2], CultureInfo.CurrentCulture),
                temperaturaFundicao = decimal.Parse(colunas[3], CultureInfo.CurrentCulture),
                statusQualidade = colunas[4]
            };

            lotesReconstruidos.Add(lote);

        }

        Console.WriteLine("\nLotes reconstruídos na memória:");
        foreach (LoteProducao lote in lotesReconstruidos)
        {
            Console.WriteLine(lote);
        }

        if (lotesReconstruidos.Count > 0)
        {
            decimal mediaTemperatura = lotesReconstruidos.Average(l => l.temperaturaFundicao);
            Console.WriteLine($"\nTotal de lotes processados : {lotesReconstruidos.Count}");
            Console.WriteLine($"Média de temperatura : {mediaTemperatura:F2} ºC");
        }
    }
}