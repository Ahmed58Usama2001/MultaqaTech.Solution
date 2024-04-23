using MultaqaTech.Core.Entities.CourseDomainEntities;

namespace MultaqaTech.Service.AuthModuleService;

public static class CreateUserFromSocialLoginExtension
{

    public static async Task<AppUser> CreateUserFromSocialLogin(this UserManager<AppUser> userManager, MultaqaTechContext context, CreateUserFromSocialLogin model, LoginProvider loginProvider)
    {
        //CHECKS IF THE USER HAS NOT ALREADY BEEN LINKED TO AN IDENTITY PROVIDER
        var user = await userManager.FindByLoginAsync(loginProvider.GetDisplayName(), model.LoginProviderSubject);

        if (user is not null)
            return user; //USER ALREADY EXISTS.

        user = await userManager.FindByEmailAsync(model.Email);

        if (user is null)
        {
                Student? student = new();
                context.Students.Add(student);

                user = new AppUser
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    ProfilePictureUrl = model.ProfilePicture,
                    RegistrationDate = DateTime.Now,
                    Student = student,
                    StudentId = student.Id
                };

            try
            {
                await userManager.CreateAsync(user);

                student.AppUser = user;
                student.AppUserId = user.Id;
                context.Students.Update(student);


                //EMAIL IS CONFIRMED; IT IS COMING FROM AN IDENTITY PROVIDER            
                user.EmailConfirmed = true;

                await userManager.UpdateAsync(user);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                context.Students.Remove(student);
                Log.Error(ex.ToString());
                return null;
            }

        }
        else
            return null;

        UserLoginInfo userLoginInfo = null;
        switch (loginProvider)
        {
            case LoginProvider.Google:
                {
                    userLoginInfo = new UserLoginInfo(loginProvider.GetDisplayName(), model.LoginProviderSubject, loginProvider.GetDisplayName().ToUpper());
                }
                break;
            case LoginProvider.Facebook:
                {
                    userLoginInfo = new UserLoginInfo(loginProvider.GetDisplayName(), model.LoginProviderSubject, loginProvider.GetDisplayName().ToUpper());
                }
                break;
            default:
                break;
        }

        //ADDS THE USER TO AN IDENTITY PROVIDER
        var result = await userManager.AddLoginAsync(user, userLoginInfo);

        if (result.Succeeded)
            return user;

        else
            return null;
    }
}