using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public class MySnowFlakeHelper
    {
        /// <summary>
        /// 开始时间截
        /// 1288834974657 是(Thu, 04 Nov 2010 01:42:54 GMT) 这一时刻到1970-01-01 00:00:00时刻所经过的毫秒数。
        /// 当前时刻减去1288834974657 的值刚好在2^41 里，因此占41位。
        /// 所以这个数是为了让时间戳占41位才特地算出来的。
        /// </summary>
        public const long Twepoch = 1288834974657L;

        /// <summary>
        /// 工作节点Id占用5位
        /// </summary>
        const int WorkerIdBits = 5;

        /// <summary>
        /// 数据中心Id占用5位
        /// </summary>
        const int DatacenterIdBits = 5;

        /// <summary>
        /// 序列号占用12位
        /// </summary>
        const int SequenceBits = 12;

        /// <summary>
        /// 支持的最大机器Id，结果是31 (这个移位算法可以很快的计算出几位二进制数所能表示的最大十进制数)
        /// </summary>
        const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits);

        /// <summary>
        /// 支持的最大数据中心Id，结果是31
        /// </summary>
        const long MaxDatacenterId = -1L ^ (-1L << DatacenterIdBits);

        /// <summary>
        /// 机器ID向左移12位
        /// </summary>
        private const int WorkerIdShift = SequenceBits;

        /// <summary>
        /// 数据标识id向左移17位(12+5)
        /// </summary>
        private const int DatacenterIdShift = SequenceBits + WorkerIdBits;

        /// <summary>
        /// 时间截向左移22位(5+5+12)
        /// </summary>
        public const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;

        /// <summary>
        /// 生成序列的掩码，这里为4095 (0b111111111111=0xfff=4095)
        /// </summary>
        private const long SequenceMask = -1L ^ (-1L << SequenceBits);

        /// <summary>
        /// 毫秒内序列(0~4095)
        /// </summary>
        private long _sequence = 0L;

        /// <summary>
        /// 上次生成Id的时间截
        /// </summary>
        private long _lastTimestamp = -1L;

        /// <summary>
        /// 工作节点Id
        /// </summary>
        public long WorkerId { get; protected set; }

        /// <summary>
        /// 数据中心Id
        /// </summary>
        public long DatacenterId { get; protected set; }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="workerId">工作ID (0~31)</param>
        /// <param name="datacenterId">数据中心ID (0~31)</param>
        public MySnowFlakeHelper(long workerId, long datacenterId)
        {
            WorkerId = workerId;
            DatacenterId = datacenterId;

            if (workerId > MaxWorkerId || workerId < 0)
            {
                throw new ArgumentException(string.Format($"工作ID必须在0至{MaxWorkerId}之间"));
            }
            if (datacenterId > MaxDatacenterId || datacenterId < 0)
            {
                throw new ArgumentException(string.Format($"数据中心ID必须在0至{MaxDatacenterId}之间"));
            }
        }

        private static readonly object _lockObj = new object();

        /// <summary>
        /// 获得下一个ID (该方法是线程安全的)
        /// </summary>
        /// <returns></returns>
        public virtual long NextId()
        {
            // 分布式或负载均衡情况下有ID冲撞[隐患]
            lock (_lockObj)
            {
                //获取当前时间戳
                var timestamp = TimeGen();

                //如果当前时间小于上一次ID生成的时间戳，说明系统时钟回退过这个时候应当抛出异常
                if (timestamp < _lastTimestamp)
                {
                    throw new InvalidOperationException(string.Format(
                        $"系统时钟出现回退，拒绝生成{_lastTimestamp - timestamp}毫秒的ID"));
                }

                //如果是同一时间生成的，则进行毫秒内序列
                if (_lastTimestamp == timestamp)
                {
                    _sequence = (_sequence + 1) & SequenceMask;
                    //毫秒内序列溢出
                    if (_sequence == 0)
                    {
                        //阻塞到下一个毫秒,获得新的时间戳
                        timestamp = TilNextMillis(_lastTimestamp);
                    }
                }

                //时间戳改变，毫秒内序列重置
                else
                {
                    _sequence = 0;
                }

                //上次生成ID的时间截
                _lastTimestamp = timestamp;

                //移位并通过或运算拼到一起组成64位的ID
                return ((timestamp - Twepoch) << TimestampLeftShift) |
                         (DatacenterId << DatacenterIdShift) |
                         (WorkerId << WorkerIdShift) | _sequence;
            }
        }

        /// <summary>
        /// 生成当前时间戳
        /// </summary>
        /// <returns>毫秒</returns>
        private static long GetTimestamp()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        /// <summary>
        /// 生成当前时间戳
        /// </summary>
        /// <returns>毫秒</returns>
        protected virtual long TimeGen()
        {
            return GetTimestamp();
        }

        /// <summary>
        /// 阻塞到下一个毫秒，直到获得新的时间戳
        /// </summary>
        /// <param name="lastTimestamp">上次生成Id的时间截</param>
        /// <returns></returns>
        protected virtual long TilNextMillis(long lastTimestamp)
        {
            var timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }
            return timestamp;
        }
    }
}
