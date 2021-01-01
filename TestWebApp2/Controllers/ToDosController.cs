﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TestWebApp2.Contracts;
using TestWebApp2.Exceptions;
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
            return _todos
                .AsQueryable()
                .ToList()
                .Select(MapReverse);
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

            return Ok(MapReverse(todo));
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
        public IActionResult PostAdd([FromBody] EditToDoDto item)
        {
            var todo = Map(item);
            todo.Id = Guid.NewGuid();
            todo.Status = ToDoStatus.Created;

            _todos.InsertOne(todo);

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
        private static ToDoDto MapReverse(ToDo item) =>
            new ToDoDto
            {
                Id = item.Id,
                Priority = item.Priority,
                Text = item.Text,
                AssignedTo = item.AssignedTo != null ? new AssignerDto { Email = item.AssignedTo.Email, Name = item.AssignedTo.Name } : null,
                Deadline = item.Deadline,
                Tags = item.Tags,
                Status = item.Status
            };

        private static ToDo Map(EditToDoDto item) =>
            new ToDo
            {
                Priority = item.Priority,
                Text = item.Text,
                AssignedTo = item.AssignedTo != null ? new Assigner { Email = item.AssignedTo.Email, Name = item.AssignedTo.Name } : null,
                Deadline = item.Deadline,
                Tags = item.Tags,
                Status = ToDoStatus.Created
            };

        /// <summary>
        ///     Редактирование дела
        /// </summary>
        /// <param name="id">Идентификатор дела</param>
        /// <param name="item">Дело</param>
        /// <returns>Измененное дело</returns>
        /// <response code="400">Некорректные данные</response>
        /// <response code="409">Конфликт сохранения</response>
        /// <response code="200">Текущее дело изменено</response>
        /// <response code="201">Дело с идентификатором не было найдено, поэтому было создано новое дело</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(ToDoDto))]
        [ProducesResponseType(201, Type = typeof(ToDoDto))]
        public IActionResult PutEdit(Guid id, [FromBody] EditToDoDto item)
        {
            var currentItem = _todos.AsQueryable().FirstOrDefault(x => x.Id == id);
            if (currentItem != null && currentItem.Status != ToDoStatus.Created)
                throw new ValidationErrorException($"Impossible to edit todo item. Current status is :{currentItem.Status}.");

            var itemToReplaced = Map(item);
            itemToReplaced.Id = id;
            var replacedItem = _todos.FindOneAndReplace(x => x.Id == id, itemToReplaced);
            if (replacedItem != null)
                return Ok(item);

            _todos.InsertOne(itemToReplaced);

            return StatusCode(201, item);
        }

        /// <summary>
        ///     Получение списка действий(событий) дел.
        /// </summary>
        /// <param name="id">Идентификатор дела</param>
        /// <returns>Список действий</returns>
        [HttpGet("{id}/actions")]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ToDoActionDto>))]
        public IActionResult GetActions(Guid id)
        {
            var currentItem = _todos.AsQueryable().FirstOrDefault(x => x.Id == id);
            if (currentItem == null)
                return NotFound(id);

            return Ok(currentItem.Log != null ? currentItem.Log.Select(x => new ToDoActionDto { Action = x.Action, Date = x.Date }) : new List<ToDoActionDto>());
        }
    }
}
