using Accounts.Application.Users.Commands.CreateUser;
using Accounts.Application.Common.Exceptions;
using Accounts.Domain.Entities;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using Accounts.Application.Users.Queries.GetUsersWithPagination;

namespace Accounts.Application.IntegrationTests.TodoItems.Commands;

using static Testing;

public class GetUsersWithPaginationTests : TestBase
{
    private readonly Fixture _fixture = null!;

    public GetUsersWithPaginationTests()
    {
        _fixture = new Fixture();
    }

    [Test]
    public async Task ShouldGetUsersWithPagination()
    {
        var expectedUser = new User { Name = $"Leonardo {new Random().Next(12, 2002)}", Email = $"leonardosimoes{new Random().Next(2222, 55555)}@gmail.com" };

        await AddAsync(expectedUser);


        var results = await SendAsync(new GetUsersWithPaginationQuery { SearchText = expectedUser.Name});

        results.Should().NotBeNull();
        results.Items.Should().HaveCount(1);

        var item = results.Items.FirstOrDefault();

        item.Should().NotBeNull();
        item!.Id.Should().NotBeEmpty();
        item.Name.Should().Be(expectedUser.Name);
        item.Email.Should().Be(expectedUser.Email); 
    }
}
