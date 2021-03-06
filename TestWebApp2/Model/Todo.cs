﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

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
    }
}
