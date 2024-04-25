using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Task.Models;

namespace API_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksModelsController : ControllerBase
    {
        private readonly TaskContext _context;

        public TasksModelsController(TaskContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TasksModel>>> Getsample()
        {
            //this will return you all key and values
            return await _context.sample.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TasksModel>> GetTasksModel(string id)
        {
            //this will return specific key's value
            var tasksModel = await _context.sample.FindAsync(id);

            if (tasksModel == null)
            {
                return NotFound();
            }

            return tasksModel;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasksModel(string id, TasksModel tasksModel)
        {
            //this will update specific record with specific key on db
            if (id != tasksModel.Key)
            {
                return BadRequest();
            }

            _context.Entry(tasksModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasksModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TasksModel>> PostTasksModel(TasksModel tasksModel)
        {
            //this will be used for post a new object to db
            _context.sample.Add(tasksModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TasksModelExists(tasksModel.Key))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTasksModel", new { id = tasksModel.Key }, tasksModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasksModel(string id)
        {
            //this will be used for deleting specific record with specific key
            var tasksModel = await _context.sample.FindAsync(id);
            if (tasksModel == null)
            {
                return NotFound();
            }

            _context.sample.Remove(tasksModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TasksModelExists(string id)
        {
            return _context.sample.Any(e => e.Key == id);
        }
    }
}
