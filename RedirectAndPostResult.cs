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
            this._data = data;
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
            StringBuilder stringBuilder1 = new StringBuilder();
            stringBuilder1.Append(string.Format("<form id=\"postform\" name=\"postform\" action=\"{0}\" method=\"POST\">", (object)_url));
            foreach (KeyValuePair<string, object> keyValuePair in _data)
                stringBuilder1.Append(string.Format("<input type=\"hidden\" name=\"{0}\" value=\"{1}\"/>", (object)keyValuePair.Key, keyValuePair.Value));
            stringBuilder1.Append("</form>");
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.Append("<script language=\"javascript\">");
            stringBuilder2.Append(string.Format("document.postform.submit();"));
            stringBuilder2.Append("</script>");
            return stringBuilder1.ToString() + stringBuilder2.ToString();
        }
    }
}