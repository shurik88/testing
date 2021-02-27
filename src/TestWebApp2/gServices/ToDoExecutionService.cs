using Grpc.Core;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using TestWebApp2.Model;
using MongoDB.Driver.Linq;
using TestWebApp2.Domain;
using TestWebApp2.Domain.Actions;
using TestWebApp2.DataAccess;
using TestWebApp2.DataAccess.Mongo.Extensions;

namespace TestWebApp2.gServices
{
    /// <summary>
    ///    gRPC сервис для выполнения операций над заданием.
    /// </summary>
    public class ToDoExecutionService: ToDoExecution.ToDoExecutionBase
    {
        private readonly IRepository<ToDo, Guid> _todos;

        public ToDoExecutionService(IRepository<ToDo, Guid> todos)
        {
            _todos = todos ?? throw new ArgumentNullException(nameof(todos));
        }

        private async Task<ToDo> GetToDoByIdAsync(string id)
        {
            if (!Guid.TryParse(id, out Guid entityId))
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"invalid guid for: {id}"));

            return await _todos.Entities.FirstOrDefaultAsync(x => x.Id == entityId) 
                ?? throw new RpcException(new Status(StatusCode.NotFound, $"todo with id:{id} not found"));
        }

        public override async Task<StartToDoReply> Start(StartToDoRequest request, ServerCallContext context)
        {
            await ExecuteActionAsync(request.Id, new StartActionOnToDo());

            return new StartToDoReply();
        }

        public override async Task<PauseToDoReply> Pause(PauseToDoRequest request, ServerCallContext context)
        {
            await ExecuteActionAsync(request.Id, new PauseActionOnToDo());

            return new PauseToDoReply();
        }

        public override async Task<ResumeToDoReply> Resume(ResumeToDoRequest request, ServerCallContext context)
        {
            await ExecuteActionAsync(request.Id, new ResumeActionOnToDo());

            return new ResumeToDoReply();
        }

        public override async Task<CancelToDoReply> Cancel(CancelToDoRequest request, ServerCallContext context)
        {
            await ExecuteActionAsync(request.Id, new CancelActionOnToDo(request.Reason));

            return new CancelToDoReply();
        }

        public override async Task<FinishToDoReply> Finish(FinishToDoRequest request, ServerCallContext context)
        {
            var todo = await ExecuteActionAsync(request.Id, new FinishActionOnToDo());

            return new FinishToDoReply { TotalDuration = todo.FactDuration.Value };
        }

        private async Task<ToDo> ExecuteActionAsync(string todoId, ActionOnToDo action)
        {
            var todo = await GetToDoByIdAsync(todoId);
            var wf = new ToDoExecutionWorkflow();
            wf.Init(todo);
            wf.DoAction(action);
            await _todos.SaveAsync(todo);
            return todo;
        }
    }
}
