using ProjectFutureAdvannced.Models.Enums;
using ProjectFutureAdvannced.Models.Model;
using ProjectFutureAdvannced.Models.Model.AccountUser;

namespace ProjectFutureAdvannced.ViewModels.AdminViewModel
    {
    public class ListOfInfoAdmin
        {
        public IEnumerable<Category> categories { get; set; }
        public GeneralInfoAdmin GeneralInfoAdmin { get; set; }
        public IEnumerable<AppUser> appUsers { get; set; }

        public TypeOfUser typeOfUser { get; set; }
    }
    }
