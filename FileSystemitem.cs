using System;

namespace OpenStudioIDE
{
    public class FileSystemItem
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public FileSystemItem(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public override string ToString()
        {
            return Name; // Display the item's name in the ListBox
        }
    }
}
