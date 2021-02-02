// package: todos
// file: todoExecution.proto

import * as jspb from "google-protobuf";

export class StartToDoRequest extends jspb.Message {
  getId(): string;
  setId(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): StartToDoRequest.AsObject;
  static toObject(includeInstance: boolean, msg: StartToDoRequest): StartToDoRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: StartToDoRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): StartToDoRequest;
  static deserializeBinaryFromReader(message: StartToDoRequest, reader: jspb.BinaryReader): StartToDoRequest;
}

export namespace StartToDoRequest {
  export type AsObject = {
    id: string,
  }
}

export class StartToDoReply extends jspb.Message {
  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): StartToDoReply.AsObject;
  static toObject(includeInstance: boolean, msg: StartToDoReply): StartToDoReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: StartToDoReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): StartToDoReply;
  static deserializeBinaryFromReader(message: StartToDoReply, reader: jspb.BinaryReader): StartToDoReply;
}

export namespace StartToDoReply {
  export type AsObject = {
  }
}

export class PauseToDoRequest extends jspb.Message {
  getId(): string;
  setId(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): PauseToDoRequest.AsObject;
  static toObject(includeInstance: boolean, msg: PauseToDoRequest): PauseToDoRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: PauseToDoRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): PauseToDoRequest;
  static deserializeBinaryFromReader(message: PauseToDoRequest, reader: jspb.BinaryReader): PauseToDoRequest;
}

export namespace PauseToDoRequest {
  export type AsObject = {
    id: string,
  }
}

export class PauseToDoReply extends jspb.Message {
  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): PauseToDoReply.AsObject;
  static toObject(includeInstance: boolean, msg: PauseToDoReply): PauseToDoReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: PauseToDoReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): PauseToDoReply;
  static deserializeBinaryFromReader(message: PauseToDoReply, reader: jspb.BinaryReader): PauseToDoReply;
}

export namespace PauseToDoReply {
  export type AsObject = {
  }
}

export class ResumeToDoRequest extends jspb.Message {
  getId(): string;
  setId(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): ResumeToDoRequest.AsObject;
  static toObject(includeInstance: boolean, msg: ResumeToDoRequest): ResumeToDoRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: ResumeToDoRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): ResumeToDoRequest;
  static deserializeBinaryFromReader(message: ResumeToDoRequest, reader: jspb.BinaryReader): ResumeToDoRequest;
}

export namespace ResumeToDoRequest {
  export type AsObject = {
    id: string,
  }
}

export class ResumeToDoReply extends jspb.Message {
  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): ResumeToDoReply.AsObject;
  static toObject(includeInstance: boolean, msg: ResumeToDoReply): ResumeToDoReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: ResumeToDoReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): ResumeToDoReply;
  static deserializeBinaryFromReader(message: ResumeToDoReply, reader: jspb.BinaryReader): ResumeToDoReply;
}

export namespace ResumeToDoReply {
  export type AsObject = {
  }
}

export class CancelToDoRequest extends jspb.Message {
  getId(): string;
  setId(value: string): void;

  getReason(): string;
  setReason(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): CancelToDoRequest.AsObject;
  static toObject(includeInstance: boolean, msg: CancelToDoRequest): CancelToDoRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: CancelToDoRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): CancelToDoRequest;
  static deserializeBinaryFromReader(message: CancelToDoRequest, reader: jspb.BinaryReader): CancelToDoRequest;
}

export namespace CancelToDoRequest {
  export type AsObject = {
    id: string,
    reason: string,
  }
}

export class CancelToDoReply extends jspb.Message {
  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): CancelToDoReply.AsObject;
  static toObject(includeInstance: boolean, msg: CancelToDoReply): CancelToDoReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: CancelToDoReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): CancelToDoReply;
  static deserializeBinaryFromReader(message: CancelToDoReply, reader: jspb.BinaryReader): CancelToDoReply;
}

export namespace CancelToDoReply {
  export type AsObject = {
  }
}

export class FinishToDoRequest extends jspb.Message {
  getId(): string;
  setId(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): FinishToDoRequest.AsObject;
  static toObject(includeInstance: boolean, msg: FinishToDoRequest): FinishToDoRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: FinishToDoRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): FinishToDoRequest;
  static deserializeBinaryFromReader(message: FinishToDoRequest, reader: jspb.BinaryReader): FinishToDoRequest;
}

export namespace FinishToDoRequest {
  export type AsObject = {
    id: string,
  }
}

export class FinishToDoReply extends jspb.Message {
  getTotalduration(): number;
  setTotalduration(value: number): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): FinishToDoReply.AsObject;
  static toObject(includeInstance: boolean, msg: FinishToDoReply): FinishToDoReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: FinishToDoReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): FinishToDoReply;
  static deserializeBinaryFromReader(message: FinishToDoReply, reader: jspb.BinaryReader): FinishToDoReply;
}

export namespace FinishToDoReply {
  export type AsObject = {
    totalduration: number,
  }
}

