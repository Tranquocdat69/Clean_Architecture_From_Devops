using Confluent.Kafka;
using ECom.BuildingBlocks.MessageQueue.KafkaMessageQueue.Configs;

namespace ECom.BuildingBlocks.MessageQueue.KafkaMessageQueue;

public class KafkaProducer<TKey, TValue> : IDisposable
{
    private readonly IProducer<TKey, TValue> _producer;

    public KafkaProducer(KafkaProducerConfig config)
    {
        ProducerConfig producerConfig = new ProducerConfig
        {
            BootstrapServers = config.BootstrapServers,
            QueueBufferingMaxMessages = config.QueueBufferingMaxMessages,
            MessageSendMaxRetries = config.MessageSendMaxRetries,
            RetryBackoffMs = config.RetryBackoffMs,
            LingerMs = 5
        };
        _producer = new ProducerBuilder<TKey, TValue>(producerConfig).Build();
    }

    public void Dispose()
    {
        _producer.Dispose();
    }

    public void Produce(Message<TKey, TValue> message, string topic, int partiton = -1)
    {
        try
        {
            if (partiton < 0)
            {
                _producer.Produce(topic, message);
            }
            else
            {
                _producer.Produce(new TopicPartition(topic, partiton), message);
            }
        }
        catch (ProduceException<Null, string> e)
        {
            if (e.Error.Code == ErrorCode.Local_QueueFull)
            {
                _producer.Poll(TimeSpan.FromSeconds(1));
            }
            else
            {
                throw;
            }
        }
    }
}