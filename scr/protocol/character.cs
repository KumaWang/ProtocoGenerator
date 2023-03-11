using System;
using System.Collections.Generic;
using System.ComponentModel;
using iuiu.server.IO;
using iuiu.server.Net;
using iuiu.server.Database;
using _SM = System.IO.MemoryStream;
using _BW = iuiu.server.IO.BinWriter;
using _BR = iuiu.server.IO.BinReader;
using iuiu.protocol.CharacterModule;

namespace iuiu.protocol.CharacterModule
{
	public struct Character
	{
		public uint gid;
		
		public int money;
		
		public int hp;
		
		public int maxHp;
		
		public int sp;
		
		public int maxSp;
		
		public short speed;
		
		public short job;
		
		public short head;
		
		public short body;
		
		public short weapon;
		
		public short accessory;
		
		public short shield;
		
		public short accessory2;
		
		public short accessory3;
		
		public short headpalette;
		
		public short bodypalette;
		
		public string name;
		
		public sbyte str;
		
		public sbyte agi;
		
		public sbyte vit;
		
		public sbyte wit;
		
		public sbyte dex;
		
		public sbyte luk;
		
		public sbyte CharNum;
		
		public sbyte haircolor;
		
		public string lastMap;
		
		public int Robe;
		
		public byte sex;
		
		public void Pack(IIOWriter _w)
		{
			_w.Write(this.gid);
			_w.Write(this.money);
			_w.Write(this.hp);
			_w.Write(this.maxHp);
			_w.Write(this.sp);
			_w.Write(this.maxSp);
			_w.Write(this.speed);
			_w.Write(this.job);
			_w.Write(this.head);
			_w.Write(this.body);
			_w.Write(this.weapon);
			_w.Write(this.accessory);
			_w.Write(this.shield);
			_w.Write(this.accessory2);
			_w.Write(this.accessory3);
			_w.Write(this.headpalette);
			_w.Write(this.bodypalette);
			_w.Write(this.name);
			_w.Write(this.str);
			_w.Write(this.agi);
			_w.Write(this.vit);
			_w.Write(this.wit);
			_w.Write(this.dex);
			_w.Write(this.luk);
			_w.Write(this.CharNum);
			_w.Write(this.haircolor);
			_w.Write(this.lastMap);
			_w.Write(this.Robe);
			_w.Write(this.sex);
		}
		
		public static Character Unpack(IIOReader _r)
		{
			Character VraiQru = new Character();
			VraiQru.gid = _r.ReadUInt32();
			VraiQru.money = _r.ReadInt32();
			VraiQru.hp = _r.ReadInt32();
			VraiQru.maxHp = _r.ReadInt32();
			VraiQru.sp = _r.ReadInt32();
			VraiQru.maxSp = _r.ReadInt32();
			VraiQru.speed = _r.ReadInt16();
			VraiQru.job = _r.ReadInt16();
			VraiQru.head = _r.ReadInt16();
			VraiQru.body = _r.ReadInt16();
			VraiQru.weapon = _r.ReadInt16();
			VraiQru.accessory = _r.ReadInt16();
			VraiQru.shield = _r.ReadInt16();
			VraiQru.accessory2 = _r.ReadInt16();
			VraiQru.accessory3 = _r.ReadInt16();
			VraiQru.headpalette = _r.ReadInt16();
			VraiQru.bodypalette = _r.ReadInt16();
			VraiQru.name = _r.ReadString();
			VraiQru.str = _r.ReadSByte();
			VraiQru.agi = _r.ReadSByte();
			VraiQru.vit = _r.ReadSByte();
			VraiQru.wit = _r.ReadSByte();
			VraiQru.dex = _r.ReadSByte();
			VraiQru.luk = _r.ReadSByte();
			VraiQru.CharNum = _r.ReadSByte();
			VraiQru.haircolor = _r.ReadSByte();
			VraiQru.lastMap = _r.ReadString();
			VraiQru.Robe = _r.ReadInt32();
			VraiQru.sex = _r.ReadByte();
			return VraiQru;
		}
	}
	
	#if INP_CLIENT
	public partial class PushCharList
	{
		public PushCharList(NetController net)
		{
			net.Register((ushort)107, _Internal_Do);
		}
		private void _Internal_Do(NetController net, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var VeA7zem = new Character[VFvAFni];
			var VFvAFni = _r.ReadInt32();
			for(var _i = 0; _i < VFvAFni; _i++)
			{
				VeA7zem[_i] = Character.Unpack(_r);
			}
			var Vnqm6zq = VeA7zem;
			On(token, Vnqm6zq);
		}
		public delegate void Callback(NetController net, Character[] chars);
		public event Callback On;
	}
	#endif
	
