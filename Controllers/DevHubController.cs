﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevHub.Models;
using Microsoft.AspNetCore.Razor.Language;

namespace DevHub.Controllers
{
    public class DevHubController : Controller
    {
        //Open Questions View, 3 most recent?
        //has to have a search
        //add question button here goes to create
        public IActionResult Index(string search = "", string column = "")
        {
            if(search == "")
            {
                ViewData["Column"] = "tags";
                ViewData["Message"] = "Recent Questions";
                return View(DataAccessor.GetRecentQuestions());
            }
            else
            {
                ViewData["Column"] = column;
                ViewData["Message"] = "Search Results";
                return View(DataAccessor.SearchQuestions(column, search));
            }
        }

        //Archive, closed questions

        //create a question view
        
        //question detail, edit and submit on this page
        //answer change, add, submit all on same page
        [HttpGet]
        public IActionResult Detail(long id)
        {
            Question q = DataAccessor.GetQuestion(id);
            q.answers = DataAccessor.GetQuestionAnswers(id);
            return View(q);
        }

        [HttpPost]
        public IActionResult Detail(long id, string category, string title, int status, string tags, string detail  )
        {
            Question q = DataAccessor.GetQuestion(id);
            q.category = category;
            q.title = title;
            q.status = status;
            q.tags = tags;
            q.detail = detail;
            DataAccessor.SaveQuestion(q);
            return RedirectToAction("detail", "devhub", new { id = id });
        }

        public IActionResult PostQuestion()
        {
            return View();
        }
    }
}
