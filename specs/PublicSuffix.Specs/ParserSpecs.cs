
using Machine.Specifications;

namespace PublicSuffix.Specs {

    public abstract class WithParser : WithDomain {
        protected static Parser parser;

        Establish context = () => {
            var list = new RulesFactory();
            var rules = RulesFactory.FromFile(@"data\effective_tld_names.dat");
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
    public class when_given_a_valid_url_with_no_subdomain : WithParser {
        Establish context = () => domain = parser.Parse("http://google.com");

        It parses_the_tld           = () => domain.TLD.ShouldEqual("com");
        It parses_the_maindomain    = () => domain.MainDomain.ShouldEqual("google");
        It parses_the_subdomain     = () => domain.SubDomain.ShouldEqual("");
    }

    [Subject(typeof(Parser))]
    public class when_given_a_valid_url_with_multiple_tld : WithParser {
        Establish context = () => domain = parser.Parse("http://www.google.com.cn");

        It parses_the_tld           = () => domain.TLD.ShouldEqual("com.cn");
        It parses_the_maindomain    = () => domain.MainDomain.ShouldEqual("google");
        It parses_the_subdomain     = () => domain.SubDomain.ShouldEqual("www");
    }

    [Subject(typeof(Parser))]
    public class when_given_a_valid_url_with_multiple_tld_and_multiple_subdomains : WithParser {
        Establish context = () => domain = parser.Parse("http://www.maps.google.com.cn");

        It parses_the_tld           = () => domain.TLD.ShouldEqual("com.cn");
        It parses_the_maindomain    = () => domain.MainDomain.ShouldEqual("google");
        It parses_the_subdomain     = () => domain.SubDomain.ShouldEqual("www.maps");
    }

    [Subject(typeof(Parser))]
    public class when_given_an_invalid_url : WithParser {
        Establish context = () => domain = parser.Parse("http://fake.zzz");

        It validates_the_tld        = () => domain.IsValid.ShouldBeFalse();
        It parses_the_tld           = () => domain.TLD.ShouldEqual("zzz");
        It parses_the_maindomain    = () => domain.MainDomain.ShouldEqual("fake");
    }

    [Subject(typeof(Parser))]
    public class when_given_an_invalid_url_with_subdomain : WithParser {
        Establish context = () => domain = parser.Parse("http://www.fake.zzz");

        It validates_the_tld        = () => domain.IsValid.ShouldBeFalse();
        It parses_the_tld           = () => domain.TLD.ShouldEqual("zzz");
        It parses_the_maindomain    = () => domain.MainDomain.ShouldEqual("fake");
        It parses_the_subdomain     = () => domain.SubDomain.ShouldEqual("www");
    }

    [Subject(typeof(Parser))]
    public class when_given_an_invalid_url_with_multiple_subdomains : WithParser {
        Establish context = () => domain = parser.Parse("http://sub1.sub2.fake.zzz");

        It validates_the_tld        = () => domain.IsValid.ShouldBeFalse();
        It parses_the_tld           = () => domain.TLD.ShouldEqual("zzz");
        It parses_the_maindomain    = () => domain.MainDomain.ShouldEqual("fake");
        It parses_the_subdomain     = () => domain.SubDomain.ShouldEqual("sub1.sub2");
    }

    [Subject(typeof(Parser))]
    public class when_given_a_wildcard_url : WithParser {
        Establish context = () => domain = parser.Parse("http://www.bbc.co.uk");

        It parses_the_tld               = () => domain.TLD.ShouldEqual("co.uk");
        It parses_the_maindomain        = () => domain.MainDomain.ShouldEqual("bbc");
        It parses_the_subdomain         = () => domain.SubDomain.ShouldEqual("www");
        It parses_the_registered_domain = () => domain.RegisteredDomain.ShouldEqual("bbc.co.uk");
    }

    [Subject(typeof(Parser))]
    public class when_given_a_wildcard_url_with_multiple_tld : WithParser {
        Establish context = () => domain = parser.Parse("http://www.site.nonmetro.tokyo.jp");

        It parses_the_tld           = () => domain.TLD.ShouldEqual("nonmetro.tokyo.jp");
        It parses_the_maindomain    = () => domain.MainDomain.ShouldEqual("site");
        It parses_the_subdomain     = () => domain.SubDomain.ShouldEqual("www");
    }

    [Subject(typeof(Parser))]
    public class when_given_a_wildcard_url_with_multiple_tld_and_multiple_subdomains : WithParser {
        Establish context = () => domain = parser.Parse("http://www.xyz.site.nonmetro.tokyo.jp");

        It parses_the_tld           = () => domain.TLD.ShouldEqual("nonmetro.tokyo.jp");
        It parses_the_maindomain    = () => domain.MainDomain.ShouldEqual("site");
        It parses_the_subdomain     = () => domain.SubDomain.ShouldEqual("www.xyz");
    }

    [Subject(typeof(Parser))]
    public class when_given_an_exception_url : WithParser {
        Establish context = () => domain = parser.Parse("http://www.metro.tokyo.jp");

        It parses_the_tld               = () => domain.TLD.ShouldEqual("tokyo.jp");
        It parses_the_maindomain        = () => domain.MainDomain.ShouldEqual("metro");
        It parses_the_subdomain         = () => domain.SubDomain.ShouldEqual("www");
        It parses_the_registered_domain = () => domain.RegisteredDomain.ShouldEqual("metro.tokyo.jp");
    }

    [Subject(typeof(Parser))]
    public class when_given_an_exception_url_with_multiple_subdomains : WithParser {
        Establish context = () => domain = parser.Parse("http://www.site.metro.tokyo.jp");

        It parses_the_tld           = () => domain.TLD.ShouldEqual("tokyo.jp");
        It parses_the_maindomain    = () => domain.MainDomain.ShouldEqual("metro");
        It parses_the_subdomain     = () => domain.SubDomain.ShouldEqual("www.site");
    }

}
