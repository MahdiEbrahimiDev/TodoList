namespace TodoList.Models.ViewModels { 
public class ProfileViewModel
{
    public string Name { get; set; }

    public DateTime CreatedTime { get; set; }

    public int TotalTasks { get; set; }

    public int CompletedTasks { get; set; }

    public int PendingTasks { get; set; }
}
}