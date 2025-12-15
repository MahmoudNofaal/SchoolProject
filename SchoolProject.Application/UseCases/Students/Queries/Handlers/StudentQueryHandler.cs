using AutoMapper;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Application.UseCases.Students.Queries.Models;
using SchoolProject.Application.UseCases.Students.Queries.Results;
using SchoolProject.Application.Wrappers;
using System.Linq.Expressions;

namespace SchoolProject.Application.UseCases.Students.Queries.Handlers;

public class StudentQueryHandler : ResponseHandler,
                                   IQueryHandler<GetStudentListQuery, Response<IEnumerable<StudentListResult>>>,
                                   IQueryHandler<GetStudentByIdQuery, Response<SingleStudentResult>>,
                                   IQueryHandler<GetStudentPaginatedListQuery, PaginatedResult<StudentPaginatedListResult>>
{
   private readonly IStudentService _studentService;
   private readonly IMapper _mapper;
   private readonly IStringLocalizer<SharedResources> _stringLocalizer;

   public StudentQueryHandler(IStudentService studentService,
                              IMapper mapper,
                              IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
   {
      this._studentService = studentService;
      this._mapper = mapper;
      this._stringLocalizer = stringLocalizer;
   }

   #region Get Student List - Handle
   public async Task<Response<IEnumerable<StudentListResult>>> Handle
   (GetStudentListQuery request, CancellationToken cancellationToken)
   {
      var studentList = await _studentService.GetStudentsListAsync();

      var studentListResponse = _mapper.Map<IEnumerable<StudentListResult>>(studentList);

      var result = Success(studentListResponse);

      result.Meta = new
      {
         TotalCount = studentListResponse.Count()
      };

      return result;
   }
   #endregion

   #region Get Single Student - Handle
   public async Task<Response<SingleStudentResult>> Handle
   (GetStudentByIdQuery request, CancellationToken cancellationToken)
   {
      var student = await _studentService.GetByIdWithIncludeAsync(request.Id);

      if (student is null)
      {
         //return NotFound<SingleStudentResult>($"Student with id: {request.Id} not found!");
         return NotFound<SingleStudentResult>(_stringLocalizer[SharedResourcesKeys.NotFound]);
      }

      var studentResponse = _mapper.Map<SingleStudentResult>(student);

      return Success(studentResponse);
   }
   #endregion

   #region Get Student Paginated - Handle
   public async Task<PaginatedResult<StudentPaginatedListResult>> Handle
   (GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
   {

      //Expression<Func<Student, StudentPaginatedListResult>> expression = e
      //   => new StudentPaginatedListResult(e.Id, e.Name_En, e.Address, e.Department.Name_En);

      var queryable = _studentService.GetStudentsAsQueryable();

      var filterQuery = _studentService.FilterStudentPaginatedQuerable(request.OrderBy, request.Search);

      //var paginatedResult = await filterQuery.Select(x => new StudentPaginatedListResult(x.Id, x.Localize(x.Name_Ar, x.Name_En),x.Address,x.Department.Localize(x.Department.Name_Ar, x.Department.Name_En)))
      //                                       .ToPaginatedListAsync(request.PageNumber, request.PageSize);

      var paginatedResult = await _mapper.ProjectTo<StudentPaginatedListResult>(filterQuery)
                                         .ToPaginatedListAsync(request.PageNumber, request.PageSize);

      paginatedResult.Meta = new
      {
         Count = paginatedResult.Data.Count(),
      };

      return paginatedResult;
   } 
   #endregion
}
