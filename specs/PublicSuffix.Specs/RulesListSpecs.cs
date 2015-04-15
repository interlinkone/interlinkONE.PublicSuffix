
using System.Collections.Generic;
using System.Linq;

using PublicSuffix.Rules;

using Machine.Specifications;

namespace PublicSuffix.Specs
{

    [Subject(typeof(RulesFactory))]
    public class when_rules_come_from_text_file
    {
        static RulesFactory list;
        static IEnumerable<Rule> rules;

        Establish context = () => list = new RulesFactory();

        Because of = () => rules = RulesFactory.FromFile(@"data\effective_tld_names.dat");

        It returns_an_array_of_rules = () => rules.Count().ShouldBeGreaterThan(0);
        It has_no_blank_lines = () => rules.ShouldNotContain("");
        It has_no_commented_lines = () => rules.ShouldEachConformTo((rule) => !rule.ToString().StartsWith("//"));
        It has_first_line = () => rules.First().ToString().ShouldEqual("ac");
        It has_last_line = () => rules.Last().ToString().ShouldEqual("*.zw");
    }
}
