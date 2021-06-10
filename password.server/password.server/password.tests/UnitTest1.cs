using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using password.api.Models;
using password.api.Services;

namespace password.tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
            PasswordServices passwordService = new PasswordServices(memoryCache);
            string testUser = "ala";
            var password = passwordService.AddAsync(testUser);
            Credentials credentials = new Credentials();
            credentials.UserId = testUser;
            credentials.Password = password;
            Assert.IsTrue(passwordService.Use(credentials));
        }

        [TestMethod]
        public void TestMethod2()
        {
            MemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
            PasswordServices passwordService = new PasswordServices(memoryCache);
            Assert.IsTrue(passwordService.Use(new Credentials { UserId= "test", Password= "1"}));
        }
    }
}
