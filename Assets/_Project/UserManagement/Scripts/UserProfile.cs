using System;
using Core.DI;
using PlayFab;
using UnityEngine;
using PlayFab.ClientModels;
using System.Globalization;
using UserManagement.Models;
using System.Collections.Generic;
using Core.Encryption;

namespace UserManagement
{
	public class UserProfile : DependentMonoBehaviour
	{
		public UserData UserData = new UserData();

		private const string PlayerPrefsEmailKey = "Email";
		private const string PlayerPrefsPasswordKey = "Password";
		private const string PlayFabUserDataKey = "UserData";
		private const string PlayerPrefsV1EmailKey = "player_email";
		private const string PlayerPrefsV1PasswordKey = "player_password";
		private const float TimeBetweenSendLastTimeOnline = 10.0f;

		public bool IsAuthorized { get; set; } = false;

		private float _lastOnlineTimer;

		protected override void OnAwake()
		{

		}

		public Tuple<string, string> GetLoginFormData()
		{
			string email = PlayerPrefs.GetString(PlayerPrefsEmailKey);
			string password = PlayerPrefs.GetString(PlayerPrefsPasswordKey);

			// For supporting an old version of the app try to use saved login data
			//if (string.IsNullOrEmpty(email))
			//{
			//    email = PlayerPrefs.GetString(PlayerPrefsV1EmailKey);
			//    password = PlayerPrefs.GetString(PlayerPrefsV1PasswordKey);
			//    SaveLoginFormData(email, password);
			//}

			string decryptedPassword = AESEncryption.Decrypt(password);

			return new Tuple<string, string>(email, decryptedPassword);
		}

		public void SaveLoginFormData(string email, string password)
		{
			string encryptedPassword = AESEncryption.Encrypt(password);

			PlayerPrefs.SetString(PlayerPrefsEmailKey, email);
			PlayerPrefs.SetString(PlayerPrefsPasswordKey, encryptedPassword);
			PlayerPrefs.Save();
		}


		public void GetUserDataFromServer(Action successfulCallback = null, Action<string> errorCallback = null)
		{
			GetUserDataByPlayFabId(UserData.PlayFabId, (userData) =>
			{
				// If account do not contains UserData profile
				// Have to create it on server
				if (userData == null)
				{
					GetAccountInfoRequest request = new GetAccountInfoRequest();
					PlayFabClientAPI.GetAccountInfo(request, (result) =>
					{
						UserData.Username = result.AccountInfo.Username;
						UpdateUserDataOnServer(successfulCallback, errorCallback);
					}, (error) => errorCallback.Invoke(error.ErrorMessage));
				}
				else
				{
					UserData = userData;
					successfulCallback?.Invoke();
				}

			}, errorCallback);
		}

		public void UpdateUserDataOnServer(Action successfulCallback = null, Action<string> errorCallback = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				errorCallback?.Invoke("User not logged");
				return;
			}

			string jsonString = JsonUtility.ToJson(UserData);
			PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
			{
				Permission = UserDataPermission.Public,
				Data = new Dictionary<string, string>()
				{
					[PlayFabUserDataKey] = jsonString
				}
			},
				result => successfulCallback?.Invoke(),
				error => errorCallback?.Invoke(error.ErrorMessage));
		}

		public void SearchUserByUsername(string username, Action<UserData> successfulCallback = null, Action<string> errorCallback = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				errorCallback?.Invoke("User not logged");
				return;
			}

			PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest
			{
				Username = username
			},
				result => GetUserDataByPlayFabId(result.AccountInfo.PlayFabId, successfulCallback, errorCallback),
				error => errorCallback?.Invoke(error.ErrorMessage));
		}

		private void GetUserDataByPlayFabId(string playFabId, Action<UserData> successfulCallback = null, Action<string> errorCallback = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				errorCallback?.Invoke("User not logged");
				return;
			}

			PlayFabClientAPI.GetUserData(new GetUserDataRequest()
			{
				PlayFabId = playFabId,
			}, result =>
			{
				if (result.Data == null)
				{
					errorCallback?.Invoke("Result.Data is null");
					return;
				}

				if (!result.Data.ContainsKey(PlayFabUserDataKey))
				{
					successfulCallback?.Invoke(null);
					return;
				}

				try
				{
					string jsonString = result.Data[PlayFabUserDataKey].Value;
					UserData userData = JsonUtility.FromJson<UserData>(jsonString);
					successfulCallback?.Invoke(userData);
				}
				catch (Exception exception)
				{
					errorCallback?.Invoke("UserData parsing error: " + exception.Message);
				}

			}, (error) => errorCallback?.Invoke("Got error retrieving user data: " + error.ErrorMessage));
		}


		private void Update()
		{
			SendLastTimeOnline();
		}

		private void SendLastTimeOnline()
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				return;
			}

			_lastOnlineTimer += Time.deltaTime;
			if (_lastOnlineTimer < TimeBetweenSendLastTimeOnline)
			{
				return;
			}

			_lastOnlineTimer = 0;
			UserData.LastTimeOnline = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
			UpdateUserDataOnServer();
		}
	}
}