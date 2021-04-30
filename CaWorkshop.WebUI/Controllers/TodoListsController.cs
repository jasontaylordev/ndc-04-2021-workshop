using System.Threading.Tasks;
using CaWorkshop.Application.TodoLists.Commands.CreateTodoList;
using CaWorkshop.Application.TodoLists.Commands.DeleteTodoList;
using CaWorkshop.Application.TodoLists.Commands.UpdateTodoList;
using CaWorkshop.Application.TodoLists.Queries.GetTodoLists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaWorkshop.WebUI.Controllers
{
    public class TodoListsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<TodosVm>> GetTodoLists()
        {
            return await Mediator.Send(new GetTodoListsQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostTodoList(CreateTodoListCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutTodoList(int id, UpdateTodoListCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoList(int id)
        {
            await Mediator.Send(new DeleteTodoListCommand { Id = id });

            return NoContent();
        }
    }
}
