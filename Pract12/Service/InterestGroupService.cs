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
    public class InterestGroupService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;
        public static ObservableCollection<InterestGroup> InterestGroups { get; set; } = new();
        public int Commit() => _db.SaveChanges();
        public void Add(InterestGroup interestGroup)
        {
            var _interestGroup = new InterestGroup
            {
                Id = interestGroup.Id,
                Title = interestGroup.Title,
                Description = interestGroup.Description,
            };
            _db.Add<InterestGroup>(_interestGroup);
            Commit();
            InterestGroups.Add(_interestGroup);

        }
        public void GetAll()
        {
            var groups = _db.InterestGroup.ToList();

            InterestGroups.Clear();
            foreach (var group in groups)
                InterestGroups.Add(group);
        }
        public InterestGroupService()
        {
            GetAll();
        }
        public void Remove(InterestGroup InterestGroup)
        {
            _db.Remove<InterestGroup>(InterestGroup);
            if (Commit() > 0)
                if (InterestGroups.Contains(InterestGroup))
                    InterestGroups.Remove(InterestGroup);
        }

        public void LoadRelation(InterestGroup group, string relation)
        {
            var entry = _db.Entry(group);
            var navigation = entry.Metadata.FindNavigation(relation)
            ?? throw new InvalidOperationException($"Navigation '{relation}' not found");

            if (navigation.IsCollection)
            {
                entry.Collection(relation).Load();
            }
            else
            {
                entry.Reference(relation).Load();
            }
        }
    }
}
