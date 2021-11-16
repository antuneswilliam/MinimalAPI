//public record Todo(Guid Id, string Title, bool Done);

public class Todo
{
    public Todo(Guid id, string title, bool done)
    {
        Id = id;
        Title = title;
        Done = done;
    }

    public Guid Id {  get; protected set; }
    public string Title { get; protected set; }
    public bool Done { get; protected set; }

    public void ChangeTitle(string newTitle)
    {
        this.Title = newTitle;
    }

    public void MarkAsDone()
    {
        this.Done = true;
    }

}