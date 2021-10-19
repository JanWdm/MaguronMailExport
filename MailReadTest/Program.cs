namespace MailReadTest
{
    using System;
    using System.IO;
    using System.Net.Mail;
    using System.Text;
    using MimeKit;

    using System.Configuration;
    using Newtonsoft.Json;

    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                var test = Directory.GetCurrentDirectory();

                var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

                var file = File.ReadAllText(path);

                var config = JsonConvert.DeserializeObject<Config>(file);


                

                string Betreff = config.Betreff;
                string MailDatei = config.MailDatei;
                string Zieldatei = config.Zieldatei;

                StringBuilder builder = new StringBuilder();

                var lines = File.ReadAllLines(MailDatei);

                var lineindex = 0;

                //Datum überprüfen

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(Betreff))
                    {
                        lineindex = i;
                        var byLine = lines[lineindex + 1];
                        var addressLine = lines[lineindex + 2];
                        var birthdayLine = lines[lineindex + 3];
                        var phoneLine = lines[lineindex + 4];
                        var mailLine = lines[lineindex + 5];
                        var memberLine = lines[lineindex + 6];
                        var siblingLine = lines[lineindex + 7];
                        var swimmingLine = lines[lineindex + 8];
                        var vegetarianLine = lines[lineindex + 9];
                        var otherEatingLine = lines[lineindex + 10];
                        var otherLine = lines[lineindex + 11];

                        var by = byLine.Replace("Von:", "");
                        var address = addressLine.Replace("Anschrift:", "");
                        var birthday = birthdayLine.Replace("Geburtstag:", "");
                        var phone = phoneLine.Replace("Telefon:", "");
                        var mail = mailLine.Replace("E-Mail:", "");
                        var member = memberLine.Replace("Mitgliedschaft:", "");
                        var sibling = siblingLine.Replace("Geschwisterermäßigung:", "");
                        var swimming = swimmingLine.Replace("Schwimmfähigkeit:", "");
                        var vegetarian = vegetarianLine.Replace("Vegetarier/in:", "");
                        var othereating = otherEatingLine.Replace("Sonstige Essgewohnheiten bzw. Unverträglichkeiten:", "");
                        var other = otherLine.Replace("Sonstige Anliegen/ wichtige Informationen:", "");

                        builder.AppendLine(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}", by, address, birthday, phone, mail, member, sibling, swimming, vegetarian, othereating, other));
                    }
                }
                File.WriteAllText(Zieldatei, builder.ToString());
                Console.WriteLine(builder.ToString());
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }
    }
}
