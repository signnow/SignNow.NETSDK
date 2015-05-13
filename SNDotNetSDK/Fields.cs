using System.Collections.Generic;
namespace com.signnow.sdk.model
{
    public class Fields
    {
        public int x { get; set; }

        public int y { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public int page_number { get; set; }

        public string role { get; set; }

        public bool required { get; set; }

        public string type { get; set; }

        public string role_id { get; set; }

        public List<Fields> radio { get; set; }

        public string email { get; set; }
    }
}