	#if INP_SERVER
	public partial class PushCharList<T> where T : new()
	{
		public static void Push(AsyncSocketUserToken<T> token, Character[] chars)
		{
			var _w = new BinWriter(token.ReversalBytes);
			_w.Write(0);
			_w.Write((ushort)107);
			_w.Write(chars.Length);
			foreach(var VFNVR3q in chars)
			{
				VFNVR3q.Pack(_w);
			}
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			token.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_CLIENT
	public partial class NOTIFY_ACCESSIBLE_MAPNAME
	{
		public NOTIFY_ACCESSIBLE_MAPNAME(NetController net)
		{
			net.Register((ushort)2112, _Internal_Do);
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
	public partial class NOTIFY_ACCESSIBLE_MAPNAME<T> where T : new()
	{
		public static void Push(AsyncSocketUserToken<T> token)
		{
			var _w = new BinWriter(token.ReversalBytes);
			_w.Write(0);
			_w.Write((ushort)2112);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			token.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_CLIENT
	public partial class Ping
	{
		public Ping(NetController net)
		{
			net.Register((ushort)391, _Internal_Do);
		}
		private void _Internal_Do(NetController net, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var VyE7JRj = _r.ReadUInt32();
			On(token, VyE7JRj);
		}
		public delegate void Callback(NetController net, uint aid);
		public event Callback On;
	}
	#endif
	
	#if INP_SERVER
	public partial class Ping<T> where T : new()
	{
		public static void Push(AsyncSocketUserToken<T> token, uint aid)
		{
			var _w = new BinWriter(token.ReversalBytes);
			_w.Write(0);
			_w.Write((ushort)391);
			_w.Write(aid);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			token.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_CLIENT
	public partial class NOTIFY_ZONESVR
	{
		public NOTIFY_ZONESVR(NetController net)
		{
			net.Register((ushort)113, _Internal_Do);
		}
		private void _Internal_Do(NetController net, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var Vf2MfAb = _r.ReadUInt32();
			var VjyEjYf = _r.ReadString();
			var VuiuI32 = _r.ReadUInt32();
			var VmMJz6j = _r.ReadUInt16();
			On(token, Vf2MfAb, VjyEjYf, VuiuI32, VmMJz6j);
		}
		public delegate void Callback(NetController net, uint gid, string mapName, uint ip, ushort port);
		public event Callback On;
	}
	#endif
	
	#if INP_SERVER
	public partial class NOTIFY_ZONESVR<T> where T : new()
	{
		public static void Push(AsyncSocketUserToken<T> token, uint gid,string mapName,uint ip,ushort port)
		{
			var _w = new BinWriter(token.ReversalBytes);
			_w.Write(0);
			_w.Write((ushort)113);
			_w.Write(gid);
			_w.Write(mapName);
			_w.Write(ip);
			_w.Write(port);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			token.WriteBuffer(_w.Flush());
		}
	}
	#endif
	
	#if INP_SERVER
	public partial class ConnectInfoChanged<T> where T : new()
	{
		public ConnectInfoChanged(AsyncSocketServer<T> server)
		{
			server.Register((ushort)512, new Action<AsyncSocketUserToken<T>, byte[], int, int>(_Internal_Do));
		}
		
		private void _Internal_Do(AsyncSocketUserToken<T> token, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var V7zuMba = _r.ReadString();
			On(token, V7zuMba);
		}
		
		public delegate void Callback(AsyncSocketUserToken<T> token, string username);
		public event Callback On;
	}
	#endif
	
	#if INP_CLIENT
	public partial class ConnectInfoChanged
	{
		public static void Pull(NetController net, string username)
		{
			var _w = new BinWriter();
			_w.Write(0);
			_w.Write((ushort)512);
			_w.Write(username);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			net.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_SERVER
	public partial class ConnectToCharacterServer<T> where T : new()
	{
		public ConnectToCharacterServer(AsyncSocketServer<T> server)
		{
			server.Register((ushort)101, new Action<AsyncSocketUserToken<T>, byte[], int, int>(_Internal_Do));
		}
		
		private void _Internal_Do(AsyncSocketUserToken<T> token, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var VUzqQJf = _r.ReadUInt32();
			var VQZ7nUf = _r.ReadUInt32();
			var VviAzYn = _r.ReadUInt32();
			var VBJnaAf = _r.ReadUInt16();
			var V6Jreaa = _r.ReadByte();
			On(token, VUzqQJf, VQZ7nUf, VviAzYn, VBJnaAf, V6Jreaa);
		}
		
		public delegate void Callback(AsyncSocketUserToken<T> token, uint aid, uint authCode, uint userLevel, ushort clientType, byte sex);
		public event Callback On;
	}
	#endif
	
	#if INP_CLIENT
	public partial class ConnectToCharacterServer
	{
		public static void Pull(NetController net, uint aid,uint authCode,uint userLevel,ushort clientType,byte sex)
		{
			var _w = new BinWriter();
			_w.Write(0);
			_w.Write((ushort)101);
			_w.Write(aid);
			_w.Write(authCode);
			_w.Write(userLevel);
			_w.Write(clientType);
			_w.Write(sex);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			net.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_SERVER
	public partial class SelectCharacter<T> where T : new()
	{
		public SelectCharacter(AsyncSocketServer<T> server)
		{
			server.Register((ushort)102, new Action<AsyncSocketUserToken<T>, byte[], int, int>(_Internal_Do));
		}
		
		private void _Internal_Do(AsyncSocketUserToken<T> token, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var Vz2Q3uy = _r.ReadSByte();
			On(token, Vz2Q3uy);
		}
		
		public delegate void Callback(AsyncSocketUserToken<T> token, sbyte charNum);
		public event Callback On;
	}
	#endif
	
	#if INP_CLIENT
	public partial class SelectCharacter
	{
		public static void Pull(NetController net, sbyte charNum)
		{
			var _w = new BinWriter();
			_w.Write(0);
			_w.Write((ushort)102);
			_w.Write(charNum);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			net.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_SERVER
	public partial class MAKE_CHAR<T> where T : new()
	{
		public MAKE_CHAR(AsyncSocketServer<T> server)
		{
			server.Register((ushort)103, new Action<AsyncSocketUserToken<T>, byte[], int, int>(_Internal_Do));
		}
		
		private void _Internal_Do(AsyncSocketUserToken<T> token, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var VRR7BFj = _r.ReadString();
			var Vn2I3Uz = _r.ReadByte();
			var Vmai6Rb = _r.ReadByte();
			var VNZnQbe = _r.ReadByte();
			var VYJbUzi = _r.ReadByte();
			var VzA777v = _r.ReadByte();
			var VbuYnai = _r.ReadByte();
			var VMN77z2 = _r.ReadByte();
			var V6NFFbe = _r.ReadInt16();
			var VIBz6f2 = _r.ReadInt16();
			On(token, VRR7BFj, Vn2I3Uz, Vmai6Rb, VNZnQbe, VYJbUzi, VzA777v, VbuYnai, VMN77z2, V6NFFbe, VIBz6f2);
		}
		
		public delegate void Callback(AsyncSocketUserToken<T> token, string name, byte str, byte agi, byte vit, byte int2, byte dex, byte luk, byte charNum, short headlPol, short head);
		public event Callback On;
	}
	#endif
	
	#if INP_CLIENT
	public partial class MAKE_CHAR
	{
		public static void Pull(NetController net, string name,byte str,byte agi,byte vit,byte int2,byte dex,byte luk,byte charNum,short headlPol,short head)
		{
			var _w = new BinWriter();
			_w.Write(0);
			_w.Write((ushort)103);
			_w.Write(name);
			_w.Write(str);
			_w.Write(agi);
			_w.Write(vit);
			_w.Write(int2);
			_w.Write(dex);
			_w.Write(luk);
			_w.Write(charNum);
			_w.Write(headlPol);
			_w.Write(head);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			net.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_SERVER
	public partial class MAKE_CHAR2<T> where T : new()
	{
		public MAKE_CHAR2(AsyncSocketServer<T> server)
		{
			server.Register((ushort)2416, new Action<AsyncSocketUserToken<T>, byte[], int, int>(_Internal_Do));
		}
		
		private void _Internal_Do(AsyncSocketUserToken<T> token, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var VuINVFn = _r.ReadString();
			var Vm6ZbUv = _r.ReadByte();
			var VM3YfIn = _r.ReadInt16();
			var VRZ3Afm = _r.ReadInt16();
			On(token, VuINVFn, Vm6ZbUv, VM3YfIn, VRZ3Afm);
		}
		
		public delegate void Callback(AsyncSocketUserToken<T> token, string name, byte charNum, short headPol, short head);
		public event Callback On;
	}
	#endif
	
	#if INP_CLIENT
	public partial class MAKE_CHAR2
	{
		public static void Pull(NetController net, string name,byte charNum,short headPol,short head)
		{
			var _w = new BinWriter();
			_w.Write(0);
			_w.Write((ushort)2416);
			_w.Write(name);
			_w.Write(charNum);
			_w.Write(headPol);
			_w.Write(head);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			net.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_SERVER
	public partial class Ping<T> where T : new()
	{
		public Ping(AsyncSocketServer<T> server)
		{
			server.Register((ushort)391, new Action<AsyncSocketUserToken<T>, byte[], int, int>(_Internal_Do));
		}
		
		private void _Internal_Do(AsyncSocketUserToken<T> token, byte[] data, int offset, int count)
		{
			var _r = new _BR(new AsyncBufferStream(data, offset, count));
			var V7j6Bf2 = _r.ReadUInt32();
			On(token, V7j6Bf2);
		}
		
		public delegate void Callback(AsyncSocketUserToken<T> token, uint aid);
		public event Callback On;
	}
	#endif
	
	#if INP_CLIENT
	public partial class Ping
	{
		public static void Pull(NetController net, uint aid)
		{
			var _w = new BinWriter();
			_w.Write(0);
			_w.Write((ushort)391);
			_w.Write(aid);
			_w.Position = 0;
			_w.Write((uint)_w.Length);
			net.WriteBuffer(_w.Flush());
		}
	}
	#endif
	#if INP_SERVER
	public class CharacterTable : SQLTable<CharacterTable.Value>
	{
		private SQLField[] mFields;
		public override string Name { get { return "CharacterTable"; } }
		public override SQLField[] Fields { get { return mFields; } }
		public SQLField id { get { return mFields[0]; } }
		public SQLField aid { get { return mFields[1]; } }
		public SQLField money { get { return mFields[2]; } }
		public SQLField hp { get { return mFields[3]; } }
		public SQLField sp { get { return mFields[4]; } }
		public SQLField speed { get { return mFields[5]; } }
		public SQLField job { get { return mFields[6]; } }
		public SQLField head { get { return mFields[7]; } }
		public SQLField body { get { return mFields[8]; } }
		public SQLField weapon { get { return mFields[9]; } }
		public SQLField accessory { get { return mFields[10]; } }
		public SQLField shield { get { return mFields[11]; } }
		public SQLField accessory2 { get { return mFields[12]; } }
		public SQLField accessory3 { get { return mFields[13]; } }
		public SQLField headpalette { get { return mFields[14]; } }
		public SQLField bodypalette { get { return mFields[15]; } }
		public SQLField name { get { return mFields[16]; } }
		public SQLField str { get { return mFields[17]; } }
		public SQLField agi { get { return mFields[18]; } }
		public SQLField vit { get { return mFields[19]; } }
		public SQLField wit { get { return mFields[20]; } }
		public SQLField dex { get { return mFields[21]; } }
		public SQLField luk { get { return mFields[22]; } }
		public SQLField CharNum { get { return mFields[23]; } }
		public SQLField haircolor { get { return mFields[24]; } }
		public SQLField lastMap { get { return mFields[25]; } }
		public SQLField lastMapX { get { return mFields[26]; } }
		public SQLField lastMapY { get { return mFields[27]; } }
		public SQLField Robe { get { return mFields[28]; } }
		public SQLField sex { get { return mFields[29]; } }
		public CharacterTable(AsyncDatabaseServer database) : base(database)
		{
			CheckTable();
			mFields = new SQLField[30];
			mFields[0] = new SQLField("id", new SQLFieldType((SQLFieldTypeKind)6, 0));
			mFields[1] = new SQLField("aid", new SQLFieldType((SQLFieldTypeKind)6, 0));
			mFields[2] = new SQLField("money", new SQLFieldType((SQLFieldTypeKind)9, 0));
			mFields[3] = new SQLField("hp", new SQLFieldType((SQLFieldTypeKind)9, 0));
			mFields[4] = new SQLField("sp", new SQLFieldType((SQLFieldTypeKind)9, 0));
			mFields[5] = new SQLField("speed", new SQLFieldType((SQLFieldTypeKind)8, 0));
			mFields[6] = new SQLField("job", new SQLFieldType((SQLFieldTypeKind)8, 0));
			mFields[7] = new SQLField("head", new SQLFieldType((SQLFieldTypeKind)8, 0));
			mFields[8] = new SQLField("body", new SQLFieldType((SQLFieldTypeKind)8, 0));
			mFields[9] = new SQLField("weapon", new SQLFieldType((SQLFieldTypeKind)8, 0));
			mFields[10] = new SQLField("accessory", new SQLFieldType((SQLFieldTypeKind)8, 0));
			mFields[11] = new SQLField("shield", new SQLFieldType((SQLFieldTypeKind)8, 0));
			mFields[12] = new SQLField("accessory2", new SQLFieldType((SQLFieldTypeKind)8, 0));
			mFields[13] = new SQLField("accessory3", new SQLFieldType((SQLFieldTypeKind)8, 0));
			mFields[14] = new SQLField("headpalette", new SQLFieldType((SQLFieldTypeKind)8, 0));
			mFields[15] = new SQLField("bodypalette", new SQLFieldType((SQLFieldTypeKind)8, 0));
			mFields[16] = new SQLField("name", new SQLFieldType((SQLFieldTypeKind)13, 24));
			mFields[17] = new SQLField("str", new SQLFieldType((SQLFieldTypeKind)4, 0));
			mFields[18] = new SQLField("agi", new SQLFieldType((SQLFieldTypeKind)4, 0));
			mFields[19] = new SQLField("vit", new SQLFieldType((SQLFieldTypeKind)4, 0));
			mFields[20] = new SQLField("wit", new SQLFieldType((SQLFieldTypeKind)4, 0));
			mFields[21] = new SQLField("dex", new SQLFieldType((SQLFieldTypeKind)4, 0));
			mFields[22] = new SQLField("luk", new SQLFieldType((SQLFieldTypeKind)4, 0));
			mFields[23] = new SQLField("CharNum", new SQLFieldType((SQLFieldTypeKind)4, 0));
			mFields[24] = new SQLField("haircolor", new SQLFieldType((SQLFieldTypeKind)4, 0));
			mFields[25] = new SQLField("lastMap", new SQLFieldType((SQLFieldTypeKind)13, 255));
			mFields[26] = new SQLField("lastMapX", new SQLFieldType((SQLFieldTypeKind)5, 0));
			mFields[27] = new SQLField("lastMapY", new SQLFieldType((SQLFieldTypeKind)5, 0));
			mFields[28] = new SQLField("Robe", new SQLFieldType((SQLFieldTypeKind)9, 0));
			mFields[29] = new SQLField("sex", new SQLFieldType((SQLFieldTypeKind)3, 0));
		}
		
		public void CheckTable()
		{
			Database.CheckTable(Name, new Dictionary<string, SQLFieldType>() { { "id", new SQLFieldType((SQLFieldTypeKind)6, 0, true, true) },{ "aid", new SQLFieldType((SQLFieldTypeKind)6, 0, false, false) },{ "money", new SQLFieldType((SQLFieldTypeKind)9, 0, false, false) },{ "hp", new SQLFieldType((SQLFieldTypeKind)9, 0, false, false) },{ "sp", new SQLFieldType((SQLFieldTypeKind)9, 0, false, false) },{ "speed", new SQLFieldType((SQLFieldTypeKind)8, 0, false, false) },{ "job", new SQLFieldType((SQLFieldTypeKind)8, 0, false, false) },{ "head", new SQLFieldType((SQLFieldTypeKind)8, 0, false, false) },{ "body", new SQLFieldType((SQLFieldTypeKind)8, 0, false, false) },{ "weapon", new SQLFieldType((SQLFieldTypeKind)8, 0, false, false) },{ "accessory", new SQLFieldType((SQLFieldTypeKind)8, 0, false, false) },{ "shield", new SQLFieldType((SQLFieldTypeKind)8, 0, false, false) },{ "accessory2", new SQLFieldType((SQLFieldTypeKind)8, 0, false, false) },{ "accessory3", new SQLFieldType((SQLFieldTypeKind)8, 0, false, false) },{ "headpalette", new SQLFieldType((SQLFieldTypeKind)8, 0, false, false) },{ "bodypalette", new SQLFieldType((SQLFieldTypeKind)8, 0, false, false) },{ "name", new SQLFieldType((SQLFieldTypeKind)13, 24, false, false) },{ "str", new SQLFieldType((SQLFieldTypeKind)4, 0, false, false) },{ "agi", new SQLFieldType((SQLFieldTypeKind)4, 0, false, false) },{ "vit", new SQLFieldType((SQLFieldTypeKind)4, 0, false, false) },{ "wit", new SQLFieldType((SQLFieldTypeKind)4, 0, false, false) },{ "dex", new SQLFieldType((SQLFieldTypeKind)4, 0, false, false) },{ "luk", new SQLFieldType((SQLFieldTypeKind)4, 0, false, false) },{ "CharNum", new SQLFieldType((SQLFieldTypeKind)4, 0, false, false) },{ "haircolor", new SQLFieldType((SQLFieldTypeKind)4, 0, false, false) },{ "lastMap", new SQLFieldType((SQLFieldTypeKind)13, 255, false, false) },{ "lastMapX", new SQLFieldType((SQLFieldTypeKind)5, 0, false, false) },{ "lastMapY", new SQLFieldType((SQLFieldTypeKind)5, 0, false, false) },{ "Robe", new SQLFieldType((SQLFieldTypeKind)9, 0, false, false) },{ "sex", new SQLFieldType((SQLFieldTypeKind)3, 0, false, false) } });
		}
		
		public class Value : ISQLValue
		{
			private SQLExpression<UInt32> _m_id;
			private SQLExpression<UInt32> _m_aid;
			private SQLExpression<Int32> _m_money;
			private SQLExpression<Int32> _m_hp;
			private SQLExpression<Int32> _m_sp;
			private SQLExpression<Int16> _m_speed;
			private SQLExpression<Int16> _m_job;
			private SQLExpression<Int16> _m_head;
			private SQLExpression<Int16> _m_body;
			private SQLExpression<Int16> _m_weapon;
			private SQLExpression<Int16> _m_accessory;
			private SQLExpression<Int16> _m_shield;
			private SQLExpression<Int16> _m_accessory2;
			private SQLExpression<Int16> _m_accessory3;
			private SQLExpression<Int16> _m_headpalette;
			private SQLExpression<Int16> _m_bodypalette;
			private SQLExpression<String> _m_name;
			private SQLExpression<SByte> _m_str;
			private SQLExpression<SByte> _m_agi;
			private SQLExpression<SByte> _m_vit;
			private SQLExpression<SByte> _m_wit;
			private SQLExpression<SByte> _m_dex;
			private SQLExpression<SByte> _m_luk;
			private SQLExpression<SByte> _m_CharNum;
			private SQLExpression<SByte> _m_haircolor;
			private SQLExpression<String> _m_lastMap;
			private SQLExpression<UInt16> _m_lastMapX;
			private SQLExpression<UInt16> _m_lastMapY;
			private SQLExpression<Int32> _m_Robe;
			private SQLExpression<Byte> _m_sex;
			
			public SQLExpression<UInt32> id { get { return _m_id; } }
			public SQLExpression<UInt32> aid { get { return _m_aid; } }
			public SQLExpression<Int32> money { get { return _m_money; } }
			public SQLExpression<Int32> hp { get { return _m_hp; } }
			public SQLExpression<Int32> sp { get { return _m_sp; } }
			public SQLExpression<Int16> speed { get { return _m_speed; } }
			public SQLExpression<Int16> job { get { return _m_job; } }
			public SQLExpression<Int16> head { get { return _m_head; } }
			public SQLExpression<Int16> body { get { return _m_body; } }
			public SQLExpression<Int16> weapon { get { return _m_weapon; } }
			public SQLExpression<Int16> accessory { get { return _m_accessory; } }
			public SQLExpression<Int16> shield { get { return _m_shield; } }
			public SQLExpression<Int16> accessory2 { get { return _m_accessory2; } }
			public SQLExpression<Int16> accessory3 { get { return _m_accessory3; } }
			public SQLExpression<Int16> headpalette { get { return _m_headpalette; } }
			public SQLExpression<Int16> bodypalette { get { return _m_bodypalette; } }
			public SQLExpression<String> name { get { return _m_name; } }
			public SQLExpression<SByte> str { get { return _m_str; } }
			public SQLExpression<SByte> agi { get { return _m_agi; } }
			public SQLExpression<SByte> vit { get { return _m_vit; } }
			public SQLExpression<SByte> wit { get { return _m_wit; } }
			public SQLExpression<SByte> dex { get { return _m_dex; } }
			public SQLExpression<SByte> luk { get { return _m_luk; } }
			public SQLExpression<SByte> CharNum { get { return _m_CharNum; } }
			public SQLExpression<SByte> haircolor { get { return _m_haircolor; } }
			public SQLExpression<String> lastMap { get { return _m_lastMap; } }
			public SQLExpression<UInt16> lastMapX { get { return _m_lastMapX; } }
			public SQLExpression<UInt16> lastMapY { get { return _m_lastMapY; } }
			public SQLExpression<Int32> Robe { get { return _m_Robe; } }
			public SQLExpression<Byte> sex { get { return _m_sex; } }
			
			public Value(Dictionary<string, SQLExpression> exps)
			{
				if(exps.ContainsKey("id")) _m_id = exps["id"] as SQLExpression<UInt32>;
				if(exps.ContainsKey("aid")) _m_aid = exps["aid"] as SQLExpression<UInt32>;
				if(exps.ContainsKey("money")) _m_money = exps["money"] as SQLExpression<Int32>;
				if(exps.ContainsKey("hp")) _m_hp = exps["hp"] as SQLExpression<Int32>;
				if(exps.ContainsKey("sp")) _m_sp = exps["sp"] as SQLExpression<Int32>;
				if(exps.ContainsKey("speed")) _m_speed = exps["speed"] as SQLExpression<Int16>;
				if(exps.ContainsKey("job")) _m_job = exps["job"] as SQLExpression<Int16>;
				if(exps.ContainsKey("head")) _m_head = exps["head"] as SQLExpression<Int16>;
				if(exps.ContainsKey("body")) _m_body = exps["body"] as SQLExpression<Int16>;
				if(exps.ContainsKey("weapon")) _m_weapon = exps["weapon"] as SQLExpression<Int16>;
				if(exps.ContainsKey("accessory")) _m_accessory = exps["accessory"] as SQLExpression<Int16>;
				if(exps.ContainsKey("shield")) _m_shield = exps["shield"] as SQLExpression<Int16>;
				if(exps.ContainsKey("accessory2")) _m_accessory2 = exps["accessory2"] as SQLExpression<Int16>;
				if(exps.ContainsKey("accessory3")) _m_accessory3 = exps["accessory3"] as SQLExpression<Int16>;
				if(exps.ContainsKey("headpalette")) _m_headpalette = exps["headpalette"] as SQLExpression<Int16>;
				if(exps.ContainsKey("bodypalette")) _m_bodypalette = exps["bodypalette"] as SQLExpression<Int16>;
				if(exps.ContainsKey("name")) _m_name = exps["name"] as SQLExpression<String>;
				if(exps.ContainsKey("str")) _m_str = exps["str"] as SQLExpression<SByte>;
				if(exps.ContainsKey("agi")) _m_agi = exps["agi"] as SQLExpression<SByte>;
				if(exps.ContainsKey("vit")) _m_vit = exps["vit"] as SQLExpression<SByte>;
				if(exps.ContainsKey("wit")) _m_wit = exps["wit"] as SQLExpression<SByte>;
				if(exps.ContainsKey("dex")) _m_dex = exps["dex"] as SQLExpression<SByte>;
				if(exps.ContainsKey("luk")) _m_luk = exps["luk"] as SQLExpression<SByte>;
				if(exps.ContainsKey("CharNum")) _m_CharNum = exps["CharNum"] as SQLExpression<SByte>;
				if(exps.ContainsKey("haircolor")) _m_haircolor = exps["haircolor"] as SQLExpression<SByte>;
				if(exps.ContainsKey("lastMap")) _m_lastMap = exps["lastMap"] as SQLExpression<String>;
				if(exps.ContainsKey("lastMapX")) _m_lastMapX = exps["lastMapX"] as SQLExpression<UInt16>;
				if(exps.ContainsKey("lastMapY")) _m_lastMapY = exps["lastMapY"] as SQLExpression<UInt16>;
				if(exps.ContainsKey("Robe")) _m_Robe = exps["Robe"] as SQLExpression<Int32>;
				if(exps.ContainsKey("sex")) _m_sex = exps["sex"] as SQLExpression<Byte>;
			}
		}
	}
	#endif
}
