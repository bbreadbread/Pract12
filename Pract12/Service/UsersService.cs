using Microsoft.EntityFrameworkCore;
using Pract12.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract12.Service
{
    public class UsersService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;
        public ObservableCollection<User> Users { get; set; } = new();
        public UsersService()
        {
            GetAll();
        }
        public void Add(User user)
        {
            var _user = new User
            {
                Login = user.Login,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                CreatedAt = user.CreatedAt,
                UserProfile = user.UserProfile,
                RoleId = user.RoleId,
                Role = user.Role,
            };
            _db.Add<User>(_user);
            Commit();
        }
        public int Commit() => _db.SaveChanges();
        public void GetAll(int? roleId = null)
        {
            if (roleId != null)
            {
                var users = _db.Users
                                    .Include(s => s.UserProfile)
                                    .Include(s => s.Role)
                                    .Where(u => u.RoleId == roleId)
                                    .ToList();

                Users.Clear();

                foreach (var user in users)
                {
                    Users.Add(user);
                }



            }
            else
            {
                var users = _db.Users
                                            .Include(s => s.UserProfile)
                                            .Include(s => s.Role)
                                            .ToList();

                Users.Clear();

                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }



        }
        public void Remove(User user)
        {
            _db.Remove<User>(user);
            if (Commit() > 0)
                if (Users.Contains(user))
                    Users.Remove(user);
        }

        public void AddProfile(UserProfile profile)
        {
            _db.UserProfiles.Add(profile);
        }

        public void UpdateProfile(UserProfile profile)
        {
            var existing = _db.UserProfiles.Find(profile.Id);
            if (existing != null)
            {
                existing.Phone = profile.Phone;
                existing.Birthday = profile.Birthday;
                existing.Bio = profile.Bio;
                existing.AvatarUrl = profile.AvatarUrl;
            }
        }


    }
}
