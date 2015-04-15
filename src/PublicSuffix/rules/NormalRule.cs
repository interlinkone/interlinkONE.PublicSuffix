
namespace PublicSuffix.Rules {

    /// <summary>
    /// A Normal Rule.
    /// </summary>
    public class NormalRule : Rule {

        /// <summary>
        /// Create a new <see cref="NormalRule" /> instance from a <see cref="Rule" /> from a <see cref="RulesFactory" />
        /// </summary>
        /// <param name="name">The rule, examples: com, net, org</param>
        public NormalRule(string name) : base(name) {
        }
    }
}
