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
            return _userRepository.GetAll().Select(x => ToListView(x)).ToList();
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

                return ToShortDetails(user);
            }
            return null;
        }

        private static UserListView ToListView(SystemUser user)
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

        private static UserDetails ToDetails(SystemUser user)
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

        private static UserShortDetails ToShortDetails(SystemUser user)
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

        private async Task<List<UserDetails>> JoinUserDataAsync(IEnumerable<SystemUser> users)
        {
            List<int> roleIds = users.Select(x => x.RoleId).Distinct().ToList();

            var join1 = _rolePermissionRepository.FindByRoleIds(roleIds)
                                                 .Join(_roleRepository.GetAll(),
                                                       rolePermission => rolePermission.RoleId, role => role.Id,
                                                       (rolePermission, role) => new { role.Id, role.Name, role.Description, role.Editable, rolePermission.PermissionId });

            var join2 = join1.GroupJoin(_permissionRepository.GetAll().Select(x => new { x.Id, x.Name }),
                                        (join1Item) => join1Item.PermissionId, permission => permission.Id,
                                        (join1Item, permission) => new { 
                                            join1Item.Id,
                                            join1Item.Name, 
                                            join1Item.Description, 
                                            join1Item.Editable, 
                                            Permissions = permission.Select(x => new PermissionDto() { Id = x.Id, Name = x.Name }) 
                                        });

            List<RoleDetails> roleDetailsList = join2.Select(x => new RoleDetails() { 
                Name = x.Name,
                Description = x.Description,
                Editable = x.Editable,
                Permissions = x.Permissions.ToList()
            }).ToList();

            List<UserDetails> userDetailsList = await join2.Join(users,
                                                                 join2Item => join2Item.Id, user => user.RoleId,
                                                                 (join2Item, user) => new UserDetails { 
                                                                     Email = user.Email,
                                                                     FullName = user.FullName,
                                                                     Name = user.Name,
                                                                     Uuid = user.Uuid,
                                                                     IsActive = user.IsActive,
                                                                     Avatar = user.Avatar,
                                                                     Role = new RoleDetails() {
                                                                         Name = join2Item.Name,
                                                                         Description = join2Item.Description,
                                                                         Editable = join2Item.Editable,
                                                                         Permissions = join2Item.Permissions.ToList()
                                                                     }
                                                                 }).ToListAsync();
            
            return userDetailsList;
        }

        private async Task<UserDetails> JoinUserDataAsync(SystemUser user)
        {
            var join1 = _rolePermissionRepository.FindByRoleId(user.RoleId)
                                                 .Join(_roleRepository.GetAll(),
                                                       rolePermission => rolePermission.RoleId, role => role.Id,
                                                       (rolePermission, role) => new { role.Id, role.Name, role.Description, role.Editable, rolePermission.PermissionId });

            RoleDetails roleDetails = await join1.GroupJoin(_permissionRepository.GetAll().Select(x => new { x.Id, x.Name }),
                                                            (join1Item) => join1Item.PermissionId, permission => permission.Id,
                                                            (join1Item, permission) => new RoleDetails {
                                                                Name = join1Item.Name,
                                                                Description = join1Item.Description,
                                                                Editable = join1Item.Editable,
                                                                Permissions = permission.Select(x => new PermissionDto() { Id = x.Id, Name = x.Name }).ToList()
                                                            }).FirstAsync();

            UserDetails userDetails = new()
            {
                Email = user.Email,
                FullName = user.FullName,
                Name = user.Name,
                Uuid = user.Uuid,
                IsActive = user.IsActive,
                Avatar = user.Avatar,
                Role = roleDetails
            };

            return userDetails;
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

            List<UserShortDetails> userShortDetails = users.OrderBy(x => x.Name)
                                                           .Skip(skipRows).Take(pageSize)
                                                           .Include(x => x.Role)
                                                           .Select(x => ToShortDetails(x))
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
                                                .Select(x => ToDetails(x))
                                                .FirstOrDefault();
            return user1 is null ? throw new NotFoundException($"Not found user with id {id}", null) : user1;
        }

        public UserDetails GetByUuid(string uuid)
        {
            UserDetails? user = _userRepository.FindByUuid(uuid)
                                               .Include(x => x.Role)
                                               .ThenInclude(x => x.Permissions)
                                               .Select(x => ToDetails(x))
                                               .FirstOrDefault();
            return user is null ? throw new NotFoundException($"Not found user with uuid {uuid}", null) : user;
        }
    }
}
