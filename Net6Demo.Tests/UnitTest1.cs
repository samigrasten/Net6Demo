using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Net6Demo.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            await using var application = new Net6DemoApplication("Debug");

            var client = application.CreateClient();
            var response = await client.GetAsync("/");
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Equal("Fetch all items from /items", responseBody);
        }
    }
}