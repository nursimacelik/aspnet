using System;
using Xunit;
using Final.Project.Core.Auth;
using System.Security.Cryptography;
using System.Text;

namespace Final.Project.Test
{
    public class UserAuthenticationTest
    {
        [Fact]
        public void CompareHashes_ReturnsTrue()
        {
            var hash1 = SHA1.HashData(Encoding.ASCII.GetBytes("ankara"));
            var hash2 = SHA1.HashData(Encoding.ASCII.GetBytes("ankara"));

            Assert.True(UserAuthentication.CompareHashes(hash1, hash2));

        }

        [Theory]
        [InlineData("ankara", "istanbul")]
        [InlineData("izmir", "izmit")]
        public void CompareHashes_ReturnsFalse(string s1, string s2)
        {
            var hash1 = SHA1.HashData(Encoding.ASCII.GetBytes(s1));
            var hash2 = SHA1.HashData(Encoding.ASCII.GetBytes(s2));
            
            Assert.False(UserAuthentication.CompareHashes(hash1, hash2));
            
        }

        [Fact]
        public void GenerateSaltedHash_ReturnsCorrectHash()
        {
            var text = "izmir";
            var salt = "12439732fef";
            var salted = "izmir12439732fef";
            var expectedHash = SHA1.HashData(Encoding.ASCII.GetBytes(salted));

            var actualHash = UserAuthentication.GenerateSaltedHash(text, salt);
            Assert.Equal(expectedHash, actualHash);
        }

    }
}
