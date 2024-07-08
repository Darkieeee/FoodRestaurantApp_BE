using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Repositories;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FoodRestaurantApp_BE.Services
{
    public class UserService(IUserRepository userRepository, IRoleRepository roleRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;

        public DbDMLStatementResult Create(SystemUser user)
        {
            return CreateAsync(user).Result;
        }

        public async Task<DbDMLStatementResult> CreateAsync(SystemUser user)
        {
            string? uniqueValueFailed = CheckUniqueValue(user);
            DbDMLStatementResult result = new();

            if (!uniqueValueFailed.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = uniqueValueFailed;

                return result;
            }
            bool created = await _userRepository.InsertAsync(user) > 0;
            
            if(created) {
                result.Success = true;
                result.Message = "Add new users successfully";
            }
            else
            {
                result.Success = false;
                result.Message = "Add new users unsuccessfully";
            }

            return result;
        }

        private string? CheckUniqueValue(SystemUser user)
        {
            if(CheckUsernameIfExists(user.Name))
            {
                return "Username has already existed";
            }
            else if(CheckEmailIfExists(user.Email))
            {
                return "Email has already existed";
            } 
            else
            {
                return null;
            }    
        }

        public DbDMLStatementResult Update(SystemUser user)
        {
            return UpdateAsync(user).Result;
        }

        public async Task<DbDMLStatementResult> UpdateAsync(SystemUser user)
        {
            string? uniqueValueFailed = CheckUniqueValue(user);
            DbDMLStatementResult result = new();

            if (!uniqueValueFailed.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = uniqueValueFailed;

                return result;
            }
            bool created = await _userRepository.UpdateAsync(user) > 0;

            if (created)
            {
                result.Success = true;
                result.Message = "Add new users successfully";
            }
            else
            {
                result.Success = false;
                result.Message = "Add new users unsuccessfully";
            }

            return result;
        }

        public List<UserShortDetailModelResponse> GetAll() {
            return _userRepository.GetAll().Select(x => GetShortInformation(x)).ToList();
        }

        public async Task<UserDetailModelResponse?> Authenticate(string username, string password)
        {
            SystemUser? user = _userRepository.FindByName(username)
                                              .Include(x => x.Role)
                                              .FirstOrDefault();
            if(user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                user.LastLogin = DateTime.Now;
                await UpdateAsync(user);

                return GetDetailInformation(user, user.Role);
            }
            return null;
        }

        private static UserDetailModelResponse GetDetailInformation(SystemUser user, Role role)
        {
            return new UserDetailModelResponse()
            {
                Email = user.Email,
                Uuid = user.Uuid,
                Name = user.Name,
                FullName = user.FullName,
                IsActive = user.IsActive,
                Role = new RoleModelResponse() { RoleId = role.Id, RoleName = role.Name }
            };
        }

        private static UserShortDetailModelResponse GetShortInformation(SystemUser user)
        {
            return new UserShortDetailModelResponse()
            {
                Email = user.Email,
                Uuid = user.Uuid,
                FullName = user.FullName,
                Name = user.Name,
            };
        }

        public bool CheckUsernameIfExists(string username)
        {
            return _userRepository.FindByName(username).Any();
        }

        public bool CheckEmailIfExists(string email)
        {
            return _userRepository.FindByEmail(email).Any();
        }

        public Pagination<UserDetailModelResponse> GetPagination(string? search, PageSizeOption pageSizeOption, int currentPage)
        {
            var users = _userRepository.GetAll();
            var roles = _roleRepository.GetAll().ToList();

            if(!search.IsNullOrEmpty())
            {
                users = users.Where(x => x.FullName.Contains(search!, StringComparison.OrdinalIgnoreCase));
            }

            int totalCount = users.Count();

            IEnumerable<UserDetailModelResponse> userRoleJoin = users.Join(roles,
                                                                           user => user.RoleId, role => role.Id,
                                                                           GetDetailInformation);
            
            
            int pageSize = (int) pageSizeOption;
            int totalPage = (int) Math.Ceiling((double) totalCount / pageSize);
            int skipRows = (currentPage - 1) * pageSize;
            List<UserDetailModelResponse> userDetailModelResponses = userRoleJoin.Skip(skipRows)
                                                                                 .Take(pageSize)
                                                                                 .ToList();

            Pagination<UserDetailModelResponse> pagination;
            pagination = PaginationHelper.CreateBuilder<UserDetailModelResponse>()
                                         .WithCurrentPage(currentPage)
                                         .WithPageSize(pageSizeOption)
                                         .WithData(userDetailModelResponses, totalCount, totalPage)
                                         .Build();
            return pagination;
        }

        public UserDetailModelResponse? GetById(int id)
        {
            UserDetailModelResponse? user = _userRepository.FindById(id)
                                                           .Include(x => x.Role)
                                                           .Select(x => GetDetailInformation(x, x.Role))
                                                           .FirstOrDefault();
            return user;
        }

        public UserDetailModelResponse? GetByUuid(string uuid)
        {
            UserDetailModelResponse? user = _userRepository.FindByUuid(uuid)
                                                           .Include(x => x.Role)
                                                           .Select(x => GetDetailInformation(x, x.Role))
                                                           .FirstOrDefault();
            return user;
        }
    }
}
