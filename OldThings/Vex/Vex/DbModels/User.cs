using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Vex.DbModels
{
    public class User
    {
        public User()
        {
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string ChineseName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NTAccount { get; set; }
        public bool Gender { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        public int Birthday_Year { get; set; }
        public int Birthday_Month { get; set; }
        public int Birthday_Day { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public string Email { get; set; }

        public string PersonalEmail { get; set; }

        [Required]
        public string MobilePhone { get; set; }

        public string FixedPhone { get; set; }
        public string OfficePhone { get; set; }

        [Required]
        public string EmergencyContact { get; set; }

        [Required]
        public string EmergencyPhone { get; set; }

        [Required]
        public string OfficeAddress { get; set; }

        [Required]
        public string DeliveryAddress { get; set; }

        [Required]
        public int WorkingLocationId { get; set; }

        [Required]
        public int SectorId { get; set; }

        [Required]
        public int BusinessFunlocId { get; set; }

        [Required]
        public int CostCenterId { get; set; }

        public UserStatus Status { get; set; }

        public string PersonalInterest { get; set; }

        public DateTime RegisterTime { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public virtual WorkingLocation WorkingLocation { get; set; }

        public virtual Sector Sector { get; set; }

        public virtual BusinessFunloc BusinessFunloc { get; set; }

        public virtual CostCenter CostCenter { get; set; }

        public bool Can(params string[] permissions)
        {
            if (permissions.Length == 0)
            {
                return true;
            }
            return Roles.Any(r => r.Permissions.Any(p => permissions.Contains(p.Name)));
        }

        public string GetName()
        {
            if (string.IsNullOrEmpty(ChineseName))
            {
                return GetEnglishName();
            }
            if (string.IsNullOrEmpty(FirstName) 
                && string.IsNullOrEmpty(LastName))
            {
                return ChineseName;
            }
            return string.Format("{0} / {1}", ChineseName, GetEnglishName());
        }

        public string GetEnglishName()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }
}