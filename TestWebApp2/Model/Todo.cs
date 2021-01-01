using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using TestWebApp2.Contracts;

namespace TestWebApp2.Model
{
    public class ToDo
    {
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        public Guid Id { get; set; }

        public string Text { get; set; }

        public int Priority { get; set; }

        [BsonIgnoreIfNull]
        public string[] Tags { get; set; }

        [BsonIgnoreIfNull]
        public DateTime? Deadline { get; set; }

        [BsonIgnoreIfNull]
        public Assigner AssignedTo { get; set; }

        [BsonDefaultValue(ToDoStatus.Created)]
        public ToDoStatus Status { get; set; }

        [BsonIgnoreIfNull]
        public ICollection<ToDoActionLog> Log { get; set; }

        [BsonIgnoreIfNull]
        public string CancelReason { get; set; }

        [BsonIgnoreIfNull]
        public int? FactDuration { get; set; }
    }
}
