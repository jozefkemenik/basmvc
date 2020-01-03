using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Repository.Model
{
  
    [MetadataType(typeof(UserProfileMetadata))]
    public partial class UserProfile
    {
        public ICollection<webpages_Roles> all_Webpages_Roles { get; set; }

        public int CurrentUserId {get;set;}


        public bool IsCurrenctUser { get { return CurrentUserId == UserId; } }
    }

    public class UserProfileMetadata
    {
        
        
        [Required]
        [Display(Name = "TextUserName")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "TextEmail")]
        public string Email { get; set; }
        
        [Required]
        [Display(Name = "TextFirsName")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "TextLastName")]
        public string LastName { get; set; }

        [Display(Name = "TextAddress")]
        public string Street { get; set; }

        [Display(Name = "TextCity")]
        public string City { get; set; }

        [Display(Name = "TextPhone")]
        public string Phone { get; set; }

        [Display(Name = "TextBirthDayYear")]
        [Required]
        public Nullable<int> BirthDayYear { get; set; }

        [Display(Name = "TextCountry")]
        public Nullable<int> CountryId { get; set; }

        [Display(Name = "TextZip")]
        public string Zip { get; set; }
   
    
    }


}
