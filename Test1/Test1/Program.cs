using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

public class Items
{
   public UInt32 CRC;
   public Int16 UseCalibration;
   public Int32 Scale;
   public Int32[] XCalVector;
   public Int32[] YCalVector;
   public Int32[] ZCalVector;
   public UInt16 CalStatus;
}

public class StructFile
{
    private static readonly string Path = "..//..//text.txt";


    public static async Task Main()
    {
        
        var dataList = new List<string>(File.ReadAllLines(Path));
        var data = new List<string>();

        foreach (var variable in dataList)
        {
            var cleanData = variable.Split(';');
            data.Add(cleanData[0]);
        }
        

        var boxOfData = Converter(3,data);
        var boxOfData2 = Converter(4, data);
        var boxOfData3 = Converter(5, data);
        var copyStruct = new List<string>();


        var structWithData = new Items
        {
            CRC = Convert.ToUInt32(data[0]),
            UseCalibration= Convert.ToInt16(data[1]),
            Scale = Convert.ToInt32(data[2]),
            XCalVector = boxOfData.ToArray(),
            YCalVector = boxOfData2.ToArray(),
            ZCalVector = boxOfData3.ToArray(),
            CalStatus = Convert.ToUInt16(data[6])
        };

        var fileName = "..//..//DataCopy.txt";

        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }

        using (FileStream fs = File.Create(fileName))
        {
        }

        copyStruct.Add(structWithData.CRC.ToString());
        copyStruct.Add(structWithData.UseCalibration.ToString());
        copyStruct.Add(structWithData.Scale.ToString());
        copyStruct.Add(String.Join(",", structWithData.XCalVector));
        copyStruct.Add(String.Join(",", structWithData.YCalVector));
        copyStruct.Add(String.Join(",", structWithData.ZCalVector));
        copyStruct.Add(structWithData.CalStatus.ToString());

        File.WriteAllLines(fileName, copyStruct, Encoding.UTF8);

        fileName = "..//..//DataCopy2.json";

        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }

        CreatJason(fileName, copyStruct);
        string dataFromJason = ReadJason(fileName);
       
        Console.WriteLine(string.Join(",",dataFromJason));
        Console.ReadKey();
    }

    public static List<Int32> Converter(int index, List<string> data)
    {

        var dataArray = data[index].Split(',');
        var boxOfData = new List<Int32>();

        foreach (var number in dataArray)
        {
            boxOfData.Add(Convert.ToInt32(number));
        }

        return boxOfData;
    }

    public static string ReadJason(string fileName)
    {
        return File.ReadAllText(fileName); ;
    }

    public static async void CreatJason(string fileName, List<string> copyStruct )
    {
        using (FileStream stream = File.Create(fileName))
        {
            await JsonSerializer.SerializeAsync(stream, copyStruct);
        }
    }
}

