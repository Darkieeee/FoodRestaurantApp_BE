using FoodRestaurantApp_BE.Exceptions;
using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Repositories;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.EntityFrameworkCore;

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

        public OperationResult<SystemUser> Create(SystemUser user)
        {
            return CreateAsync(user).Result;
        }

        public async Task<OperationResult<SystemUser>> CreateAsync(SystemUser user)
        {            
            OperationResult<SystemUser> result = new();
            try 
            {
                bool created = await _userRepository.InsertAsync(user) > 0;

                if (created)
                {
                    result.Success = true;
                    result.Message = "Add new user successfully";
                    result.Value = user;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Add new user unsuccessfully";
                }
            } catch(Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Exception = ex;
            }
            return result;
        }

        public OperationResult<SystemUser> Update(SystemUser user)
        {
            return UpdateAsync(user).Result;
        }

        public async Task<OperationResult<SystemUser>> UpdateAsync(SystemUser user)
        {            
            OperationResult<SystemUser> result = new();
            try
            {
                bool updated = await _userRepository.UpdateAsync(user) > 0;

                if (updated)
                {
                    result.Success = true;
                    result.Message = "Updated user successfully";
                    result.Value = user;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Updated user unsuccessfully";
                }
            } catch(Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Exception = ex;
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

        private static UserDetails GetUserDetails(SystemUser user, RoleDetails roleDetails)
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

        private static UserDetails GetUserDetails(SystemUser user)
        {
            return new UserDetails()
            {
                Email = user.Email,
                FullName = user.FullName,
                Avatar = user.Avatar,
                IsActive = user.IsActive,
                Name = user.Name,
                Uuid = user.Uuid,
                Role = new RoleDetails()
                {
                    Description = user.Role.Description,
                    Editable = user.Role.Editable,
                    Name = user.Role.Name,
                    Permissions = user.Role.Permissions.Select(x => new PermissionDto { 
                        Id = x.Id, 
                        Name = x.Name 
                    }).ToList(),
                }
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
                    Id = user.Role.Id,
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
            List<int> roleIds = users.Select(x => x.RoleId).ToList();
            List<Role> roles = await _roleRepository.FindByIds(roleIds).ToListAsync();

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
                userDetailsList.Add(GetUserDetails(u, roleDetailsDict[u.RoleId]));
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

            return GetUserDetails(user, roleDetails);
        }

        public Pagination<UserShortDetails> GetPagination(string? search, PageSizeOption pageSizeOption, int currentPage)
        {
            var users = _userRepository.GetAll();
            
            if(!string.IsNullOrEmpty(search))
            {
                users = users.Where(x => x.FullName.Contains(search!));
            }

            int totalCount = users.Count();

            int pageSize = (int) pageSizeOption;
            int totalPage = (int) Math.Ceiling((double) totalCount / pageSize);
            int skipRows = (currentPage - 1) * pageSize;

            /*List<SystemUser> userList = users.OrderBy(x => x.Name).Skip(skipRows).Take(pageSize).ToList();
            List<Role> roles = _roleRepository.FindByIds(userList.Select(x => x.RoleId).Distinct().ToList()).ToList();
            IEnumerable<UserShortDetails> userRoleJoin = userList.Join(roles,
                                                                       user => user.RoleId, role => role.Id,
                                                                       (user, role) =>
                                                                       {
                                                                           user.Role = role;
                                                                           return GetShortUserDetails(user);
                                                                       });*/

            List<UserShortDetails> userShortDetails = users.OrderBy(x => x.Name)
                                                           .Skip(skipRows).Take(pageSize)
                                                           .Include(x => x.Role)
                                                           .Select(x => GetShortUserDetails(x))
                                                           .ToList();

            Pagination<UserShortDetails> pagination;
            pagination = PaginationHelper.CreateBuilder<UserShortDetails>()
                                         .WithCurrentPage(currentPage)
                                         .WithPageSize(pageSizeOption)
                                         .WithData(userShortDetails, totalCount, totalPage)
                                         .Build();

            return pagination;
        }

        public UserDetails GetById(int id)
        {
            UserDetails? user1 = _userRepository.FindById(id)
                                                .Include(x => x.Role)
                                                .ThenInclude(x => x.Permissions)
                                                .Select(x => GetUserDetails(x))
                                                .FirstOrDefault();
            return user1 is null ? throw new NotFoundException($"Not found user with id {id}", null) : user1;
        }

        public UserDetails GetByUuid(string uuid)
        {
            UserDetails? user = _userRepository.FindByUuid(uuid)
                                               .Include(x => x.Role)
                                               .ThenInclude(x => x.Permissions)
                                               .Select(x => GetUserDetails(x))
                                               .FirstOrDefault();
            return user is null ? throw new NotFoundException($"Not found user with uuid {uuid}", null) : user;
        }
    }
}
