using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaxFormGeneratorApi.Controllers;
using TaxFormGeneratorApi.Dtos;
using TaxFormGeneratorApi.Services;
using Xunit;

namespace Tests.Unit
{
    public class SettingsControllerTest
    {
        private const int UserId = 1;
        private readonly SettingsController _controller;
        private readonly Mock<IUserSettingsService> _mockService;
        private readonly UserSettingsDto _userSettings =  new UserSettingsDto
        {
            City = new CitySettingsDto
            {
                CityName = "Ruzic",
                CityCode = "12345",
                CityIban = "HR123009024894203",
                Surtax = 0.05
            },
            Company = new CompanySettingsDto
            {
                CompanyCity = "New York",
                CompanyEmail = "fds@gfds.com",
                CompanyName = "Kramerica Industries LLC",
                CompanyOib = "1234567890",
                CompanyStreet = "Fifth Avenue 34"
            },
            Dividend = new DividendSettingsDto
            {
                DividendTax = 0.12
            },
            Personal = new PersonalSettingsDto
            {
                City = "Ruzic",
                PersonalOib = "548379578434",
                Postcode = "22322",
                StreetName = "Bojcici",
                StreetNumber = "16"
            }
        };
        
        public SettingsControllerTest()
        {
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim("id", UserId.ToString()));
            var principal = new ClaimsPrincipal(identity);
            var mockContext = new Mock<HttpContext>(MockBehavior.Strict);
            mockContext.SetupGet(hc => hc.User).Returns(principal);
            
            _mockService = new Mock<IUserSettingsService>();
            _mockService.Setup(service => service.Get(UserId)).Returns(_userSettings);
            _mockService.Setup(service => service.Upsert(UserId, _userSettings));
            
            _controller = new SettingsController(_mockService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockContext.Object
                }
            };
        }

        [Fact]
        public void Get_WhenCalled_ReturnsLoggedInUserSettings()
        {
            var result = _controller.Get();

            _mockService.Verify(mock => mock.Get(UserId), Times.Once);
            Assert.Same(result, _userSettings);
        }

        [Fact]
        public void Post_WhenCalledWithUserSettingsDto_CallsUpsertOnSettingsServiceWithItAndLoggedInUserId()
        {
            _controller.Post(_userSettings);
            
            _mockService.Verify(mock => mock.Upsert(UserId, _userSettings), Times.Once);
        }
    }
}