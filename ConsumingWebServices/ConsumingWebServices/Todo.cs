namespace ConsumingWebServices
{
    public class Todo
    {
        public int UserId { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public bool Completed { get; set; }

        public override string ToString()
        {
            return string.Format("ID: {0}\nTitle: {1}\n", this.Id, this.Title);
        }
    }
}
