﻿
using Machine.Specifications;

namespace PublicSuffix.Specs {

    public abstract class WithParser : WithDomain {
        protected static Parser parser;

        Establish context = () => {
            var list = new RulesList();
            var rules = list.FromFile(@"data\effective_tld_names.dat");
            parser = new Parser(rules);
        };
    }

    [Subject(typeof(Parser))]
    public class when_given_a_valid_url : WithParser {
        Establish context = () => domain = parser.Parse("http://www.google.com");

        It parses_the_tld           = () => domain.TLD.ShouldEqual("com");
        It parses_the_maindomain    = () => domain.MainDomain.ShouldEqual("google");
        It parses_the_subdomain     = () => domain.SubDomain.ShouldEqual("www");
        It validates_the_tld        = () => domain.IsValid.ShouldBeTrue();
    }

    [Subject(typeof(Parser))]
    public class when_given_an_invalid_url : WithParser {
        Establish context = () => domain = parser.Parse("http://fake.zzz");

        It validates_the_tld = () => domain.IsValid.ShouldBeFalse();
    }

    [Subject(typeof(Parser))]
    public class when_given_a_wildcard_url : WithParser {
        Establish context = () => domain = parser.Parse("http://www.bbc.co.uk");

        It parses_the_tld           = () => domain.TLD.ShouldEqual("co.uk");
        It parses_the_maindomain    = () => domain.MainDomain.ShouldEqual("bbc");
        It parses_the_subdomain     = () => domain.SubDomain.ShouldEqual("www");
    }

    [Subject(typeof(Parser))]
    public class when_given_an_exception_url : WithParser {
        Establish context = () => domain = parser.Parse("http://www.metro.tokyo.jp");

        It parses_the_tld           = () => domain.TLD.ShouldEqual("tokyo.jp");
        It parses_the_maindomain    = () => domain.MainDomain.ShouldEqual("metro");
        It parses_the_subdomain     = () => domain.SubDomain.ShouldEqual("www");
    }

}
