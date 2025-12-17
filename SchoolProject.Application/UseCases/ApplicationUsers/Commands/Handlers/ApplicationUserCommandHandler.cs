using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Application.UseCases.ApplicationUsers.Commands.Models;
using SchoolProject.Domain.Entities.Identity;

namespace SchoolProject.Application.UseCases.ApplicationUsers.Commands.Handlers;

public class ApplicationUserCommandHandler : ResponseHandler,
                                             ICommandHandler<AddApplicationUserCommand, Response<string>>,
                                             ICommandHandler<UpdateApplicationUserCommand, Response<string>>,
                                             ICommandHandler<DeleteApplicationUserCommand, Response<string>>,
                                             ICommandHandler<ChangeUserPasswordCommand, Response<string>>
{
   private readonly IStringLocalizer<SharedResources> _stringLocalizer;
   private readonly IMapper _mapper;
   private readonly UserManager<ApplicationUser> _userManager;
   private readonly IApplicationUserService _applicationUserService;

   public ApplicationUserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                        IMapper mapper,
                                        UserManager<ApplicationUser> userManager,
                                        IApplicationUserService applicationUserService) : base(stringLocalizer)
   {
      this._stringLocalizer = stringLocalizer;
      this._mapper = mapper;
      this._userManager = userManager;
      this._applicationUserService = applicationUserService;
   }

   #region Add User - Handle
   public async Task<Response<string>> Handle(AddApplicationUserCommand request, CancellationToken cancellationToken)
   {
      // 1. Check if email is exists
      var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);

      // 2. If exists unprocessable action
      if (userWithSameEmail != null)
      {
         return UnprocessableEntity<string>("Email is already exists!");
      }

      // 3. Mapping to applicationUser
      var userToCreate = _mapper.Map<ApplicationUser>(request);

      // 4. Create new user
      var result = await _applicationUserService.CreateUserAsync(userToCreate, request.Password);

      if (result == "Success")
      {
         // 6. Return success
         return Created("User created successfully.");
      }

      return BadRequest<string>();
   }
   #endregion

   #region Update User - Handle
   public async Task<Response<string>> Handle(UpdateApplicationUserCommand request, CancellationToken cancellationToken)
   {
      // 1. check if user is exists
      var userToUpdate = await _userManager.FindByIdAsync(request.Id.ToString());

      // 2. not found
      if (userToUpdate is null)
      {
         return NotFound<string>();
      }

      // 3. mapping
      var mappedUser = _mapper.Map(request, userToUpdate);

      // 4. update
      var resultOfUpdate = await _userManager.UpdateAsync(mappedUser);

      // 5. check of result
      if (!resultOfUpdate.Succeeded)
      {
         return BadRequest<string>();
      }

      // 6. return success
      return Success("");
   }
   #endregion

   #region Delete User - HAndle
   public async Task<Response<string>> Handle(DeleteApplicationUserCommand request, CancellationToken cancellationToken)
   {
      // 1. check if user is exists
      var userToDelete = await _userManager.FindByIdAsync(request.Id.ToString());

      // 2. not found
      if (userToDelete is null)
      {
         return NotFound<string>();
      }

      // 3. delete
      var resultOfUpdate = await _userManager.DeleteAsync(userToDelete);

      // 4. check of result
      if (!resultOfUpdate.Succeeded)
      {
         return BadRequest<string>();
      }

      // 5. return success
      return Success("");
   }
   #endregion

   #region Change Password - Handle
   public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
   {
      // 1. check if user is exists
      var userToUpdate = await _userManager.FindByIdAsync(request.Id.ToString());

      // 2. not found
      if (userToUpdate is null)
      {
         return NotFound<string>();
      }

      // 3. update
      var resultOfUpdate = await _userManager.ChangePasswordAsync(userToUpdate,
                                                                  request.CurrentPassword,
                                                                  request.NewPassword);

      // 5. check of result
      if (!resultOfUpdate.Succeeded)
      {
         return BadRequest<string>();
      }

      // 6. return success
      return Success("");
   }
   #endregion

}
