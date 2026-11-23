# Atividade de Serialização em C#

Projeto desenvolvido como atividade do curso técnico de **Desenvolvimento de Sistemas** do **Senai Centro4.0**.

O objetivo é demonstrar os conceitos de **serialização** e **desserialização** de dados em três formatos diferentes:

- JSON
- XML
- CSV

## Funcionalidades

### Atividade 1 - JSON

A classe `DroneStatus` cria e serializa os dados de um drone para o arquivo `status_drone.json`. Em seguida, o arquivo é lido e os dados são reconstruídos em um objeto.

### Atividade 2 - XML

A classe `RelatorioAnalise` cria um relatório de análise laboratorial e o serializa para o arquivo `relatorio_amostra.xml`. Os dados são desserializados e exibidos como resultado da auditoria.

### Atividade 3 - CSV

A classe `LoteProducao` cria cinco lotes de produção e os registra no arquivo `producao_turno.csv`, usando ponto e vírgula como separador. O programa lê o arquivo, reconstrói os objetos e calcula a média das temperaturas de fundição.

## Estrutura do projeto

```text
Atv_Serializacao/
|-- Program.cs
|-- Atividade1.cs
|-- Atividade2.cs
|-- Atividade3.cs
|-- status_drone.json
|-- relatorio_amostra.xml
|-- producao_turno.csv
|-- Atv_Serializacao.csproj
`-- README.md
```

## Tecnologias utilizadas

- C#
- .NET
- `System.Text.Json`
- `System.Xml.Serialization`
- `System.IO`
- `System.Globalization`

## Pré-requisitos

- .NET SDK 10.0 ou superior
- Visual Studio, Visual Studio Code ou outro editor compatível

## Como executar

1. Abra o terminal na pasta do projeto.
2. Execute o comando:

```bash
dotnet run
```

Por padrão, o projeto executa a **Atividade 3 - CSV**, definida em `Program.cs`.

Para executar outra atividade, comente a chamada atual e descomente uma das outras opções:

```csharp
class Program
{
    static void Main()
    {
        // DroneStatus.Executar();        // Atividade 1: JSON
        // RelatorioAnalise.Executar();   // Atividade 2: XML
        LoteProducao.Executar();          // Atividade 3: CSV
    }
}
```

## Arquivos gerados

- `status_drone.json`: dados serializados do drone.
- `relatorio_amostra.xml`: relatório laboratorial serializado em XML.
- `producao_turno.csv`: registro dos lotes de produção em CSV.
