using CsvHelper.Configuration;
using GerenciamentoPatrimonio.DTOs.PatrimonioDto;
using System.Runtime.InteropServices;


namespace GerenciamentoPatrimonio.Applications.Mapeamentos
{
    //ClassMap -> é tipo um "tradutor de colunas", define como ler o csv
    public class ImportarPatrimonioCsvMap : ClassMap<ImportarPatrimonioCsvDto>
    {
        public ImportarPatrimonioCsvMap()
        {
            // map -> escolhe a propriedade da DTO
            // Name -> diz qual o nome da coluna do CSV para essa propriedade
            Map(m => m.NumeroPatrimonio).Name("N° invent.");
            Map(m => m.Denominacao).Name("Denominacao do imobilizado");
            Map(m => m.DataIncorporacao).Name("Dt.incorp.");
            Map(m => m.ValorAquisicao).Name("ValAquis.");
        }
    }
}
