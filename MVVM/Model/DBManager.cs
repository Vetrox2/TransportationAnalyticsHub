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
    }
}
