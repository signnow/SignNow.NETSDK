﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Serializers;
using Newtonsoft.Json;

namespace com.signnow.sdk.model
{
    public class User {
        private string Id;

        private string firstName;

        private string lastName;

        private string Email;

        private string Password;

        private string Verified;

        private Oauth2Token OAuth2Token;
	
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
}
}
