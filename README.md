# FileOperation
Uzdevumus var pildīt lietojot Visual Studio (versija nav svarīga), vienā vai vairākos projektos.
1. Uzdevums

Failā, no tā sākuma pozīcijas, tiek glabāta šāda C++ struktura:


struct AccelCalibrationData
{

    uint32_t          CRC;
    int16_t           UseCalibration;
    int32_t           Scale;
    int32_t           XCalVector[4];
    int32_t           YCalVector[4];
    int32_t           ZCalVector[4];
    uint16_t         CalStatus;
};
}


a) C# valodā izveidot attiecīgu struktūru vai klasi.
b) Šajā klasē izveidot metodi, kura nolasa šīs struktūras vērtības no faila.
c) Izveidot metodi, kura saglabā dotās vērtības failā ar tādu pašu formātu.
d) Izveidot metodi, kura saglabā vertības json formāta failā.
e) Izveidot metodi, kura ielādē vērtības no json formāta faila.

2. Uzdevums.
	a) Dotas 3 metodes:

static void write1()
{

    Console.WriteLine("1");
    Thread.Sleep(100);
}

static void write2()
{

    Console.WriteLine("2");
    Thread.Sleep(200);
}

static void write3()
{

    Console.WriteLine("3");
    Thread.Sleep(300);
}

Izveidot 3 plūsmas (Thread),  katra no tām izsauc savu write metodi (write1(), write2(), write3()). Sinhronizēt plūsmu darbību tā, lai rezultāta konsoles izvadā tiktu iegūta šāda secība: 1 2 2 3 3 3 3 3 3 2 2 1 3 2 1 1 2 3 
Kā arī jāsagaida visu plūsmu darbības beigas.
![image](https://user-images.githubusercontent.com/90256392/156054712-d7b5b6e6-f2dd-4b2a-8168-a6d5197518ca.png)




	b) Iegūt konsolē tieši tādu secību kā a), bet asinhroni, izsaucot sekojošas metodes:

static async Task write1()
{

    Console.WriteLine("1");
    await Task.Delay(100);
}

static async Task write2()
{

    Console.WriteLine("2");
    await Task.Delay(200);
}

static async Task write3()
{

    Console.WriteLine("3");
    await Task.Delay(300);
}

3. Uzdevums
Zemāk dota klase.
a) Atrast kļūdas un piedāvāt rekomendācijas kā to var uzlabot.
b) Piedāvāt savu pārstrādāto (refactored) variantu, kurš varētu tikt izmatots reālajā projektā.
 /**
 * This class is thread safe.
 */
 
public class Parser
{

    private string filePath;
    public void setPath(string f)
    {
        filePath = f;
    }
    
    public string getPath()
    {
        return filePath;
    }
    
    public string getContent()
    {
        FileStream i = new FileStream(filePath, FileMode.Open);
        string output = "";
        int data;
        while ((data = i.ReadByte()) > 0)
        {
            output += (char)data;
        }
        return output;
    }
    
    public string getContentWithoutUnicode()
    {
        FileStream i = new FileStream(filePath, FileMode.Create);
        string output = "";
        int data;
        lock (i)
        {
            while ((data = i.ReadByte()) > 0)
            {
                if (data < 0x80)
                {
                    output += (char)data;
                }
            }
        }
        return output;
    }
    public void saveContent(string content)
    {
        FileStream o = new FileStream(filePath, FileMode.Create);
        lock (o)
        {
            for (int i = 0; i < content.Length; i += 1)
            {
                o.WriteByte((byte)content[i]);
            }
        }
    }
}
