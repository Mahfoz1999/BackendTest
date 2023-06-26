using BackendTest_Commneds.Commneds.AuthenticationCommends.Commend;
using BackendTest_Commneds.Exceptions;
using BackendTest_Commneds.Util;
using BackendTest_Database.DatabaseConnection;
using BackendTest_Models.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BackendTest_Commneds.Commneds.AuthenticationCommends.CommendHandler;

public class AddStudentCommendHandler : IRequestHandler<AddStudentCommend, IdentityResult>
{
    private IHttpContextAccessor _httpContextAccessor { get; set; }
    private readonly UserManager<Student> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMediator _mediator;
    private readonly BackendTestDbContext Context;
    public AddStudentCommendHandler(IHttpContextAccessor httpContextAccessor, IMediator mediator,
        UserManager<Student> userManager, BackendTestDbContext Context, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _httpContextAccessor = httpContextAccessor;
        _mediator = mediator;
        this.Context = Context;
    }
    public async Task<IdentityResult> Handle(AddStudentCommend request, CancellationToken cancellationToken)
    {
        try
        {
            Student? student = await _userManager.FindByNameAsync(request.formDto.FullName)!;
            if (student is null)
            {
                string MainPortfolio = await FileManagment.SaveFile(request.formDto.MainPortfolio);

                student = new()
                {
                    UserName = request.formDto.FullName,
                    FullName = request.formDto.FullName,
                    MainPortfolio = MainPortfolio,
                    StudentReg = request.formDto.StudentReg,
                    RegistrationDate = DateTimeOffset.Now.ToUnixTimeSeconds()
                };
                if (request.formDto.Portfolios is not null)
                {
                    List<string> Portfolios = await FileManagment.SaveFiles(request.formDto.Portfolios);
                    foreach (var item in Portfolios)
                    {
                        Context.Portfolios.Add(new Portfolio() { Student = student, Url = item });
                    }
                }
                var result = await _userManager.CreateAsync(student);

                await Context.SaveChangesAsync(cancellationToken);

                return result;
            }
            else throw new ValidException("User already exists");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
