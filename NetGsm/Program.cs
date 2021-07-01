using System;
using System.IO;
using Telegram.Bot;
using System.Threading;
using NetGsm.ServiceReference1;
using NetGsm;
using System.Xml.Serialization;

public class Program
{
    public static void Main()
    {
        string kullaniciAdi = "3129113308";
        string sifre = "811033";
        string cikanDeger = "";
        for (int i = 0; i <= 10; i++)
        {
            DateTime simdi = DateTime.Now;
            string simdi2 = simdi.ToString("dd''MM''yyyy");
            string baslaTarih = simdi2 + "0000";
            string bitisTarih = simdi2 + "2359";
            smsnnClient gelensms = new smsnnClient();
            string xml = gelensms.gelensmsV2(kullaniciAdi,sifre, baslaTarih, bitisTarih, 1);
            XmlSerializer serializer = new XmlSerializer(typeof(Mainbody));
            if (xml != "60" && xml != "80" && xml != "100" && xml != "101") //Tüm hata kodları (60 sms yoksa gelir)
                using (StringReader reader = new StringReader(xml))
                {
                    var xmlSorgu = (Mainbody)serializer.Deserialize(reader);
                    string gelenDeger = xmlSorgu.Msg.Mesaj.Trim();
                    TelegramBotClient bot = new TelegramBotClient("1856099941:AAHhlAVCx6Obf3gL_GSr05_VZbk-oky0pPY");
                    if (cikanDeger != gelenDeger)
                    {
                        Console.WriteLine(gelenDeger);
                        string smsMesaji = $"{xmlSorgu.Msg.Gonderen} numaralı telefondan gelen SMS mesajı \"{gelenDeger}\"";
                        bot.SendTextMessageAsync(-1001482798866, smsMesaji);
                        cikanDeger = gelenDeger;
                    }
                }
            Thread.Sleep(10000);
            if (i == 10)
                i = 0;
        }
    }
}