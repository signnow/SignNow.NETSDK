using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace com.signnow.sdk.model
{
    public class Oauth2Token
    {
    
    private string accessToken;

    private string tokenType;

    private string refreshToken;

    private string scope;

    private int expiresIn;

    public Oauth2Token() {
    }

    public Oauth2Token(string accessToken, string tokenType, string refreshToken, string scope, int expiresIn) {
        this.accessToken = accessToken;
        this.tokenType = tokenType;
        this.refreshToken = refreshToken;
        this.scope = scope;
        this.expiresIn = expiresIn;
    }

    public string access_token
	{
        get { return accessToken; }
		set { this.accessToken = value; }
	}

    public string token_type
	{
		get {   return tokenType; }
		set {   this.tokenType = value; }
	}

    public string refresh_token
	{
		get {   return refreshToken; }
		set {   this.refreshToken = value; }
	}
	
	public string Scope
	{
		get {   return scope; }
		set {   this.scope = value; }
	}

    public int expires_in
	{
		get {   return expiresIn; }
		set {   this.expiresIn = value; }
	}
}
}
