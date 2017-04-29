﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTest4.EgeViewModels;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.Services.Impls
{
    public class UserServiceImpl : IUserService
    {
        private readonly ExamAppDbContext _context;
        public UserServiceImpl(ExamAppDbContext context)
        {
            _context = context;
        }
        public UserViewModel GetUser(int id)
        {
           return  GetRatedUserViewModels().FirstOrDefault(x => x.id == id);
        }

        public IEnumerable<UserViewModel> GetRatingForUser(int userId)
        {
            return GetRatedUserViewModels();
        }

        private  IEnumerable<UserViewModel> GetRatedUserViewModels()
        {
            
            return
                _context.Users
                    .Select(
                        x => new
                        {
                            user = x,
                            points = x.Trains.OfType<ExamTrain>()
                            .Sum(etr => etr.Points) ?? 0 
                                      + x.Trains.OfType<FreeTrain>()
                                         .SelectMany(ftr => ftr.TaskAttempts)
                                         .GroupBy(ftta => ftta.ExamTask.Id, (z, y) => new { id = z, attempts = y })
                                         .Select(g => g.attempts.Max(t => t.Points))
                                         .Sum()

                        }
                    )
                .Where(res => res.points != null)
                .OrderByDescending(res => res.points)
                .ToList()
                .Select(
                    (res, i) => new UserViewModel(res.user, (i + 1), res.points)
                );
            

        }

        public void AddCurrentExam(User user, Type type)
        {
            user.CurrentExam = _context.Exams.FirstOrDefault(x => x.GetType() == type);
        }
    }
}