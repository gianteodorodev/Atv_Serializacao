using System.Text;
using System.Xml.Serialization;

[XmlRoot("RelatorioAnalise")]
public class RelatorioAnalise
{
    [XmlElement("amostra")]
    public DadosAmostra Amostra {get; set;} = new();

    [XmlElement("responsavel")]
    public ResponsavelTecnico Responsavel {get; set;} = new();

    [XmlArray("resultados")]
    [XmlArrayItem("componente")]
    public List<string> Resultados {get; set;} = new();
    
    public static void Executar()
    {
        string caminhoArquivo = "relatorio_amostra.xml";

        // criando objeto
        RelatorioAnalise relatorioOriginal = new RelatorioAnalise
        {
            Amostra = new DadosAmostra
            {
                idAmostra = "BIO-2026-8841",
                tipoMaterial = "Tecido Celular Hepático",
                dataColeta = new DateTime (2026, 11, 17, 09, 30, 00)
            },
            Responsavel = new ResponsavelTecnico
            {
                nomeCientista = "Dra. Helena Vasconcelos",
                registroProfissional = "CRBio 45982/04-D"
            },
            Resultados = new List<string>
            {
                "Proteína X (Alta concentração)",
                "Enzima Y (Atividade catalítica normal)",
                "Marcador Biomolecular Z-14",
                "Peptídeo Regulador Alfa"
            }
        };

        XmlSerializer serializer = new XmlSerializer(typeof(RelatorioAnalise));

        // serialização para xml
        using (StreamWriter writer = new StreamWriter(caminhoArquivo, false, Encoding.UTF8))
        {
            serializer.Serialize(writer, relatorioOriginal);
        }

        Console.WriteLine("Serialização XML concluída! Contéudo gerado em 'relatorio_amostra.xml':\n");
        Console.WriteLine(File.ReadAllText(caminhoArquivo, Encoding.UTF8));
        Console.WriteLine(new string('=', 60));

        // desserialização e simulação
        RelatorioAnalise? relatorioAuditado;
        using (StreamReader reader = new StreamReader(caminhoArquivo, Encoding.UTF8))
        {
            relatorioAuditado = (RelatorioAnalise?)serializer.Deserialize(reader);
        }
        
        if (relatorioAuditado != null)
        {
            Console.WriteLine("\n[AUDITORIA DO ÓRGÃO - ANVISA/MAPA]");
            Console.WriteLine($"ID da Amostra       : {relatorioAuditado.Amostra.idAmostra}");
            Console.WriteLine($"Material            : {relatorioAuditado.Amostra.tipoMaterial}");
            Console.WriteLine($"Data da Coleta      : {relatorioAuditado.Amostra.dataColeta}");
            Console.WriteLine($"Responsável Téc     : {relatorioAuditado.Responsavel.nomeCientista} ({relatorioAuditado.Responsavel.registroProfissional})");
            Console.WriteLine("Componentes Identificados");

            foreach (string componente in relatorioAuditado.Resultados)
            {
                Console.WriteLine($"  -{componente}");
            }

            Console.WriteLine("\nStatus da Auditoria: DOCUMENTAÇÃO ÍNTEGRA E CONFORME.");
        }
    }
}

// Duas classes auxiliares pra não criar muitos arquivos pequenos
public class DadosAmostra
{
    public string idAmostra {get; set;} = string.Empty;
    public string tipoMaterial{get; set;} = string.Empty;
    public DateTime dataColeta {get; set;}
}

public class ResponsavelTecnico
{
    public string nomeCientista {get; set;} = string.Empty;
    public string registroProfissional {get; set;} = string.Empty;
}