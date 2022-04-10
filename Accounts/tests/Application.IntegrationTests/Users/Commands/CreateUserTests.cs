using Accounts.Application.Users.Commands.CreateUser;
using Accounts.Application.Common.Exceptions;
using Accounts.Domain.Entities;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;

namespace Accounts.Application.IntegrationTests.TodoItems.Commands;

using static Testing;

public class CreateUserTests : TestBase
{
    private readonly Fixture _fixture = null!;

    public CreateUserTests()
    {
        _fixture = new Fixture();
    }

    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        // TODO
        //var userIdentityId = await RunAsAdministratorAsync();

        //var command = new CreateUserCommand();

        //await FluentActions.Invoking(() =>
        //    SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    public async Task ShouldCreateUser()
    {
        // TODO
        //var userIdentityId = await RunAsAdministratorAsync();
        //var command = new CreateUserCommand()
        //{
        //    Email = "email@123.com",
        //    Name = "Name"
        //};

        //var userResult = await SendAsync(command);

        //var user = await FindAsync<User>(userResult.Id);
        //var identity = await FindIdentityAsync(userResult.IdentityUserId.ToString());

        //user.Should().NotBeNull();
        //user!.Id.Should().NotBeEmpty();
        //user.Name.Should().Be(command.Name);
        //user.Email.Should().Be(command.Email);
        //user.CreatedBy.Should().Be(userIdentityId);
        //user.Created.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMilliseconds(10000));
        //user.LastModifiedBy.Should().BeNull();
        //user.LastModified.Should().BeNull();

        //identity.Should().NotBeNull();
        //identity!.Id.Should().NotBeEmpty();
        //identity.UserName.Should().Be(command.Email);
    }
}
