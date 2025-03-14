using System.Collections.Generic;
using ServiceStack;
using ServiceStack.OrmLite;
using SocialBootstrapApi.Models;

namespace SocialBootstrapApi.ServiceInterface
{
    public class Users
    {
        public int[] UserIds { get; set; }
    }

    public class UsersResponse : IHasResponseStatus
    {
        public UsersResponse()
        {
            this.ResponseStatus = new ResponseStatus();
        }

        public List<UserInfo> Results { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }

    public class UsersService : Service
    {
        public object Get(Users request)
        {
            var response = new UsersResponse();

            if (request.UserIds.Length == 0)
                return response;

            var users = Db.SelectByIds<User>(request.UserIds);

            var userInfos = users.ConvertAll(x => x.ConvertTo<UserInfo>());

            return new UsersResponse
            {
                Results = userInfos
            };
        }
    }

    public class UserInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string TwitterUserId { get; set; }
        public string TwitterScreenName { get; set; }
        public string FacebookUserId { get; set; }
        public string ProfileImageUrl64 { get; set; }
    }
}