
namespace PublicSuffix.Rules {

    /// <summary>
    /// A Default Rule matches invalid domains.
    /// - If no rules match, the prevailing rule is "*"
    /// </summary>
    public class DefaultRule : Rule {
        
        /// <summary>
        /// Create a new <see cref="DefaultRule" /> instance from a <see cref="Rule" /> from a <see cref="RulesFactory" />
        /// </summary>
        public DefaultRule() : base("*") {
        }

        /// <summary>
        /// Parses a domain name from the supplied url and current <see cref="Rule" /> instance.
        /// Gets the Top, Second and Third level domains populated (if present.)
        /// </summary>
        /// <param name="url">A url with an invalid domain name.</param>
        /// <returns>A <see cref="Domain"/> with <see cref="Domain.IsValid" /> set to false.</returns>
        public override Domain Parse(string url) {
            var domain = base.Parse(url);
            domain.IsValid = false;

            return domain;
        }
    }
}
