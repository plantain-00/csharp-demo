using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

using Vex.DbModels;

namespace Vex.Services
{
    public class AccountService : BaseService
    {
        private BusinessFunloc[] _businessFunlocs;
        private CostCenter[] _costCenters;
        private User _currentUser;
        private Sector[] _sectors;
        private WorkingLocation[] _workingLocations;

        public User CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    if (HasSession<User>("currentUser"))
                    {
                        _currentUser = GetSession<User>("currentUser");
                    }
                    else
                    {
                        _currentUser = GetCurrentUser();
                        SetSession("currentUser", _currentUser);
                    }
                }
                return _currentUser;
            }
        }
        public WorkingLocation[] WorkingLocations
        {
            get
            {
                if (_workingLocations == null)
                {
                    if (HasCache<WorkingLocation[]>("WorkingLocations"))
                    {
                        _workingLocations = GetCache<WorkingLocation[]>("WorkingLocations");
                    }
                    else
                    {
                        _workingLocations = Entities.WorkingLocations.ToArray();
                        SetCache("WorkingLocations", _workingLocations, new TimeSpan(0, 20, 0));
                    }
                }
                return _workingLocations;
            }
        }

        public Sector[] Sectors
        {
            get
            {
                if (_sectors == null)
                {
                    if (HasCache<Sector[]>("Sectors"))
                    {
                        _sectors = GetCache<Sector[]>("Sectors");
                    }
                    else
                    {
                        _sectors = Entities.Sectors.ToArray();
                        SetCache("Sectors", _sectors, new TimeSpan(0, 20, 0));
                    }
                }
                return _sectors;
            }
        }

        public BusinessFunloc[] BusinessFunlocs
        {
            get
            {
                if (_businessFunlocs == null)
                {
                    if (HasCache<BusinessFunloc[]>("BusinessFunlocs"))
                    {
                        _businessFunlocs = GetCache<BusinessFunloc[]>("BusinessFunlocs");
                    }
                    else
                    {
                        _businessFunlocs = Entities.BusinessFunlocs.ToArray();
                        SetCache("BusinessFunlocs", _businessFunlocs, new TimeSpan(0, 20, 0));
                    }
                }
                return _businessFunlocs;
            }
        }

        public CostCenter[] CostCenters
        {
            get
            {
                if (_costCenters == null)
                {
                    if (HasCache<CostCenter[]>("CostCenters"))
                    {
                        _costCenters = GetCache<CostCenter[]>("CostCenters");
                    }
                    else
                    {
                        _costCenters = Entities.CostCenters.ToArray();
                        SetCache("CostCenters", _costCenters, new TimeSpan(0, 20, 0));
                    }
                }
                return _costCenters;
            }
        }

        public User GetCurrentUser()
        {
            var ntAccount = HttpContext.Current.User.Identity.Name;
            var user = Entities.Users.Include(u => u.WorkingLocation).Include(u => u.Sector).Include(u => u.BusinessFunloc).Include(u => u.CostCenter).Include(u => u.Roles).Include(u => u.Roles.Select(r => r.Permissions)).FirstOrDefault(u => u.NTAccount == ntAccount);
            return user;
        }

        public void ClearAllSession()
        {
            RemoveSession("currentUser");
        }

        public async Task Register(User user)
        {
            user.Status = UserStatus.Applied;
            user.NTAccount = HttpContext.Current.User.Identity.Name;
            user.Birthday_Year = user.Birthday.Year;
            user.Birthday_Month = user.Birthday.Month;
            user.Birthday_Day = user.Birthday.Day;
            user.RegisterTime = DateTime.Now;

            Entities.Users.Add(user);
            await Entities.SaveChangesAsync();

            ClearAllSession();
        }

        public async Task ModifyProfile(User user)
        {
            var oldUser = await Entities.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (oldUser == null)
            {
                throw new Exception("no user has the id.");
            }
            oldUser.ChineseName = user.ChineseName;
            oldUser.FirstName = user.FirstName;
            oldUser.LastName = user.LastName;
            if (CurrentUser.Id != user.Id
                || CurrentUser.Status == UserStatus.Unregistered
                || CurrentUser.Roles.Any(r => r.Name == Constants.HR_ROLE_NAME)
                || CurrentUser.Roles.Any(r => r.Name == Constants.ADMIN_ROLE_NAME))
            {
                oldUser.Gender = user.Gender;
                oldUser.Birthday = user.Birthday;
                oldUser.Birthday_Year = user.Birthday.Year;
                oldUser.Birthday_Month = user.Birthday_Month;
                oldUser.Birthday_Day = user.Birthday.Day;
            }
            oldUser.EmployeeId = user.EmployeeId;
            oldUser.Email = user.Email;
            oldUser.PersonalEmail = user.PersonalEmail;
            oldUser.MobilePhone = user.MobilePhone;
            oldUser.FixedPhone = user.FixedPhone;
            oldUser.OfficePhone = user.OfficePhone;
            oldUser.EmergencyContact = user.EmergencyContact;
            oldUser.EmergencyPhone = user.EmergencyPhone;
            oldUser.OfficeAddress = user.OfficeAddress;
            oldUser.DeliveryAddress = user.DeliveryAddress;
            oldUser.WorkingLocationId = user.WorkingLocationId;
            oldUser.SectorId = user.SectorId;
            oldUser.BusinessFunlocId = user.BusinessFunlocId;
            oldUser.CostCenterId = user.CostCenterId;
            oldUser.PersonalInterest = user.PersonalInterest;

            await Entities.SaveChangesAsync();

            if (CurrentUser.Id == user.Id)
            {
                ClearAllSession();
            }
        }

        public List<User> GetUnverifiedUsers()
        {
            return Entities.Users.Include(u => u.WorkingLocation).Include(u => u.Sector).Include(u => u.BusinessFunloc).Include(u => u.CostCenter).Where(u => u.Status == UserStatus.Applied).ToList();
        }

        public List<User> GetMembers()
        {
            return Entities.Users.Include(u => u.WorkingLocation).Include(u => u.Sector).Include(u => u.BusinessFunloc).Include(u => u.CostCenter).Where(u => u.Status == UserStatus.Active).ToList();
        }

        public List<User> GetEmployees(int skipped, int taked, out int count, string name = null, bool? gender = null, int? year = null, int? month = null, int? day = null, string employeeId = null, string email = null, string mobilePhone = null, int? location = null, int? sector = null, int? funloc = null, int? costCenter = null, UserStatus? status = null, bool isDesc = false, string column = null)
        {
            Expression<Func<User, bool>> nameCondition;
            if (string.IsNullOrEmpty(name))
            {
                nameCondition = u => true;
            }
            else
            {
                nameCondition = u => u.ChineseName.Contains(name) || u.FirstName.Contains(name) || u.LastName.Contains(name);
            }

            Expression<Func<User, bool>> genderCondition;
            if (gender == null)
            {
                genderCondition = u => true;
            }
            else
            {
                genderCondition = u => u.Gender == gender.Value;
            }

            Expression<Func<User, bool>> yearCondition;
            if (year == null)
            {
                yearCondition = u => true;
            }
            else
            {
                yearCondition = u => u.Birthday_Year == year.Value;
            }

            Expression<Func<User, bool>> monthCondition;
            if (month == null)
            {
                monthCondition = u => true;
            }
            else
            {
                monthCondition = u => u.Birthday_Month == month.Value;
            }

            Expression<Func<User, bool>> dayCondition;
            if (day == null)
            {
                dayCondition = u => true;
            }
            else
            {
                dayCondition = u => u.Birthday_Day == day.Value;
            }

            Expression<Func<User, bool>> employeeIdCondition;
            if (string.IsNullOrEmpty(employeeId))
            {
                employeeIdCondition = u => true;
            }
            else
            {
                employeeIdCondition = u => u.EmployeeId.Contains(employeeId);
            }

            Expression<Func<User, bool>> emailCondition;
            if (string.IsNullOrEmpty(email))
            {
                emailCondition = u => true;
            }
            else
            {
                emailCondition = u => u.Email.Contains(email);
            }

            Expression<Func<User, bool>> mobilePhoneCondition;
            if (string.IsNullOrEmpty(mobilePhone))
            {
                mobilePhoneCondition = u => true;
            }
            else
            {
                mobilePhoneCondition = u => u.MobilePhone.Contains(mobilePhone);
            }

            Expression<Func<User, bool>> locationCondition;
            if (location == null)
            {
                locationCondition = u => true;
            }
            else
            {
                locationCondition = u => u.WorkingLocationId == location.Value;
            }

            Expression<Func<User, bool>> sectorCondition;
            if (sector == null)
            {
                sectorCondition = u => true;
            }
            else
            {
                sectorCondition = u => u.SectorId == sector.Value;
            }

            Expression<Func<User, bool>> funlocCondition;
            if (funloc == null)
            {
                funlocCondition = u => true;
            }
            else
            {
                funlocCondition = u => u.BusinessFunlocId == funloc.Value;
            }

            Expression<Func<User, bool>> costCenterExpression;
            if (costCenter == null)
            {
                costCenterExpression = u => true;
            }
            else
            {
                costCenterExpression = u => u.CostCenterId == costCenter.Value;
            }

            Expression<Func<User, bool>> statusCondition;
            if (status == null)
            {
                statusCondition = u => u.Status != UserStatus.Unregistered;
            }
            else
            {
                statusCondition = u => u.Status == status.Value;
            }

            count = Entities.Users.Where(nameCondition).Where(genderCondition).Where(yearCondition).Where(monthCondition).Where(dayCondition).Where(employeeIdCondition).Where(emailCondition).Where(mobilePhoneCondition).Where(locationCondition).Where(sectorCondition).Where(funlocCondition).Where(costCenterExpression).Where(statusCondition).Count();

            var filteredUsers = Entities.Users.Include(u => u.WorkingLocation).Include(u => u.Sector).Include(u => u.BusinessFunloc).Include(u => u.CostCenter).Where(nameCondition).Where(genderCondition).Where(yearCondition).Where(monthCondition).Where(dayCondition).Where(employeeIdCondition).Where(emailCondition).Where(mobilePhoneCondition).Where(locationCondition).Where(sectorCondition).Where(funlocCondition).Where(costCenterExpression).Where(statusCondition);

            IQueryable<User> orderedUsers;
            switch (column)
            {
                case "chineseNameOrder":
                    if (isDesc)
                    {
                        orderedUsers = filteredUsers.OrderByDescending(u => u.ChineseName);
                    }
                    else
                    {
                        orderedUsers = filteredUsers.OrderBy(u => u.ChineseName);
                    }
                    break;
                case "englishNameOrder":
                    if (isDesc)
                    {
                        orderedUsers = filteredUsers.OrderByDescending(u => u.FirstName).ThenByDescending(u => u.LastName);
                    }
                    else
                    {
                        orderedUsers = filteredUsers.OrderBy(u => u.FirstName).ThenBy(u => u.LastName);
                    }
                    break;
                case "genderOrder":
                    if (isDesc)
                    {
                        orderedUsers = filteredUsers.OrderByDescending(u => u.Gender);
                    }
                    else
                    {
                        orderedUsers = filteredUsers.OrderBy(u => u.Gender);
                    }
                    break;
                case "birthdayOrder":
                    if (isDesc)
                    {
                        orderedUsers = filteredUsers.OrderByDescending(u => u.Birthday);
                    }
                    else
                    {
                        orderedUsers = filteredUsers.OrderBy(u => u.Birthday);
                    }
                    break;
                case "employeeIdOrder":
                    if (isDesc)
                    {
                        orderedUsers = filteredUsers.OrderByDescending(u => u.EmployeeId);
                    }
                    else
                    {
                        orderedUsers = filteredUsers.OrderBy(u => u.EmployeeId);
                    }
                    break;
                case "emailOrder":
                    if (isDesc)
                    {
                        orderedUsers = filteredUsers.OrderByDescending(u => u.Email);
                    }
                    else
                    {
                        orderedUsers = filteredUsers.OrderBy(u => u.Email);
                    }
                    break;
                case "cellphoneOrder":
                    if (isDesc)
                    {
                        orderedUsers = filteredUsers.OrderByDescending(u => u.MobilePhone);
                    }
                    else
                    {
                        orderedUsers = filteredUsers.OrderBy(u => u.MobilePhone);
                    }
                    break;
                case "officephoneOrder":
                    if (isDesc)
                    {
                        orderedUsers = filteredUsers.OrderByDescending(u => u.OfficePhone);
                    }
                    else
                    {
                        orderedUsers = filteredUsers.OrderBy(u => u.OfficePhone);
                    }
                    break;
                case "officeAddressOrder":
                    if (isDesc)
                    {
                        orderedUsers = filteredUsers.OrderByDescending(u => u.OfficeAddress);
                    }
                    else
                    {
                        orderedUsers = filteredUsers.OrderBy(u => u.OfficeAddress);
                    }
                    break;
                case "deliveryAddressOrder":
                    if (isDesc)
                    {
                        orderedUsers = filteredUsers.OrderByDescending(u => u.DeliveryAddress);
                    }
                    else
                    {
                        orderedUsers = filteredUsers.OrderBy(u => u.DeliveryAddress);
                    }
                    break;
                case "workLocationOrder":
                    if (isDesc)
                    {
                        orderedUsers = filteredUsers.OrderByDescending(u => u.WorkingLocation.Name);
                    }
                    else
                    {
                        orderedUsers = filteredUsers.OrderBy(u => u.WorkingLocation.Name);
                    }
                    break;
                case "sectorOrder":
                    if (isDesc)
                    {
                        orderedUsers = filteredUsers.OrderByDescending(u => u.Sector.Name);
                    }
                    else
                    {
                        orderedUsers = filteredUsers.OrderBy(u => u.Sector.Name);
                    }
                    break;
                case "funlocOrder":
                    if (isDesc)
                    {
                        orderedUsers = filteredUsers.OrderByDescending(u => u.BusinessFunloc.Name);
                    }
                    else
                    {
                        orderedUsers = filteredUsers.OrderBy(u => u.BusinessFunloc.Name);
                    }
                    break;
                case "costCenterOrder":
                    if (isDesc)
                    {
                        orderedUsers = filteredUsers.OrderByDescending(u => u.CostCenter.Name);
                    }
                    else
                    {
                        orderedUsers = filteredUsers.OrderBy(u => u.CostCenter.Name);
                    }
                    break;
                case "statusOrder":
                    if (isDesc)
                    {
                        orderedUsers = filteredUsers.OrderByDescending(u => u.Status);
                    }
                    else
                    {
                        orderedUsers = filteredUsers.OrderBy(u => u.Status);
                    }
                    break;
                case "registerOrder":
                    if (isDesc)
                    {
                        orderedUsers = filteredUsers.OrderByDescending(u => u.RegisterTime);
                    }
                    else
                    {
                        orderedUsers = filteredUsers.OrderBy(u => u.RegisterTime);
                    }
                    break;
                default:
                    if (isDesc)
                    {
                        orderedUsers = filteredUsers.OrderByDescending(u => u.NTAccount);
                    }
                    else
                    {
                        orderedUsers = filteredUsers.OrderBy(u => u.NTAccount);
                    }
                    break;
            }
            
            return orderedUsers.Skip(skipped).Take(taked).ToList();
        }

        public User GetUser(int id)
        {
            var user = Entities.Users.Include(u => u.WorkingLocation).Include(u => u.Sector).Include(u => u.BusinessFunloc).Include(u => u.CostCenter).Include(u => u.Roles).Include(u => u.Roles.Select(r => r.Permissions)).FirstOrDefault(u => u.Id == id);
            return user;
        }

        public async Task<Tuple<UserStatus, User>> ModifyStatus(int id, UserStatus status)
        {
            var user = GetUser(id);
            if (user != null)
            {
                var oldStatus = user.Status;
                user.Status = status;

                if (status == UserStatus.Active)
                {
                    if (user.Roles.All(r => r.Name != Constants.UNION_MEMBER))
                    {
                        user.Roles.Add(Entities.Roles.FirstOrDefault(r => r.Name == Constants.UNION_MEMBER));
                    }
                    var role = user.Roles.FirstOrDefault(r => r.Name == Constants.NON_UNION_MEMBER);
                    if (role != null)
                    {
                        user.Roles.Remove(role);
                    }
                }
                else
                {
                    if (user.Roles.All(r => r.Name != Constants.NON_UNION_MEMBER))
                    {
                        user.Roles.Add(Entities.Roles.FirstOrDefault(r => r.Name == Constants.UNION_MEMBER));
                    }
                    var role = user.Roles.FirstOrDefault(r => r.Name == Constants.UNION_MEMBER);
                    if (role != null)
                    {
                        user.Roles.Remove(role);
                    }
                }

                await Entities.SaveChangesAsync();

                ClearAllSession();

                return Tuple.Create(oldStatus, user);
            }
            throw new Exception("no user's id is : " + id);
        }

        public User[] GetHrs()
        {
            var users = Entities.Roles.FirstOrDefault(r => r.Name == Constants.HR_ROLE_NAME).Users.ToArray();
            return users;
        }

        public User[] GetAdmins()
        {
            var users = Entities.Roles.FirstOrDefault(r => r.Name == Constants.ADMIN_ROLE_NAME).Users.ToArray();
            return users;
        }

        public async Task<List<User>> Unregister(List<string> emails)
        {
            var userEmails = new List<string>();
            foreach (var email in emails)
            {
                var localEmail = email;
                var user = await Entities.Users.FirstOrDefaultAsync(u => u.Email == localEmail);
                if (user != null
                    && user.Status == UserStatus.Active)
                {
                    user.Status = UserStatus.Inactive;
                    userEmails.Add(user.Email);
                }
            }
            await Entities.SaveChangesAsync();

            ClearAllSession();

            return await Entities.Users.Where(u => userEmails.Contains(u.Email)).ToListAsync();
        }
    }
}