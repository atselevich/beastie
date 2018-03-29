using System;
using System.Globalization;
using System.Text;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace Beastie.Net.Extensions.Extensions
{
    public static class HtmlAgilityPackExtensions
    {
        public static TObject AddHtml<TObject>(this TObject element, string wysiwyg) where TObject : XElement
        {
            if (element != null && !string.IsNullOrWhiteSpace(wysiwyg))
                if (wysiwyg.IndexOf('<') == -1 || wysiwyg.IndexOf('>') == -1)
                {
                    element.Add(wysiwyg);
                }
                else
                {
                    var xml = HtmlToXElement(wysiwyg);

                    foreach (var node in xml.Nodes()) element.Add(node);
                }

            return element;
        }

        public static TObject AddWysiwyg<TObject>(
            this TObject element,
            string wysiwyg,
            params XAttribute[] rootTagAttributes) where TObject : XElement
        {
            if (element != null)
            {
                var xml = HtmlToXElement(wysiwyg);

                if (rootTagAttributes != null)
                    foreach (var attr in rootTagAttributes)
                        xml.Add(attr);

                element.Add(xml);
            }

            return element;
        }

        public static string ConvertPlainTextAsHtml(this string input, bool newLineAsBreakTag = true)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                var innerText = RemoveHtmlFromText(input);
                if (!string.IsNullOrWhiteSpace(innerText))
                {
                    var length = innerText.Length;
                    var htmlBuilder = new StringBuilder(length + 32);

                    var previous = '\0';

                    for (var i = 0; i < length; i++)
                    {
                        var current = innerText[i];
                        switch (current)
                        {
                            case '\n':
                                if (newLineAsBreakTag) htmlBuilder.Append("<br />");

                                break;

                            case '\r':
                                break;

                            case ' ':
                                if (previous != ' ') htmlBuilder.Append(' ');

                                break;

                            case '"':
                                htmlBuilder.Append("&quot;");
                                break;

                            case '&':
                                htmlBuilder.Append("&amp;");
                                break;

                            default:
                                if (current >= '\x00a0' && current < 'Ā')
                                {
                                    htmlBuilder.Append("&#");
                                    htmlBuilder.Append(((int) current).ToString(NumberFormatInfo.InvariantInfo));
                                    htmlBuilder.Append(';');
                                }
                                else
                                {
                                    htmlBuilder.Append(current);
                                }

                                break;
                        }

                        previous = current;
                    }

                    return htmlBuilder.ToString();
                }
            }

            return string.Empty;
        }

        [CLSCompliant(false)]
        public static HtmlNode LoadHtmlNode(
            this string html,
            string excludeNodes = "//comment()|//script|//link|//style")
        {
            if (!string.IsNullOrWhiteSpace(html))
            {
                var doc = new HtmlDocument
                {
                    /* http://stackoverflow.com/questions/5556089/html-agility-pack-removes-break-tag-close */
                    OptionWriteEmptyNodes = true
                };

                doc.LoadHtml(html);

                var scriptNodes = doc.DocumentNode.SelectNodes(excludeNodes);

                if (scriptNodes != null)
                    foreach (var script in scriptNodes)
                        script.Remove();

                return doc.DocumentNode;
            }

            return null;
        }

        public static string RemoveHtmlFromText(
            this string html,
            string excludeNodes = "//comment()|//script|//link|//style")
        {
            if (string.IsNullOrWhiteSpace(html) || html.IndexOf('<') == -1 || html.IndexOf('>') == -1) return html;

            var htmlNode = LoadHtmlNode(html, excludeNodes);

            if (htmlNode != null) return htmlNode.InnerText;

            return string.Empty;
        }

        private static XElement HtmlToXElement(this string wysiwyg)
        {
            var doc = new HtmlDocument
            {
                /* http://stackoverflow.com/questions/5556089/html-agility-pack-removes-break-tag-close */
                OptionWriteEmptyNodes = true,
                OptionCheckSyntax = true,
                OptionOutputAsXml = true
            };

            var html = string.Concat("<div>", wysiwyg, "</div>");
            doc.LoadHtml(html);

            var xml = XElement.Parse(doc.DocumentNode.OuterHtml);
            return xml;
        }
    }
}