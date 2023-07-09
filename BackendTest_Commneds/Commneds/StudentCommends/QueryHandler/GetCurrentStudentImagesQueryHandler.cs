using BackendTest_Commneds.Commneds.StudentCommends.Query;
using BackendTest_Database.DatabaseConnection;
using BackendTest_Models.Models;
using BackEndTest_SharedKernal.enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BackendTest_Commneds.Commneds.StudentCommends.QueryHandler;

public class GetCurrentStudentImagesQueryHandler : IRequestHandler<GetCurrentStudentImagesQuery, List<string>>
{
    private readonly UserManager<Student> _userManager;
    private readonly BackendTestDbContext Context;
    private IHttpContextAccessor _httpContextAccessor { get; set; }
    public GetCurrentStudentImagesQueryHandler(UserManager<Student> userManager, BackendTestDbContext Context, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        this.Context = Context;
    }
    public async Task<List<string>> Handle(GetCurrentStudentImagesQuery request, CancellationToken cancellationToken)
    {
        var _httpcontext = _httpContextAccessor.HttpContext;
        if (_httpcontext != null
                && _httpcontext.User != null
                && _httpcontext.User.Identity != null
                && _httpcontext.User.Identity.IsAuthenticated)
        {
            var user = await _userManager.GetUserAsync(_httpcontext.User)!;
            Student? student = await Context.Users.Include(e => e.Portfolios).Where(e => e.Id == user!.Id).SingleOrDefaultAsync()!;
            List<string> images = new List<string>();
            if (request.ImageType == ImageType.Main)
            {
                images.Add(_httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + "/" + student!.MainPortfolio);
                return images;
            }
            else
            {
                foreach (var image in student!.Portfolios)
                {
                    images.Add(_httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + "/" + image.Url);
                }
                return images;
            }
        }
        else
            throw new UnauthorizedAccessException("Student is not Authenticated");
    }
}
