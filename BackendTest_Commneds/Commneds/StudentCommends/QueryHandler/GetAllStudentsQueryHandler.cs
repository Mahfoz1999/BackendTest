using BackendTest_Commneds.Commneds.StudentCommends.Query;
using BackendTest_Database.DatabaseConnection;
using BackendTest_Models.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BackendTest_Commneds.Commneds.StudentCommends.QueryHandler;

public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, IEnumerable<Student>>
{
    private readonly BackendTestDbContext Context;
    public GetAllStudentsQueryHandler(BackendTestDbContext Context)
    {
        this.Context = Context;
    }
    public async Task<IEnumerable<Student>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        return await Context.Users.ToListAsync(cancellationToken);
    }
}
