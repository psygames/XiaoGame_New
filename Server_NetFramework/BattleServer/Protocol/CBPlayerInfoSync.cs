// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: CBPlayerInfoSync.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Message {

  /// <summary>Holder for reflection information generated from CBPlayerInfoSync.proto</summary>
  public static partial class CBPlayerInfoSyncReflection {

    #region Descriptor
    /// <summary>File descriptor for CBPlayerInfoSync.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CBPlayerInfoSyncReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChZDQlBsYXllckluZm9TeW5jLnByb3RvEgdtZXNzYWdlIiIKEENCUGxheWVy",
            "SW5mb1N5bmMSDgoGZnJvbUlEGAEgASgFYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Message.CBPlayerInfoSync), global::Message.CBPlayerInfoSync.Parser, new[]{ "FromID" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// 客户端 -> 主服 登录请求
  /// </summary>
  public sealed partial class CBPlayerInfoSync : pb::IMessage<CBPlayerInfoSync> {
    private static readonly pb::MessageParser<CBPlayerInfoSync> _parser = new pb::MessageParser<CBPlayerInfoSync>(() => new CBPlayerInfoSync());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<CBPlayerInfoSync> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Message.CBPlayerInfoSyncReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CBPlayerInfoSync() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CBPlayerInfoSync(CBPlayerInfoSync other) : this() {
      fromID_ = other.fromID_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CBPlayerInfoSync Clone() {
      return new CBPlayerInfoSync(this);
    }

    /// <summary>Field number for the "fromID" field.</summary>
    public const int FromIDFieldNumber = 1;
    private int fromID_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int FromID {
      get { return fromID_; }
      set {
        fromID_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as CBPlayerInfoSync);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(CBPlayerInfoSync other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (FromID != other.FromID) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (FromID != 0) hash ^= FromID.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (FromID != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(FromID);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (FromID != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(FromID);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(CBPlayerInfoSync other) {
      if (other == null) {
        return;
      }
      if (other.FromID != 0) {
        FromID = other.FromID;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            FromID = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
