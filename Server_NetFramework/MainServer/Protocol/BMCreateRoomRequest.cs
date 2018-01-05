// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: BMCreateRoomRequest.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Message {

  /// <summary>Holder for reflection information generated from BMCreateRoomRequest.proto</summary>
  public static partial class BMCreateRoomRequestReflection {

    #region Descriptor
    /// <summary>File descriptor for BMCreateRoomRequest.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static BMCreateRoomRequestReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChlCTUNyZWF0ZVJvb21SZXF1ZXN0LnByb3RvEgdtZXNzYWdlGgtJbmZvcy5w",
            "cm90byI5ChNCTUNyZWF0ZVJvb21SZXF1ZXN0EiIKBXVzZXJzGAEgAygLMhMu",
            "bWVzc2FnZS5QbGF5ZXJJbmZvYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Message.InfosReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Message.BMCreateRoomRequest), global::Message.BMCreateRoomRequest.Parser, new[]{ "Users" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// 主服 -> 战场 创建房间请求
  /// </summary>
  public sealed partial class BMCreateRoomRequest : pb::IMessage<BMCreateRoomRequest> {
    private static readonly pb::MessageParser<BMCreateRoomRequest> _parser = new pb::MessageParser<BMCreateRoomRequest>(() => new BMCreateRoomRequest());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<BMCreateRoomRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Message.BMCreateRoomRequestReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BMCreateRoomRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BMCreateRoomRequest(BMCreateRoomRequest other) : this() {
      users_ = other.users_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BMCreateRoomRequest Clone() {
      return new BMCreateRoomRequest(this);
    }

    /// <summary>Field number for the "users" field.</summary>
    public const int UsersFieldNumber = 1;
    private static readonly pb::FieldCodec<global::Message.PlayerInfo> _repeated_users_codec
        = pb::FieldCodec.ForMessage(10, global::Message.PlayerInfo.Parser);
    private readonly pbc::RepeatedField<global::Message.PlayerInfo> users_ = new pbc::RepeatedField<global::Message.PlayerInfo>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Message.PlayerInfo> Users {
      get { return users_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as BMCreateRoomRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(BMCreateRoomRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!users_.Equals(other.users_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= users_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      users_.WriteTo(output, _repeated_users_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += users_.CalculateSize(_repeated_users_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(BMCreateRoomRequest other) {
      if (other == null) {
        return;
      }
      users_.Add(other.users_);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            users_.AddEntriesFrom(input, _repeated_users_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code