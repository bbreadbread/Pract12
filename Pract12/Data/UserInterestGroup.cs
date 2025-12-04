using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract12.Data
{
    public class UserInterestGroup : ObservableObject
    {
        private int _userId;
        private int _InterestGroupId;
        private InterestGroup _interestGroup;

        private User _user;
        private DateOnly _joinedAt;
        private bool _isModerator;

        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }
        public int InterestGroupId
        {
            get => _InterestGroupId;
            set => SetProperty(ref _InterestGroupId, value);
        }
        public InterestGroup InterestGroup
        {
            get => _interestGroup;
            set => SetProperty(ref _interestGroup, value);
        }
        public User User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
        public DateOnly JoinedAt
        {
            get => _joinedAt;
            set => SetProperty(ref _joinedAt, value);
        }
        public bool IsModerator
        {
            get => _isModerator;
            set => SetProperty(ref _isModerator, value);
        }
    }
}
