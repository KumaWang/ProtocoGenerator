using System;
using System.Collections.Generic;
using System.ComponentModel;
using iuiu.server.IO;
using iuiu.server.Net;
using iuiu.server.Database;
using _SM = System.IO.MemoryStream;
using _BW = iuiu.server.IO.BinWriter;
using _BR = iuiu.server.IO.BinReader;
using iuiu.protocol.MapModule;

namespace iuiu.protocol.MapModule
{
	#if INP_CLIENT
	public partial class ACCEPT_ENTER
	{
		public ACCEPT_ENTER(NetController net)
		{
			net.Register((ushort)115, _Internal_Do);
		}
		private void _Internal_Do(NetController net, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var VBZneau = _r.ReadUInt32();
			var VZvIJVf = _r.ReadUInt16();
			var VRZfMfq = _r.ReadUInt16();
			var VmeUBji = _r.ReadByte();
			On(token, VBZneau, VZvIJVf, VRZfMfq, VmeUBji);
		}
		public delegate void Callback(NetController net, uint startTime, ushort x, ushort y, byte dir);
		public event Callback On;
	}
	#endif
	
	#if INP_SERVER
	public partial class ACCEPT_ENTER<T> where T : new()
	{
		public static void Push(AsyncSocketUserToken<T> token, uint startTime,ushort x,ushort y,byte dir)
		{
			var _w = new BinWriter(token.ReversalBytes);
			_w.Write(0);
			_w.Write((ushort)115);
			_w.Write(startTime);
			_w.Write(x);
			_w.Write(y);
			_w.Write(dir);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			token.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_CLIENT
	public partial class NOTIFY_TIME
	{
		public NOTIFY_TIME(NetController net)
		{
			net.Register((ushort)127, _Internal_Do);
		}
		private void _Internal_Do(NetController net, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var Vq6Vzee = _r.ReadUInt32();
			On(token, Vq6Vzee);
		}
		public delegate void Callback(NetController net, uint time);
		public event Callback On;
	}
	#endif
	
	#if INP_SERVER
	public partial class NOTIFY_TIME<T> where T : new()
	{
		public static void Push(AsyncSocketUserToken<T> token, uint time)
		{
			var _w = new BinWriter(token.ReversalBytes);
			_w.Write(0);
			_w.Write((ushort)127);
			_w.Write(time);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			token.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_CLIENT
	public partial class ACK_REQNAME
	{
		public ACK_REQNAME(NetController net)
		{
			net.Register((ushort)149, _Internal_Do);
		}
		private void _Internal_Do(NetController net, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var VU7ZZVz = _r.ReadUInt32();
			var Va63QBb = _r.ReadString();
			On(token, VU7ZZVz, Va63QBb);
		}
		public delegate void Callback(NetController net, uint aid, string name);
		public event Callback On;
	}
	#endif
	
	#if INP_SERVER
	public partial class ACK_REQNAME<T> where T : new()
	{
		public static void Push(AsyncSocketUserToken<T> token, uint aid,string name)
		{
			var _w = new BinWriter(token.ReversalBytes);
			_w.Write(0);
			_w.Write((ushort)149);
			_w.Write(aid);
			_w.Write(name);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			token.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_CLIENT
	public partial class NOTIFY_MOVE
	{
		public NOTIFY_MOVE(NetController net)
		{
			net.Register((ushort)134, _Internal_Do);
		}
		private void _Internal_Do(NetController net, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var VNjMZBj = _r.ReadUInt32();
			var VEnaAVr = _r.ReadSingle();
			var VUFneQ3 = _r.ReadSingle();
			var VbyMZfe = _r.ReadUInt32();
			On(token, VNjMZBj, VEnaAVr, VUFneQ3, VbyMZfe);
		}
		public delegate void Callback(NetController net, uint gid, float x, float y, uint moveStartTime);
		public event Callback On;
	}
	#endif
	
	#if INP_SERVER
	public partial class NOTIFY_MOVE<T> where T : new()
	{
		public static void Push(AsyncSocketUserToken<T> token, uint gid,float x,float y,uint moveStartTime)
		{
			var _w = new BinWriter(token.ReversalBytes);
			_w.Write(0);
			_w.Write((ushort)134);
			_w.Write(gid);
			_w.Write(x);
			_w.Write(y);
			_w.Write(moveStartTime);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			token.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_CLIENT
	public partial class NOTIFY_PLAYERMOVE
	{
		public NOTIFY_PLAYERMOVE(NetController net)
		{
			net.Register((ushort)135, _Internal_Do);
		}
		private void _Internal_Do(NetController net, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var VrIriuq = _r.ReadUInt32();
			var VV36viy = _r.ReadSingle();
			var VfAjMNb = _r.ReadSingle();
			var Vm6Frea = _r.ReadUInt16();
			var VZzyAzy = _r.ReadUInt16();
			On(token, VrIriuq, VV36viy, VfAjMNb, Vm6Frea, VZzyAzy);
		}
		public delegate void Callback(NetController net, uint moveStartTime, float fromX, float fromY, ushort toX, ushort toY);
		public event Callback On;
	}
	#endif
	
	#if INP_SERVER
	public partial class NOTIFY_PLAYERMOVE<T> where T : new()
	{
		public static void Push(AsyncSocketUserToken<T> token, uint moveStartTime,float fromX,float fromY,ushort toX,ushort toY)
		{
			var _w = new BinWriter(token.ReversalBytes);
			_w.Write(0);
			_w.Write((ushort)135);
			_w.Write(moveStartTime);
			_w.Write(fromX);
			_w.Write(fromY);
			_w.Write(toX);
			_w.Write(toY);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			token.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_CLIENT
	public partial class ACCEPT_QUIT
	{
		public ACCEPT_QUIT(NetController net)
		{
			net.Register((ushort)131, _Internal_Do);
		}
		private void _Internal_Do(NetController net, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			On(token);
		}
		public delegate void Callback(NetController net);
		public event Callback On;
	}
	#endif
	
	#if INP_SERVER
	public partial class ACCEPT_QUIT<T> where T : new()
	{
		public static void Push(AsyncSocketUserToken<T> token)
		{
			var _w = new BinWriter(token.ReversalBytes);
			_w.Write(0);
			_w.Write((ushort)131);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			token.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_CLIENT
	public partial class NOTIFY_CHAT
	{
		public NOTIFY_CHAT(NetController net)
		{
			net.Register((ushort)141, _Internal_Do);
		}
		private void _Internal_Do(NetController net, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var VE7Vjiy = _r.ReadUInt32();
			var VRFJBvm = _r.ReadString();
			On(token, VE7Vjiy, VRFJBvm);
		}
		public delegate void Callback(NetController net, uint gid, string msg);
		public event Callback On;
	}
	#endif
	
	#if INP_SERVER
	public partial class NOTIFY_CHAT<T> where T : new()
	{
		public static void Push(AsyncSocketUserToken<T> token, uint gid,string msg)
		{
			var _w = new BinWriter(token.ReversalBytes);
			_w.Write(0);
			_w.Write((ushort)141);
			_w.Write(gid);
			_w.Write(msg);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			token.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_CLIENT
	public partial class NOTIFY_NEWENTRY7
	{
		public NOTIFY_NEWENTRY7(NetController net)
		{
			net.Register((ushort)153, _Internal_Do);
		}
		private void _Internal_Do(NetController net, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var Vj6FFNb = _r.ReadByte();
			var VEzEJza = _r.ReadUInt32();
			var VVBJNZf = _r.ReadUInt32();
			var VJ7zy6f = _r.ReadUInt16();
			var VbeQfuq = _r.ReadUInt16();
			var Vv6vAJj = _r.ReadUInt16();
			var VI7FZ32 = _r.ReadInt32();
			var VARJfaq = _r.ReadInt32();
			var Vj63yAf = _r.ReadInt64();
			var VmYjIbe = _r.ReadInt16();
			var V73INfy = _r.ReadInt16();
			var VVBNBNb = _r.ReadInt16();
			var VuABNry = _r.ReadInt16();
			var VBB7J3e = _r.ReadInt16();
			var ViqmQJn = _r.ReadInt16();
			var VIVNjma = _r.ReadInt16();
			var VUvieUn = _r.ReadInt16();
			var V6riEVf = _r.ReadInt64();
			var VmiaIzm = _r.ReadByte();
			var VUN3Yn2 = _r.ReadByte();
			On(token, Vj6FFNb, VEzEJza, VVBJNZf, VJ7zy6f, VbeQfuq, Vv6vAJj, VI7FZ32, VARJfaq, Vj63yAf, VmYjIbe, V73INfy, VVBNBNb, VuABNry, VBB7J3e, ViqmQJn, VIVNjma, VUvieUn, V6riEVf, VmiaIzm, VUN3Yn2);
		}
		public delegate void Callback(NetController net, byte objecttype, uint gid, uint speed, ushort bodyState, ushort healthState, ushort effectState, int job, int head, long weapon, short accessory, short accessory2, short accessory3, short haedplette, short bodypalette, short robe, short gemblemVer, short honor, long virtue, byte isPKModeOn, byte sex);
		public event Callback On;
	}
	#endif
	
	#if INP_SERVER
	public partial class NOTIFY_NEWENTRY7<T> where T : new()
	{
		public static void Push(AsyncSocketUserToken<T> token, byte objecttype,uint gid,uint speed,ushort bodyState,ushort healthState,ushort effectState,int job,int head,long weapon,short accessory,short accessory2,short accessory3,short haedplette,short bodypalette,short robe,short gemblemVer,short honor,long virtue,byte isPKModeOn,byte sex)
		{
			var _w = new BinWriter(token.ReversalBytes);
			_w.Write(0);
			_w.Write((ushort)153);
			_w.Write(objecttype);
			_w.Write(gid);
			_w.Write(speed);
			_w.Write(bodyState);
			_w.Write(healthState);
			_w.Write(effectState);
			_w.Write(job);
			_w.Write(head);
			_w.Write(weapon);
			_w.Write(accessory);
			_w.Write(accessory2);
			_w.Write(accessory3);
			_w.Write(haedplette);
			_w.Write(bodypalette);
			_w.Write(robe);
			_w.Write(gemblemVer);
			_w.Write(honor);
			_w.Write(virtue);
			_w.Write(isPKModeOn);
			_w.Write(sex);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			token.WriteBuffer(_w.Flush());
		}
	}
	#endif
	
	#if INP_SERVER
	public partial class ENTER<T> where T : new()
	{
		public ENTER(AsyncSocketServer<T> server)
		{
			server.Register((ushort)114, new Action<AsyncSocketUserToken<T>, byte[], int, int>(_Internal_Do));
		}
		
		private void _Internal_Do(AsyncSocketUserToken<T> token, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var V2IjYje = _r.ReadUInt32();
			var VqaaIze = _r.ReadUInt32();
			var VmYjIZ3 = _r.ReadUInt32();
			var VeuyUZj = _r.ReadUInt32();
			var VjUvaem = _r.ReadByte();
			On(token, V2IjYje, VqaaIze, VmYjIZ3, VeuyUZj, VjUvaem);
		}
		
		public delegate void Callback(AsyncSocketUserToken<T> token, uint aid, uint gid, uint authCode, uint clientTime, byte sex);
		public event Callback On;
	}
	#endif
	
	#if INP_CLIENT
	public partial class ENTER
	{
		public static void Pull(NetController net, uint aid,uint gid,uint authCode,uint clientTime,byte sex)
		{
			var _w = new BinWriter();
			_w.Write(0);
			_w.Write((ushort)114);
			_w.Write(aid);
			_w.Write(gid);
			_w.Write(authCode);
			_w.Write(clientTime);
			_w.Write(sex);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			net.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_SERVER
	public partial class REQUEST_TIME<T> where T : new()
	{
		public REQUEST_TIME(AsyncSocketServer<T> server)
		{
			server.Register((ushort)126, new Action<AsyncSocketUserToken<T>, byte[], int, int>(_Internal_Do));
		}
		
		private void _Internal_Do(AsyncSocketUserToken<T> token, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var VrU3I7n = _r.ReadUInt32();
			On(token, VrU3I7n);
		}
		
		public delegate void Callback(AsyncSocketUserToken<T> token, uint clientTime);
		public event Callback On;
	}
	#endif
	
	#if INP_CLIENT
	public partial class REQUEST_TIME
	{
		public static void Pull(NetController net, uint clientTime)
		{
			var _w = new BinWriter();
			_w.Write(0);
			_w.Write((ushort)126);
			_w.Write(clientTime);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			net.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_SERVER
	public partial class NOTIFY_ACTORINIT<T> where T : new()
	{
		public NOTIFY_ACTORINIT(AsyncSocketServer<T> server)
		{
			server.Register((ushort)125, new Action<AsyncSocketUserToken<T>, byte[], int, int>(_Internal_Do));
		}
		
		private void _Internal_Do(AsyncSocketUserToken<T> token, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			On(token);
		}
		
		public delegate void Callback(AsyncSocketUserToken<T> token);
		public event Callback On;
	}
	#endif
	
	#if INP_CLIENT
	public partial class NOTIFY_ACTORINIT
	{
		public static void Pull(NetController net)
		{
			var _w = new BinWriter();
			_w.Write(0);
			_w.Write((ushort)125);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			net.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_SERVER
	public partial class REQUEST_MOVE<T> where T : new()
	{
		public REQUEST_MOVE(AsyncSocketServer<T> server)
		{
			server.Register((ushort)133, new Action<AsyncSocketUserToken<T>, byte[], int, int>(_Internal_Do));
		}
		
		private void _Internal_Do(AsyncSocketUserToken<T> token, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var VZ32emi = _r.ReadUInt16();
			var V3IRnqi = _r.ReadUInt16();
			On(token, VZ32emi, V3IRnqi);
		}
		
		public delegate void Callback(AsyncSocketUserToken<T> token, ushort x, ushort y);
		public event Callback On;
	}
	#endif
	
	#if INP_CLIENT
	public partial class REQUEST_MOVE
	{
		public static void Pull(NetController net, ushort x,ushort y)
		{
			var _w = new BinWriter();
			_w.Write(0);
			_w.Write((ushort)133);
			_w.Write(x);
			_w.Write(y);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			net.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_SERVER
	public partial class REQNAME<T> where T : new()
	{
		public REQNAME(AsyncSocketServer<T> server)
		{
			server.Register((ushort)148, new Action<AsyncSocketUserToken<T>, byte[], int, int>(_Internal_Do));
		}
		
		private void _Internal_Do(AsyncSocketUserToken<T> token, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var VnuMRvm = _r.ReadUInt32();
			On(token, VnuMRvm);
		}
		
		public delegate void Callback(AsyncSocketUserToken<T> token, uint aid);
		public event Callback On;
	}
	#endif
	
	#if INP_CLIENT
	public partial class REQNAME
	{
		public static void Pull(NetController net, uint aid)
		{
			var _w = new BinWriter();
			_w.Write(0);
			_w.Write((ushort)148);
			_w.Write(aid);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			net.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_SERVER
	public partial class REQUEST_QUIT<T> where T : new()
	{
		public REQUEST_QUIT(AsyncSocketServer<T> server)
		{
			server.Register((ushort)130, new Action<AsyncSocketUserToken<T>, byte[], int, int>(_Internal_Do));
		}
		
		private void _Internal_Do(AsyncSocketUserToken<T> token, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			On(token);
		}
		
		public delegate void Callback(AsyncSocketUserToken<T> token);
		public event Callback On;
	}
	#endif
	
	#if INP_CLIENT
	public partial class REQUEST_QUIT
	{
		public static void Pull(NetController net)
		{
			var _w = new BinWriter();
			_w.Write(0);
			_w.Write((ushort)130);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			net.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_SERVER
	public partial class REQUEST_CHAT<T> where T : new()
	{
		public REQUEST_CHAT(AsyncSocketServer<T> server)
		{
			server.Register((ushort)140, new Action<AsyncSocketUserToken<T>, byte[], int, int>(_Internal_Do));
		}
		
		private void _Internal_Do(AsyncSocketUserToken<T> token, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var VzyaM7z = _r.ReadString();
			On(token, VzyaM7z);
		}
		
		public delegate void Callback(AsyncSocketUserToken<T> token, string msg);
		public event Callback On;
	}
	#endif
	
	#if INP_CLIENT
	public partial class REQUEST_CHAT
	{
		public static void Pull(NetController net, string msg)
		{
			var _w = new BinWriter();
			_w.Write(0);
			_w.Write((ushort)140);
			_w.Write(msg);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			net.WriteBuffer(_w.Flush());
		}
	}
	#endif
}
