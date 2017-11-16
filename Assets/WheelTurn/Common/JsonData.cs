using System;

public class JsonData {
	public const string UNSUPPORTED = "UNSUPPORTED";
}

[Serializable]
public class Json_Text {
	public string text;

	public Json_Text (string text) {
		this.text = text;
	}
}

[Serializable]
public class Json_ADDR_AND_PORT {
	public string addr;
	public int port;

	public Json_ADDR_AND_PORT (string addr, int port) {
		this.addr = addr;
		this.port = port;
	}
}
