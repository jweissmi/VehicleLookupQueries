using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleLookupQueries
{
    class Program
    {
        static void Main(string[] args)
        {
            VehicleConfigDataContext VCdb = new VehicleConfigDataContext();

            if (!VCdb.DatabaseExists())
                throw new Exception();

            Console.WriteLine();
            Console.WriteLine("Years:");

            //Get Years
            var syear = (from yr in VCdb.Years
                         where yr.YearID >= 1990
                         select yr.YearID).Distinct().OrderByDescending(yr => yr);

            foreach (var yr in syear)
            {
                Console.WriteLine(yr);
            }
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Makes for Selected Year:");
            //Get Makes for Selected Year
            var yearmake = (from make in VCdb.Makes
                            join bv in VCdb.BaseVehicles on make.MakeID equals bv.MakeID
                            where bv.YearID == 2015
                            select make).Distinct();

            foreach (var make in yearmake)
            {
                Console.WriteLine(make.MakeName);
            }

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Models for Selected Year and Make:");
            //Get Models for Selected Year and Make
            var yrmakemod = (from mak in VCdb.Makes
                             join bvi in VCdb.BaseVehicles on mak.MakeID equals bvi.MakeID
                             join mod in VCdb.Models on bvi.ModelID equals mod.ModelID
                             where bvi.YearID == 2015 && mak.MakeName == "Ford"
                             select mod).Distinct();

            foreach (var mod in yrmakemod)
            {
                Console.WriteLine(mod.ModelName);
            }

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Distinct Year, Make, Model and BaseVehicleID:");
            //Get BaseVehicleID for Selected Year, Make, and Model
            var yrmakemodbvid = (from mak in VCdb.Makes
                                 join bvi in VCdb.BaseVehicles on mak.MakeID equals bvi.MakeID
                                 join mod in VCdb.Models on bvi.ModelID equals mod.ModelID
                                 orderby mod.ModelName
                                 where bvi.YearID == 2015 && mak.MakeName == "Ford" && mod.ModelName == "Fusion"
                                 select new { bvi.YearID, mak.MakeName, mod.ModelName, bvi.BaseVehicleID }).Distinct();

            foreach (var bvi in yrmakemodbvid)
            {
                Console.WriteLine(bvi.YearID.ToString() + bvi.MakeName + bvi.ModelName + bvi.BaseVehicleID);
            }


            Console.ReadLine();
        }
    }
}
