using System;
using System.Linq;
using Services.Domain;
using System.IO;

namespace Services.DataContext
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            //It's use for keep all data that's created 1st build


            return;
            /*
           context.Database.EnsureDeleted();

           context.Database.EnsureCreated();

           //Insert Role Data
           GenerateRoleDataInDb(context);

           //Insert User Data 
           GenerateUserDataInDb(context);

           //Insert User company Data
           GenerateUserCopmanyDataInDb(context);

           //Insert Payments Data
           GeneratePaymentsInformationDataInDb(context);

           //Insert Locaton Of Use Data
           GenerateLocationOfUseDataInDb(context);

           //Insert Duration Of Exposure Data
           GenerateDurationOfExposureDataInDb(context);

           //Insert Frequency Of Use Data
           GenerateFrequencyOfUseDataInDb(context);

           //Insert Number Of Person Exposed Data
           GenerateNumberOfPersonsExposedDataInDb(context);

           //Insert Susceptible Workers Data
           GenerateSusceptibleWorkersDataInDb(context);

           //Insert COSHHSubstanceImages Data
           GenerateCOSHHSubstanceImagesDataInDb(context);

           //Insert Hazard Statement Data
           //GenerateHazardStatementsDataInDb(context);

           //Insert Precaution Data
           //GeneratePrecautionsDataInDb(context);

           //Insert COSHH Substance Data 
           GenerateCOSHHSubstanceDataInDb(context);

           //Insert COSHH Engineering Controls Data 
           GenerateCOSHHEngineeringControlsDataInDb(context);


           //COSHH Activity Contaminants Data
           GenerateCOSHHActivityContaminantsDataInDb(context);

           //Insert MethodType Data
           GenerateMethodTypeDataInDb(context);

           GenerateCOSHHActivityHealthSurveillancesDataInDb(context);

           InsertDataFromSQLScript(context);

           GenerateImageTypeDataInDb(context);

            GeneratePersonsAtRiskTypeDataInDb(context);
           */
        }


        private static void InsertDataFromSQLScript(AppDbContext context)
        {

            TimeSpan timeSpan = new TimeSpan(0, 50, 0);
            
            //context.Database.SetCommandTimeout(timeSpan);

            var sqlScriptFileList = Directory.GetFiles("SQL-Scripts", "*.sql").OrderBy(x => x);

            foreach (string sqlFile in sqlScriptFileList)
            {
                string sqlText = File.ReadAllText(sqlFile).Trim();
                string[] scriptTextSplit = sqlText.Split(new string[] { "GO\r\n", "GO ", "GO\t" }, StringSplitOptions.RemoveEmptyEntries);
                string appendString = "";

                foreach (string splitScript in scriptTextSplit)
                {
                    appendString += splitScript;

                }
                context.Database.ExecuteSqlCommand(appendString);
            }



        }


        private static void GenerateUserDataInDb(AppDbContext context)
        {
            if (context.Users.Any())
            {
                return; // DB has been seeded
            }

            for (int counter = 0; counter < 2; counter++)
            {
                User userToInsert = new User();

                if (counter == 0)
                {
                    //userToInsert.UserRoleId = 2;
                    userToInsert.Name = "Jhon";

                }

                if (counter == 1)
                {
                    //userToInsert.UserRoleId = 2;
                    userToInsert.Name = "Sample";

                }
                context.Users.Add(userToInsert);
                context.SaveChanges();

            }

        }//end function



        


    }//end class


}//end namespace