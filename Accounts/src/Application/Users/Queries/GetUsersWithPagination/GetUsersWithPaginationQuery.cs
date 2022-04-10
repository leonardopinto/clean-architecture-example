using AutoMapper;
using AutoMapper.QueryableExtensions;
using Accounts.Application.Common.Interfaces;
using Accounts.Application.Common.Mappings;
using Accounts.Application.Common.Models;
using MediatR;

namespace Accounts.Application.Users.Queries.GetUsersWithPagination;

public class GetUsersWithPaginationQuery : IRequest<PaginatedList<UserViewModel>>
{
    public string? SearchText { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetUsersWithPaginationQueryHandler : IRequestHandler<GetUsersWithPaginationQuery, PaginatedList<UserViewModel>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUsersWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<UserViewModel>> Handle(GetUsersWithPaginationQuery request,
                                                             CancellationToken cancellationToken)
    {
        var userQuery = _context.DomainUsers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            userQuery = userQuery.Where(c => c.Name.Contains(request.SearchText)
            || c.Email.Contains(request.SearchText));
        }

        return await userQuery
            .OrderBy(x => x.Name)
            .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
