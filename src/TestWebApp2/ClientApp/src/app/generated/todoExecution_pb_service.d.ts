// package: todos
// file: todoExecution.proto

import * as todoExecution_pb from "./todoExecution_pb";
import {grpc} from "@improbable-eng/grpc-web";

type ToDoExecutionStart = {
  readonly methodName: string;
  readonly service: typeof ToDoExecution;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof todoExecution_pb.StartToDoRequest;
  readonly responseType: typeof todoExecution_pb.StartToDoReply;
};

type ToDoExecutionPause = {
  readonly methodName: string;
  readonly service: typeof ToDoExecution;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof todoExecution_pb.PauseToDoRequest;
  readonly responseType: typeof todoExecution_pb.PauseToDoReply;
};

type ToDoExecutionResume = {
  readonly methodName: string;
  readonly service: typeof ToDoExecution;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof todoExecution_pb.ResumeToDoRequest;
  readonly responseType: typeof todoExecution_pb.ResumeToDoReply;
};

type ToDoExecutionCancel = {
  readonly methodName: string;
  readonly service: typeof ToDoExecution;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof todoExecution_pb.CancelToDoRequest;
  readonly responseType: typeof todoExecution_pb.CancelToDoReply;
};

type ToDoExecutionFinish = {
  readonly methodName: string;
  readonly service: typeof ToDoExecution;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof todoExecution_pb.FinishToDoRequest;
  readonly responseType: typeof todoExecution_pb.FinishToDoReply;
};

export class ToDoExecution {
  static readonly serviceName: string;
  static readonly Start: ToDoExecutionStart;
  static readonly Pause: ToDoExecutionPause;
  static readonly Resume: ToDoExecutionResume;
  static readonly Cancel: ToDoExecutionCancel;
  static readonly Finish: ToDoExecutionFinish;
}

export type ServiceError = { message: string, code: number; metadata: grpc.Metadata }
export type Status = { details: string, code: number; metadata: grpc.Metadata }

interface UnaryResponse {
  cancel(): void;
}
interface ResponseStream<T> {
  cancel(): void;
  on(type: 'data', handler: (message: T) => void): ResponseStream<T>;
  on(type: 'end', handler: (status?: Status) => void): ResponseStream<T>;
  on(type: 'status', handler: (status: Status) => void): ResponseStream<T>;
}
interface RequestStream<T> {
  write(message: T): RequestStream<T>;
  end(): void;
  cancel(): void;
  on(type: 'end', handler: (status?: Status) => void): RequestStream<T>;
  on(type: 'status', handler: (status: Status) => void): RequestStream<T>;
}
interface BidirectionalStream<ReqT, ResT> {
  write(message: ReqT): BidirectionalStream<ReqT, ResT>;
  end(): void;
  cancel(): void;
  on(type: 'data', handler: (message: ResT) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'end', handler: (status?: Status) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'status', handler: (status: Status) => void): BidirectionalStream<ReqT, ResT>;
}

export class ToDoExecutionClient {
  readonly serviceHost: string;

  constructor(serviceHost: string, options?: grpc.RpcOptions);
  start(
    requestMessage: todoExecution_pb.StartToDoRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: todoExecution_pb.StartToDoReply|null) => void
  ): UnaryResponse;
  start(
    requestMessage: todoExecution_pb.StartToDoRequest,
    callback: (error: ServiceError|null, responseMessage: todoExecution_pb.StartToDoReply|null) => void
  ): UnaryResponse;
  pause(
    requestMessage: todoExecution_pb.PauseToDoRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: todoExecution_pb.PauseToDoReply|null) => void
  ): UnaryResponse;
  pause(
    requestMessage: todoExecution_pb.PauseToDoRequest,
    callback: (error: ServiceError|null, responseMessage: todoExecution_pb.PauseToDoReply|null) => void
  ): UnaryResponse;
  resume(
    requestMessage: todoExecution_pb.ResumeToDoRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: todoExecution_pb.ResumeToDoReply|null) => void
  ): UnaryResponse;
  resume(
    requestMessage: todoExecution_pb.ResumeToDoRequest,
    callback: (error: ServiceError|null, responseMessage: todoExecution_pb.ResumeToDoReply|null) => void
  ): UnaryResponse;
  cancel(
    requestMessage: todoExecution_pb.CancelToDoRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: todoExecution_pb.CancelToDoReply|null) => void
  ): UnaryResponse;
  cancel(
    requestMessage: todoExecution_pb.CancelToDoRequest,
    callback: (error: ServiceError|null, responseMessage: todoExecution_pb.CancelToDoReply|null) => void
  ): UnaryResponse;
  finish(
    requestMessage: todoExecution_pb.FinishToDoRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: todoExecution_pb.FinishToDoReply|null) => void
  ): UnaryResponse;
  finish(
    requestMessage: todoExecution_pb.FinishToDoRequest,
    callback: (error: ServiceError|null, responseMessage: todoExecution_pb.FinishToDoReply|null) => void
  ): UnaryResponse;
}

