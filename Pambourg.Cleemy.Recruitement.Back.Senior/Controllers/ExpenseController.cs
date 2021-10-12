using Microsoft.AspNetCore.Mvc;
using Pambourg.Cleemy.Recruitement.Back.Senior.Models.DTO;
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

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ExpenseDTO>> GetAsync(int userId)
        {
            if (userId == 0)
            {
                return BadRequest(userId);
            }

            try
            {
                IEnumerable<ExpenseDTO> expenses = await _expenseService.GetExpenseByUserIdAsync(userId);
                if (!expenses.Any())
                {
                    return NotFound(userId);
                }

                return Ok(expenses);
            }
            catch (ArgumentNullException)
            {
                return BadRequest(userId);
            }
        }

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
