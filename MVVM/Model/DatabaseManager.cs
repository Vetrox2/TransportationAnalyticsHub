using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationAnalyticsHub.MVVM.Model.DBModels;

namespace TransportationAnalyticsHub.MVVM.Model
{
    public class DatabaseManager
    {
        public RozliczeniePrzejazdowSamochodowCiezarowychContext DBContext;

        public DatabaseManager()
        {
           
        }
    }
}
