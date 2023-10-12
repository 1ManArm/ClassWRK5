namespace ClassWRK5
{
    internal class Backup
    {
        public abstract class Storage
        {
            private string name;
            private string model;

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public string Model
            {
                get { return model; }
                set { model = value; }
            }

            public abstract double getStorageValue();
            public abstract void copyDataToDevice(File[] files, out double timeSpend, out File[] remainingFiles);
            public abstract double getFreeSpaceValue();
            public abstract string getDeviceInfo();
        }

        public class File
        {
            private double size;

            public double Size
            {
                get { return size; }
                set { size = value; }
            }

            public File()
            {
                size = 780;
            }

            public File(double size)
            {
                Size = size;
            }
        }


        public class Flash : Storage
        {
            private double speedUSB30;
            private double storageValue;
            private double freeSpace;

            public Flash()
            {
                SpeedUSB30 = 625;
                StorageValue = 4000;
                FreeSpace = StorageValue;
                Name = "Flash ";
                Model = String.Format("{0}GB USB 3.0", StorageValue / 1000);
            }
            public double SpeedUSB30
            {
                get { return speedUSB30; }
                set { speedUSB30 = value; }
            }
            public double StorageValue
            {
                get { return storageValue; }
                set { storageValue = value; }
            }
            public double FreeSpace
            {
                get { return freeSpace; }
                set { freeSpace = value; }
            }

            public override void copyDataToDevice(File[] files, out double timeSpend, out File[] remainingFiles)
            {
                double filesSize = 0;

                int i = 0;
                while (filesSize < StorageValue)
                    filesSize += files[i++].Size;

                timeSpend = filesSize/SpeedUSB30;
                FreeSpace = StorageValue - filesSize;

                remainingFiles = new File[files.Length - i];
                int j = 0;
                while (i < files.Length)
                    remainingFiles[j++] = files[i];

            }
            public override string getDeviceInfo()
            {
                return String.Format("{0}{1}\nСкорость - {2}\nОбъем памяти - {3}\nСвободное место - {4}\n", Name, Model, SpeedUSB30, StorageValue, FreeSpace);
            }
            public override double getFreeSpaceValue()
            {
                return FreeSpace;
            }
            public override double getStorageValue()
            {
                return StorageValue;
            }
        }
        class DVD : Storage
        {
            private double readWriteSpd;
            private string type;
            private double freeSpace;
            private double storageValue;

            public double ReadWriteSpd
            {
                get { return readWriteSpd; }
                set { readWriteSpd = value; }
            }
            public string Type
            {
                get { return type; }
                set { type = value; }
            }

            public double FreeSpace
            {
                get { return freeSpace; }
                set { freeSpace = value; }
            }
            public double StorageValue
            {
                get { return storageValue; }
                set { storageValue = value; }
            }

            public DVD()
            {
                ReadWriteSpd = 67;
                Console.WriteLine(Type);
                StorageValue = 9;
                FreeSpace = StorageValue;
                Name = "DVD";
                Model = String.Format("{0}DVD", StorageValue / 1000);
            }
            public override void copyDataToDevice(File[] files, out double timeSpend, out File[] remainingFiles)
            {
                double filesSize = 0;

                int i = 0;
                while (filesSize < StorageValue)
                    filesSize += files[i++].Size;

                timeSpend = filesSize / readWriteSpd;
                FreeSpace = StorageValue - filesSize;

                remainingFiles = new File[files.Length - i];
                int j = 0;
                while (i < files.Length)
                    remainingFiles[j++] = files[i];

            }
            public override string getDeviceInfo()
            {
                return String.Format("{0}{1}\nСкорость записи и чтения - {2}\nОбъем памяти - {3}\nСвободное место - {4}\nТип диска - {5}\n", Name, Model, readWriteSpd, StorageValue, FreeSpace, Type);
            }
            public override double getFreeSpaceValue()
            {
                return FreeSpace;
            }
            public override double getStorageValue()
            {
                return StorageValue;
            }
        }
        class HDD : Storage
        {
            private double speedUSB20;
            private double partitionQuant;
            private double partitionSpace;
            private double freeSpace;
            private double storageValue;
            public double SpeedUSB20
            {
                get { return speedUSB20; }
                set { speedUSB20 = value; }
            }
            public double PartitionQuant
            {
                get { return partitionQuant; }
                set { partitionQuant = value; }
            }
            public double PartitionSpace
            {
                get { return partitionSpace; }
                set { partitionSpace = value; }
            }
            public double StorageValue
            {
                get { return storageValue; }
                set { storageValue = value; }
            }
            public double FreeSpace
            {
                get { return freeSpace; }
                set { freeSpace = value; }
            }
            public HDD()
            {
                SpeedUSB20 = 480;
                PartitionQuant = 3;
                PartitionSpace = 250;
                StorageValue = PartitionQuant * PartitionSpace;
                FreeSpace = StorageValue;
                Name = "HDD";
                Model = String.Format("{0}HDD", StorageValue / 1000);
            }
            public override void copyDataToDevice(File[] files, out double timeSpend, out File[] remainingFiles)
            {
                double filesSize = 0;

                int i = 0;
                while (filesSize < StorageValue)
                    filesSize += files[i++].Size;

                timeSpend = filesSize / SpeedUSB20;
                FreeSpace = StorageValue - filesSize;

                remainingFiles = new File[files.Length - i];
                int j = 0;
                while (i < files.Length)
                    remainingFiles[j++] = files[i];

            }
            public override string getDeviceInfo()
            {
                return String.Format("{0}{1}\nСкорость записи - {2}\nОбъем памяти - {3}\nСвободное место - {4}\nКоличество разделов - {5}\n Размер одного раздела - {6}\n", Name, Model, SpeedUSB20, StorageValue, FreeSpace, PartitionQuant, PartitionSpace);
            }
            public override double getFreeSpaceValue()
            {
                return FreeSpace;
            }
            public override double getStorageValue()
            {
                return StorageValue;
            }
        }

        private static int GetV()
        {
            return 1;
        }

        static void Main(string[] args, int v)
        {
            Console.WriteLine("Практика 1\n");
            Storage[] devices = new Storage[3];

            double data;
            double dataref;

            devices[0] = new Flash();
            devices[1] = new DVD();
            devices[2] = new HDD();

            Console.WriteLine(devices[0].getDeviceInfo());
            Console.WriteLine(devices[0].getFreeSpaceValue());
            Console.WriteLine(devices[0].getStorageValue());
            //Console.WriteLine(devices[0].copyDataToDevice(12, out data, out dataref));
            Console.WriteLine();

            Console.WriteLine(devices[1].getDeviceInfo());
            Console.WriteLine(devices[1].getFreeSpaceValue());
            Console.WriteLine(devices[1].getStorageValue());
            //Console.WriteLine(devices[1].copyDataToDevice(12, out data, out dataref));
            Console.WriteLine();

            Console.WriteLine(devices[2].getDeviceInfo());
            Console.WriteLine(devices[2].getFreeSpaceValue());
            Console.WriteLine(devices[2].getStorageValue());
            //Console.WriteLine(devices[2].copyDataToDevice(12, out data, out dataref));
            Console.WriteLine();
        }
    }
}