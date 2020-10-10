using System.Collections.Generic;

namespace FormHandler.Repositories
{
    public class FormRepository
    {
        private readonly Dictionary<string, string> forms = new Dictionary<string, string>()
        {
            {"06332e35-9e64-4bbc-82b6-2a7f94be6b06", "http://localhost:1313/gracias"}
        };

        public string GetRedirectUrlById(string formId)
        {
            return forms.ContainsKey(formId) ? forms[formId] : null;
        }
    }
}