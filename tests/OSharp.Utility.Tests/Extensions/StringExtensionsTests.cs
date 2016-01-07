using System.Linq;

using Xunit;

namespace OSharp.Utility.Extensions.Tests
{

    public class StringExtensionsTests
    {
        [Fact()]
        public void IsMatchTest()
        {
            const string pattern = @"\d.*";
            Assert.False(((string)null).IsMatch(pattern));
            Assert.False("abc".IsMatch(pattern));
            Assert.True("abc123".IsMatch(pattern));
        }

        [Fact()]
        public void MatchTest()
        {
            const string pattern = @"\d.*";
            Assert.Null(((string)null).Match(pattern));
            Assert.Equal("abc".Match(pattern), string.Empty);
            Assert.Equal("abc123".Match(pattern), "123");
        }

        [Fact()]
        public void MatchesTest()
        {
            const string pattern = @"\d";
            Assert.Equal(((string)null).Matches(pattern).Count(), 0);
            Assert.Equal("abc".Matches(pattern).Count(), 0);
            Assert.Equal("abc123".Matches(pattern).Count(), 3);
        }

        [Fact()]
        public void StrLengthTest()
        {
            Assert.Equal("".TextLength(), 0);
            Assert.Equal("123".TextLength(), 3);
            Assert.Equal("abc".TextLength(), 3);
            Assert.Equal("$%^*&".TextLength(), 5);
            Assert.Equal("汉字测试".TextLength(), 8);
        }

        [Fact()]
        public void IsEmailTest()
        {
            string value = null;
            Assert.False(value.IsEmail());
            value = "123";
            Assert.False(value.IsEmail());
            value = "abc123.fds";
            Assert.False(value.IsEmail());
            value = "abc.yeah.net";
            Assert.False(value.IsEmail());
            value = "abc@yeah.net";
            Assert.True(value.IsEmail());
            value = "abc.a@yeah.net";
            Assert.True(value.IsEmail());
        }

        [Fact()]
        public void IsIpAddressTest()
        {
            string value = null;
            Assert.False(value.IsIpAddress());
            value = "0.0.0.0";
            Assert.True(value.IsIpAddress());
            value = "1.1.1.1";
            Assert.True(value.IsIpAddress());
            value = "192.168.0.1";
            Assert.True(value.IsIpAddress());
            value = "255.255.255.255";
            Assert.True(value.IsIpAddress());
        }

        [Fact()]
        public void AddQueryStringTest()
        {
            const string url = "http://localhost:801";
            string excepted = url + "?id=1";
            Assert.Equal(url.AddQueryString("id=1"), excepted);
            excepted = url + "?name=abc";
            Assert.Equal(url.AddQueryString("name=abc"), excepted);
            excepted = url + "?id=1&name=abc";
            Assert.Equal(url.AddQueryString("id=1", "name=abc"), excepted);
        }

        [Fact()]
        public void AddHashFragmentTest()
        {
            const string url = "http://localhost:801";
            string excepted = url + "#title";
            Assert.Equal(url.AddHashFragment("title"), excepted);
        }
    }
}
