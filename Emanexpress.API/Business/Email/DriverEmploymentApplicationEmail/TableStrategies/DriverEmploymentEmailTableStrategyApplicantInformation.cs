using Emanexpress.API.DataTransferObjects;
using System;

namespace Emanexpress.API.Business.Email
{
    public class DriverEmploymentEmailTableStrategyApplicantInformation : IDriverEmploymentEmailTableStrategy
    {
        public DriverEmploymentApplicationEmailTableType emailTableType => DriverEmploymentApplicationEmailTableType.ApplicantInformation;

        public EmailTable GetEmailTable(DtoDriverEmploymentApplication driverEmploymentApplication)
        {
            var applicatInformationTable = new EmailTable("Aplicant Information");
            
            var firstName = new EmailRowFieldTable("First Name", driverEmploymentApplication.FirstName);
            var middleName = new EmailRowFieldTable("Middle Name", driverEmploymentApplication.MiddleName);
            var lastName = new EmailRowFieldTable("Last Name",driverEmploymentApplication.LastName);
            applicatInformationTable.AddRow(firstName, middleName, lastName);            
            
            var phoneNumber = new EmailRowFieldTable("Phone Number", driverEmploymentApplication.Phone);
            var driverEmail = new EmailRowFieldTable("Email", driverEmploymentApplication.DriverEmail);
            var birthDay = new EmailRowFieldTable("Date of birth", GetDate(driverEmploymentApplication.DateOfBirth));
            applicatInformationTable.AddRow(phoneNumber, driverEmail, birthDay);
            
            var socialSecurity = new EmailRowFieldTable("Social Security", driverEmploymentApplication.SocialSecurity);
            var dateAvailableTostart = new EmailRowFieldTable("Date available to start", GetDate(driverEmploymentApplication.DateAvailableToStart));
            var positionAppliedFor = new EmailRowFieldTable("Position", driverEmploymentApplication.PositionAppliedfor);
            applicatInformationTable.AddRow(socialSecurity, dateAvailableTostart, positionAppliedFor);
            
            var legalright = new EmailRowFieldTable("Do you have the legal right to work in the United States?", GetYesNo(driverEmploymentApplication.HaveLegalRightToWorkInUsa));
            applicatInformationTable.AddRow(legalright);

            applicatInformationTable.TitleSeparator("Have you ever worked for this company before?");
            
            var workedForUsWhere = new EmailRowFieldTable("Where", driverEmploymentApplication.WorkedBeforeForUsWhere);
            var workedForUsFrom = new EmailRowFieldTable("Date From", GetDate(driverEmploymentApplication.WorkedBeforeForUsFrom));
            var workedForUsTo = new EmailRowFieldTable("Date To", GetDate(driverEmploymentApplication.WorkedBeforeForUsTo));
            var workedForUsRateOfPay = new EmailRowFieldTable("Rate Of Pay", driverEmploymentApplication.WorkedBeforeForUsRateOfPay);
            applicatInformationTable.AddRow(workedForUsWhere, workedForUsFrom, workedForUsTo, workedForUsRateOfPay);

            var workedForUsPosition = new EmailRowFieldTable("Position", driverEmploymentApplication.WorkedBeforeForUsPosition);
            var workedForUsReasonLeaving = new EmailRowFieldTable("Reason of leaving", driverEmploymentApplication.WorkedBeforeForUsReasonOfLeaving);
            applicatInformationTable.AddRow(workedForUsPosition, workedForUsReasonLeaving);

            applicatInformationTable.TitleSeparator("");

            var areYouNowEmployed = new EmailRowFieldTable("Are you now employed?", GetYesNo(driverEmploymentApplication.AreYouNowEmployed));
            var ifNotHowLongSinceLast = new EmailRowFieldTable("If not, how long since last employment?", driverEmploymentApplication.HowLongSinceLastEmployment);
            applicatInformationTable.AddRow(areYouNowEmployed, ifNotHowLongSinceLast);

            applicatInformationTable.TitleSeparator("");

            var whoReferedYou = new EmailRowFieldTable("Who refered You?", driverEmploymentApplication.WhoReferedYou);
            var rateOfPayExpected = new EmailRowFieldTable("Rate of pay expected", driverEmploymentApplication.RateOfPayExpected);
            applicatInformationTable.AddRow(whoReferedYou, rateOfPayExpected);

            applicatInformationTable.TitleSeparator("");

            var haveYouBonded = new EmailRowFieldTable("Have you ever been bonded?", GetYesNo(driverEmploymentApplication.HaveYouEverBeenBonded));
            var nameOfBondingCompany = new EmailRowFieldTable("Name of bonding company", driverEmploymentApplication.NameOfBondingCompany);
            applicatInformationTable.AddRow(haveYouBonded, nameOfBondingCompany);

            applicatInformationTable.TitleSeparator("");

            var haveYouFelony = new EmailRowFieldTable("Have you ever been convicted of a felony?", GetYesNo(driverEmploymentApplication.HaveYouEverBeenConvictedOfAFelony));
            applicatInformationTable.AddRow(haveYouFelony);
            
            var felonyReasons = new EmailRowFieldTable("If you have been convicted of a felony, explain fully.", driverEmploymentApplication.FelonyDetails);
            applicatInformationTable.AddRow(felonyReasons);

            applicatInformationTable.TitleSeparator("");

            var unableToPerformJob = new EmailRowFieldTable("Is there any reason you might be unable to perform the functions of the job for which you have applied?", GetYesNo(driverEmploymentApplication.UnableToPerformFunctionsJob));
            applicatInformationTable.AddRow(unableToPerformJob);
            
            var unableToPerformJobDetails = new EmailRowFieldTable("If you are unable to perform the functions of the job, explain fully.", driverEmploymentApplication.UnableToPerformFunctionsJobDetails);
            applicatInformationTable.AddRow(unableToPerformJobDetails);

            return applicatInformationTable;
        }

        private string GetYesNo(bool value)
        {
            if (value)
            {
                return "YES";
            }

            return "NO";
        }

        private string GetDate(DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToString("MM/dd/yyyy");
            }
            
            return "";            
        }
    }
}