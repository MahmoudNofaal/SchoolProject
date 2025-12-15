using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.Resources;
using SchoolProject.Application.UseCases.ApplicationUsers.Commands.Models;
using SchoolProject.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

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

   public ApplicationUserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                        IMapper mapper,
                                        UserManager<ApplicationUser> userManager) : base(stringLocalizer)
   {
      this._stringLocalizer = stringLocalizer;
      this._mapper = mapper;
      this._userManager = userManager;
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
      var resultOfCreate = await _userManager.CreateAsync(userToCreate, request.Password);

      // 5. Check result if true
      if (!resultOfCreate.Succeeded)
      {
         return BadRequest<string>("Failed to create user");
      }

      // 5. Assign User to User Role
      var assignRoleResult = await _userManager.AddToRoleAsync(userToCreate, "User");
      if (!assignRoleResult.Succeeded)
      {
         return BadRequest<string>();
      }

      // 6. Return success
      return Created("User created successfully.");
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
