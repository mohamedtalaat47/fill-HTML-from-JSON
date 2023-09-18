using System;
using System.Text.Json;
using System.Threading;
using Aspose.Html;
using Aspose.Html.Dom;
using exportPdf;

string jsonFilePath = "../../../data.json";
string jsonString = File.ReadAllText(jsonFilePath);
var jsonData = JsonSerializer.Deserialize<Data>(jsonString);

string htmlPath = "../../../index.html";
var document = new HTMLDocument(htmlPath);

document.GetElementById("header").InnerHTML = jsonData.header;

    var theadElement = document.GetElementsByTagName("thead").FirstOrDefault();
    if (theadElement != null)
    {
        var trElement = theadElement.GetElementsByTagName("tr").FirstOrDefault();
        trElement?.Remove();

        var newTr = theadElement.AppendChild(document.CreateElement("tr"));
        foreach (var h in jsonData.th)
        {
            var newTd = document.CreateElement("td");
            newTd.TextContent = h;
            newTr.AppendChild(newTd);
        }
    }

var tbodyElement = document.GetElementsByTagName("tbody").FirstOrDefault();

if (tbodyElement != null)
{
    var trElements = tbodyElement.GetElementsByTagName("tr").ToList();
    foreach(var trElement in trElements)
    {
        trElement.Remove();
    }

    foreach (var row in jsonData.rows)
    {
        var newTr = tbodyElement.AppendChild(document.CreateElement("tr"));

        foreach (var column in row)
        {
            var newTd = document.CreateElement("td");
            newTd.TextContent = column;
            newTr.AppendChild(newTd);
        }
    }
}

document.Save(htmlPath);

Console.WriteLine("Done");