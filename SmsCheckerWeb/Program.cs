using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmsService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Telegram.Bot;

namespace SmsCheckerWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();


        }

        static Task CheckMessage()
        {
            DateTime baslamaTarihi = DateTime.Now;
            return Task.Run(() =>
            {
                while (true)
                {
                    string kullaniciAdi = "3129113308";
                    string sifre = "xxxxx";

                    smsnnClient gelensms = new smsnnClient();
                    string xml = gelensms.gelensmsV2(kullaniciAdi, sifre, baslamaTarihi.ToString("ddMMyyyffff"), DateTime.Now.ToString("ddMMyyyffff"), 1);
                    XmlSerializer serializer = new XmlSerializer(typeof(Mainbody));
                    if (xml != "60" && xml != "80" && xml != "100" && xml != "101") //Tüm hata kodlarý (60 sms yoksa gelir)
                        using (StringReader reader = new StringReader(xml))
                        {
                            var xmlSorgu = (Mainbody)serializer.Deserialize(reader);
                            string gelenDeger = xmlSorgu.Msg.Mesaj.Trim();
                            TelegramBotClient bot = new TelegramBotClient("1856099941:AAHhlAVCx6Obf3gL_GSr05_VZbk-oky0pPY");
                            string smsMesaji = $"{xmlSorgu.Msg.Gonderen} numaralý telefondan gelen SMS mesajý \"{gelenDeger}\"";
                            bot.SendTextMessageAsync(-1001482798866, smsMesaji);
                            baslamaTarihi = DateTime.Now;
                        }

                }
            });
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(async webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    await CheckMessage();

                });


    }
}
