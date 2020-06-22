using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TestWebApp2.Model;

namespace TestWebApp2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDosController: ControllerBase
    {
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<TestWebApp2.Model.ToDo> _todos;
        public ToDosController(IMongoDatabase database)
        {
            _db = database ?? throw new ArgumentNullException(nameof(database));
            _todos = _db.GetCollection<ToDo>("todos");
        }

        [HttpGet]
        public IEnumerable<ToDoDto> Get()
        {
            return _todos.AsQueryable<ToDo>().ToList().Select(x => new ToDoDto { Id = x.Id, Priority = x.Priority, Text = x.Text });
        }

        /// <summary>
        /// Получение дела
        /// </summary>
        /// <param name="id">Идентификактор запланированного дела</param>
        /// <returns></returns>
        [HttpGet("{id:Guid}")]
        [Produces(typeof(ToDoDto))]
        public IActionResult GetById(Guid id)
        {
            var todo = _todos.AsQueryable<ToDo>().FirstOrDefault(x => x.Id == id);
            if (todo == null)
                return NotFound(id);
            return Ok(new ToDoDto { Id = todo.Id, Priority = todo.Priority, Text = todo.Text });
        }

        [HttpPost]
        public IActionResult PostAdd([FromBody] ToDoDto item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            item.Id = Guid.NewGuid();
            _todos.InsertOne(new ToDo { Id = item.Id.Value, Priority = item.Priority, Text = item.Text });

            return Ok(item);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {

            var deleted = _todos.FindOneAndDelete(x => x.Id == id);
            if (deleted == null)
                return NotFound(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult PutEdit(string id, [FromBody] ToDoDto item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var replacedItem = _todos.FindOneAndReplace(x => x.Id == Guid.Parse(id), new ToDo { Id = Guid.Parse(id), Priority = item.Priority, Text = item.Text });
            if (replacedItem == null)
                return NotFound(id);

            return Ok(item);
        }
    }
}
