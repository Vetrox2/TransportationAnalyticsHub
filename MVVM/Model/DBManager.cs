using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model.DBModels;

namespace TransportationAnalyticsHub.MVVM.Model
{
    public static class DBManager
    {
        public static void AddNewItemToDB<T>(T newObject, ViewModelBase vmToUpdate) where T : class
        {
            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                context.Add(newObject);
                SaveChanges(context);
            }

            vmToUpdate.UpdateSource();
        }

        public static void UpdateItemInDB<T>(T newObject, ViewModelBase vmToUpdate) where T : class
        {
            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                context.Update(newObject);
                SaveChanges(context);
            }

            vmToUpdate.UpdateSource();
        }

        public static void DeleteItemFromDB<T>(T item, ViewModelBase vmToUpdate) where T : class
        {
            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                context.Remove(item);
                SaveChanges(context);
            }

            vmToUpdate.UpdateSource();
        }

        public static void ReplaceRidePointsInDB(List<PunktyTrasy> points, ViewModelBase vmToUpdate)
        {
            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                var pointsToRemove = context.PunktyTrasies.Where(p => p.PrzejazdId == points[0].Przejazd.PrzejazdId).ToList();
                pointsToRemove.ForEach(point =>
                {
                    context.Remove(point);
                });
                SaveChanges(context);

                points.ForEach(point =>
                {
                    point.AdresId = point.Adres.AdresId;
                    point.Adres = null;
                    context.PunktyTrasies.Attach(point);
                });

                SaveChanges(context);
            }

            vmToUpdate.UpdateSource();
        }
        
        private static void SaveChanges(RozliczeniePrzejazdowSamochodowCiezarowychContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch(Exception e) 
            {
                MessageBox.Show("Nie udało się zapisać zmian: " + e.Message);
            }
        }
    }
}
