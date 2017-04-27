using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace WinServiceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<Program>(s =>
                {
                    s.ConstructUsing(tc => new Program());
                    s.WhenStarted(tc =>
                    {
                        var dir = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                        using (var log = new StreamWriter(File.OpenWrite(Path.Combine(dir, "log.txt"))))
                        {
                            Run(log);
                        }
                    });
                    s.WhenStopped(tc => { });
                });

                x.RunAsLocalSystem();
                x.SetDescription("Java hang test");
                x.SetDisplayName("Java hang test");
                x.SetServiceName("JavaHangTest");
            });
        }

        private static void Run(StreamWriter log)
        {
            log.WriteLine("Starting...");
            try
            {
                var psi = new ProcessStartInfo()
                {
                    WorkingDirectory = @"c:\workspace\docker_javahang",
                    FileName = @"c:\j2sdk1.4.2_19\jre\bin\java.exe",
                    Arguments = "HelloWorld",
                };
                var process = new Process() {StartInfo = psi};
                var res = process.Start();
                log.WriteLine("Running java. Result:" + res);
            }
            catch (Exception e)
            {
                log.WriteLine(e);
            }
        }
    }
}
