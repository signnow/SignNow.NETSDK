
namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model class used to create the User in SignNow Application
     */
    public class User {
        private string Id;

        private string firstName;

        private string lastName;

        private string Email;

        private string Password;

        private string Verified;

        private Oauth2Token OAuth2Token;

        private string Error;

        private int Code;
	
	    public string id
	    {
		    get {   return Id; }
		    set { this.Id = value; }
	    }

        public string first_name
	    {
		    get {   return firstName; }
		    set {   this.firstName = value; }
	    }

        public string last_name
	    {
		    get {   return lastName;}
		    set {   this.lastName = value; }
	    }

        public string email
	    {
            get { return Email; }
            set { this.Email = value; }
	    }

        public string password
	    {
            get { return Password; }
            set { this.Password = value; }
	    }
	
	    public string verified
	    {
		    get {   return Verified; }
		    set {   this.Verified = value; }
	    }
	
	    public Oauth2Token oauth2Token
	    {
		    get {   return OAuth2Token; }
		    set {   this.OAuth2Token = value; }
	    }

        public string error
        {
            get { return Error; }
            set { this.Error = value; }
        }

        public int code
        {
            get { return Code; }
            set { this.Code = value; }
        }
}
}