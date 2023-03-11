using System;
using System.Collections.Generic;
using System.ComponentModel;
using iuiu.server.IO;
using iuiu.server.Net;
using iuiu.server.Database;
using _SM = System.IO.MemoryStream;
using _BW = iuiu.server.IO.BinWriter;
using _BR = iuiu.server.IO.BinReader;
using iuiu.protocol.AccountModule;

namespace iuiu.protocol.AccountModule
{
	public struct ServerInfo
	{
		public uint ip;
		
		public ushort port;
		
		public string name;
		
		public ushort usercount;
		
		public ushort state;
		
		public ushort property;
		
		public void Pack(IIOWriter _w)
		{
			_w.Write(this.ip);
			_w.Write(this.port);
			_w.Write(this.name);
			_w.Write(this.usercount);
			_w.Write(this.state);
			_w.Write(this.property);
		}
		
		public static ServerInfo Unpack(IIOReader _r)
		{
			ServerInfo VjeY7ni = new ServerInfo();
			VjeY7ni.ip = _r.ReadUInt32();
			VjeY7ni.port = _r.ReadUInt16();
			VjeY7ni.name = _r.ReadString();
			VjeY7ni.usercount = _r.ReadUInt16();
			VjeY7ni.state = _r.ReadUInt16();
			VjeY7ni.property = _r.ReadUInt16();
			return VjeY7ni;
		}
	}
	
	#if INP_CLIENT
	public partial class PushLogin
	{
		public PushLogin(NetController net)
		{
			net.Register((ushort)105, _Internal_Do);
		}
		private void _Internal_Do(NetController net, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var VuiyMRf = _r.ReadInt32();
			var VYbIJFb = _r.ReadUInt32();
			var VEFvuUf = _r.ReadUInt32();
			var VVNZzyi = _r.ReadUInt32();
			var VQJJrMj = _r.ReadString();
			var VIBbUVn = _r.ReadByte();
			var Vjq6jEb = new ServerInfo[VumqMNz];
			var VumqMNz = _r.ReadInt32();
			for(var _i = 0; _i < VumqMNz; _i++)
			{
				Vjq6jEb[_i] = ServerInfo.Unpack(_r);
			}
			var VfqiiIj = Vjq6jEb;
			On(token, VuiyMRf, VYbIJFb, VEFvuUf, VVNZzyi, VQJJrMj, VIBbUVn, VfqiiIj);
		}
		public delegate void Callback(NetController net, int authCode, uint aid, uint userLevel, uint lastLoginIP, string lastLoginTime, byte sex, ServerInfo[] info);
		public event Callback On;
	}
	#endif
	
