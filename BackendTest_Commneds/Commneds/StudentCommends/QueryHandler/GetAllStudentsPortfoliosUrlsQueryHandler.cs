using BackendTest_Commneds.Commneds.StudentCommends.Query;
using BackendTest_Database.DatabaseConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BackendTest_Commneds.Commneds.StudentCommends.QueryHandler;

public class GetAllStudentsPortfoliosUrlsQueryHandler : IRequestHandler<GetAllStudentsPortfoliosUrlsQuery, List<string>>
{
    private readonly BackendTestDbContext Context;
    public GetAllStudentsPortfoliosUrlsQueryHandler(BackendTestDbContext Context)
    {
        this.Context = Context;
    }
    public async Task<List<string>> Handle(GetAllStudentsPortfoliosUrlsQuery request, CancellationToken cancellationToken)
    {
        List<string> urls = await Context.Portfolios.Include(e => e.Student).Where(e => e.Student.Id == request.id).Select(e => e.Url).ToListAsync(cancellationToken);
        return urls;
    }
}
