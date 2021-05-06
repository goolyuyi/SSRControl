using System.IO;
using System.Linq;
using Rest.Model;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class Test_SSRServerConfig
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test_SSRServerConfig(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test_Load_String()
        {
            using var fs = File.OpenText("./Data/export.json");
            var str = fs.ReadToEnd();
            var config = new SsrServerConfig(str);
            _testOutputHelper.WriteLine(config.ToJson());
        }

        [Fact]
        public void Test_Query_Server_Field()
        {
            using var fs = File.OpenText(Path.GetFullPath("./Data/test.json"));
            var str = fs.ReadToEnd();
            var config = new SsrServerConfig(str);

            var res1 = config.ToJson(new SsrServerQuery() {Server = "t1"});
            Assert.NotNull(res1);
            Assert.True(res1.Count() == 1);

            var res2 = config.ToJson(new SsrServerQuery());
            Assert.True(res2.Count() == 0);
        }
        
        [Fact]
        public void Test_Query_Remark_Field()
        {
            using var fs = File.OpenText(Path.GetFullPath("./Data/test.json"));
            var str = fs.ReadToEnd();
            var config = new SsrServerConfig(str);

            var res2 = config.ToJson(new SsrServerQuery()
                {Remarks = "test1"});
            Assert.NotNull(res2);
        }

    }
}