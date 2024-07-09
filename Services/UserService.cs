using FoodRestaurantApp_BE.Exceptions;
using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Repositories;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FoodRestaurantApp_BE.Services
{
    public class UserService(IUserRepository userRepository, IRoleRepository roleRepository,
                             IRolePermissionRepository rolePermissionRepository, 
                             IPermissionRepository permissionRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository = rolePermissionRepository;
        private readonly IPermissionRepository _permissionRepository = permissionRepository;

        public OperationResult Create(SystemUser user)
        {
            return CreateAsync(user).Result;
        }

        public async Task<OperationResult> CreateAsync(SystemUser user)
        {            
            OperationResult result = new();
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

        public OperationResult Update(SystemUser user)
        {
            return UpdateAsync(user).Result;
        }

        public async Task<OperationResult> UpdateAsync(SystemUser user)
        {            
            OperationResult result = new();

            bool updated = await _userRepository.UpdateAsync(user) > 0;

            if(updated)
            {
                result.Success = true;
                result.Message = "Updated user successfully";
            }
            else
            {
                result.Success = false;
                result.Message = "Updated user unsuccessfully";
            }

            return result;
        }

        public List<UserListView> GetAll() {
            return _userRepository.GetAll().Select(x => GetUserListView(x)).ToList();
        }

        public async Task<UserShortDetails?> Authenticate(string username, string password, bool isAdmin)
        {
            SystemUser? user = _userRepository.FindByNameAndIsAdmin(username, isAdmin)
                                              .Include(x => x.Role)
                                              .FirstOrDefault();
            if(user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                user.LastLogin = DateTime.Now;
                await UpdateAsync(user);

                return GetShortUserDetails(user);
            }
            return null;
        }

        private static UserListView GetUserListView(SystemUser user)
        {
            return new UserListView()
            {
                Email = user.Email,
                Uuid = user.Uuid,
                Name = user.Name,
                Avatar = user.Avatar,
                FullName = user.FullName,
                IsActive = user.IsActive,
            };
        }

        private static UserDetails GetUserDetail(SystemUser user, RoleDetails roleDetails)
        {
            return new UserDetails()
            {
                Uuid = user.Uuid,
                Name = user.Name,
                Email = user.Email,
                Avatar = user.Avatar,
                FullName = user.FullName,
                IsActive = user.IsActive,
                Role = roleDetails
            };
        }

        private static UserShortDetails GetShortUserDetails(SystemUser user)
        {
            return new UserShortDetails()
            {
                Email = user.Email,
                Uuid = user.Uuid,
                Name = user.Name,
                Avatar = user.Avatar,
                FullName = user.FullName,
                IsActive = user.IsActive,
                Role = new RoleListView() { 
                    Description = user.Role.Description, 
                    Name = user.Role.Name 
                }
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

        private async Task<List<UserDetails>> JoinUserData(List<SystemUser> users)
        {
            List<Role> roles = await _roleRepository.FindByIds(users.Select(x => x.RoleId).ToList()).ToListAsync();

            Dictionary<int, List<string>> rolespermissions = await _rolePermissionRepository.FindByRoleIds(roles.Select(x => x.Id).ToList())
                                                                                            .GroupBy(x => x.RoleId)
                                                                                            .ToDictionaryAsync(k => k.Key,
                                                                                                               v => v.Select(x => x.PermissionId).ToList());
            List<string> permissionIds = rolespermissions.SelectMany(x => x.Value).ToList();

            Dictionary<string, PermissionDto> permissions = await _permissionRepository.FindByIds(permissionIds)
                                                                                       .ToDictionaryAsync(k => k.Id,
                                                                                                          v => new PermissionDto() { Id = v.Id, Name = v.Name });

            Dictionary<int, RoleDetails> roleDetailsDict = roles.ToDictionary(k => k.Id, 
                                                                              v => new RoleDetails() { 
                                                                                  Description = v.Description, 
                                                                                  Editable = v.Editable, 
                                                                                  Name = v.Name, 
                                                                                  Permissions = rolespermissions[v.Id].Select(x => permissions[x]).ToList() });

            List<UserDetails> userDetailsList = [];
            
            users.ForEach(u => {
                userDetailsList.Add(GetUserDetail(u, roleDetailsDict[u.RoleId]));
            });

            return userDetailsList;
        }

        private async Task<UserDetails> JoinUserData(SystemUser user)
        {
            Role role = await _roleRepository.FindById(user.RoleId).FirstAsync();
            List<string> permissionIds = _rolePermissionRepository.FindByRoleId(user.RoleId)
                                                                  .Select(x => x.PermissionId)
                                                                  .ToList();
            List<PermissionDto> permissions = _permissionRepository.FindByIds(permissionIds)
                                                                   .Select(x => new PermissionDto() { Id = x.Id, Name = x.Name })
                                                                   .ToList();
            RoleDetails roleDetails =  new()
            {
                Name = role.Name,
                Description = role.Description,
                Editable = true,
                Permissions = permissions
            };

            return GetUserDetail(user, roleDetails);
        }

        public Pagination<UserShortDetails> GetPagination(string? search, PageSizeOption pageSizeOption, int currentPage)
        {
            var users = _userRepository.GetAll();
            
            var permissions = _permissionRepository.GetAll().ToList();

            if(!search.IsNullOrEmpty())
            {
                users = users.Where(x => x.FullName.Contains(search!));
            }

            int totalCount = users.Count();

            int pageSize = (int) pageSizeOption;
            int totalPage = (int) Math.Ceiling((double) totalCount / pageSize);
            int skipRows = (currentPage - 1) * pageSize;
            List<SystemUser> userList = users.OrderBy(x => x.Name).Skip(skipRows).Take(pageSize).ToList();
            List<Role> roles = _roleRepository.FindByIds(users.Select(x => x.RoleId).ToList()).ToList();
            IEnumerable<UserShortDetails> userRoleJoin = userList.Join(roles,
                                                                       user => user.RoleId, role => role.Id,
                                                                       (user, role) => {
                                                                           user.Role = role;
                                                                           return GetShortUserDetails(user);
                                                                       });

            Pagination<UserShortDetails> pagination;
            pagination = PaginationHelper.CreateBuilder<UserShortDetails>()
                                         .WithCurrentPage(currentPage)
                                         .WithPageSize(pageSizeOption)
                                         .WithData(userRoleJoin.ToList(), totalCount, totalPage)
                                         .Build();
            return pagination;
        }

        public UserDetails GetById(int id)
        {
            SystemUser? user = _userRepository.FindById(id)
                                              .FirstOrDefault();
            return user is null ? throw new NotFoundException($"Not found user with id {id}", null) : JoinUserData(user).Result;
        }

        public UserDetails GetByUuid(string uuid)
        {
            SystemUser? user = _userRepository.FindByUuid(uuid)
                                              .FirstOrDefault();
            return user is null ? throw new NotFoundException($"Not found user with uuid {uuid}", null) : JoinUserData(user).Result;
        }
    }
}
