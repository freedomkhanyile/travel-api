//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using FluentAssertions;
//using Travel.Api.IntegrationTests.Common;
//using Travel.Api.IntegrationTests.Helpers;
//using Travel.Api.Models.Accounts;
//using Travel.Api.Models.Users;
//using Xunit;

//namespace Travel.Api.IntegrationTests.Account
//{
//    [Collection("ApiCollection")]
//    public class PostShould
//    {
//        private readonly ApiServer _server;
//        private readonly HttpClientWrapper _client;
//        private readonly Random _random;

//        public PostShould(ApiServer server)
//        {
//            _random = new Random();
//            _server = server;
//            _client = new HttpClientWrapper(_server.Client);
//        }

//        [Fact]
//        public async Task AuthenticateAdmin()
//        {
//            var email = "test@mail.com";
//            var password = "password";
//        }

//        public async Task<UserWithTokenModel> Authenticate(string email, string password)
//        {
//            var response = await _client.PostAsync<UserWithTokenModel>("api/Account/Authenticate", new LoginModel
//            {
//                Email = email,
//                Password = password
//            });

//            return response;
//        }

//        [Fact]
//        public async Task<UserModel> RegisterNewUser()
//        {
//            // arrange
//            var requestItem = new RegisterModel
//            {
//                Email = "newuser@mail.com",
//                Password = "password",
//                FirstName = "new",
//                Surname = "userEntity",
//                PhoneNumber = "0000000000",
//            };

//            // act
//            var createdUser = await _client.PostAsync<UserModel>("api/Account/Register", requestItem);

//            // assert
//            createdUser.Roles.Should().BeEmpty();
//            createdUser.Email.Should().Be(requestItem.Email);
//            createdUser.Surname.Should().Be(requestItem.Surname);
//            createdUser.PhoneNumber.Should().Be(requestItem.PhoneNumber);

//            return createdUser;
//        }

//        [Fact]
//        public async Task ChangeUserPassword()
//        {
//            // arrange
//            var requestItem = new RegisterModel
//            {
//                Email = "newuser@mail.com",
//                Password = "password",
//                FirstName = "new",
//                Surname = "userEntity",
//                PhoneNumber = "0000000000",
//            };

//            await _client.PostAsync<UserModel>("api/Account/Register", requestItem);
//            var newClient = new HttpClientWrapper(_server.GetAuthenticatedClient(requestItem.Email, requestItem.Password));

//            var newPassword = _random.Next().ToString();
//            await newClient.PostAsync($"api/Account/Password", new ChangeUserPasswordModel {Password = newPassword});

//            await Authenticate(requestItem.Email, newPassword);
//        }
//    }
//}
