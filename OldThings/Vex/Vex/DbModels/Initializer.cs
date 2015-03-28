using System.Collections.Generic;
using System.Data.Entity;

using Despise;

namespace Vex.DbModels
{
    /// <summary>
    ///     DropCreateDatabaseIfModelChanges
    ///     CreateDatabaseIfNotExists
    ///     DropCreateDatabaseAlways
    /// </summary>
    public class Initializer : CreateDatabaseIfNotExists<Entities>
    {
        protected override void Seed(Entities context)
        {
            var register = new Permission
                           {
                               Name = Constants.REGISTER
                           };
            context.Permissions.Add(register);
            var modifyProfile = new Permission
                                {
                                    Name = Constants.MODIFY_PROFILE
                                };
            context.Permissions.Add(modifyProfile);
            var modifyStatus = new Permission
                               {
                                   Name = Constants.MODIFY_STATUS
                               };
            context.Permissions.Add(modifyStatus);
            var memberQueryAndExport = new Permission
                                       {
                                           Name = Constants.MEMBER_QUERY_AND_EXPORT
                                       };
            context.Permissions.Add(memberQueryAndExport);

            var unionMember = new Role
                              {
                                  Name = Constants.UNION_MEMBER
                              };
            unionMember.Permissions.Add(modifyProfile);
            context.Roles.Add(unionMember);
            var nonUnionMember = new Role
                                 {
                                     Name = Constants.NON_UNION_MEMBER
                                 };
            nonUnionMember.Permissions.Add(register);
            context.Roles.Add(nonUnionMember);
            var admin = new Role
                        {
                            Name = Constants.ADMIN_ROLE_NAME
                        };
            admin.Permissions.Add(modifyProfile);
            admin.Permissions.Add(modifyStatus);
            admin.Permissions.Add(memberQueryAndExport);
            context.Roles.Add(admin);
            var hr = new Role
                     {
                         Name = Constants.HR_ROLE_NAME
                     };
            hr.Permissions.Add(modifyProfile);
            hr.Permissions.Add(modifyStatus);
            hr.Permissions.Add(memberQueryAndExport);
            context.Roles.Add(hr);

            var generator = new Generator();

            var workingLocations = new List<WorkingLocation>();

            for (var i = 0; i < generator.Generate(3, 7); i++)
            {
                var workingLocation = new WorkingLocation
                                      {
                                          Name = generator.Get<EnglishWordGenerator>().Generate()
                                      };
                workingLocations.Add(workingLocation);
                context.WorkingLocations.Add(workingLocation);
            }

            var sectors = new List<Sector>();
            for (var i = 0; i < generator.Generate(3, 7); i++)
            {
                var sector = new Sector
                             {
                                 Name = generator.Get<EnglishWordGenerator>().Generate()
                             };
                sectors.Add(sector);
                context.Sectors.Add(sector);
            }

            var businessFunlocs = new List<BusinessFunloc>();
            for (var i = 0; i < generator.Generate(3, 7); i++)
            {
                var businessFunloc = new BusinessFunloc
                                     {
                                         Name = generator.Get<EnglishWordGenerator>().Generate()
                                     };
                businessFunlocs.Add(businessFunloc);
                context.BusinessFunlocs.Add(businessFunloc);
            }

            var costCenters = new List<CostCenter>();
            for (var i = 0; i < generator.Generate(3, 7); i++)
            {
                var costCenter = new CostCenter
                                 {
                                     Name = generator.Get<EnglishWordGenerator>().Generate()
                                 };
                costCenters.Add(costCenter);
                context.CostCenters.Add(costCenter);
            }

            for (var i = 0; i < generator.Generate(100, 150); i++)
            {
                var user = new User
                           {
                               Birthday = generator.Get<DateTimeGenerator>().Generate(),
                               ChineseName = generator.Get<ChineseNameGenerator>().Generate(),
                               FirstName = generator.Get<EnglishNameGenerator>().Generate(),
                               LastName = generator.Get<EnglishNameGenerator>().Generate(),
                               NTAccount = new string(generator.Get<EnglishUpperCaseCharGenerator>().GenerateMany(8)),
                               Gender = generator.Get<BoolGenerator>().Generate(),
                               EmployeeId = generator.Get<PhoneNumberGenerator>().Generate("00000000", "000000000"),
                               Email = generator.Get<EmailGenerator>().Generate(),
                               PersonalEmail = generator.Get<EmailGenerator>().Generate(),
                               MobilePhone = generator.Get<PhoneNumberGenerator>().Generate(),
                               FixedPhone = generator.Get<PhoneNumberGenerator>().Generate(),
                               OfficePhone = generator.Get<PhoneNumberGenerator>().Generate(),
                               EmergencyContact = generator.Get<ChineseNameGenerator>().Generate(),
                               EmergencyPhone = generator.Get<PhoneNumberGenerator>().Generate(),
                               OfficeAddress = new string(generator.Get<ChineseCharGenerator>().GenerateMany(10, 20)),
                               DeliveryAddress = new string(generator.Get<ChineseCharGenerator>().GenerateMany(10, 20)),
                               WorkingLocation = workingLocations[generator.Generate(0, workingLocations.Count)],
                               Sector = sectors[generator.Generate(0, sectors.Count)],
                               BusinessFunloc = businessFunlocs[generator.Generate(0, businessFunlocs.Count)],
                               CostCenter = costCenters[generator.Generate(0, costCenters.Count)],
                               Status = (UserStatus) (generator.Generate(1, 5)),
                               PersonalInterest = new string(generator.Get<ChineseCharGenerator>().GenerateMany(10, 100)),
                               RegisterTime = generator.Get<DateTimeGenerator>().Generate()
                           };
                user.Birthday_Year = user.Birthday.Year;
                user.Birthday_Month = user.Birthday.Month;
                user.Birthday_Day = user.Birthday.Day;

                if (i == 0)
                {
                    user.NTAccount = "";
                    user.Roles.Add(admin);
                }
                else if (user.Status == UserStatus.Active)
                {
                    user.Roles.Add(unionMember);
                }
                else
                {
                    user.Roles.Add(nonUnionMember);
                }

                context.Users.Add(user);
            }

            base.Seed(context);
        }
    }
}