using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace NYSE.API.Test
{
    public class UnitTest1
    {

        [Fact]
        public async Task TestGetDailyPrices()
        {
            // GetDailyPrices

            using (var client = new TestProvider().Client)
            { 

                var response = await client.GetAsync("api/dailyprices/hellowworld");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }


        }


        [Fact]
        public void ShouldAddInt()
        {
            var sut = new Calculator();

            var result = sut.Add(1, 2);

            Assert.Equal(3, result);

        }

        [Fact]
        public void ShouldAddDouble()
        {
            var sut = new Calculator();

            double result = sut.Add(1.1, 2.2);

            Assert.Equal(3.3, result, 1);

        }

        [Fact]
        public void ShouldMultiply()
        {
            var sut = new Calculator();

            var result = sut.Multiply(2, 2);

            Assert.Equal(4, result);

        }

    }
}
