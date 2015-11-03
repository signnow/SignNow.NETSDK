using System.Collections.Generic;
namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model is useful to build the Fields Object to have different types of files like, CheckBox, Texts and Initials.
     */
    public class Fields
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int PageNumber { get; set; }

        public string Role { get; set; }

        public bool Required { get; set; }

        public string Type { get; set; }

        public string RoleId { get; set; }

        public List<Fields> Radio { get; set; }

        public string Email { get; set; }
    }
}