﻿namespace MultaqaTech.Service.AuthModuleService;

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
            var userName = IsUsernameLatinChars(model.UserName) ? model?.UserName : model.Email.Split('@').First();


            user = new AppUser
                {
                    Email = model.Email,
                    UserName = userName,
                    ProfilePictureUrl = model.ProfilePicture,
                    RegistrationDate = DateTime.Now,
                };

            try
            {
                await userManager.CreateAsync(user);

                //EMAIL IS CONFIRMED; IT IS COMING FROM AN IDENTITY PROVIDER            
                user.EmailConfirmed = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return new AppUser();
            }

                await context.SaveChangesAsync();
        }
       

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

    private static bool IsUsernameLatinChars(string username)
    {
        // Matches only letters (a-z and A-Z)
        var regex = @"^[a-zA-Z]+$";
        return Regex.IsMatch(username, regex);
    }
}