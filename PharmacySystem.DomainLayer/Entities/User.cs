using Microsoft.AspNetCore.Identity;
using PharmacySystem.DomainLayer.Entities.Constants;


namespace PharmacySystem.DomainLayer.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AppRoles Role { get; set; }

        // Navigation properties based on role
        public virtual Pharmacy? Pharmacy { get; set; }
        public virtual WareHouse? WareHouse { get; set; }
        public virtual Representative? Representative { get; set; }
    }
}

//[Auzorize = Role="Admin"]
//[Auzorize = Role="Pharmacy"]
//[Auzorize = Role="Pharmacy"]
//[Auzorize = Role="Pharmacy"]

// terms & contion
// policy
// gateway order created successfully
// gateway error
// gateway timeout
// /about
