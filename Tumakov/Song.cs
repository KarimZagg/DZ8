namespace Tumakov
{
    internal class Song
    {

        string name;
        string author;
        Song prev;
        public Song()
        {
            name = "Без названия";
            author = "Неизвестно";
            prev = null;
        }
        public Song(string name, string author)
        {
            this.name = name;
            this.author = author;
            prev = null;
        }
        public Song(string name, string author, Song prev)
        {
            this.name = name;
            this.author = author;
            this.prev = prev;
        }
        public void SetName(string name)
        {
            this.name = name;
        }
        public void SetAuthor(string author)
        {
            this.author = author;
        }
        public void SetPrev(Song prev)
        {
            this.prev = prev;
        }
        public void Print()
        {
            Console.WriteLine(Title());
        }
        public string Title()
        {
            return $"{name} - {author}";
        }
        public override bool Equals(object d)
        {
            Song song = d as Song;
            if (song == null)
            {
                return false;
            }
            return name == song.name && author == song.author;
        }
    }
}