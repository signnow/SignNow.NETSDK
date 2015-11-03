
namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This class is used to create the check box on the document
     */
    public class Checkbox : Fields
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int PageNumber { get; set; }
    }
}