using System.Collections.Generic;
using System.Text;

namespace Antix.Blackhole
{
    public class BlackholeTemplateEngine
    {
        public string Execute(
            string template, IDictionary<string, string> data)
        {
            var result = new StringBuilder();

            var index = 0;
            var delimiters = new FindDelimiters {{"{", "}"}};

            do
            {
                // get a token
                var tokenStart = template.Find(index, delimiters, "{");
                if (tokenStart == null)
                {
                    // add last bit
                    result.Append(template.Substring(index));
                    break;
                }

                var tokenEnd = template.Find(tokenStart + 1, delimiters, "}");
                var token = template.Substring(tokenStart + 1, tokenEnd - tokenStart - 1);

                // parse the token
                var tokenNameStart = token.Find(delimiters, "{");
                var tokenNameEnd = token.Find(tokenNameStart + 1, delimiters, "}");

                var tokenName = token.Substring(tokenNameStart + 1, tokenNameEnd - tokenNameStart - 1).Split('|');

                if (!data.ContainsKey(tokenName[0]))
                    throw new BlackholePathNotFoundException(tokenName[0]);

                var value = data[tokenName[0]]
                            ?? (tokenName.Length == 2 ? tokenName[1] : null);

                // add anything before the token
                result.Append(template.Substring(index, tokenStart.Index - index));

                if (value != null)
                {
                    result.Append(token.Substring(0, tokenNameStart));
                    result.Append(value);
                    result.Append(token.Substring(tokenNameEnd + 1));
                }

                index = tokenEnd.Index + 1;
            } while (index < template.Length);

            return result.ToString();
        }
    }
}