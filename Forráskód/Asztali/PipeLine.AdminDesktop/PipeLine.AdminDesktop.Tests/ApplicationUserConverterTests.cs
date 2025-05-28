using System;
using Xunit;
using PipeLine.Shared.Models.UserModels;
using PipeLine.Shared.Dtos.UserDtos;
using PipeLine.Shared.Converters;

namespace PipeLine.AdminDesktop.Tests
{
    public class ApplicationUserConverterTests
    {
        [Fact]
        public void ToDto_ShouldMapAllFieldsCorrectly()
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = "TestUser",
                Email = "test@example.com",
                FirstName = "Test",
                LastName = "User",
                PhoneNumber = "123456789"
            };

            var dto = user.ToDto();

            Assert.Equal(user.Id, dto.Id);
            Assert.Equal(user.UserName, dto.UserName);
            Assert.Equal(user.Email, dto.Email);
            Assert.Equal(user.FirstName, dto.FirstName);
            Assert.Equal(user.LastName, dto.LastName);
            Assert.Equal(user.PhoneNumber, dto.PhoneNumber);
        }

        [Fact]
        public void ToModel_ShouldMapAllFieldsCorrectly()
        {
            var dto = new ApplicationUserDto
            {
                Id = Guid.NewGuid(),
                UserName = "AnotherUser",
                Email = "another@example.com",
                FirstName = "Another",
                LastName = "Tester",
                PhoneNumber = "987654321"
            };

            var model = dto.ToModel();

            Assert.Equal(dto.Id, model.Id);
            Assert.Equal(dto.UserName, model.UserName);
            Assert.Equal(dto.Email, model.Email);
            Assert.Equal(dto.FirstName, model.FirstName);
            Assert.Equal(dto.LastName, model.LastName);
            Assert.Equal(dto.PhoneNumber, model.PhoneNumber);
            Assert.Equal(dto.UserName.ToUpper(), model.NormalizedUserName);
            Assert.Equal(dto.Email.ToUpper(), model.NormalizedEmail);
        }

        [Fact]
        public void ToDto_WithNullOptionalFields_ShouldSetEmptyStrings()
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = "NullUser",
                Email = "nulluser@example.com",
                FirstName = null,
                LastName = null,
                PhoneNumber = null
            };

            var dto = user.ToDto();

            Assert.Equal(string.Empty, dto.FirstName);
            Assert.Equal(string.Empty, dto.LastName);
            Assert.Equal(string.Empty, dto.PhoneNumber);
        }
    }
}
