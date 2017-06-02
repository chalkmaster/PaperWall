using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaperWall.Core.DomainObject.Validation
{
    public static class MessageValidator
    {
        private const int MinMessageSize = 1;
        private const int MaxMessageSize = 140;
        public static bool Validate(Message messageToValidate, out List<String> brokenRules)
        {
            brokenRules = new List<string>();
            if (messageToValidate == null)
                brokenRules.Add("Objeto de mensagem nulo. Falha Interna.");
            else
            {
                if (String.IsNullOrWhiteSpace(messageToValidate.MessageText) || messageToValidate.MessageText.Length < MinMessageSize)
                    brokenRules.Add(String.Format("A mensagem deve ter pelo menos {0} caracter", MinMessageSize));
                if (!String.IsNullOrWhiteSpace(messageToValidate.MessageText) && messageToValidate.MessageText.Length > MaxMessageSize)
                    brokenRules.Add(String.Format("A mensagem deve ter no máximo {0} caracteres", MaxMessageSize));
            }
            return brokenRules.Any();
        }
    }
}
