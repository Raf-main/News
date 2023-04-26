namespace PRAS.Testovoe.Main.ViewModels.Response;

public class NewsResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Subtitle { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string ImageName { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
}