
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using PublicSuffix.Rules;

namespace PublicSuffix
{

    /// <summary>
    /// From: http://publicsuffix.org/format/
    /// - The Public Suffix List consists of a series of lines, separated by \n.
    /// - Each line is only read up to the first whitespace; entire lines can also be commented using //.
    /// - Each line which is not entirely whitespace or begins with a comment contains a rule.
    /// See http://mxr.mozilla.org/mozilla-central/source/netwerk/dns/effective_tld_names.dat?raw=1 for the latest file.
    /// </summary>
    public class RulesFactory
    {

        /// <summary>
        /// Reads a PublixSuffix formatted file.
        /// </summary>
        /// <param name="file">The a text file.</param>
        /// <returns>An array of <see cref="Rule" />s.</returns>
        public static IEnumerable<Rule> FromFile(string file)
        {
            var lines = (from line in File.ReadAllLines(file, Encoding.UTF8)
                         where IsValidRule(line)
                         select RuleFactory.Get(line)).ToArray();

            return lines;
        }

        /// <summary>
        /// Reads a PublixSuffix formatted file from an embedded resource.
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public static IEnumerable<Rule> FromResource(Assembly assembly, string resourceName)
        {
            var rules = new List<Rule>();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (IsValidRule(line))
                    {
                        rules.Add(RuleFactory.Get(line));
                    }
                }
            }

            return rules;
        }

        private static bool IsValidRule(string rule)
        {
            return !string.IsNullOrEmpty(rule) && !rule.StartsWith("//");
        }
    }
}
