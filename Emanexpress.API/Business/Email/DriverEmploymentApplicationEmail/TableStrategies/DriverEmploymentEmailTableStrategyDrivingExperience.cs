using Emanexpress.API.DataTransferObjects;
using System;

namespace Emanexpress.API.Business.Email
{
    public class DriverEmploymentEmailTableStrategyDrivingExperience : IDriverEmploymentEmailTableStrategy
    {
        public DriverEmploymentApplicationEmailTableType emailTableType => DriverEmploymentApplicationEmailTableType.DrivingExperience;

        public EmailTable GetEmailTable(DtoDriverEmploymentApplication driverEmploymentApplication)
        {
            var drivingExperienceTable = new EmailTable("Driving Experience");

            if(driverEmploymentApplication.DrivingExperience != null)
            {
                foreach(var drivingExperience in driverEmploymentApplication.DrivingExperience)
                {                  
                    var name = new EmailRowFieldTable("Equipment Name", drivingExperience.Name, 30);
                    var typeOfEquipment = new EmailRowFieldTable("Type Of Equipment", drivingExperience.TypeOfEquipment, 30);
                    var from = new EmailRowFieldTable("From Mo/Year", drivingExperience.FromMonthYear,12);
                    var to = new EmailRowFieldTable("To Mo/Year",drivingExperience.ToMonthYear, 12);
                    var aproxMiles = new EmailRowFieldTable("Aprox. Miles",drivingExperience.AproxMiles, 15);
                    drivingExperienceTable.AddRow(name, typeOfEquipment, from, to, aproxMiles);
                }
            }

            drivingExperienceTable.TitleSeparator("");

            var statesOperatedInLastFourYears = new EmailRowFieldTable("States operated in for last five years", driverEmploymentApplication.StatesOperatedForLastYears);            
            drivingExperienceTable.AddRow(statesOperatedInLastFourYears);

            var specialCourses = new EmailRowFieldTable("Special courses or training that will help you as driver", driverEmploymentApplication.SpecialCoursesOfTraining);            
            drivingExperienceTable.AddRow(specialCourses);

            var safeDrivingAwards = new EmailRowFieldTable("Which safe driving awards do you hold and from whom?", driverEmploymentApplication.SafeDrivingAwards);            
            drivingExperienceTable.AddRow(safeDrivingAwards);
            
            return drivingExperienceTable;
        }       
    }
}