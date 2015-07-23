1. Parsing HTML

    var doc = new Document("...<a id='theId' href='#'>content</a>...");
	var a = doc.GetElementById("theId");
	var href = a?.As<Tag>()?["href"];//#
	var content = a?.Children?[0].As<PlainText>()?.Text;//content