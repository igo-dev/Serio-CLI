using System.Collections.Generic;
using serio_cli.Interfaces;
using serio_cli.Models;
using System.Linq;
using System;

namespace serio_cli.Repositories
{
    public class SeriesRepo : ISeriesRepo
    {
        private List<SerieModel> listaSeries = new List<SerieModel>(); 

        public void AddSerie(SerieModel serieModel)
        {
            serieModel.Id = listaSeries.Count+1;
            listaSeries.Add(serieModel);
        }

        public List<SerieModel> GetAllSeries()
        {
            return listaSeries;
        }

        public void RemoveSerie(int id)
        {
            
            listaSeries.Remove(listaSeries.Find(o => o.Id == id));
        }

        public SerieModel SelectSerie(int id)
        {
            return listaSeries.Find(o => o.Id == id);
        }

        public void UpdateSerie(SerieModel serieNew, int id)
        {
            var serieOld = SelectSerie(id);
            if(serieNew.Title != ""){serieOld.Title = serieNew.Title;}
            else if(serieNew.Year !=  0){serieOld.Year = serieNew.Year;}
            else if(Enum.IsDefined<Genre>(serieNew.Genre))
            {
                serieOld.Genre = serieNew.Genre;
            }
        }

    }
}