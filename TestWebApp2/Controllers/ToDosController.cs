﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TestWebApp2.Model;

namespace TestWebApp2.Controllers
{
    /// <summary>
    ///     Контроллер для работы со списком дел
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ToDosController: ControllerBase
    {
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<ToDo> _todos;
        public ToDosController(IMongoDatabase database)
        {
            _db = database ?? throw new ArgumentNullException(nameof(database));
            _todos = _db.GetCollection<ToDo>("todos");
        }

        /// <summary>
        ///     Получение списка дел
        /// </summary>
        /// <returns>Дела</returns>
        [HttpGet]
        public IEnumerable<ToDoDto> Get()
        {
            return _todos.AsQueryable().ToList().Select(x => new ToDoDto { Id = x.Id, Priority = x.Priority, Text = x.Text });
        }

        /// <summary>
        ///     Получение дела
        /// </summary>
        /// <param name="id">Идентификактор запланированного дела</param>
        /// <returns>Дело</returns>
        /// <response code="404">Элемент не найден</response>
        [HttpGet("{id:Guid}")]
        [Produces(typeof(ToDoDto))]
        [ProducesResponseType(404, Type = typeof(Guid))]
        [ProducesResponseType(200, Type = typeof(ToDoDto))]
        public IActionResult GetById(Guid id)
        {
            var todo = _todos.AsQueryable().FirstOrDefault(x => x.Id == id);
            if (todo == null)
                return NotFound(id);

            return Ok(new ToDoDto { Id = todo.Id, Priority = todo.Priority, Text = todo.Text });
        }

        /// <summary>
        ///     Добавление нового дела
        /// </summary>
        /// <param name="item">Дело</param>
        /// <returns>Сохраненное дело</returns>
        /// <response code="400">Некорректные данные</response>
        /// <response code="200">Дело добавлено</response>
        [ProducesResponseType(200, Type = typeof(ToDoDto))]
        [HttpPost]
        public IActionResult PostAdd([FromBody] ToDoDto item)
        {
            item.Id = Guid.NewGuid();
            _todos.InsertOne(new ToDo { Id = item.Id.Value, Priority = item.Priority, Text = item.Text });

            return Ok(item);
        }

        /// <summary>
        ///     Удаление дела
        /// </summary>
        /// <param name="id">Идентификатор дела</param>
        /// <returns></returns>
        /// <response code="404">Элемент не найден</response>
        /// <response code="204">Элемент успешно удален</response> 
        [ProducesResponseType(404, Type = typeof(Guid))]
        [ProducesResponseType(204)]
        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {

            var deleted = _todos.FindOneAndDelete(x => x.Id == id);
            if (deleted == null)
                return NotFound(id);

            return NoContent();
        }

        /// <summary>
        ///     Редактирование дела
        /// </summary>
        /// <param name="id">Идентификатор дела</param>
        /// <param name="item">Дело</param>
        /// <returns>Измененное дело</returns>
        /// <response code="400">Некорректные данные</response>
        /// <response code="200">Текущее дело изменено</response>
        /// <response code="201">Дело с идентификатором не было найдено, поэтому было создано новое дело</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(ToDoDto))]
        [ProducesResponseType(201, Type = typeof(ToDoDto))]
        public IActionResult PutEdit(string id, [FromBody] ToDoDto item)
        {
            var replacedItem = _todos.FindOneAndReplace(x => x.Id == Guid.Parse(id), new ToDo { Id = Guid.Parse(id), Priority = item.Priority, Text = item.Text });
            if (replacedItem != null)
                return Ok(item);

            _todos.InsertOne(new ToDo { Id = Guid.Parse(id), Priority = item.Priority, Text = item.Text });

            return StatusCode(201, item);
        }
    }
}
