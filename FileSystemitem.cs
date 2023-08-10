namespace OpenStudioIDE
{
    public class FileSystemItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsFolder { get; set; }
        public string filething { get; set; }

        public FileSystemItem(string name, string path, bool isFolder)
        {
            Name = name;
            Path = path;
            IsFolder = isFolder;
            filething = Path;
        }

        public override string ToString()
        {
            return Name; // Display the item's name in the ListBox
        }
    }
}
