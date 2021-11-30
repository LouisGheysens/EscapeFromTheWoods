using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;

namespace BusinessLayer {
    public class Log {

        public string pathtomypersonaldisk = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\EscapeFromTheWoods";
        public List<Task> TaskList = new List<Task>();
        private int s = 2;
        private int th = 20;

        public Log() {
            DirectoryInfo dir = new DirectoryInfo(pathtomypersonaldisk);
            if (!dir.Exists)
                dir.Create();

            foreach(FileInfo file in dir.GetFiles()) {
                file.Delete();
            }

        }

        public void CreateBitMap(Forest f) {
            Console.WriteLine($"{f.id} Starting writing bitmap for forest {f.id}");
            Bitmap image = new Bitmap(f.XMaxValue * s, f.YMaxValue * s);
            Pen pen = new Pen(Color.Green);
            Graphics g = Graphics.FromImage(image);
            foreach(Tree t in f.Trees) {
                g.DrawEllipse(pen, t.X * s, t.Y * s, th, th);
            }
            f.BitMap = image;
        }

        public void WriteMonkeyToImage(Forest f, Tree t) {
            Brush MonkeyBrush = new SolidBrush(Color.White);
            Image img = f.BitMap;
            Graphics g = Graphics.FromImage(img);
            g.FillEllipse(MonkeyBrush, t.X * s, t.Y * s, th, th);
        }

        public void LetMonkeyJumpInTheWood(Forest f, Tree t1, Tree t2) {
            Pen p = new Pen(Color.White);
            Image imag = f.BitMap;
            Graphics g = Graphics.FromImage(imag);
            g.DrawLine(p, new Point { X = t1.X * s + th / 2, Y = t1.Y * s + th / 2 },
                new Point { X = t2.X * s + th / 2, Y = t2.Y * s + th/2 });
        }

        public void WriteImage(int key, Image tr) {
            tr.Save(Path.Combine(pathtomypersonaldisk, $"Woodmap_{key}.jpg"), ImageFormat.Jpeg);
        }

        public void ActionLog(int woodid, int monkeyid, string msg) {
            Console.WriteLine($"Writing log: {msg}");
            LogController.DB.AddActionLog(
                new ActionLog
                {
                    WoodId = woodid,
                    MonkeyId = monkeyid,
                    Message = msg
                }
                );
        }

        public void WriteMonkeyLog(Forest f, Monkey m, Tree t) {
            Console.WriteLine($"Writing monkeylog: {m.Id}, {m.Naam}");
            LogController.DB.AddMonkeyLog(
                new MonkeyLog
                {
                    MonkeyId = m.Id,
                    MonkeyName = m.Naam,
                    WoodId = f.id,
                    SequenceNumber = m.Hops,
                    TreeId = t.Id,
                    X = t.X,
                    Y = t.Y
                }
                );
        }

        public void TreeLog(Forest f , Tree t) {
            Console.WriteLine($"Writing TreeLog: Forest_{f.id} Tree_{t.Id}");
            LogController.DB.AddTreeLog(
                new TreeLog
                {
                    WoodId = f.id,
                    TreeId = t.Id,
                    X = t.X,
                    Y = t.Y
                }
                );
        }

        public void DefineTxtLog(Forest f) {
            Console.WriteLine($"Writing textlog: Forest_{f.id}");
            FileStream fm = new FileStream($"ForestLog_id_{f.id}", FileMode.OpenOrCreate);
            using(StreamWriter writer = new StreamWriter(pathtomypersonaldisk + $@"\ForestLog_id_{f.id}.txt")) {
                foreach(string r in f.Logs) {
                    writer.WriteLine(r);
                };
                fm.Close();
            }
        }


    }
}
