// package: todos
// file: todoExecution.proto

var todoExecution_pb = require("./todoExecution_pb");
var grpc = require("@improbable-eng/grpc-web").grpc;

var ToDoExecution = (function () {
  function ToDoExecution() {}
  ToDoExecution.serviceName = "todos.ToDoExecution";
  return ToDoExecution;
}());

ToDoExecution.Start = {
  methodName: "Start",
  service: ToDoExecution,
  requestStream: false,
  responseStream: false,
  requestType: todoExecution_pb.StartToDoRequest,
  responseType: todoExecution_pb.StartToDoReply
};

ToDoExecution.Pause = {
  methodName: "Pause",
  service: ToDoExecution,
  requestStream: false,
  responseStream: false,
  requestType: todoExecution_pb.PauseToDoRequest,
  responseType: todoExecution_pb.PauseToDoReply
};

ToDoExecution.Resume = {
  methodName: "Resume",
  service: ToDoExecution,
  requestStream: false,
  responseStream: false,
  requestType: todoExecution_pb.ResumeToDoRequest,
  responseType: todoExecution_pb.ResumeToDoReply
};

ToDoExecution.Cancel = {
  methodName: "Cancel",
  service: ToDoExecution,
  requestStream: false,
  responseStream: false,
  requestType: todoExecution_pb.CancelToDoRequest,
  responseType: todoExecution_pb.CancelToDoReply
};

ToDoExecution.Finish = {
  methodName: "Finish",
  service: ToDoExecution,
  requestStream: false,
  responseStream: false,
  requestType: todoExecution_pb.FinishToDoRequest,
  responseType: todoExecution_pb.FinishToDoReply
};

exports.ToDoExecution = ToDoExecution;

function ToDoExecutionClient(serviceHost, options) {
  this.serviceHost = serviceHost;
  this.options = options || {};
}

ToDoExecutionClient.prototype.start = function start(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(ToDoExecution.Start, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

ToDoExecutionClient.prototype.pause = function pause(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(ToDoExecution.Pause, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

ToDoExecutionClient.prototype.resume = function resume(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(ToDoExecution.Resume, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

ToDoExecutionClient.prototype.cancel = function cancel(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(ToDoExecution.Cancel, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

ToDoExecutionClient.prototype.finish = function finish(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(ToDoExecution.Finish, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

exports.ToDoExecutionClient = ToDoExecutionClient;