	#if INP_SERVER
	public partial class PushLogin<T> where T : new()
	{
		public static void Push(AsyncSocketUserToken<T> token, int authCode,uint aid,uint userLevel,uint lastLoginIP,string lastLoginTime,byte sex,ServerInfo[] info)
		{
			var _w = new BinWriter(token.ReversalBytes);
			_w.Write(0);
			_w.Write((ushort)105);
			_w.Write(authCode);
			_w.Write(aid);
			_w.Write(userLevel);
			_w.Write(lastLoginIP);
			_w.Write(lastLoginTime);
			_w.Write(sex);
			_w.Write(info.Length);
			foreach(var Vb2UjMr in info)
			{
				Vb2UjMr.Pack(_w);
			}
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			token.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_CLIENT
	public partial class PushLoginError
	{
		public PushLoginError(NetController net)
		{
			net.Register((ushort)106, _Internal_Do);
		}
		private void _Internal_Do(NetController net, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var VNBRBNj = _r.ReadByte();
			var VNna2ue = _r.ReadString();
			On(token, VNBRBNj, VNna2ue);
		}
		public delegate void Callback(NetController net, byte errorid, string message);
		public event Callback On;
	}
	#endif
	
	#if INP_SERVER
	public partial class PushLoginError<T> where T : new()
	{
		public static void Push(AsyncSocketUserToken<T> token, byte errorid,string message)
		{
			var _w = new BinWriter(token.ReversalBytes);
			_w.Write(0);
			_w.Write((ushort)106);
			_w.Write(errorid);
			_w.Write(message);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			token.WriteBuffer(_w.Flush());
		}
	}
	#endif
	
	#if INP_SERVER
	public partial class Login<T> where T : new()
	{
		public Login(AsyncSocketServer<T> server)
		{
			server.Register((ushort)102, new Action<AsyncSocketUserToken<T>, byte[], int, int>(_Internal_Do));
		}
		
		private void _Internal_Do(AsyncSocketUserToken<T> token, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var VvuqAr2 = _r.ReadUInt32();
			var VzA36bi = _r.ReadString();
			var VUr2ymy = _r.ReadString();
			var V36NVNf = _r.ReadByte();
			On(token, VvuqAr2, VzA36bi, VUr2ymy, V36NVNf);
		}
		
		public delegate void Callback(AsyncSocketUserToken<T> token, uint version, string account, string password, byte clienttype);
		public event Callback On;
	}
	#endif
	
	#if INP_CLIENT
	public partial class Login
	{
		public static void Pull(NetController net, uint version,string account,string password,byte clienttype)
		{
			var _w = new BinWriter();
			_w.Write(0);
			_w.Write((ushort)102);
			_w.Write(version);
			_w.Write(account);
			_w.Write(password);
			_w.Write(clienttype);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			net.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_SERVER
	public class AccountTable : SQLTable<AccountTable.Value>
	{
		private SQLField[] mFields;
		public override string Name { get { return "AccountTable"; } }
		public override SQLField[] Fields { get { return mFields; } }
		public SQLField id { get { return mFields[0]; } }
		public SQLField username { get { return mFields[1]; } }
		public SQLField password { get { return mFields[2]; } }
		public SQLField sex { get { return mFields[3]; } }
		public SQLField lastlogintime { get { return mFields[4]; } }
		public AccountTable(AsyncDatabaseServer database) : base(database)
		{
			CheckTable();
			mFields = new SQLField[5];
			mFields[0] = new SQLField("id", new SQLFieldType((SQLFieldTypeKind)6, 0));
			mFields[1] = new SQLField("username", new SQLFieldType((SQLFieldTypeKind)13, 24));
			mFields[2] = new SQLField("password", new SQLFieldType((SQLFieldTypeKind)13, 24));
			mFields[3] = new SQLField("sex", new SQLFieldType((SQLFieldTypeKind)3, 0));
			mFields[4] = new SQLField("lastlogintime", new SQLFieldType((SQLFieldTypeKind)7, 0));
		}
		
		public void CheckTable()
		{
			Database.CheckTable(Name, new Dictionary<string, SQLFieldType>() { { "id", new SQLFieldType((SQLFieldTypeKind)6, 0, true, true) },{ "username", new SQLFieldType((SQLFieldTypeKind)13, 24, false, false) },{ "password", new SQLFieldType((SQLFieldTypeKind)13, 24, false, false) },{ "sex", new SQLFieldType((SQLFieldTypeKind)3, 0, false, false) },{ "lastlogintime", new SQLFieldType((SQLFieldTypeKind)7, 0, false, false) } });
		}
		
		public class Value : ISQLValue
		{
			private SQLExpression<UInt32> _m_id;
			private SQLExpression<String> _m_username;
			private SQLExpression<String> _m_password;
			private SQLExpression<Byte> _m_sex;
			private SQLExpression<UInt64> _m_lastlogintime;
			
			public SQLExpression<UInt32> id { get { return _m_id; } }
			public SQLExpression<String> username { get { return _m_username; } }
			public SQLExpression<String> password { get { return _m_password; } }
			public SQLExpression<Byte> sex { get { return _m_sex; } }
			public SQLExpression<UInt64> lastlogintime { get { return _m_lastlogintime; } }
			
			public Value(Dictionary<string, SQLExpression> exps)
			{
				if(exps.ContainsKey("id")) _m_id = exps["id"] as SQLExpression<UInt32>;
				if(exps.ContainsKey("username")) _m_username = exps["username"] as SQLExpression<String>;
				if(exps.ContainsKey("password")) _m_password = exps["password"] as SQLExpression<String>;
				if(exps.ContainsKey("sex")) _m_sex = exps["sex"] as SQLExpression<Byte>;
				if(exps.ContainsKey("lastlogintime")) _m_lastlogintime = exps["lastlogintime"] as SQLExpression<UInt64>;
			}
		}
	}
	#endif
}
