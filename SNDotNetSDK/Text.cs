
namespace com.signnow.sdk.model
{
    public class Text : Fields
    {
        public int size { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int page_number { get; set; }
        public string font { get; set; }
        public string data { get; set; }
        public double line_height { get; set; }
    }
}
