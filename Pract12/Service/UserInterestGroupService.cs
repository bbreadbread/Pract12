using Microsoft.EntityFrameworkCore;
using Pract12.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pract12.Service
{
    public class UserInterestGroupService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;
        public ObservableCollection<UserInterestGroup> UserInterestGroups { get; set; } = new();
        public int Commit() => _db.SaveChanges();
        public void Add(UserInterestGroup groupUser)
        {
            var _groupUser = new UserInterestGroup
            {
                UserId = groupUser.UserId,
                InterestGroupId = groupUser.InterestGroupId,

                JoinedAt = groupUser.JoinedAt,
                IsModerator = groupUser.IsModerator,

                User = groupUser.User,
                InterestGroup = groupUser.InterestGroup,
            };
            _db.Add<UserInterestGroup>(_groupUser);
            _db.SaveChanges();
        }

        public void GetAll(int? groupId = null)
        {
            if (groupId != null)
            {
                var users = _db.UserInterestGroup
                                   .Include(s => s.User)
                                   .ThenInclude(u => u.UserProfile)
                                   .Include(uig => uig.InterestGroup)
                                   .Where(uig => uig.InterestGroupId == groupId)
                                   .ToList();

                UserInterestGroups.Clear();

                foreach (var user in users)
                {
                    UserInterestGroups.Add(user);
                }
            }
        }

        public void GetAllGroupsForUser(int userId)
        {
            var userGroups = _db.UserInterestGroup
                               .Include(ug => ug.User)
                               .Include(ug => ug.InterestGroup)
                               .Where(ug => ug.UserId == userId)
                               .ToList();

            UserInterestGroups.Clear();

            foreach (var userGroup in userGroups)
            {
                UserInterestGroups.Add(userGroup);
            }
        }

        public void Remove(UserInterestGroup InterestGroup)
        {
            _db.Remove<UserInterestGroup>(InterestGroup);
            if (Commit() > 0)
                if (UserInterestGroups.Contains(InterestGroup))
                    UserInterestGroups.Remove(InterestGroup);
        }

        public void UpdateModeratorStatus(int userId, int groupId, bool isModerator)
        {
            try
            {
                var userGroup = _db.UserInterestGroup
                    .FirstOrDefault(ug => ug.UserId == userId && ug.InterestGroupId == groupId);

                if (userGroup != null)
                {
                    userGroup.IsModerator = isModerator;
                    _db.SaveChanges();

                    var localItem = UserInterestGroups
                        .FirstOrDefault(ug => ug.UserId == userId && ug.InterestGroupId == groupId);

                    if (localItem != null)
                    {
                        localItem.IsModerator = isModerator;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
