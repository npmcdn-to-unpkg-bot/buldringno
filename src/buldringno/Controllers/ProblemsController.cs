﻿using AutoMapper;
using BuldringNo.Entities;
using BuldringNo.Infrastructure.Core;
using BuldringNo.Infrastructure.Repositories;
using BuldringNo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BuldringNo.Controllers
{
    [Route("api/[controller]")]
    public class ProblemsController : Controller
    {
        private readonly IProblemRepository _problemRepository;
        private readonly ILoggingRepository _loggingRepository;

        public ProblemsController(IProblemRepository problemRepository, ILoggingRepository loggingRepository)
        {
            _problemRepository = problemRepository;
            _loggingRepository = loggingRepository;
        }

        [HttpGet("{page:int=0}/{pageSize=12}")]
        public PaginationSet<ProblemViewModel> Get(int? page, int? pageSize)
        {
            PaginationSet<ProblemViewModel> pagedSet = null;

            try
            {
                int currentPage = page.Value;
                int currentPageSize = pageSize.Value;

                List<Problem> _problems = null;
                int _totalProblems = new int();

                _problems = _problemRepository
                    .AllIncluding(p => p.Boulder)
                    .OrderBy(p => p.Id)
                    .Skip(currentPage * currentPageSize)
                    .Take(currentPageSize)
                    .ToList();

                _totalProblems = _problemRepository.GetAll().Count();

                IEnumerable<ProblemViewModel> _problemsVM = Mapper.Map<IEnumerable<Problem>, IEnumerable<ProblemViewModel>>(_problems);

                pagedSet = new PaginationSet<ProblemViewModel>()
                {
                    Page = currentPage,
                    TotalCount = _totalProblems,
                    TotalPages = (int)Math.Ceiling((decimal)_totalProblems / currentPageSize),
                    Items = _problemsVM
                };
            }
            catch (Exception ex)
            {
                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();
            }

            return pagedSet;
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            IActionResult _result = new ObjectResult(false);
            GenericResult _removeResult = null;

            try
            {
                Problem _problemToRemove = this._problemRepository.GetSingle(id);
                _problemRepository.Delete(_problemToRemove);
                _problemRepository.Commit();

                _removeResult = new GenericResult()
                {
                    Succeeded = true,
                    Message = "Problem removed."
                };
            }
            catch (Exception ex)
            {
                _removeResult = new GenericResult()
                {
                    Succeeded = false,
                    Message = ex.Message
                };

                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();
            }

            _result = new ObjectResult(_removeResult);
            return _result;
        }
    }
}
