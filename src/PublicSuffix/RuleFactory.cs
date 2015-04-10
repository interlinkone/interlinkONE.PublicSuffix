
using PublicSuffix.Rules;

namespace PublicSuffix {

    /// <summary>
    /// The RuleFactory produces <see cref="Rule"/> objects based on their line indicators.
    /// </summary>
    public class RuleFactory {
        
        /// <summary>
        /// Inspects a string of text and returns a matching <see cref="Rule" />
        /// From: http://publicsuffix.org/format/
        /// - The wildcard character * (asterisk) matches any valid sequence of characters in a hostname part. (Note: the list uses Unicode, not Punycode forms, and is encoded using UTF-8.)
        /// - An exclamation mark (!) at the start of a rule marks an exception to a previous wildcard rule.
        /// </summary>
        /// <param name="line">A line from a <see cref="RulesFactory" /></param>
        /// <returns>A matching <see cref="Rule" /></returns>
        public static Rule Get(string line) {
            switch(line[0]) {
                case '*'    : return new WildcardRule(line);
                case '!'    : return new ExceptionRule(line);
                default     : return new NormalRule(line);
            }
        }
    }
}
