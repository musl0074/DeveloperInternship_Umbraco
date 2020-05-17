﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Umbraco_InternShip_MVC.Data;
using Umbraco_InternShip_MVC.Models;
using Umbraco_InternShip_MVC.ViewModel;

namespace Umbraco_InternShip_MVC.Controllers
{
    public class DrawController : Controller
    {
        private readonly MvcDrawContext _mvcDrawContext;

        //Dependency Injection with my Database Context
        public DrawController(MvcDrawContext mvcDrawContext)
        {
            _mvcDrawContext = mvcDrawContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubmissionForm submissionForm)
        {
            if (!ModelState.IsValid) { return RedirectToAction("Index", submissionForm);}

            DateTime minimum = DateTime.Now.AddYears(-18);

            //Check age
            if(minimum < submissionForm.Age) { return BadRequest(); }
            

            var serialNumber = await _mvcDrawContext.SerialNumbers.Where(s => s.SerialNumberValue == submissionForm.SerialNumber).FirstOrDefaultAsync();

            //Valid serial number?
            if (serialNumber == null || serialNumber.IsActive == false) { return BadRequest(); }
              

            //Does User Exist?
            var user = await _mvcDrawContext.Users.Where(u => u.EmailAddress == submissionForm.EmailAddress).FirstOrDefaultAsync();

            if(user == null)
            {
                user = new UserDraw() { FirstName = submissionForm.FirstName, LastName = submissionForm.LastName, EmailAddress = submissionForm.EmailAddress };
                var result = await _mvcDrawContext.Users.AddAsync(user);
                await _mvcDrawContext.SaveChangesAsync();
            }

            Draw draw = new Draw() { SerialNumberId = serialNumber.Id, UserDrawId = user.Id };
            var resultForDraw = await _mvcDrawContext.Draws.AddAsync(draw);
            serialNumber.AmountUsed += 1;
            if (serialNumber.AmountUsed >= 2)
            {
                serialNumber.IsActive = false;
            }
            await _mvcDrawContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> FormSubmissions()
        {
            var draws = await _mvcDrawContext.Draws.ToListAsync();

            List<UserDraw> users = new List<UserDraw>();
            List<SerialNumber> serialNumbers = new List<SerialNumber>();

            foreach (Draw item in draws)
            {
                users.Add(await _mvcDrawContext.Users.Where(u => item.UserDrawId == u.Id).FirstOrDefaultAsync());
                serialNumbers.Add(await _mvcDrawContext.SerialNumbers.Where(s => item.SerialNumberId == s.Id).FirstOrDefaultAsync());
            }
            
            return View(new FormSubmissionsViewModel(users, serialNumbers));
        }
    }
}