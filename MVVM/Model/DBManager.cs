using Microsoft.EntityFrameworkCore;
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
                context.SaveChanges();
            }

            vmToUpdate.UpdateSource();
        }

        public static void UpdateItemInDB<T>(T newObject, ViewModelBase vmToUpdate) where T : class
        {
            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                context.Update(newObject);
                context.SaveChanges();
            }

            vmToUpdate.UpdateSource();
        }

        public static void DeleteItemFromDB<T>(T item, ViewModelBase vmToUpdate) where T : class
        {
            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                context.Remove(item);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e) { }
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
                context.SaveChanges();

                points.ForEach(point =>
                {
                    point.AdresId = point.Adres.AdresId;
                    point.Adres = null;
                    context.PunktyTrasies.Attach(point);
                });

                context.SaveChanges();
            }

            vmToUpdate.UpdateSource();
        }
    }
}
