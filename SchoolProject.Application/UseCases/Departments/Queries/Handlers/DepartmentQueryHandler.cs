using AutoMapper;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Application.UseCases.Departments.Queries.Models;
using SchoolProject.Application.UseCases.Departments.Queries.Results;
using SchoolProject.Application.Wrappers;
using SchoolProject.Domain.Entities;
using System.Linq.Expressions;

namespace SchoolProject.Application.UseCases.Departments.Queries.Handlers;

public class DepartmentQueryHandler : ResponseHandler,
                                      IQueryHandler<GetDepartmentByIdQuery, Response<SingleDepartmentResult>>
{
   private readonly IStringLocalizer<SharedResources> _stringLocalizer;
   private readonly IDepartmentService _departmentService;
   private readonly IMapper _mapper;
   private readonly IStudentService _studentService;

   public DepartmentQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                 IDepartmentService departmentService,
                                 IMapper mapper,
                                 IStudentService studentService) : base(stringLocalizer)
   {
      this._stringLocalizer = stringLocalizer;
      this._departmentService = departmentService;
      this._mapper = mapper;
      this._studentService = studentService;
   }

   #region Get Single Department - Handle
   public async Task<Response<SingleDepartmentResult>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
   {
      // 1. Get data from database
      var department = await _departmentService.GetByIdWithIncludeAsync(request.Id);

      // 2. Check on id
      if (department == null)
      {
         return NotFound<SingleDepartmentResult>(_stringLocalizer[SharedResourcesKeys.NotFound]);
      }

      // 3. Map data to response
      var departmentReponse = _mapper.Map<SingleDepartmentResult>(department);

      // 4. Pagination: Get Student PaginatedList
      Expression<Func<Student, SingleStudentResult>> expression = e => new SingleStudentResult(e.Id, e.Localize(e.Name_Ar, e.Name_En));
      var studentQuerable = _studentService.GetStudentsByDepartmentIdAsQueryable(request.Id);
      var paginatedList = await studentQuerable.Select(expression).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);

      departmentReponse.StudentsList = paginatedList;

      // 5. Return response
      return Success(departmentReponse);
   } 
   #endregion

}
