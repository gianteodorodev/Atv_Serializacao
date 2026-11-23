using System.Runtime.InteropServices;
using System.Text.Json;
using Microsoft.VisualBasic;

public class DroneStatus
{
    public string codigoIdentificador {get;set;}
    public int nivelBateria {get;set;}
    public double latitude {get;set;}
    public double longitude {get;set;}
    public bool emMissao{get;set;}

    public override string ToString()
    {
        return $"Drone [{codigoIdentificador}] | Bateria: {nivelBateria}% " + 
               $"Coordenadas: ({latitude}, {longitude}) | Em missão: {emMissao}";
    }

    public static void Executar()
    {
        string caminhoArquivo = "status_drone.json";

        // instanciando dados fictícios
        DroneStatus droneOriginal = new DroneStatus
        {
            codigoIdentificador = "DRN-2026-BR",
            nivelBateria = 87,
            latitude = -19.9321,
            longitude = -44.0539,
            emMissao = true
        };

        var opcoesJson = new JsonSerializerOptions {WriteIndented = true};

        // serialização
        string jsonString = JsonSerializer.Serialize(droneOriginal, opcoesJson);
        File.WriteAllText(caminhoArquivo, jsonString);

        Console.WriteLine("Serialização concluída! Arquivo 'status_drone.json' gerado:\n");
        Console.WriteLine(jsonString);
        Console.WriteLine(new string('-', 50));

        // desserialização e reconstruindo objeto 
        string jsonLido = File.ReadAllText(caminhoArquivo);
        DroneStatus? droneReconstruido = JsonSerializer.Deserialize<DroneStatus>(jsonLido);

        if(droneReconstruido != null)
        {
        Console.WriteLine("Objeto reconstruído na memória:");
        Console.WriteLine(droneReconstruido);
        Console.WriteLine($"Classe instanciada; {droneReconstruido.GetType().Name}");
        }
    }
}