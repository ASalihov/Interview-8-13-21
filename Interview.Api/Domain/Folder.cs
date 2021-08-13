namespace WebApplication1.Domain
{
    public class Folder
    {
        public Folder(string index, string name)
        {
            Index = index;
            Name = name;
        }
        public string Index { get; set; }
        public string Name { get; set; }
    }
}