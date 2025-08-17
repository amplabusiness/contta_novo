namespace OmniXProduct.Scrapper.Extensions
{
    public class HtmlUtils
    {
        public static string Search(string html, string initial)
        {
            int pos;
            string returns = string.Empty;

            pos = html.IndexOf(initial);

            if (pos > -1)
            {
                returns = html.Remove(0, pos);
                pos = returns.IndexOf("\">");

                if (pos > -1)
                {
                    returns = returns.Substring(0, pos);
                    returns = returns.Replace(initial, string.Empty);
                }
            }

            return returns;
        }

        public static string Search(string html, string initial, string final)
        {
            int pos;
            string returns = string.Empty;

            pos = html.IndexOf(initial);

            if (pos > -1)
            {
                returns = html.Remove(0, pos);
                pos = returns.IndexOf(final);

                if (pos > -1)
                {
                    returns = returns.Substring(0, pos);
                    returns = returns.Replace(initial, string.Empty);
                }
            }

            return returns;
        }
    }
}
