# ProtocoGenerator
协议生成

# 关于生成

- 新建inp后缀文件并根据Demo实例编写协议
- 将inp文件属性生成操作改为INP，自定义工具改为MSBuild:Compile
- 生成项目后inp文件将自动生成cs代码到inp文件折叠内

```js
module AccountModule
{
	table AccountTable
	{
		uint *id++,
		varchar(24) username,
		varchar(24) password,
		byte sex,
		ulong lastlogintime
	}

	packet ServerInfo 
	{
		uint ip,
		ushort port,
		string name,
		ushort usercount,
		ushort state,
		ushort property
	}

	pull void Login(uint version, string account, string password, byte clienttype) -> 102;
	push void PushLogin(int authCode, uint aid, uint userLevel, uint lastLoginIP, string lastLoginTime, byte sex, ServerInfo[] info) -> 105;
	push void PushLoginError(byte errorid, string message) -> 106;
}
```
将转换为
```csharp
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
			ServerInfo V3yErie = new ServerInfo();
			V3yErie.ip = _r.ReadUInt32();
			V3yErie.port = _r.ReadUInt16();
			V3yErie.name = _r.ReadString();
			V3yErie.usercount = _r.ReadUInt16();
			V3yErie.state = _r.ReadUInt16();
			V3yErie.property = _r.ReadUInt16();
			return V3yErie;
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
			var VuauuE3 = _r.ReadInt32();
			var VjyUZJz = _r.ReadUInt32();
			var VjErqy2 = _r.ReadUInt32();
			var VABbiAj = _r.ReadUInt32();
			var VUzaQrq = _r.ReadString();
			var ViEJJni = _r.ReadByte();
			var Vmmmqqu = new ServerInfo[VA77zmu];
			var VA77zmu = _r.ReadInt32();
			for(var _i = 0; _i < VA77zmu; _i++)
			{
				Vmmmqqu[_i] = ServerInfo.Unpack(_r);
			}
			var VrUVB7b = Vmmmqqu;
			On(token, VuauuE3, VjyUZJz, VjErqy2, VABbiAj, VUzaQrq, ViEJJni, VrUVB7b);
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
			foreach(var VeqQfUj in info)
			{
				VeqQfUj.Pack(_w);
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

	....
```

# 关于使用
- 在服务端添加 INP_SERVER 编译属性
- 在客户端添加 INP_CLIENT 编译属性
- 根据生成的代码进行调用
