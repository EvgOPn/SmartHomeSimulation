using System;
using Core.DI;
using PlayFab;
using PlayFab.ClientModels;
using UserManagement.Other;

namespace UserManagement
{
	/// <summary>
	/// LoginAPI.cs responsible for communicating with backend. Currently, we use PlayFab as a backend service
	/// </summary>
	public class LoginAPI : DependentMonoBehaviour
	{		
		[Inject] private UserProfile _userProfile;

		protected override void OnAwake()
		{

		}

		public void Login(string email, string password, Action successfulCallback, Action<string> errorCallback)
		{
			if (!FieldValidator.ValidateEmail(email))
			{
				errorCallback?.Invoke("Please, check your email");
				return;
			}

			if (!FieldValidator.ValidatePassword(password))
			{
				errorCallback?.Invoke("Password should be between 8-20 characters and contains at least one upper char");
				return;
			}

			LoginWithEmailAddressRequest request = new LoginWithEmailAddressRequest
			{
				Email = email,
				Password = password
			};

			PlayFabClientAPI.ForgetAllCredentials();
			PlayFabClientAPI.LoginWithEmailAddress(request,
				result =>
				{
					_userProfile.UserData.PlayFabId = result.PlayFabId;
					_userProfile.GetUserDataFromServer(successfulCallback, errorCallback);
				},
				error => errorCallback?.Invoke(error.ErrorMessage));
		}

		public void RegisterAccount(string email, string password, string username, Action successfulCallback, Action<string> errorCallback)
		{
			if (!FieldValidator.ValidateEmail(email))
			{
				errorCallback?.Invoke("Please, check your email: " + email);
				return;
			}

			if (!FieldValidator.ValidatePassword(password))
			{
				errorCallback?.Invoke("Password should be between 6-20 characters and contains at least one upper char");
				return;
			}


			RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest
			{
				Email = email,
				Password = password,
				Username = username,
				DisplayName = username,
				RequireBothUsernameAndEmail = true
			};

			PlayFabClientAPI.RegisterPlayFabUser(request,
				result =>
				{
					_userProfile.UserData.Username = username;
					_userProfile.UserData.PlayFabId = result.PlayFabId;
					successfulCallback?.Invoke();
				},
				error => errorCallback?.Invoke(error.ErrorMessage));
		}
	}
}