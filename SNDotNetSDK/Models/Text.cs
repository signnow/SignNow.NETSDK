
namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     *
     * This model class is used to place the Texts on the documents for a given document ID.
     */
    public class Text : Fields
    {
        public int Size { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int PageNumber { get; set; }

        public string Font { get; set; }

        public string Data { get; set; }

        public double LineHeight { get; set; }
    }
}
