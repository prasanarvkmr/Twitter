using NUnit.Framework;
using Twitter.Controllers;
using System.Web.MVC;

namespace Tests
{
    [TestFixture]
    public class HomeControllerTest
    {
        public HomeController HomeController { get; set; }
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            // Arrange
            HomeController = new HomeController();

            // Act
            ViewResult result = HomeController.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.Pass();
        }
    }
}