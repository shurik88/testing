using Grpc.Core;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using TestWebApp2.Model;
using System.Linq;
using MongoDB.Driver.Linq;
using TestWebApp2.Domain;
using TestWebApp2.Domain.Actions;

namespace TestWebApp2.gServices
{
    /// <summary>
    ///    gRPC сервис для выполнения операций над заданием.
    /// </summary>
    public class ToDoExecutionService: ToDoExecution.ToDoExecutionBase
    {
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<ToDo> _todos;

        public ToDoExecutionService(IMongoDatabase database)
        {
            _db = database ?? throw new ArgumentNullException(nameof(database));
            _todos = _db.GetCollection<ToDo>("todos");
        }

        private async Task<ToDo> GetToDoByIdAsync(string id)
        {
            if (!Guid.TryParse(id, out Guid entityId))
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"invalid guid for: {id}"));

            return await _todos.AsQueryable().FirstOrDefaultAsync(x => x.Id == entityId) 
                ?? throw new RpcException(new Status(StatusCode.NotFound, $"todo with id:{id} not found"));
        }

        public override async Task<StartToDoReply> Start(StartToDoRequest request, ServerCallContext context)
        {
            var todo = await GetToDoByIdAsync(request.Id);
            var wf = new ToDoExecutionWorkflow();
            wf.Init(todo);
            wf.DoAction(new StartActionOnToDo());
            await _todos.ReplaceOneAsync(x => x.Id == todo.Id, todo);

            return new StartToDoReply();
        }

        public override async Task<PauseToDoReply> Pause(PauseToDoRequest request, ServerCallContext context)
        {
            var todo = await GetToDoByIdAsync(request.Id);
            var wf = new ToDoExecutionWorkflow();
            wf.Init(todo);
            wf.DoAction(new PauseActionOnToDo());
            await _todos.ReplaceOneAsync(x => x.Id == todo.Id, todo);

            return new PauseToDoReply();
        }

        public override async Task<ResumeToDoReply> Resume(ResumeToDoRequest request, ServerCallContext context)
        {
            var todo = await GetToDoByIdAsync(request.Id);
            var wf = new ToDoExecutionWorkflow();
            wf.Init(todo);
            wf.DoAction(new ResumeActionOnToDo());
            await _todos.ReplaceOneAsync(x => x.Id == todo.Id, todo);

            return new ResumeToDoReply();
        }

        public override async Task<CancelToDoReply> Cancel(CancelToDoRequest request, ServerCallContext context)
        {
            var todo = await GetToDoByIdAsync(request.Id);
            var wf = new ToDoExecutionWorkflow();
            wf.Init(todo);
            wf.DoAction(new CancelActionOnToDo(request.Reason));
            await _todos.ReplaceOneAsync(x => x.Id == todo.Id, todo);

            return new CancelToDoReply();
        }

        public override async Task<FinishToDoReply> Finish(FinishToDoRequest request, ServerCallContext context)
        {
            var todo = await GetToDoByIdAsync(request.Id);
            var wf = new ToDoExecutionWorkflow();
            wf.Init(todo);
            wf.DoAction(new FinishActionOnToDo());
            await _todos.ReplaceOneAsync(x => x.Id == todo.Id, todo);

            return new FinishToDoReply { TotalDuration = todo.FactDuration.Value };
        }
    }
}
