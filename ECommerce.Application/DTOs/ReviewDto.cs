public class ReviewDto
{
    public string? Id { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CreateReviewDto
{
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
} 