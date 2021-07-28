using System.Collections.Generic;
using serio_cli.Models;

namespace serio_cli.Interfaces
{
    public interface ISeriesRepo
    {
         List<SerieModel> GetAllSeries();
         void AddSerie(SerieModel serieModel);
         SerieModel SelectSerie(int id);
         void RemoveSerie(int id);
         void UpdateSerie(SerieModel serieModel,int id);


    }
}