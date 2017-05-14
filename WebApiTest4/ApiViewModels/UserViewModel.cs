﻿using System.Collections.Generic;
using System.Linq;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.ApiViewModels
{
    public class UserViewModel
    {
        public UserViewModel(User sourceUser, int ratingPlace, int points)
        {
            id = sourceUser.Id;
            name = sourceUser.Name;
            avatar = sourceUser.Avatar;
            this.points = points;
            rating_place = ratingPlace;
            use_point = sourceUser.UsePoints;
            badges = sourceUser.Badges.Select(x => new BadgeViewModel(x)).ToList();
        }

        public int id { get; private set; }
        public string name { get; private set; }
        public string avatar { get; private set; }

        public int points { get; private set; }
        public int rating_place { get; private set; }
        public int use_point { get; private set; }
        public List<BadgeViewModel> badges { get; set; }
    }
}