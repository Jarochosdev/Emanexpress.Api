using Emanexpress.API.Business.Email.Common.HtmlStructure;
using Emanexpress.API.Converter;
using Emanexpress.API.DataTransferObjects;
using System;

namespace Emanexpress.API.Business.Email
{
    public class DriverEmploymentEmailTableStrategyExperienceQualifications : IDriverEmploymentEmailTableStrategy
    {
        public DriverEmploymentApplicationEmailTableType emailTableType => DriverEmploymentApplicationEmailTableType.ExperienceQualifications;

        public ConverterHelper ConverterHelper { get; }

        public DriverEmploymentEmailTableStrategyExperienceQualifications(ConverterHelper converterHelper)
        {
            ConverterHelper = converterHelper;
        }

        public EmailTable GetEmailTable(DtoDriverEmploymentApplication driverEmploymentApplication)
        {
            var applicatInformationTable = new EmailTable("Experience, Qualifications and Education");
            
            var otherExperience = new EmailRowFieldTable("Show any trucking, transportation or other experience that may help in your work for this company", 
                                                    driverEmploymentApplication.OtherExperience);
            applicatInformationTable.AddRow(otherExperience);            

            var coursesAndTraining = new EmailRowFieldTable("List courses and training other than shown elsewere in this application", driverEmploymentApplication.CoursesAndTraining);            
            applicatInformationTable.AddRow(coursesAndTraining);            

            var specialEquipment = new EmailRowFieldTable("List special equipment or technical materials you can work with (other than those already shown)", driverEmploymentApplication.SpecialEquipment);            
            applicatInformationTable.AddRow(specialEquipment);            
            
            var gradecompleted = new EmailRowFieldTable("Highest grade completed", driverEmploymentApplication.HighestGradeCompleted);
            var highschool = new EmailRowFieldTable("High School", driverEmploymentApplication.HighSchool);
            var college = new EmailRowFieldTable("College", driverEmploymentApplication.CollegeLevel);
            applicatInformationTable.AddRow(gradecompleted, highschool, college);
            
            var lastAttendedSchool = new EmailRowFieldTable("Last school attended", driverEmploymentApplication.LastAttendedSchool);
            applicatInformationTable.AddRow(lastAttendedSchool);

            var enrolledInclearingHouseProgram = new EmailRowFieldTable("Are you enrolled in clearing house program?", ConverterHelper.ToYesNo(driverEmploymentApplication.EnrolledInClearingHouse));
            applicatInformationTable.AddRow(enrolledInclearingHouseProgram);

            return applicatInformationTable;
        }
    }
}