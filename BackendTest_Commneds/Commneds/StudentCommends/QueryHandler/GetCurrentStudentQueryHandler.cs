using BackendTest_Commneds.Commneds.StudentCommends.Query;
using BackendTest_Models.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BackendTest_Commneds.Commneds.StudentCommends.QueryHandler;

public class GetCurrentStudentQueryHandler : IRequestHandler<GetCurrentStudentQuery, Student>
{
    private readonly UserManager<Student> _userManager;
    private IHttpContextAccessor _httpContextAccessor { get; set; }
    public GetCurrentStudentQueryHandler(UserManager<Student> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<Student> Handle(GetCurrentStudentQuery request, CancellationToken cancellationToken)
    {
        var _httpcontext = _httpContextAccessor.HttpContext;
        if (_httpcontext != null
                && _httpcontext.User != null
                && _httpcontext.User.Identity != null
                && _httpcontext.User.Identity.IsAuthenticated)
        {
            Student? user = await _userManager.GetUserAsync(_httpcontext.User)!;
            return user!;
        }
        else
            throw new UnauthorizedAccessException("Student is not Authenticated");
    }
}
