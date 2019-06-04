# RedirectAndPostCore
Dotnet core library to redirect to another url with POST data
 Usage:
 
 Using RedirectAndPostCore
 
 public ActionResult RedirectWithPost(){
 var data = new Dictionary<string, object>();
            data.Add("name", "Subhendu");
            data.Add("Age", 25);
            data.Add("nationailty","indian");
            return new RedirectAndPostResult("http://subhendubharti.in", data);
 }
