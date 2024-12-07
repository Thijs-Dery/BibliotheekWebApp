using System.Collections.Generic;

namespace BibliotheekWebApp.Models
{
    public class EditRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
        public List<string> AllRoles { get; set; }
        public List<string> SelectedRoles { get; set; }

        public EditRolesViewModel()
        {
            Roles = new List<string>();
            AllRoles = new List<string>();
            SelectedRoles = new List<string>();
        }
    }
}

