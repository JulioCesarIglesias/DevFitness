﻿using AutoMapper;
using DevFitness.API.Core.Entities;
using DevFitness.API.Models.inputModels;
using DevFitness.API.Models.viewModels;
using DevFitness.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFitness.API.Controllers
{
    // api/users/4/meals
    [Route("api/users/{userId}/meals")]
    public class MealsControler : ControllerBase
    {
        private readonly DevFitnessDbContext _dbContext;
        private readonly IMapper _mapper;

        public MealsControler(DevFitnessDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // api/users/4/meals  HTTP GET
        [HttpGet]
        public IActionResult GetAll(int userId)
        {
            var allMeals = _dbContext.Meals.Where(m => m.UserId == userId && m.Active);

            var allMealsViewModels = allMeals.Select(m => new MealViewModel(m.Id, m.Description, m.Calories, m.Date));


            return Ok(allMealsViewModels);
        }

        // api/users/4/meals/3  HTTP GET
        [HttpGet("{mealId}")]
        public IActionResult Get(int userId, int mealId)
        {
            var meal = _dbContext.Meals.SingleOrDefault(m => m.Id == mealId && m.UserId == userId);

            if (meal == null)
                return NotFound();

            // SEM AUTOMAPPER
            // var mealViewModel = new MealViewModel(meal.Id, meal.Description, meal.Calories, meal.Date);
            var mealViewModel = _mapper.Map<MealViewModel>(meal);

            return Ok(mealViewModel);
        }

        // api/users/4/meals  HTTP POST
        [HttpPost]
        public IActionResult Post(int userId, [FromBody] CreateMealInputModel inputModel)
        {
            // SEM AUTOMAPPER
            //var meal = new Meal(inputModel.Description, inputModel.Calories, inputModel.Date, userId);
            var meal = _mapper.Map<Meal>(inputModel);

            _dbContext.Meals.Add(meal);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(Get), new { userId = userId, mealId = meal.Id }, inputModel);
        }

        // api/users/4/meals/3  HTTP PUT
        [HttpPut("{mealId}")]
        public IActionResult Put(int userId, int mealId, [FromBody] UpdateMealInputModel inputModel)
        {
            var meal = _dbContext.Meals.SingleOrDefault(m => m.UserId == userId && m.Id == mealId);

            if (meal == null)
                return NotFound();

            meal.Update(inputModel.Description, inputModel.Calories, inputModel.Date);
            _dbContext.SaveChanges();

            return NoContent();
        }

        // api/users/4/meals/3  HTTP DELETE
        [HttpDelete("{mealId}")]
        public IActionResult Delete(int userId, int mealId)
        {
            var meal = _dbContext.Meals.SingleOrDefault(m => m.UserId == userId && m.Id == mealId);

            if (meal == null)
                return NotFound();

            meal.Desactivate();
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
