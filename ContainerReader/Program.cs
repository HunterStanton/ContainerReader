using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ContainerReader
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 1)
            {
                Console.WriteLine("ContainerReader\nA program that prints infomation about Windows Containers.index files, used to store metadata (such as the package family name, guid, and filename) about configuration/save game files for UWP apps and games.\nContainer \"filenames\" are actually directories, as they can have more than one file inside of them.\nUsage: ContainerReader containers.index\nNOTE: Doesn't, at the current time, handle all forms of containers.index. Will add support, but most are supported.");
                return;
            }
            try
            {
                // Open a filestream with the user selected file
                FileStream file = new FileStream(args[0], FileMode.Open);

                // Create a binary reader that will be used to read the file
                BinaryReader reader = new BinaryReader(file);

                int type = reader.ReadInt32();
                // Could be something other than int32, but very unlikely
                int numFiles = reader.ReadInt32();

                Console.WriteLine("Friendly Name: "+BinaryReaderHelper.ReadUnicodeString(reader, reader.ReadInt32()));
                Console.WriteLine("Package Family Name: " + BinaryReaderHelper.ReadUnicodeString(reader, reader.ReadInt32()));

                // Not awfully sure what this is, so I'll just skip past it until I can figure out what it is. Possibly title ID or other internal data
                reader.ReadBytes(0xc);

                // Not sure what this is either, but I'll print it
                Console.WriteLine("Unknown GUID: " + BinaryReaderHelper.ReadUnicodeString(reader, reader.ReadInt32()));

                // Loop through every file in the index, and print info about it
                for (int i = 0; i < numFiles;i++)
                {
                    string fileName = BinaryReaderHelper.ReadUnicodeString(reader, reader.ReadInt32());

                    // 4 padding bytes
                    reader.ReadBytes(4);

                    // Unknown value, surrounded by quotes for some reason
                    string UnknownValue = BinaryReaderHelper.ReadUnicodeString(reader, reader.ReadInt32());

                    // Unknown, will add later if it is important
                    reader.ReadBytes(5);

                    // The guid folder that the files reside in
                    byte[] guid1 = reader.ReadBytes(4);
                    Array.Reverse(guid1);
                    byte[] guid2 = reader.ReadBytes(2);
                    Array.Reverse(guid2);
                    byte[] guid3 = reader.ReadBytes(2);
                    Array.Reverse(guid3);
                    byte[] guid4 = reader.ReadBytes(2);
                    byte[] guid5 = reader.ReadBytes(6);

                    // More unknown data... really gotta try to figure this out
                    reader.ReadBytes(0x18);

                    // holy unwieldy code batman...gotta condense this sometime
                    // TODO: condense this
                    Console.WriteLine(fileName+" | "+UnknownValue+" | "+BitConverter.ToString(guid1).Replace("-", string.Empty) + "-"+ BitConverter.ToString(guid2).Replace("-", string.Empty) + "-"+ BitConverter.ToString(guid3).Replace("-", string.Empty) + "-"+ BitConverter.ToString(guid4).Replace("-", string.Empty) + "-"+ BitConverter.ToString(guid5).Replace("-", string.Empty));

                }
                return;


            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found!");
                return;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("File could not be accessed!");
                return;
            }
            return;
        }
    }
}
