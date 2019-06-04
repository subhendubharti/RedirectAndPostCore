using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace RedirectAndPostCore{
public class RedirectAndPostResult : ActionResult
    {
        private string _url;
        private Dictionary<string, object> _data;
        public RedirectAndPostResult(string url, Dictionary<string, object> data)
        {
            this._url = url;
            this._data = data??new Dictionary<string, object>();
        }
        public override void ExecuteResult(ActionContext context)
        {
            string s = this.GenerateForm();
            context.HttpContext.Response.StatusCode = 302;
            context.HttpContext.Response.ContentType = "text/html";
            using (var sw = new StreamWriter(context.HttpContext.Response.Body))
            {
                sw.Write(s);
            }
        }
        private string GenerateForm()
        {
            StringBuilder s = new StringBuilder();
            s.Append(string.Format("<form id=\"postform\" name=\"postform\" action=\"{0}\" method=\"POST\">", _url));
            foreach (KeyValuePair<string, object> keyValuePair in _data)
                s.Append(string.Format("<input type=\"hidden\" name=\"{0}\" value=\"{1}\"/>", keyValuePair.Key, keyValuePair.Value));
            s.Append("</form>");
            s.Append("<script language=\"javascript\">");
            s.Append("document.postform.submit();");
            s.Append("</script>");
            return s.ToString();
        }
    }
}
