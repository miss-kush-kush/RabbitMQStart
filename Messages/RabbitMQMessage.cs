namespace ITSTEPRabbitMQ.Messages
{
    public interface IRabbitMQMessage
    {
        int Id { get; set; }
        string Message { get; set; }
    }
}
