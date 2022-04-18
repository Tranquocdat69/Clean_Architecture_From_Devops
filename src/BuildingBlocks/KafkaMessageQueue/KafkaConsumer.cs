using Confluent.Kafka;
using ECom.BuildingBlocks.MessageQueue.KafkaMessageQueue.Configs;

namespace ECom.BuildingBlocks.MessageQueue.KafkaMessageQueue;
public class KafkaConsumer<TKey, TValue>
{
    private readonly KafkaConsumerConfig _config;
    public KafkaConsumer(KafkaConsumerConfig config)
    {
        _config = config;
    }

    public void Consume(Action<ConsumeResult<TKey, TValue>?> consumeCallback, CancellationToken cancellationToken,
    string topic, int partition = -1, long offset = -1)
    {
        ConsumerConfig consumerConfig = new ConsumerConfig
        {
            GroupId = _config.GroupId,
            BootstrapServers = _config.BootstrapServers,
            EnableAutoCommit = _config.EnableAutoCommit,
            SessionTimeoutMs = _config.SessionTimeoutMs,
            QueuedMinMessages = _config.QueuedMinMessages
        };
        using (var consumer = new ConsumerBuilder<TKey, TValue>(consumerConfig).Build())
        {
            AssignOrSubscribeTopic(consumer, topic, partition, offset);
            while (!cancellationToken.IsCancellationRequested)
            {
                ConsumeResult<TKey, TValue>? record = consumer.Consume(TimeSpan.FromSeconds(1));
                consumeCallback(record);
            }
        }
    }

    private void AssignOrSubscribeTopic(IConsumer<TKey, TValue> consumer, string topic, int partition = -1, long offset = -1)
    {
        if (partition >= 0)
        {
            if (offset >= 0)
            {
                consumer.Assign(new TopicPartitionOffset(new TopicPartition(topic, partition),
            new Offset(offset)));
            }
            else
            {
                consumer.Assign(new TopicPartition(topic, partition));
            }
        }
        else
        {
            consumer.Subscribe(topic);
        }
    }
}

