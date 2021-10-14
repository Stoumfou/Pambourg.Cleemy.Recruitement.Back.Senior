using Microsoft.AspNetCore.Mvc;
using Pambourg.Cleemy.Recruitement.Back.Senior.Models.DTO;
using Pambourg.Cleemy.Recruitement.Back.Senior.Services;
using Pambourg.Cleemy.Recruitement.Back.Senior.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Controllers
{
    [ApiController]
    [Route(ApiPrefix + "/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private const string ApiPrefix = "api";

        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        /// <summary>
        /// Lister les dépenses pour un utilisateur donné
        /// </summary>
        /// <param name="userId"></param>
        ///<param name="sortBy">optional</param>
        /// <param name="sortOrder">optional</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ExpenseDTO>> GetAsync(int userId, string sortBy, string sortOrder)
        {
            if (userId == 0)
            {
                return BadRequest(userId);
            }

            if (!string.IsNullOrWhiteSpace(sortBy) && !ExpenseConstant.SortBy.Contains(sortBy))
            {
                return BadRequest(sortBy);
            }
            else if (!string.IsNullOrWhiteSpace(sortOrder) && !ExpenseConstant.SortOrder.Contains(sortOrder.ToLowerInvariant()))
            {
                return BadRequest(sortOrder);
            }

            try
            {
                IEnumerable<ExpenseDTO> expenses = await _expenseService.GetExpenseByUserIdAsync(userId, sortBy, sortOrder);
                if (!expenses.Any())
                {
                    return NotFound(userId);
                }

                return Ok(expenses);
            }
            catch (Exception)
            {
                return BadRequest(userId);
            }
        }

        /// <summary>
        /// Trier les dépenses par montant ou par date => Do you mean by userId too ? if Yes use [GET] api/expense?userId=1&
        /// </summary>
        /// <param name="userId"></param>
        ///<param name="sortBy">optional</param>
        /// <param name="sortOrder">optional</param>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ExpenseDTO>> GetAllAsync(string sortBy, string sortOrder)
        {
            if (!string.IsNullOrWhiteSpace(sortBy) && !ExpenseConstant.SortBy.Contains(sortBy))
            {
                return BadRequest(sortBy);
            }
            else if (!string.IsNullOrWhiteSpace(sortOrder) && !ExpenseConstant.SortOrder.Contains(sortOrder.ToLowerInvariant()))
            {
                return BadRequest(sortOrder);
            }

            try
            {
                IEnumerable<ExpenseDTO> expenses = await _expenseService.GetAllExpenseAsync(sortBy, sortOrder);
                return Ok(expenses);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Création d'une dépense
        /// </summary>
        /// <param name="createExpenseDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync(CreateExpenseDTO createExpenseDTO)
        {
            if (createExpenseDTO == null)
            {
                return BadRequest(createExpenseDTO);
            }

            try
            {
                await _expenseService.CreateAsync(createExpenseDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
