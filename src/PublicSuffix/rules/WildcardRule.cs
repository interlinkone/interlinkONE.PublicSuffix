
namespace PublicSuffix.Rules {

    /// <summary>
    /// A Wildcard Rule starts with a "*"
    /// Matches any valid sequence of characters in a hostname part. (Note: the list uses Unicode, not Punycode forms, and is encoded using UTF-8.)
    /// May only be used to wildcard an entire level. That is, they must be surrounded by dots (or implicit dots, at the beginning of a line).
    /// </summary>
    public class WildcardRule : Rule {
        
        /// <summary>
        /// Create a new <see cref="WildcardRule" /> instance from a <see cref="Rule" /> from a <see cref="RulesFactory" />
        /// </summary>
        /// <param name="name">The rule, example: *.uk</param>
        public WildcardRule(string name) : base(name) {
        }
    }
}
