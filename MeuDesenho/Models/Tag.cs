namespace MeuDesenho.Models
{
    public class Tag
    {
        public string Name { get; set; }
        public float Score { get; set; }

        public Tag(string tagName) { }
        public Tag(string name, float score)
        {
            this.Name = name;
            this.Score = score;
        }
    }
}