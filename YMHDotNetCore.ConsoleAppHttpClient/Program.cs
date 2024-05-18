using Newtonsoft.Json;

Console.WriteLine("Hello World");

string jsonStr = await File.ReadAllTextAsync("data.json");
var model = JsonConvert.DeserializeObject<MainDto>(jsonStr);

Console.WriteLine(jsonStr);

foreach(var question in model.questions)
{
    Console.WriteLine(question.questionNo);
}   

Console.ReadLine();

//static string ToNumber(string num)
//{
//    num = num.Replace("၀", 3);
//    num = num.Replace("၁", 3);
//    num = num.Replace("၂", 3);
//    num = num.Replace("၃", 3);
//    num = num.Replace("၄", 3);
//    num = num.Replace("၅", 3);
//    num = num.Replace("၆", 3);
//    num = num.Replace("၇", 3);
//    num = num.Replace("၈", 3);
//    num = num.Replace("၉", 3);

//    return num;
//}

// Json TO C# Package
// C# to json

public class MainDto
{
    public Question[] questions { get; set; }
    public Answer[] answers { get; set; }
    public string[] numberList { get; set; }
}

public class Question
{
    public int questionNo { get; set; }
    public string questionName { get; set; }
}

public class Answer
{
    public int questionNo { get; set; }
    public int answerNo { get; set; }
    public string answerResult { get; set; }
}
