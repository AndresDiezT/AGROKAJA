using Backend.DTOs.AddressDTOs;
using Backend.DTOs.RoleDTOs;
using Backend.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DTOs.UserDTOs
{
    public class ProfileDto
    {
        public string Document { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateOnly BirthDate { get; set; }

        public bool EmailIsVerified { get; set; }

        public bool PhoneIsVerified { get; set; }

        public string ProfileImage { get; set; }

        // RELATIONS
        public int IdTypeDocument { get; set; }
        public string NameTypeDocument { get; set; }

        public IEnumerable<ReadAddressDto> Addresses { get; set; } = new List<ReadAddressDto>();
        public IEnumerable<ReadRoleDto> Roles { get; set; } = new List<ReadRoleDto>();
    }
}
