using Amazon.SQS.Model;

namespace Commons;

public interface ISqsMessenger
{
    Task<SendMessageResponse> SendMessageAsync<T>(T message);
}